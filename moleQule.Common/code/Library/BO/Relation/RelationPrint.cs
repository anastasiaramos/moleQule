using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using Csla;
using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class RelationPrint : RelationInfo
    {
        #region Attributes & Properties
			
		#endregion
		
		#region Business Methods

        protected void CopyValues(RelationInfo source)
        {
            if (source == null) return;

			_base.CopyValues(source);
			
			
        }

        #endregion

        #region Factory Methods

        private RelationPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static RelationPrint New(RelationInfo source)
        {
            RelationPrint item = new RelationPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion
    }
}
