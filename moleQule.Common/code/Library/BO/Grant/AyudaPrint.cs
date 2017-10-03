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
    public class AyudaPrint : AyudaInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(AyudaInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private AyudaPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static AyudaPrint New(AyudaInfo source)
        {
            AyudaPrint item = new AyudaPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
