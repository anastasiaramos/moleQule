using System;
using System.Collections.Generic;
using System.Data;

using moleQule.Library.CslaEx; 
using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CargoInfo : ReadOnlyBaseEx<CargoInfo>
    {
        #region Business Methods

        public JobBase _base = new JobBase();

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public virtual string Valor
        {
            get
            {
                return _base.Record.Valor;
            }
        }

        #endregion

        #region Factory Methods

        private CargoInfo() { /* require use of factory methods */ }
        internal CargoInfo(Cargo source)
        {
			_base.CopyValues(source);
		}
        private CargoInfo(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static CargoInfo GetChild(IDataReader reader, bool childs) { return new CargoInfo(reader, childs); }

        #endregion

        #region Data Access

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
        }

        #endregion
    }
}
