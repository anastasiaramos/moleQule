using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class IRPFPrint : IRPFInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods
        
        #endregion

        #region Factory Methods

        private IRPFPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static IRPFPrint New(IRPFInfo source)
        {
            IRPFPrint item = new IRPFPrint();
            item._base.CopyValues(source);

            return item;
        }

        #endregion
    }
}
