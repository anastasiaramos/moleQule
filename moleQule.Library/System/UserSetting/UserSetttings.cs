using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Csla;
using Csla.Validation;
using NHibernate;

using moleQule.Library.CslaEx;  

namespace moleQule.Library
{
    [Serializable()]
    public class UserSettings : BusinessListBaseEx<UserSettings, UserSetting>
    {
        #region Business Methods

		public UserSetting NewItem(User parent, UserSetting source)
		{
			this.NewItem(UserSetting.NewChild(parent, source));
			return this[Count - 1];
		}

		public UserSetting GetItem(string name)
		{
            //HAY SETTINGS REPETIDOS PARA UN MISMO USUARIO
			return Items.First(item => item.Name == name);
		}
		
		public string GetValue(string name)
		{
			UserSetting item = GetItem(name);
			if (item != null) return item.Value;

			return string.Empty;
		}

		public void SetValue(string name, string value)
		{
			UserSetting item = GetItem(name);
			if (item != null) item.Value = value;
		}

        public bool ExistOtherItem(UserSetting child)
        {
            foreach (UserSetting obj in this)
				if ((obj.Oid != child.Oid) && (obj.Name == child.Name))
                    return true;
            return false;
        }

		public void CopyFrom(User parent)
		{
			UserSettings settings;

			if ((AppContext.Principal != null) &&
				(SettingsMng.Instance.UserSettings != null))
			{
				settings = SettingsMng.Instance.UserSettings;
			}
			else
			{
				settings = UserSettings.GetListByUser(1);
			}

			foreach (UserSetting item in settings)
				NewItem(parent, item);
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

        private UserSettings() { }
		private UserSettings(User source) 
		{
			CopyFrom(source);
		}

        public void NewItem() { this.Add(UserSetting.New()); }

        public static UserSettings NewList() { return new UserSettings(); }
		public static UserSettings NewList(User parent) { return new UserSettings(parent); }

		public static UserSettings GetList()
		{
            CriteriaEx criteria = UserSetting.GetCriteria(UserSetting.OpenSession());
			criteria.Query = SELECT();

			UserSetting.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<UserSettings>(criteria);
		}
		public static UserSettings GetListByUser(User user)
		{
			CriteriaEx criteria = UserSetting.GetCriteria(UserSetting.OpenSession());
			criteria.Query = SELECT(user);

			UserSetting.BeginTransaction(criteria.SessionCode);

			//No criteria. Retrieve all de List
			return DataPortal.Fetch<UserSettings>(criteria);
		}
		public static UserSettings GetListByUser(long oid)
		{
			CriteriaEx criteria = UserSetting.GetCriteria(UserSetting.OpenSession());

			QueryConditions conditions = new QueryConditions { User = User.New().GetInfo(false) };
			conditions.User.Oid = oid;

			criteria.Query = SELECT(conditions);

			UserSetting.BeginTransaction(criteria.SessionCode);

			//No criteria. Retrieve all de List
			return DataPortal.Fetch<UserSettings>(criteria);
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
					//UserSetting.DoLOCK(Session());

					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
						this.AddItem(UserSetting.GetChild(SessionCode, reader));
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
            foreach (UserSetting obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

			try
			{
				// add/update any current child objects
				foreach (UserSetting obj in this)
				{
					if (obj.IsNew)
						obj.Insert(this);
					else
						obj.Update(this);
				}

                if (!SaveAsChildList && !SharedTransaction) Transaction().Commit();
			}
			catch (Exception ex)
			{
				if ((!SaveAsChildList && !SharedTransaction) && (Transaction() != null)) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
                if (!SaveAsChildList && !SharedTransaction) BeginTransaction();
				this.RaiseListChangedEvents = true;
			}            
        }

        #endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return UserSetting.SELECT(conditions, false); }
		public static string SELECT(User user) { return UserSetting.SELECT(new QueryConditions { User = user.GetInfo() }, true); }

		#endregion
	}
}
