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
    public class ImpuestoPrint : ImpuestoInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods
        
        #endregion

        #region Factory Methods

        private ImpuestoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static ImpuestoPrint New(ImpuestoInfo source)
        {
            ImpuestoPrint item = new ImpuestoPrint();
            item._base.CopyValues(source);

            return item;
        }

        #endregion

    }
}
