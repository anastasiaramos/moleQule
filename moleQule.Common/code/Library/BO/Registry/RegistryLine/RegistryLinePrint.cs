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
    public class LineaRegistroPrint : LineaRegistroInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(LineaRegistroInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);			
        }

        #endregion

        #region Factory Methods

        private LineaRegistroPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static LineaRegistroPrint New(LineaRegistroInfo source)
        {
            LineaRegistroPrint item = new LineaRegistroPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}