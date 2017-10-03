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
    public class AyudaPeriodoPrint : AyudaPeriodoInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(AyudaPeriodoInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private AyudaPeriodoPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static AyudaPeriodoPrint New(AyudaPeriodoInfo source)
        {
            AyudaPeriodoPrint item = new AyudaPeriodoPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
