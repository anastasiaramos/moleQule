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
    public class TarjetaCreditoPrint : CreditCardInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods
        
        #endregion

        #region Factory Methods

        private TarjetaCreditoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static TarjetaCreditoPrint New(CreditCardInfo source)
        {
            TarjetaCreditoPrint item = new TarjetaCreditoPrint();
            item._base.CopyValues(source);

            return item;
        }

        #endregion

    }
}
