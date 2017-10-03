using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
	/// <summary>
	/// Editable Business Object Root Collection
	/// </summary>
    [Serializable()]
    public class Relations : BusinessListBaseEx<Relations, Relation>
    {
		#region Business Methods
	
        public Relation NewItem()
        {
            this.AddItem(Relation.NewChild());
            return this[Count - 1];
        }
        public Relation NewItem(IEntity parent, IEntity child)
        {
            this.AddItem(Relation.NewChild(parent, child));
            return this[Count - 1];
        }

        public bool ContainsRelationChild(IEntity child)
        {
            return GetRelationChild(child) != null;
        }

        public Relation GetRelationChild(IEntity child)
        {
            return Items.FirstOrDefault(x => x.OidChild == child.Oid && x.ChildType == child.EntityType);
        }
        
        #endregion
		
		#region Common Factory Methods

        private Relations() { }

        public List<long> ToChildsOidList()
        { 
            List<long> oid_list = new List<long>(
                                        from st in Items
                                        select st.OidChild
                                    );

            return oid_list;
        }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Relations NewList() 
		{ 	
			if (!Relation.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new Relations(); 
		}

		private static Relations GetList(string query, bool childs)
		{
			if (!Relation.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return GetList(query, childs, -1);
		}		
		
		public static Relations GetList(bool childs = true) 
		{
			return GetList(Relations.SELECT(), childs);
		}
		public static Relations GetList(QueryConditions conditions, bool childs)
		{
			return GetList(Relations.SELECT(conditions), childs);
		}
		
        #endregion
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private Relations(IList<Relation> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
		private Relations(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
		
		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
		public static Relations NewChildList() 
        {
			Relations list = new Relations(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static Relations GetChildList(IList<Relation> lista) { return new Relations(lista); }
		public static Relations GetChildList(int sessionCode, IDataReader reader, bool childs = true) { return new Relations(sessionCode, reader, childs); }

		public static Relations GetChildList(IEntity parent, bool childs)
		{
			CriteriaEx criteria = Relation.GetCriteria(parent.SessionCode);
			criteria.Query = SELECT(parent);
			criteria.Childs = childs;

			return DataPortal.Fetch<Relations>(criteria);
		}

		#endregion

		#region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            try
            {
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
                {
                    Relation.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Relation.GetChild(SessionCode, reader, Childs));
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Relation obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (Relation obj in this)
				{
					if (obj.IsNew)
						obj.Insert(this);
					else
						obj.Update(this);
				}

                if (!SharedTransaction) Transaction().Commit();
            }
            catch (Exception ex)
            {
                if ((!SharedTransaction) && (Transaction() != null)) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!SharedTransaction) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion		

		#region Child Data Access

		// called to copy objects data from list
		private void Fetch(IList<Relation> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (Relation item in lista)
			{
				this.AddItem(Relation.GetChild(item));
			}

			this.RaiseListChangedEvents = true;
		}

		private void Fetch(IDataReader reader)
		{
			this.RaiseListChangedEvents = false;

			MaxSerial = 0;

			while (reader.Read())
			{
				Relation item = Relation.GetChild(SessionCode, reader);
				this.AddItem(item);
			}

			this.RaiseListChangedEvents = true;
		}

		public void Update(IEntity parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (Relation obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (Relation obj in this)
			{
				if (obj.IsNew)
					obj.Insert(parent);
				else
					obj.Update(parent);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion
			
        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Relation.SELECT(conditions, true); }
		public static string SELECT(IEntity parent) 
        { 
            QueryConditions conditions = new QueryConditions { Relation = RelationInfo.New() };
            conditions.Relation.OidParent = parent.Oid;
            conditions.Relation.ParentType = parent.EntityType;

            return Relation.SELECT(conditions, true); 
        }

		#endregion
    }
}

