using System;
using System.Data;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	[Serializable()]
	public class SchemaUserInfo : ReadOnlyBaseEx<SchemaUserInfo>
	{
		#region Business Methods

        public SchemaUserBase _base = new SchemaUserBase();

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidUser { get { return _base.Record.OidUser; } }
		public long OidSchema { get { return _base.Record.OidSchema; } }

		#endregion

		#region Factory Methods

		private SchemaUserInfo()	{ /* require use of factory methods */ }

        internal SchemaUserInfo(SchemaUser source)
		{
			_base.CopyValues(source);
		}

		#endregion

		#region Data Access

		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				_base.CopyValues((SchemaUser)(criteria.UniqueResult()));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}

		#endregion
	}
}
