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
    public class PesajePrint : PesajeInfo
    {

        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(PesajeInfo source)
        {
            if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private PesajePrint() { /* require use of factory methods */ }

        // called to load data from source
        public static PesajePrint New(PesajeInfo source)
        {
            PesajePrint item = new PesajePrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
