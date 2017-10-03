using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

using Csla;
using NHibernate;

namespace moleQule.Library.CslaEx
{
    [Serializable()]
    public abstract class BusinessListBaseEx<T, C> :
        BusinessListBase<T, C>
        where T : BusinessListBaseEx<T, C>
        where C : BusinessBaseEx<C>
    {
        #region Attributes

        protected int _sessCode = -1;
        protected bool _childs = true;
        protected bool _g_childs = true;
        protected Hashtable _hash_list = new Hashtable();
		
		protected long _max_serial = 0;
        protected Dictionary<long, long> _max_serials = new Dictionary<long, long>();
		protected bool _save_as_child = false;

		#endregion

		#region Properties

		/// <summary>
		/// Manejador del motor de persistencia
		/// </summary>
		/// <returns></returns>
		public virtual nHManager nHMng { get { return nHManager.Instance; } }

		public virtual int SessionCode { get { return _sessCode; } set { _sessCode = value; } }

        //public SortedDictionary<long, C> KeyValueList { get { return _key_value_list; }}
        public Hashtable HashList { get {return _hash_list;} }

        /// <summary>
        /// Indica si se quiere que el objeto cargue los hijos
        /// </summary>
        public virtual bool Childs { get { return _childs; } set { _childs = value; } }

        /// <summary>
        /// Indica si se quiere que el objeto cargue los nietos
        /// </summary>
        public virtual bool GChilds { get { return _g_childs; } set { _g_childs = value; } }
		
		public long MaxSerial { get { return _max_serial; } set { _max_serial = value; } }
        public Dictionary<long, long> MaxSerials { get { return _max_serials; } set { _max_serials = value; } }


		/// <summary>
		/// Indica si se quiere guardar como parte de una transaccion externa
		/// </summary>
		public virtual bool SharedTransaction { get; set; }

		/// <summary>
		/// Indica si se quiere guardar como hijo, es decir, que forma parte de una transaccion externa
		/// DEPRECATED: Use SharedTransaction
		/// </summary>
		public bool SaveAsChildList { get { return _save_as_child; } set { _save_as_child = value; } }

		#endregion

		#region Business Methods

		/// <summary>
        /// Marca toda la lista como nueva
        /// </summary>
        public virtual void MarkAsNew() { foreach (C item in Items) item.MarkItemNew(); }

        public new void Add(C item) { this.NewItem(item); }

        /// <summary>
        /// A�ade un elemento a la lista principal y a la de busqueda HASH
        /// El elemento SE CREARA en la tabla correspondiente
        /// </summary>
        /// <param name="item">Objeto a a�adir</param>
        public void NewItem(C item)
        {
            this.AddItem(item);

            //Lo  marcamos como nuevo
            item.MarkItemNew();
        }

        public virtual void AddItem(C item) { AddItem(item, true); }
        
        public virtual void AddItem(C item, bool isNew)
        {
            PropertyDescriptor prop = TypeDescriptor.GetProperties(item).Find("Oid", false);

            base.Add(item);

            long oid = (long)prop.GetValue(item);

            while (HashList.Contains(oid) && item.IsNew)
            {
                Random r = new Random();
                oid = (long)r.Next();
                item.Oid = oid;
            }

            HashList.Add(oid, item);

            if (!isNew)
            {
                item.MarkItemOld();
                item.MarkItemDirty();
            }
        }

        //copia un elemento a la lista si considerarlo nuevo si isNEW=false
        //Se usa para traspasar objetos hijos de una lista padre a otra sin volver
        //a insertarlos en la tabla
        public virtual void MoveItem(C item) { AddItem(item, false); }

        /// <summary>
        /// Devuelve una lista de los elementos del criterio
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static T GetList(CriteriaEx criteria)
        {
            BusinessListBaseEx<T, C>.BeginTransaction(criteria.SessionCode);
            return DataPortal.Fetch<T>(criteria);
        }

        /// <summary>
        /// Devuelve un elemento a partir de los datos de la lista actual
        /// </summary>
        /// <param name="criteria">Filtro</param>
        /// <returns>Objeto C</returns>
        public virtual C GetItem(FCriteria criteria)
        {
            if (Items.Count == 0) return default(C);

            PropertyDescriptor property = TypeDescriptor.GetProperties(Items[0]).Find(criteria.GetProperty(), false);

            foreach (C item in Items)
            {
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                    if (prop.Name == property.Name)
                    {
                        object value = prop.GetValue(item);

                        switch(criteria.Operation)
                        { 
                            case Operation.Equal:
                                if (value.ToString().ToLower().Equals(criteria.GetValue().ToString().ToLower()))
                                    return item;
                                break;
                            default:
                                if (value.ToString().ToLower().Contains(criteria.GetValue().ToString().ToLower()))
                                    return item;
                                break;
                        }


                    }
            }

            return default(C);
        }

        public C GetItem(long oid)
        {
            if (Items.Count == 0) return default(C);

            try
            {
                C obj = (C)HashList[oid];
                if (obj == null)
                {
                    foreach (C item in this)
                    {
                        if (item.Oid == oid)
                            return item;
                    }
                }
                return obj;
            }
            catch
            {
                return default(C);
            }
        }

        /// <summary>
        /// Elimina un elemento de la lista principal y de la de HASH
        /// </summary>
        /// <param name="oid"></param>
        public virtual void Remove(long oid)
        {
            C obj = this.GetItem(oid);

            if (obj != null)
            {
                Remove(obj);
                HashList.Remove(oid);
            }
        }

		public virtual void RemoveAll()
		{
			base.Clear();
			HashList.Clear();
		}

        /// <summary>
        /// Consulta si existe un elemento
        /// </summary>
        /// <param name="oid">Oid del elemento</param>
        /// <returns></returns>
        public bool Contains(long oid) { return (this.GetItem(oid) != null); }

        /// <summary>
        /// Consulta si existe un elemento
        /// </summary>
        /// <param name="oid">Oid del elemento</param>
        /// <returns></returns>
        public new bool Contains(C item) 
        { 
            foreach (C listItem in this)
                if (listItem.IsLike(item)) return true;
            return false;
        }

        /// <summary>
        /// Consulta si existe un elemento borrado
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public bool ContainsDeleted(long oid)
        {
            foreach (C obj in DeletedList)
                if (((BusinessBaseEx<C>)obj).Oid == oid) return true;

            return false;
        }
 
        /// <summary>
        /// Devuelve una lista a partir de los datos de la lista actual
        /// </summary>
        /// <param name="criteria">Filtro</param>
        /// <returns>Lista</returns>
        /*public virtual List<C> GetSubList(FCriteria criteria)
        {
            List<C> list = new List<C>();

            if (Items.Count == 0) return list;

            PropertyDescriptor property = TypeDescriptor.GetProperties(Items[0]).Find(criteria.GetProperty(), false);

            foreach (C item in Items)
            {
                foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                    if (prop.Name == property.Name)
                    {
                        object value = prop.GetValue(item);
                        if (value.ToString().ToLower().Contains(criteria.GetValue().ToString().ToLower()))
                            list.Add(item);
                    }
            }

            return list;
        }*/

        /// <summary>
        /// Devuelve una lista filtrada
        /// </summary>
        /// <param name="criteria">Filtro</param>
        /// <returns>Lista ordenada</returns>
        public List<C> GetSubList(FCriteria criteria)
        {
            List<C> list = new List<C>();

            if (Items.Count == 0) return list;

            PropertyDescriptor property = TypeDescriptor.GetProperties(Items[0]).Find(criteria.GetProperty(), false);

            switch (criteria.Operation)
            {
                case Operation.StartsWith:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (value.ToString().ToLower().StartsWith(criteria.GetValue().ToString().ToLower()))
                                        list.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Equal:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Equal(value))
                                        list.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Less:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Less(value))
                                        list.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.LessOrEqual:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.LessOrEqual(value))
                                        list.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Greater:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Greater(value))
                                        list.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.GreaterOrEqual:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.GreaterOrEqual(value))
                                        list.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Between:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Between(value))
                                        list.Add(item);
                                    break;
                                }
                        }
                    } break;

                default:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (value.ToString().ToLower().Contains(criteria.GetValue().ToString().ToLower()))
                                        list.Add(item);
                                    break;
                                }
                        }
                    } break;
            }

            return list;
        }

		public static SortedBindingList<C> GetSortedList(BusinessListBaseEx<T, C> list) { return new SortedBindingList<C>(list); }

		/// <summary>
        /// Devuelve una lista ordenada y filtrada a partir de los datos de la lista
        /// actual
        /// </summary>
        /// <param name="criteria">Filtro</param>
        /// <param name="sortProperty">Campo de ordenaci�n</param>
        /// <param name="sortDirection">Sentido de ordenaci�n</param>
        /// <returns>Lista ordenada</returns>
		public SortedBindingList<C> GetSortedList() { return new SortedBindingList<C>(this); }
		public SortedBindingList<C> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<C> sorted_list = new SortedBindingList<C>(this);
			sorted_list.ApplySort(sortProperty, sortDirection);
			return sorted_list;
		}
		public SortedBindingList<C> GetSortedSubList(FCriteria criteria, string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<C> sortedList = GetSortedSubList(criteria);       
            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public SortedBindingList<C> GetSortedSubList(FCriteria criteria)
        {
            List<C> list = new List<C>();

            SortedBindingList<C> sortedList = new SortedBindingList<C>(list);

            if (Items.Count == 0) return sortedList;

            PropertyDescriptor property = TypeDescriptor.GetProperties(Items[0]).Find(criteria.GetProperty(), false);

            switch (criteria.Operation)
            {
                case Operation.StartsWith:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (value.ToString().ToLower().StartsWith(criteria.GetValue().ToString().ToLower()))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Equal:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Equal(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Less:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Less(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.LessOrEqual:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.LessOrEqual(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Greater:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Greater(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.GreaterOrEqual:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.GreaterOrEqual(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                default:
                    {
                        foreach (C item in Items)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (value.ToString().ToLower().Contains(criteria.GetValue().ToString().ToLower()))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;
            }

            return sortedList;
        }
        public SortedBindingList<C> GetSortedSubList(FCriteria criteria, List<string> properties_list)
        {
            List<C> list = new List<C>();
            SortedBindingList<C> sortedList = new SortedBindingList<C>(list);

            if (Items.Count == 0) return sortedList;

            PropertyDescriptor property = null;

            if (criteria.GetProperty() != null)
                property = TypeDescriptor.GetProperties(Items[0]).Find(criteria.GetProperty(), false);
            else
                property = null;

            Type type = typeof(C);
            System.Reflection.PropertyInfo prop = null;

            switch (criteria.Operation)
            {
                case Operation.Equal:
                    {
                        foreach (C item in Items)
                        {
                            foreach (string propName in properties_list)
                            {
                                prop = type.GetProperty(propName);

                                if (prop == null) continue;

                                //Buscamos en una propiedad en concreto
                                if (property != null)
                                {
                                    if (prop.Name == property.Name)
                                    {
                                        object value = prop.GetValue(item, null);
                                        if (value == null) break;
                                        if (value.ToString().ToLower().Equals(criteria.GetValue().ToString().ToLower()))
                                            sortedList.Add(item);
                                        break;
                                    }
                                }
                                //Buscamos en todas las propiedades de la lista
                                else
                                {
                                    object value = prop.GetValue(item, null);
                                    if (value == null) continue;
                                    if (value.ToString().ToLower().Equals(criteria.GetValue().ToString().ToLower()))
                                    {
                                        sortedList.Add(item);
                                        break;
                                    }
                                }
                            }
                        }
                    } break;

                case Operation.StartsWith:
                    {
                        foreach (C item in Items)
                        {
                            foreach (string propName in properties_list)
                            {
                                prop = type.GetProperty(propName);

                                if (prop == null) continue;

                                //Buscamos en una propiedad en concreto
                                if (property != null)
                                {
                                    if (prop.Name == property.Name)
                                    {
                                        object value = prop.GetValue(item, null);
                                        if (value == null) break;
                                        if (value.ToString().ToLower().StartsWith(criteria.GetValue().ToString().ToLower()))
                                            sortedList.Add(item);
                                        break;
                                    }
                                }
                                //Buscamos en todas las propiedades de la lista
                                else
                                {
                                    object value = prop.GetValue(item, null);
                                    if (value == null) continue;
                                    if (value.ToString().ToLower().StartsWith(criteria.GetValue().ToString().ToLower()))
                                    {
                                        sortedList.Add(item);
                                        break;
                                    }
                                }
                            }
                        }
                    } break;

                case Operation.Less:
                    {
                        foreach (C item in Items)
                        {
                            foreach (string propName in properties_list)
                            {
                                prop = type.GetProperty(propName);

                                if (prop == null) continue;

                                //Buscamos en una propiedad en concreto
                                if (property != null)
                                {
                                    if (prop.Name == property.Name)
                                    {
                                        object value = prop.GetValue(item, null);
                                        if (value == null) break;
                                        if (criteria.Less(value))
                                            sortedList.Add(item);
                                        break;
                                    }
                                }
                                //Buscamos en todas las propiedades de la lista
                                else
                                {
                                    object value = prop.GetValue(item, null);
                                    if (value == null) continue;
                                    if (criteria.Less(value))
                                    {
                                        sortedList.Add(item);
                                        break;
                                    }
                                }
                            }
                        }
                    } break;

                case Operation.LessOrEqual:
                    {
                        foreach (C item in Items)
                        {
                            foreach (string propName in properties_list)
                            {
                                prop = type.GetProperty(propName);

                                if (prop == null) continue;

                                //Buscamos en una propiedad en concreto
                                if (property != null)
                                {
                                    if (prop.Name == property.Name)
                                    {
                                        object value = prop.GetValue(item, null);
                                        if (value == null) break;
                                        if (criteria.LessOrEqual(value))
                                            sortedList.Add(item);
                                        break;
                                    }
                                }
                                //Buscamos en todas las propiedades de la lista
                                else
                                {
                                    object value = prop.GetValue(item, null);
                                    if (value == null) continue;
                                    if (criteria.LessOrEqual(value))
                                    {
                                        sortedList.Add(item);
                                        break;
                                    }
                                }
                            }
                        }
                    } break;

                case Operation.Greater:
                    {
                        foreach (C item in Items)
                        {
                            foreach (string propName in properties_list)
                            {
                                prop = type.GetProperty(propName);

                                if (prop == null) continue;

                                //Buscamos en una propiedad en concreto
                                if (property != null)
                                {
                                    if (prop.Name == property.Name)
                                    {
                                        object value = prop.GetValue(item, null);
                                        if (value == null) break;
                                        if (criteria.Greater(value))
                                            sortedList.Add(item);
                                        break;
                                    }
                                }
                                //Buscamos en todas las propiedades de la lista
                                else
                                {
                                    object value = prop.GetValue(item, null);
                                    if (value == null) continue;
                                    if (criteria.Greater(value))
                                    {
                                        sortedList.Add(item);
                                        break;
                                    }
                                }
                            }
                        }
                    } break;

                case Operation.GreaterOrEqual:
                    {
                        foreach (C item in Items)
                        {
                            foreach (string propName in properties_list)
                            {
                                prop = type.GetProperty(propName);

                                if (prop == null) continue;

                                //Buscamos en una propiedad en concreto
                                if (property != null)
                                {
                                    if (prop.Name == property.Name)
                                    {
                                        object value = prop.GetValue(item, null);
                                        if (value == null) break;
                                        if (criteria.GreaterOrEqual(value))
                                            sortedList.Add(item);
                                        break;
                                    }
                                }
                                //Buscamos en todas las propiedades de la lista
                                else
                                {
                                    object value = prop.GetValue(item, null);
                                    if (value == null) continue;
                                    if (criteria.GreaterOrEqual(value))
                                    {
                                        sortedList.Add(item);
                                        break;
                                    }
                                }
                            }
                        }
                    } break;

                case Operation.Contains:
                default:
                    {
                        foreach (C item in Items)
                        {
                            foreach (string propName in properties_list)
                            {
                                prop = type.GetProperty(propName);

                                if (prop == null) continue;

                                //Buscamos en una propiedad en concreto
                                if (property != null)
                                {
                                    if (prop.Name == property.Name)
                                    {
                                        object value = prop.GetValue(item, null);
                                        if (value == null) break;
                                        if (value.ToString().ToLower().Contains(criteria.GetValue().ToString().ToLower()))
                                            sortedList.Add(item);
                                        break;
                                    }
                                }
                                //Buscamos en todas las propiedades de la lista
                                else
                                {
                                    object value = prop.GetValue(item, null);
                                    if (value == null) continue;
                                    if (value.ToString().ToLower().Contains(criteria.GetValue().ToString().ToLower()))
                                    {
                                        sortedList.Add(item);
                                        break;
                                    }
                                }
                            }
                        }
                    } break;
            }

            return sortedList;
        }

        /// <summary>
        /// Ordena una lista
        /// </summary>
        /// <param name="list">Lista a ordenar</param>
        /// <param name="sortProperty">Campo de ordenaci�n</param>
        /// <param name="sortDirection">Sentido de ordenaci�n</param>
        /// <returns>Lista ordenada</returns>
        public static SortedBindingList<C> SortList(BusinessListBaseEx<T, C> list,
                                                    string sortProperty,
                                                    ListSortDirection sortDirection)
        {
            SortedBindingList<C> sortedList = new SortedBindingList<C>(list);
            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        /// <summary>
        /// Carga los valores del registro en el objeto
        /// </summary>
        /// <param name="source"></param>
        /*protected IList<C> CopyValues(IDataReader source)
        {

            System.Collections.ICollection col;
            IList<C> list = new List<C>() ;

            col = nHMng.Cfg.GetClassMapping(typeof(C)).PropertyCollection;
            
            Type objType = Type.GetType(C);
            Type[] ctorParams = new Type[] { typeof(C) };

            while (source.Read())
            {

                C objeto = objType.GetConstructor(typeof(C));

                foreach (Property prop in col)
                {
                    Column columna = (Column)(((IList)(prop.ColumnCollection))[0]);
                    object value = source[columna.Text];
                    this.SetPropertyValue(prop.Name, value);
                }
            }
        }*/

        #endregion

        #region Factory Methods

        /// <summary>
        /// Clausura la transaccion y sesion actual 
        /// </summary>
        public virtual void CloseDBObject()
        {
            if (Transaction() != null)
            {
                Transaction().Rollback();
                Transaction().Dispose();
            }

            CloseSession();
        }

		public static T GetList(string query, bool childs = false, int sessionCode = -1)
		{
			CriteriaEx criteria = GetCriteria((sessionCode != -1) ? sessionCode : OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL) criteria.Query = query;

			if (sessionCode == -1) BeginTransaction(criteria.SessionCode);

			T obj = DataPortal.Fetch<T>(criteria);

			obj.SharedTransaction = (sessionCode != -1);
			obj.SaveAsChildList = (sessionCode != -1); //DEPRECATED

			return obj;
		}

		public static T GetList(bool childs, int sessionCode)
		{
			string query = (string)typeof(T).GetMethod("SELECT").Invoke(null, null);
			return GetList(query, childs, sessionCode);
		}

		public List<long> GetOidList()
		{
			List<long> list = new List<long>();
			foreach (C item in this) list.Add(item.Oid);

			return list;
		}

		public override T Save()
		{
			try
			{
				return base.Save();
			}
			finally
			{
				if (!SharedTransaction) CloseSession();
			}
		}

		public T SaveAsChild()
		{
			SaveAsChildList = true;
			return base.Save();
		}
		public T SaveAsChild(int session)
		{
			SessionCode = session;
			SaveAsChildList = true;
			return base.Save();
		}

		protected virtual void SetSharedSession(int sessionCode)
		{
			SessionCode = sessionCode;
			SharedTransaction = (sessionCode != -1);
		}

        #endregion

        #region NHibernate Default Interface

		public virtual ITransaction BeginTransaction() { return BeginTransaction(SessionCode); }

		public virtual void CloseSession()
		{
			if (_sessCode == -1) return;

			CloseSession(_sessCode);
			_sessCode = -1;
		}

        /// <summary>
        /// Devuelve un criterio de b�squeda para este tipo asociado a la sesi�n abierta
        /// </summary>
        /// <returns></returns>
        public virtual CriteriaEx GetCriteria() { return GetCriteria(SessionCode); }

		public virtual void NewSession() { SessionCode = nHManager.Instance.OpenSession(); }
		public virtual void NewSharedTransaction() { NewTransaction(); SetSharedSession(SessionCode); }
		public virtual void NewTransaction() { NewSession(); BeginTransaction(); }

		public virtual void OpenNewSession() { SessionCode = OpenSession(); }

        public virtual ISession Session() { return Session(SessionCode); }

        public virtual ITransaction Transaction() { return Transaction(_sessCode); }

        #endregion

        #region NHibernate By Code Interface

		/// <summary>
		/// Inicia una transacci�n para la sessi�n actual
		/// </summary>
		/// <returns></returns>
		public static ITransaction BeginTransaction(int sessionCode) { return nHManager.Instance.BeginTransaction(sessionCode); }

		/// <summary>
		/// Cierra la sesi�n que se cre� para el objeto
		/// </summary>
		/// <returns></returns>
		public static void CloseSession(int sessionCode) { nHManager.Instance.CloseSession(sessionCode); }

		/// <summary>
        /// Devuelve un criterio de b�squeda para este tipo asociado a una sesi�n abierta
        /// </summary>
        /// <returns></returns>
        public static CriteriaEx GetCriteria(int sessionCode) { return nHManager.Instance.GetCriteria(typeof(T), sessionCode); }

		/// <summary>
		/// Abre una nueva sesi�n 
		/// </summary>
		/// <returns>C�digo de la sesi�n</returns>
		public static int OpenSession() { return nHManager.Instance.OpenSession(); }

        /// <summary>
        /// Devuelve la sesi�n correspondiente a este objeto
        /// </summary>
        /// <returns></returns>
        public static ISession Session(int sessionCode) { return nHManager.Instance.GetSession(sessionCode); }

        /// <summary>
        /// Devuelve la transacci�n correspondiente a este objeto
        /// </summary>
        /// <returns></returns>
        public static ITransaction Transaction(int sessionCode) { return nHManager.Instance.GetTransaction(sessionCode); }

        #endregion

        #region Data Access

        /// <summary>
        /// Construye el SELECT de la lista y lo ejecuta
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="sesion"></param>
        /// <returns></returns>
        public static IDataReader DoNativeSELECT(string query, ISession sesion)
        {
            return nHManager.Instance.SQLNativeSelect(query, sesion);
        }

        #endregion
    }
}