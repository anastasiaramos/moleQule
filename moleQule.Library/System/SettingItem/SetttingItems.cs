using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx;  
using NHibernate;



namespace moleQule.Library
{
    [Serializable()]
    public class SettingItems : BusinessListBaseEx<SettingItems, SettingItem>
    {
        #region Business Methods

		public new SettingItem NewItem(SettingItem source)
		{
			this.NewItem(SettingItem.NewChild(source));
			return this[Count - 1];
		}

		public SettingItem GetItem(string name)
		{
			return Items.First(item => item.Name == name);
		}
		
		public string GetValue(string name)
		{
			SettingItem item = GetItem(name);
			if (item != null) return item.Comments;

			return string.Empty;
		}

		public void SetValue(string name, string value)
		{
			SettingItem item = GetItem(name);
			if (item != null) item.Comments = value;
		}

        public bool ExistOtherItem(SettingItem child)
        {
            foreach (SettingItem obj in this)
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

        #region Root Factory Methods

        private SettingItems() { }

        public void NewItem() { this.Add(SettingItem.New()); }

        public static SettingItems NewList() { return new SettingItems(); }

		public static SettingItems GetList()
		{
            CriteriaEx criteria = SettingItem.GetCriteria(SettingItem.OpenSession());
			criteria.Query = SELECT();

			SettingItem.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<SettingItems>(criteria);
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
					SettingItem.DoLOCK(Session());

					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
						this.AddItem(SettingItem.GetChild(SessionCode, reader));
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
            foreach (SettingItem obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

			try
			{
				// add/update any current child objects
				foreach (SettingItem obj in this)
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
		public static string SELECT(QueryConditions conditions) { return SettingItem.SELECT(conditions, false); }
		public static string SELECT(User user) { return SettingItem.SELECT(new QueryConditions { User = user.GetInfo() }, true); }

		#endregion
	}
}
