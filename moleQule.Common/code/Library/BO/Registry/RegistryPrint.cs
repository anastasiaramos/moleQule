using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
    [Serializable()]
    public class RegistryPrint : RegistroInfo
    {
        #region Attributes & Properties
			
		#endregion
		
        #region Factory Methods

        private RegistryPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static RegistryPrint New(RegistroInfo source)
        {
            RegistryPrint item = new RegistryPrint();
            item._base.CopyValues(source);

            return item;
        }

        #endregion
    }
}