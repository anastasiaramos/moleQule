using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Reflection;
using System.ComponentModel;

using Csla;
using Csla.Core;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping;

namespace moleQule.Library.CslaEx
{
	[Serializable()]
	public abstract class BusinessBaseEx<T> :
		BusinessBase<T> where T : BusinessBaseEx<T>
	{
		#region Attributes

        private long _oid;
        protected bool _g_childs = true;
        protected bool _save_childs = true;
        protected bool _is_root_clon = false;
		protected bool _selected;
		protected bool _close_sessions = true;

		private int _sessCode = -1;

		#endregion

		#region Properties

        public bool ItemIsChild { get { return IsChild; } }

		/// <summary>
		/// Indica si se quiere que el objeto cargue los hijos
		/// </summary>
		public virtual bool IsRootClon
		{
			get { return _is_root_clon; }
			set { _is_root_clon = value; /*MarkAsRoot();*/ }
		}

		public virtual long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _oid;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);
				_oid = value;
			}
		}

		public virtual bool CloseSessions { get { return _close_sessions; } set { _close_sessions = value; } }
		public virtual int SessionCode { get { return _sessCode; } set { _sessCode = value; } }

		/// <summary>
		/// Indica si se quiere guardar como parte de una transaccion externa
		/// </summary>
		public virtual bool SharedTransaction { get; set; }

		/// <summary>
		/// Indica si se quiere que el objeto cargue los hijos
		/// </summary>
		public virtual bool Childs { get; set; }

		/// <summary>
		/// Indica si se quiere que el objeto cargue los nietos
		/// </summary>
		public virtual bool GChilds { get { return _g_childs; } set { _g_childs = value; } }

		/// <summary>
		/// Indica si se quiere que el objeto guarde los hijos
		/// </summary>
		public virtual bool SaveChilds { get { return _save_childs; } set { _save_childs = value; } }

		public virtual bool IsSelected { get { return _selected; } set { _selected = value; } }

		/// <summary>
		/// Manejador del motor de persistencia
		/// </summary>
		/// <returns></returns>
		public virtual nHManager nHMng { get { return nHManager.Instance; } }

		/// <summary>
		/// Devuelve el valor de una propiedad a partir de su nombre
		/// </summary>
		/// <param name="name">Nombre de la propiedad</param>
		/// <returns></returns>
		public virtual object GetPropertyValue(string name)
		{
			Type type = typeof(T);
			System.Reflection.PropertyInfo prop = type.GetProperty(name);

			return prop.GetValue(this, null);
		}

		/// <summary>
		/// Asigna el valor de una propiedad a partir de su nombre
		/// </summary>
		/// <param name="name">Nombre de la propiedad</param>
		/// <param name="value">Valor</param>
		public virtual void SetPropertyValue(string name, object value)
		{
			Type type = typeof(T);
			System.Reflection.PropertyInfo prop = type.GetProperty(name);

			// Para no rellenar los campos de los inner join que no est�n mapeados
			if (prop != null) prop.SetValue(this, value, null);
		}

        protected override object GetIdValue() { return Oid; }

		/// <summary>
		/// Compara todas las propiedades salvo el OID
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public virtual bool IsLike(object obj)
		{
			T item = (T)(obj);

            PersistentClass pclass;
            IEnumerable<Property> cols;

            pclass = nHMng.Cfg.GetClassMapping(AppControllerBase.AppControler.RecordEntities[typeof(T)]);
            cols = pclass.PropertyIterator;

            foreach (Property prop in cols)
            {
                if (this.GetPropertyValue(prop.Name) != item.GetPropertyValue(prop.Name))
                    return false;
            }

			return true;
		}

        public override int GetHashCode() { return base.GetHashCode(); }
        
		// Para las listas de objetos hijo
		public virtual void MarkItemNew() { MarkNew(); }

		// Interfaz p�blica para poder hacer listas de objetos root
		public virtual void MarkItemChild() { MarkAsChild(); }
		public virtual void MarkItemOld() { MarkOld(); }

		// Interfaz p�blica para poder agregar elementos a una lista hija
		public virtual void MarkItemDirty() { MarkDirty(); }

		/// <summary>
		/// Performs processing required when the current
		/// property has changed.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This method calls CheckRules(propertyName), MarkDirty and
		/// OnPropertyChanged(propertyName). MarkDirty is called such
		/// that no event is raised for IsDirty, so only the specific
		/// property changed event for the current property is raised.
		/// </para><para>
		/// This implementation uses System.Diagnostics.StackTrace to
		/// determine the name of the current property, and so must be called
		/// directly from the property to be checked.
		/// </para>
		/// </remarks>
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
		protected void PropertyHasChangedEx(string property)
		{
			try
			{
				string propertyName =
				  new System.Diagnostics.StackTrace().GetFrame(1).GetMethod().Name.Substring(4);

				PropertyHasChanged(propertyName);
			}
			catch
			{
				string tipo = this.GetType().Name;
				throw new Exception("BusinessBaseEx::PropertyHasChanged: Error mapeando la propiedad '" + property + "' en el objeto " + tipo);
			}
		}

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

		public static T Get(string query, bool childs = false, int sessionCode = -1)
		{
            if (query == string.Empty) return null;

			CriteriaEx criteria = GetCriteria((sessionCode != -1) ? sessionCode : OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL) criteria.Query = query;

			if (sessionCode == -1) BeginTransaction(criteria.Session);

			T obj = DataPortal.Fetch<T>(criteria);

			obj.SharedTransaction = (sessionCode != -1);

			return (obj.Oid != 0) ? obj : null;
		}

		public static T Get(long oid, bool childs, int sessionCode)
		{
			string query = (string)typeof(T).InvokeMember("SELECT"
													, BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public
													, null, null, new object[1] { oid });
			return Get(query, childs, sessionCode);
		}

		/// <summary>
		/// Saves the object to the database when is a list child.
		/// </summary>
		/// <remarks>
		public virtual T SaveAsChild()
		{
			if (IsDirty)
				return (T)DataPortal.Update(this);
			else
				return (T)this;
		}

		public virtual T SharedSave(int sessionCode)
		{
			SessionCode = sessionCode;
			return SharedSave();
		}
		protected virtual T SharedSave() { throw new NotImplementedException(); }

		public virtual void SetSharedSession(int sessionCode)
		{
			SessionCode = sessionCode;
			SharedTransaction = (sessionCode != -1);
		}

		#endregion

		#region Common Data Access

		[Serializable()]
		protected class CriteriaCs : CriteriaBase
		{
			private struct Exp
			{
				public string Name;
				public object Value;
			}

			private List<Exp> _exps = new List<Exp>();
			private int _sessCode = -1;

			public long Oid
			{
				get { return (long)GetValue("Oid"); }
				set { Add("Oid", value); }
			}
			public int SessionCode { get { return _sessCode; } set { _sessCode = value; } }

			public CriteriaCs(long oid, int sessionCode = -1)
				: base(typeof(T))
			{
				Oid = oid;
				_sessCode = sessionCode;
			}

			public CriteriaCs(string name, object value)
				: base(typeof(T))
			{
				Add(name, value);
			}

			public void Add(string name, object value)
			{
				Exp exp = new Exp();

				exp.Name = name;
				exp.Value = value;

				_exps.Add(exp);
			}

			public object GetValue(string name)
			{
				foreach (Exp exp in _exps)
					if (exp.Name.Equals(name))
						return (long)exp.Value;

				return 0;
			}
		}

		#endregion

		#region Data Access

		/// <summary>
		/// Construye y ejecuta un LOCK para el esquema dado
		/// </summary>
		/// <param name="schema"></param>
		/// <param name="sesion">sesi�n abierta para la transacci�n</param>
		public static void DoLOCK(ISession session)
		{
			/*string query = nHManager.Instance.LOCK(typeof(T), null);
			if (query == string.Empty) return;
			nHManager.Instance.SQLNativeExecute(query, session);*/
		}

		/// <summary>
		/// Construye y ejecuta un LOCK para el esquema dado
		/// </summary>
		/// <param name="schema"></param>
		/// <param name="sesion">sesi�n abierta para la transacci�n</param>
		public static void DoLOCK(string schema, ISession session)
		{
			/*string query = nHManager.Instance.LOCK(typeof(T), null);
			if (query == string.Empty) return;
			nHManager.Instance.SQLNativeExecute(query, session);*/
		}

		/// <summary>
		/// Construye y ejecuta un SELECT para el esquema dado
		/// </summary>
		/// <param name="schema"></param>
		/// <param name="sesion">Sesi�n abierta para la transacci�n</param>
		/// <returns></returns>
		public static IDataReader DoSELECT(string schema, ISession session, long oid)
		{
			string query = SELECT_DEPRECATED(schema, oid);
			return nHManager.Instance.SQLNativeSelect(query, session);
		}

		/// <summary>
		/// Construye y ejecuta un SELECT para el esquema dado
		/// </summary>
		/// <param name="schema"></param>
		/// <param name="sesion">sesi�n abierta para la transacci�n</param>
		/// <returns></returns>
		public static IDataReader DoNativeSELECT(string query, ISession session)
		{
			return nHManager.Instance.SQLNativeSelect(query, session);
		}

		public static void ExecuteSQL(CriteriaEx criteria)
		{
			nHManager.Instance.SQLNativeExecute(criteria.Query, Session(criteria.SessionCode));
		}

		#endregion

		#region NHibernate Default Interface

		/// <summary>
		/// Devuelve un criterio de b�squeda para este tipo asociado a la sesi�n abierta
		/// </summary>
		/// <returns></returns>
		public virtual CriteriaEx GetCriteria() { return GetCriteria(SessionCode); }

		public virtual ITransaction BeginTransaction() { return BeginTransaction(SessionCode); }

		public virtual ISession Session() { return Session(SessionCode); }

		public virtual ITransaction Transaction() { return Transaction(_sessCode); }

		public virtual void CloseSession()
		{
			if (_sessCode == -1) return;

			CloseSession(_sessCode);
			_sessCode = -1;
		}

		public virtual void NewSession() { SessionCode = nHManager.Instance.OpenSession(); }
		public virtual void NewTransaction() { NewSession(); BeginTransaction(SessionCode); }
		public virtual void NewSharedSession() { NewSession(); SharedTransaction = true; }
		public virtual void NewSharedTransaction() { NewTransaction(); SharedTransaction = true; }
		
		public virtual void CloseSharedSession(bool commit = true) 
		{
			if (Transaction() != null)
			{
				if (commit)
				{
					if (!Transaction().WasRolledBack) Transaction().Commit();
				}
				else
				{
					if (!Transaction().WasRolledBack) Transaction().Rollback();
				}
			}

			CloseSession();
		}

		#endregion

		#region NHibernate By Code Interface

		/// <summary>
		/// Abre una nueva sesi�n 
		/// </summary>
		/// <returns>C�digo de la sesi�n</returns>
		public static int OpenSession() { return nHManager.Instance.OpenSession(); }

		/// <summary>
		/// Devuelve un criterio de b�squeda para este tipo asociado a una sesi�n abierta
		/// </summary>
		/// <returns></returns>
        public static CriteriaEx GetCriteria(int sessionCode) { return nHManager.Instance.GetCriteria(nHManager.Instance.Cfg.GetClassMapping(AppControllerBase.AppControler.RecordEntities[typeof(T)]).MappedClass, sessionCode); }

		/// <summary>
		/// Inicia una transacci�n para la sessi�n actual
		/// </summary>
		/// <returns></returns>
		public static ITransaction BeginTransaction(int sessionCode) { return nHManager.Instance.BeginTransaction(sessionCode); }

		/// <summary>
		/// Devuelve la sesi�n correspondiente a este objeto
		/// </summary>
		/// <returns></returns>
		public static ISession Session(int sessionCode)
		{
			try
			{
				return nHManager.Instance.GetSession(sessionCode);
			}
			catch (iQCslaSessionException)
			{
				throw new iQCslaSessionException(typeof(T), sessionCode);
			}
		}

		/// <summary>
		/// Devuelve la transacci�n correspondiente a este objeto
		/// </summary>
		/// <returns></returns>
		public static ITransaction Transaction(int sessionCode) { return nHManager.Instance.GetTransaction(sessionCode); }

		/// <summary>
		/// Cierra la sesi�n que se cre� para el objeto
		/// </summary>
		/// <returns></returns>
		public static void CloseSession(int sessionCode) { nHManager.Instance.CloseSession(sessionCode); }

		#endregion

		#region NHibernate By Session Interface

		/// <summary>
		/// Devuelve un criterio de b�squeda para este tipo asociado a la sesi�n abierta
		/// </summary>
		/// <returns></returns>
		public static CriteriaEx GetCriteria(ISession sess) { return nHManager.Instance.GetCriteria(sess, typeof(T)); }

		/// <summary>
		/// Inicia una transacci�n para una sessi�n
		/// </summary>
		/// <returns></returns>
		public static ITransaction BeginTransaction(ISession sess) { return nHManager.Instance.BeginTransaction(sess); }

		/// <summary>
		/// Devuelve la transacci�n correspondiente a una sesi�n
		/// </summary>
		/// <returns></returns>
		public static ITransaction Transaction(ISession sess) { return nHManager.Instance.GetTransaction(sess); }

		#endregion

		#region NHibernate By Oid Interface (No tira)

		/// <summary>
		/// Devuelve la sesi�n correspondiente a este objeto
		/// </summary>
		/// <returns></returns>
		public static ISession Session(long oid)
		{
			return nHManager.Instance.GetSession(typeof(T), oid);
		}

		/// <summary>
		/// Inicia una transacci�n para la sessi�n actual
		/// </summary>
		/// <returns></returns>
		public static ITransaction BeginTransaction(long oid)
		{
			return nHManager.Instance.BeginTransaction(Session(oid));
		}

		/// <summary>
		/// Devuelve la transacci�n correspondiente a este objeto
		/// </summary>
		/// <returns></returns>
		public static ITransaction Transaction(long oid)
		{
			return nHManager.Instance.GetTransaction(typeof(T), oid);
		}

		#endregion

        #region SQL

        public static string GROUPBY(GroupList groups, string tableAlias, Dictionary<String, ForeignField> foreignFields)
        {
            if (groups == null || groups.Count == 0) return string.Empty;

            return FilterMng.GET_GROUPBY_SQL(groups, tableAlias, foreignFields);
        }

        public static string LIMIT(PagingInfo pagingInfo)
        {
            if (pagingInfo == null) return string.Empty;

            return @"
				LIMIT " + pagingInfo.ItemsPerPage + " OFFSET " + pagingInfo.CurrentPage * pagingInfo.ItemsPerPage;
        }

        /// <summary>
        /// Construye un LOCK para el esquema dado
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="sesion">sesi�n abierta para la transacci�n</param>
        public static string LOCK()
        {
            return nHManager.Instance.LOCK(typeof(T), null);
        }
        public static string LOCK(string schema)
        {
            return nHManager.Instance.LOCK(typeof(T), schema);
        }

        public static string ORDER(OrderList orders, string tableAlias, Dictionary<String, ForeignField> foreignFields)
        {
            if (orders == null || orders.Count == 0) return string.Empty;

            return FilterMng.GET_ORDERS_SQL(orders, tableAlias, foreignFields);
        }

        /// <summary>
        /// Construye un SELECT para el esquema dado
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="sesion">sesi�n abierta para la transacci�n</param>
        /// <returns></returns>
        public static string SELECT(long oid)
        {
            return nHManager.Instance.SELECT(AppControllerBase.AppControler.RecordEntities[typeof(T)], null, true, "Oid", oid, null);
        }
        public static string SELECT_DEPRECATED(string schema, long oid)
        {
            return nHManager.Instance.SELECT(AppControllerBase.AppControler.RecordEntities[typeof(T)], schema, true, "Oid", oid, null);
        }

        public static string SELECT_COUNT(CriteriaEx criteria)
        {
            criteria.Select = @"SELECT COUNT(*) AS ""TOTAL_ROWS""";
            criteria.From = criteria.Query.Substring(criteria.Query.IndexOf("FROM"));

            int unionPos = criteria.From.IndexOf("UNION");

            int trimPos = 0;

            //FALTA RECORRER LOS UNION PARA INCLUIRLOS
            //AHORA MISMO SE QUEDA SOLO CON EL PRIMER SELECT DEL UNION
            if (unionPos >= 0)
                trimPos = unionPos;
            else
                trimPos = criteria.From.IndexOf("ORDER BY ");

            trimPos = (trimPos < 0) ? 0 : trimPos;

            criteria.From = criteria.From.Substring(0, trimPos);

            return criteria.Query;
        }

        #endregion
	}

    [Serializable()]
    public abstract class BusinessBaseEx<T, C> : BusinessBaseEx<T>
        where T : BusinessBaseEx<T>
        where C : SQLBuilder
    {
        #region Factory Methods

        public new static T Get(long oid, bool childs, int sessionCode)
        {
            string query = (string)typeof(C).InvokeMember("SELECT"
                                                , BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public
                                                , null, null, new object[1] { oid });
            return Get(query, childs, sessionCode);
        }

        #endregion

        #region SQL

        public static string SELECT(CriteriaEx criteria, bool lockTable)
        {
            return (string)typeof(C).InvokeMember("SELECT"
                                                , BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public
                                                , null, null, new object[2] { criteria, false });
        }

        #endregion
    }
}
