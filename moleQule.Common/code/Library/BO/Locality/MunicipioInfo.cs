using System;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class MunicipioInfo : ReadOnlyBaseEx<MunicipioInfo>
	{
		#region Attributes

		public LocalityBase _base = new LocalityBase();

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public virtual string Localidad { get { return _base.Record.Localidad; } }
		public virtual string Nombre { get { return _base.Record.Valor; } }
		public virtual string Provincia { get { return _base.Record.Provincia; } }
		public virtual string Pais { get { return _base.Record.Pais; } }
		public virtual string CodPostal { get { return _base.Record.CodPostal; } }

        #endregion

        #region Business Methods


        public void CopyFrom(Municipio source) { _base.CopyValues(source); }

		#endregion

        #region Root Factory Methods

        /// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static MunicipioInfo Get(long oid, bool childs = false)
        {
            CriteriaEx criteria = Municipio.GetCriteria(Municipio.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = Municipio.SELECT(oid, false);

            MunicipioInfo obj = DataPortal.Fetch<MunicipioInfo>(criteria);
            Municipio.CloseSession(criteria.SessionCode);

            return obj;
        }

		public static MunicipioInfo New(long oid = 0) { return new MunicipioInfo() { Oid = oid }; }

        #endregion

		#region  Child Factory Methods

		private MunicipioInfo()	{ /* require use of factory methods */ }
        internal MunicipioInfo(Municipio source)
		{
            _base.CopyValues(source);
        }
        private MunicipioInfo (IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        /// <summary>
        /// Copia los datos al objeto desde un IDataReader 
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static MunicipioInfo GetChild(IDataReader reader, bool childs = false)
        {
            return new MunicipioInfo(reader, childs);
        }

		#endregion

        #region Root Data Access

        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

        #region Child Data Access

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

		#region SQL

		#endregion

	}
}
