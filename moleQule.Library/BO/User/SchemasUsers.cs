using System;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	/// <summary>
	/// Editable Child Collection
	/// </summary>
    [Serializable()]
    public class SchemasUsers : BusinessListBaseEx<SchemasUsers, SchemaUser>
    {
        #region Business Methods

		/// <summary>
		/// Crea y añade un elemento hijo
		/// </summary>
		/// <param name="parent">Usuario Padre</param>
		/// <returns>Nuevo UsuarioEmpresa</returns>
		public SchemaUser NewItem(User parent)
		{
			this.Add(SchemaUser.NewChild(parent));
    		return this[Count - 1];
		}

		public SchemaUser NewItem(User parent, long oid_schema)
		{
			this.Add(SchemaUser.NewChild(parent, oid_schema));
			return this[Count - 1];
		}

        public SchemaUser GetItemBySchema(long oid_schema)
        {
            foreach (SchemaUser item in this)
                if (item.OidSchema == oid_schema)
                    return item;

            return null;
        }

		public void RemoveItem(ISchemaInfo schema)
		{
			SchemaUser to_delete = null;

			foreach (SchemaUser item in this)
				if (schema.Oid == item.OidSchema)
				{
					to_delete = item;
					break;
				}

			if (to_delete != null) RemoveItem(this.IndexOf(to_delete));
		}

        #endregion

        #region Factory Methods

		private SchemasUsers()
		{
			MarkAsChild();
		}
		private SchemasUsers(IList<SchemaUser> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private SchemasUsers(int sessionCode, IDataReader reader)
        {
            MarkAsChild();
			SessionCode = sessionCode;
            Fetch(reader);
        }

		public static SchemasUsers NewChildList() { return new SchemasUsers(); }

        public static SchemasUsers GetChildList(IList<SchemaUser> lista) { return new SchemasUsers(lista); }
        public static SchemasUsers GetChildList(int sessionCode, IDataReader reader) { return new SchemasUsers(sessionCode, reader); }
		public static SchemasUsers GetChildList(User parent, bool childs)
		{
			CriteriaEx criteria = SchemaUser.GetCriteria(parent.SessionCode);

			criteria.Query = SchemasUsers.SELECT(parent);
			criteria.Childs = childs;

			return DataPortal.Fetch<SchemasUsers>(criteria);
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
					SchemaUser.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
						this.AddItem(SchemaUser.GetChild(SessionCode, reader));
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

		#endregion

        #region Child Data Access

		// called to copy objects data from list
        private void Fetch(IList<SchemaUser> lista)
        {
            this.RaiseListChangedEvents = false;
            
			foreach (SchemaUser item in lista)
                this.AddItem(SchemaUser.GetChild(item));
            
			this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(SchemaUser.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(User usuario)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (SchemaUser obj in DeletedList)
                obj.DeleteSelf(usuario);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (SchemaUser obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(usuario);
                else
                    obj.Update(usuario);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

		#region SQL

		internal static string SELECT(QueryConditions conditions) { return SchemaUser.SELECT(conditions, true); }
		internal static string SELECT(User source) { return SELECT(new QueryConditions { User = source.GetInfo() }); }

		#endregion
    }
}
