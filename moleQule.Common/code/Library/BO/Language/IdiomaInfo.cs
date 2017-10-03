using System;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class IdiomaInfo : ReadOnlyBaseEx<IdiomaInfo>
    {
        #region Business Methods

        private string _valor = string.Empty;

        public virtual string Valor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _valor;
            }
        }
        
        public override string ToString() { return _valor; }

        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_valor = Format.DataReader.GetString(source, "VALOR");
        }

        #endregion

        #region Factory Methods

        private IdiomaInfo() { /* require use of factory methods */ }

        internal IdiomaInfo(long oid, string valor)
        {
            Oid = oid;
            _valor = valor;
        }

        private IdiomaInfo(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IdiomaInfo Get(IDataReader reader, bool childs)
        {
            return new IdiomaInfo(reader, childs);
        }

        #endregion

        #region Data Access

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                CopyValues(source);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
        }

        #endregion

    }
}
