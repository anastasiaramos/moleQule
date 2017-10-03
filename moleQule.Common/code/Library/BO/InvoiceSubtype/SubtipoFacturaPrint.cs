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
    public class SubtipoFacturaPrint : SubtipoFacturaInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(SubtipoFacturaInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private SubtipoFacturaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static SubtipoFacturaPrint New(SubtipoFacturaInfo source)
        {
            SubtipoFacturaPrint item = new SubtipoFacturaPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
