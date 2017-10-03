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
    public class TPVPrint : TPVInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods
        
        #endregion

        #region Factory Methods

        private TPVPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static TPVPrint New(TPVInfo source)
        {
            TPVPrint item = new TPVPrint();
            item._base.CopyValues(source);

            return item;
        }

        #endregion

    }
}
