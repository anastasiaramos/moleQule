using System;
using System.Data;
using System.Collections.Generic;

using NHibernate;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	[Serializable()]
	public class UserSettingInfo : ReadOnlyBaseEx<UserSettingInfo>
	{
		#region Attributes

        public UserSettingBase _base = new UserSettingBase();

		private bool _copyable = false;

		#endregion

		#region Properties

		public override long Oid { get { return _base.Record.Oid; } }
		public virtual long OidUser { get { return _base.Record.OidUser; } }
		public virtual long OidSetting { get { return _base.Record.OidSetting; } }
		public virtual string Name { get { return _base.Record.Name; } }
        public virtual string Value { get { return _base.Record.Value; } }

		public virtual bool Copyable { get { return _copyable; } }

		#endregion

		#region Business Methods

		public override string ToString() {	return _base.Record.Name; }

        protected void CopyValues(IDataReader source)
        {
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
            _base.CopyValues(source);

			_copyable = Format.DataReader.GetBool(source, "COPYABLE");
        }
		protected virtual void CopyValues(UserSetting source)
		{
			if (source == null) return;

			Oid = source.Oid;
            _base.CopyValues(source);

			_copyable = source.Copyable;
		}

		#endregion
		
		#region Common Factory Methods

		private UserSettingInfo() { /* require use of factory methods */ }
		private UserSettingInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal UserSettingInfo(UserSetting source)
		{
			CopyValues(source);
		}
		
		public static UserSettingInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static UserSettingInfo GetChild(int sessionCode, IDataReader reader, bool childs)
		{
			return new UserSettingInfo(sessionCode, reader, childs);
		}

		#endregion

		#region Factory Methods

		public static UserSettingInfo Get(long oid)
		{
			CriteriaEx criteria = UserSetting.GetCriteria(UserSetting.OpenSession());
            
            criteria.Query = UserSetting.SELECT(oid);

            UserSettingInfo obj = DataPortal.Fetch<UserSettingInfo>(criteria);
			UserSetting.CloseSession(criteria.SessionCode);
			
            return obj;
		}

        #endregion

		#region Common Data Access

		private void Fetch(IDataReader source)
		{
			try
			{
				CopyValues(source);

				if (Childs)
				{
				}
			}
			catch (Exception ex) { throw ex; }
		}

		#endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
            {               
				if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = null;

					reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        CopyValues(reader);
                }
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

        #region SQL

		public static string SELECT(long oid) { return UserSetting.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return UserSetting.SELECT(conditions, false); }

        #endregion
	}
}
