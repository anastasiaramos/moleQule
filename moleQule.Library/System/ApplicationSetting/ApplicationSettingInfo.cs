using System;
using System.Data;
using System.Collections.Generic;

using NHibernate;
using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	[Serializable()]
	public class ApplicationSettingInfo : ReadOnlyBaseEx<ApplicationSettingInfo>
	{
		#region Attributes

		public ApplicationSettingBase _base = new ApplicationSettingBase();

		#endregion

		#region  Properties

		public ApplicationSettingBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } }
		public virtual string Name { get { return _base.Record.Name; } }
        public virtual string Value { get { return _base.Record.Value; } }

		public override string ToString() {	return _base.Record.Name; }

		/// <summary>
		/// Copia los atributos del objeto
		/// </summary>
		/// <param name="_s">Objeto origen</param>
		protected virtual void CopyValues(ApplicationSetting source)
		{
			if (source == null) return;

			Oid = source.Oid;
            _base.CopyValues(source);
		}


        /// <summary>
        /// Carga los valores de un registro apuntado por un IDataReader en el objeto.
        /// Consulta el fichero de mapeo de tablas de nHibernate para rellenar las propiedades.
        /// </summary>
        /// <param name="source"></param>
        protected void CopyValues(IDataReader source)
        {
            //base.CopyValues(source);

            Oid = Convert.ToInt32(source["OID"]);
            _base.CopyValues(source);
        }

		#endregion

		#region Factory Methods

		private ApplicationSettingInfo()	{ /* require use of factory methods */ }

		internal ApplicationSettingInfo(ApplicationSetting source)
		{
            _base.CopyValues(source);
		}

		public static ApplicationSettingInfo Get(string nombre)
		{
            try
            {
                CriteriaEx criteria = ApplicationSetting.GetCriteria(ApplicationSetting.OpenSession());

                criteria.Query = ApplicationSetting.SELECT_BY_NAME(nombre);

                ApplicationSettingInfo obj = DataPortal.Fetch<ApplicationSettingInfo>(criteria);
                ApplicationSetting.CloseSession(criteria.SessionCode);

                return obj;
            }
            catch (Exception)
            {
                return null;
            }
		}

        #endregion

        #region Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
            {               
				if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = null;

                    reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        CopyValues(reader);
                }
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

        #region SQL

        public static string SELECT_BY_NAME(string name) { return ApplicationSetting.SELECT_BY_NAME(name, false); }

        #endregion
	}
}
