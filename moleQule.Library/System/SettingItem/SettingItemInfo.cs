using System;
using System.Data;
using System.Collections.Generic;

using NHibernate;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	[Serializable()]
	public class SettingItemInfo : ReadOnlyBaseEx<SettingItemInfo>
    {
        #region Attributes

        public SettingItemBase _base = new SettingItemBase();

        #endregion

        #region Properties

		public SettingItemBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } }
        public string Name { get { return _base.Record.Name; } }
        public bool Copyable { get { return _base.Record.Copyable;} }
        public string Value { get { return _base.Record.Comments; } }

		public override string ToString() {	return _base.Record.Name; }

        #endregion

        #region Business Methods

        protected void CopyValues(IDataReader source)
        {
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
            _base.CopyValues(source);
        }

		#endregion
		
		#region Common Factory Methods

		private SettingItemInfo() { /* require use of factory methods */ }
		private SettingItemInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal SettingItemInfo(SettingItem source)
		{
			_base.CopyValues(source);
		}
		
		public static SettingItemInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static SettingItemInfo GetChild(int sessionCode, IDataReader reader, bool childs)
		{
			return new SettingItemInfo(sessionCode, reader, childs);
		}

		#endregion

		#region Factory Methods

		public static SettingItemInfo Get(long oid)
		{
			CriteriaEx criteria = SettingItem.GetCriteria(SettingItem.OpenSession());
            
            criteria.Query = SettingItem.SELECT(oid);

            SettingItemInfo obj = DataPortal.Fetch<SettingItemInfo>(criteria);
			SettingItem.CloseSession(criteria.SessionCode);
			
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

		public static string SELECT(long oid) { return SettingItem.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return SettingItem.SELECT(conditions, false); }

        #endregion
	}
}
