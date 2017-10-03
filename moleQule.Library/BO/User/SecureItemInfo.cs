using System;
using System.Data;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
    [Serializable()]
    public class SecureItemInfo : ReadOnlyBaseEx<SecureItemInfo>
    {
        #region Business Methods

        public SecureItemBase _base = new SecureItemBase();

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public string Nombre { get { return _base.Record.Name; } }
        public long Tipo { get { return _base.Record.Tipo; } }

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
            _base.CopyValues(source);
		}
        protected void CopyValues(SecureItem source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _base.CopyValues(source);
        }

        #endregion

        #region Factory Methods

        private SecureItemInfo() { /* require use of factory methods */ }
        internal SecureItemInfo(SecureItem source)
        {
			_base.CopyValues(source);
		}
		private SecureItemInfo(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        public static SecureItemInfo Get(long oid)
        {
            CriteriaEx criteria = SecureItem.GetCriteria(SecureItem.OpenSession());
            criteria.AddOidSearch(oid);
            SecureItemInfo obj = DataPortal.Fetch<SecureItemInfo>(criteria);
            SecureItem.CloseSession(criteria.SessionCode);
            return obj;
        }

		public static SecureItemInfo GetChild(IDataReader reader, bool childs) { return new SecureItemInfo(reader, childs); }

        #endregion

        #region Common Data Access

		//called to copy data from IDataReader
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}

		#endregion

		#region Root Data Access
		
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                _base.Record.Oid = 0;
                CopyValues((SecureItem)(criteria.UniqueResult()));
                SessionCode = criteria.SessionCode;
            }
            catch (Exception ex)
            {
                string msg = Resources.Errors.NO_OPERATION + System.Environment.NewLine +
                                iQExceptionHandler.GetAllMessages(ex);
                throw new iQPersistentException(msg);
            }
        }

        #endregion
    }
}
