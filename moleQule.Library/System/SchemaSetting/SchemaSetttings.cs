using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx;  

using NHibernate;

using moleQule.Library.Resources;

namespace moleQule.Library
{
    [Serializable()]
    public class SchemaSettings : BusinessListBaseEx<SchemaSettings, SchemaSetting>
    {
        #region Business Methods

		public SchemaSetting GetItem(string nombre)
		{
			return this.FirstOrDefault(x => x.Name == nombre);
		}

		public string GetValue(string name)
		{
			SchemaSetting item = GetItem(name);
			if (item != null) return item.Value;

			return string.Empty;
		}

		public void SetValue(string name, string value)
		{
			SchemaSetting item = GetItem(name);
			if (item != null) item.Value = value;
		}

        public bool ExistOtherItem(SchemaSetting child)
        {
			return this.Any(x => x.Oid != child.Oid && x.Name == child.Name);
        }

        #endregion

		#region Authorization Rules

		public static bool CanAddObject()
		{
			return AutorizationRulesControler.CanAddObject(Resources.SecureItems.VARIABLE);
		}

		public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.VARIABLE);
		}

		public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.VARIABLE);
		}

		public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.VARIABLE);
		}

		#endregion

        #region Factory Methods

        private SchemaSettings() { }

        public void NewItem() { this.Add(SchemaSetting.New()); }

        public static SchemaSettings NewList() { return new SchemaSettings(); }

		public static SchemaSettings GetList()
		{
            CriteriaEx criteria = SchemaSetting.GetCriteria(SchemaSetting.OpenSession());

			criteria.Query = SELECT();

			SchemaSetting.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<SchemaSettings>(criteria);
		}

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

			SessionCode = criteria.SessionCode;

            try
            {
				//SchemaSetting.DoLOCK(Session());

				IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

				while (reader.Read())
					this.AddItem(SchemaSetting.GetChild(SessionCode, reader, Childs));
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

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (SchemaSetting obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

			try
			{
				// add/update any current child objects
				foreach (SchemaSetting obj in this)
				{
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

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return SchemaSetting.SELECT(conditions, true); }
		
		#endregion
	}
}
