using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class MonitorPrint : MonitorInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(MonitorInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private MonitorPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static MonitorPrint New(MonitorInfo source)
        {
            MonitorPrint item = new MonitorPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
