using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace moleQule.Library.CslaEx
{
    [Serializable()]
    public class RecordBase
	{
		#region Attributes
		
		private long _oid = 0;

		#endregion

		#region Properties

		public virtual long Oid { get { return _oid; } 
            set { _oid = value; } }

		#endregion

		#region Factory Methods

		public override bool Equals(object obj)
        {
            if (obj == null) return false;

            return this.Oid == ((RecordBase)obj).Oid;
        }

        public override int GetHashCode()
        {
            object id = Oid;
            return id.GetHashCode();
        }

        public RecordBase() { }

		#endregion
	}
}
