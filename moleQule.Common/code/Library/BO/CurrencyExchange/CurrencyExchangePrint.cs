using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CurrencyExchangePrint : CurrencyExchangeInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(CurrencyExchangeInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private CurrencyExchangePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static CurrencyExchangePrint New(CurrencyExchangeInfo source)
        {
            CurrencyExchangePrint item = new CurrencyExchangePrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
