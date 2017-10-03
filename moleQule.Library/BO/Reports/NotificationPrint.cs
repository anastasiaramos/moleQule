using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace moleQule.Library
{
    [Serializable()]
    public class NotificationPrint
    {

        #region Attributes & Properties

        private string _notification = string.Empty;

        public string Notification { get { return _notification; } }

		#endregion
		
		#region Business Methods

        /// <summary>
        /// Copia los atributos del objeto
        /// </summary>
        /// <param name="source">Objeto origen</param>
        protected void CopyValues(string source)
        {
            if (source == null) return;
                        
            _notification = source;			
        }

        #endregion

        #region Factory Methods

        private NotificationPrint() { /* require use of factory methods */ }

        // called to load data from source
        public static NotificationPrint New(string source)
        {
            NotificationPrint item = new NotificationPrint();
            item.CopyValues(source);

            return item;
        }

        #endregion

    }
}
