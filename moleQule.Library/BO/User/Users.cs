using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using NHibernate;

namespace moleQule.Library
{
    /// <summary>
    /// Editable Root Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class Users : BusinessListBaseEx<Users, User>
    {
        #region Business Methods

        public User NewItem()
        {
            User item = User.NewChild();
            this.Add(item);
            item.SessionCode = SessionCode;
            return item;
        }

		public User AttachItem(UserInfo source, ISchemaInfo schema)
		{
            User item = GetItem(source.Oid);

			if (item == null)
			{
                item = User.Get(source.Oid);

                if (item == null)
                {
                    item = User.New();
                    item.CopyFrom(source);
                }

				item.MarkItemChild();

				this.Add(item);
				item.MarkItemOld();
			}
			else
				if (item.EEstado == EEstadoItem.Registered) return null;

			item.AttachSchema(schema);
			item.SessionCode = SessionCode;
			item.EEstado = EEstadoItem.Registered;

			return item;
		}
        public void DettachItem(User source, ISchemaInfo schema)
		{
			if (source.EEstado == EEstadoItem.Baja) return;

			source.DettachSchema(schema);
			source.EEstado = EEstadoItem.Baja;
		}

		public override void Remove(long oid)
		{
			User item = this.GetItem(oid);
			if (item == null) return;

			if (item.Oid == 1) throw new iQException(String.Format(Resources.Messages.DELETE_USER_NOT_ALLOWED, item.Name));
			
			base.Remove(oid);
		}

        #endregion

        #region Factory Methods

        private Users() { }

        public static Users NewList() { return new Users(); }

        /// <summary>
        /// Devuelve los usuarios que no tienen empresa asignada
        /// </summary>
        /// <returns>Users</returns>
        public static Users GetNoSchemaList()
        {
            CriteriaEx criteria = User.GetCriteria(User.OpenSession());
            User.BeginTransaction(criteria.SessionCode);


            criteria.Query = SELECT_NO_SCHEMA_LIST();
            criteria.Childs = false;

            return DataPortal.Fetch<Users>(criteria);
        }

		public static Users GetList() { return GetList(null); }
		public static Users GetList(ISchemaInfo schema) { return GetList(schema, true); }
        public static Users GetList(ISchemaInfo schema, bool childs)
        {
            CriteriaEx criteria = User.GetCriteria(User.OpenSession());
            User.BeginTransaction(criteria.SessionCode);

			string query = string.Empty;

			QueryConditions conditions = new QueryConditions
			{
				Schema = schema
			};

			criteria.Query = SELECT(conditions);
            criteria.Childs = childs;

            return DataPortal.Fetch<Users>(criteria);
        }

        #endregion

        #region Root Data Access

		// Called to retrieve data from db
        private void DataPortal_Fetch(CriteriaEx criteria) { Fetch(criteria); }
        private void DataPortal_Fetch(string hql_query) { Fetch(hql_query); }

        private void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    User.DoLOCK(Session());

                    IDataReader reader = null;
					reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(User.GetChild(SessionCode, reader, criteria.Childs));
                }
            }
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
        }

        private void Fetch(string hql_query)
        {
            this.RaiseListChangedEvents = false;

			try
			{
				IList list = nHMng.HQLSelect(hql_query);
                SessionCode = User.OpenSession();

				if (list.Count > 0)
				{
                    foreach (User item in list)
					{
						item.MarkItemChild();
						item.SessionCode = SessionCode;

						CriteriaEx criteria = Privilege.GetCriteria(SessionCode);
						criteria.AddEq("OidUser", item.Oid);
						item.Licences = Privileges.GetChildList(criteria.List<Privilege>());

						criteria = SchemaUser.GetCriteria(SessionCode);
						criteria.AddEq("OidUser", item.Oid);
						item.Schemas = SchemasUsers.GetChildList(criteria.List<SchemaUser>());

						this.AddItem(item);
					}
				}

				BeginTransaction();
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
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
            foreach (User obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (User obj in this)
                {
                    if (!obj.IsDirty) continue;

                    if (obj.IsNew)
                        obj.Insert(this);
                    else
                        obj.Update(this);
                }

                Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region IsValid

        /// <summary>
        /// Gets a value indicating whether this object is currently in
        /// a valid state (has no broken validation rules).
        /// </summary>
        public override bool IsValid
        {
            get
            {
                // run through all the child objects
                // and if any are invalid then the
                // collection is invalid
                foreach (User child in this)
                    if (child.IsDirty && !child.IsValid)
                        return false;
                return true;
            }
        }

        #endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) 
		{ 
			string query = string.Empty;
			
			query = User.SELECT(conditions, true);

			return query;
		}
        public static string SELECT_NO_SCHEMA_LIST()
        {
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(SchemaUserRecord));

            string query = "SELECT *" +
                            " FROM " + us +
                            " WHERE \"OID\" NOT IN ( SELECT DISTINCT \"OID_USER\"" +
                            "                           FROM " + su + ")" +
                            " ORDER BY \"OID\" ASC";

            return query;
        }

		#endregion
	}
}
