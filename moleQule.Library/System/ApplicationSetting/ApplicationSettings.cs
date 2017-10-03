using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx;  
using NHibernate;

namespace moleQule.Library
{
    [Serializable()]
    public class ApplicationSettings : BusinessListBaseEx<ApplicationSettings, ApplicationSetting>
    {
        #region Business Methods

		public ApplicationSetting GetItem(string nombre)
		{
			return Items.Single(item => item.Name == nombre);
		}

		public string GetValue(string name)
		{
			ApplicationSetting item = GetItem(name);
			if (item != null) return item.Value;

			return string.Empty;
		}

		public void SetValue(string name, string value)
		{
			ApplicationSetting item = GetItem(name);
			if (item != null) item.Value = value;
		}

        public bool ExistOtherItem(ApplicationSetting child)
        {
            foreach (ApplicationSetting obj in this)
				if ((obj.Oid != child.Oid) && (obj.Name == child.Name))
                    return true;
            return false;
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

        private ApplicationSettings() { }

        public void NewItem() { this.Add(ApplicationSetting.New()); }

        public static ApplicationSettings NewList() { return new ApplicationSettings(); }

		public static ApplicationSettings GetList()
		{
            CriteriaEx criteria = ApplicationSetting.GetCriteria(ApplicationSetting.OpenSession());
			criteria.Query = ApplicationSetting.SELECT(new QueryConditions(), false);

			ApplicationSetting.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<ApplicationSettings>(criteria);
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
				if (nHMng.UseDirectSQL)
				{
					ApplicationSetting.DoLOCK(Session());

					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
						this.AddItem(ApplicationSetting.GetChild(SessionCode, reader));
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

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (ApplicationSetting obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

			try
			{
				// add/update any current child objects
				foreach (ApplicationSetting obj in this)
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
    }
}
