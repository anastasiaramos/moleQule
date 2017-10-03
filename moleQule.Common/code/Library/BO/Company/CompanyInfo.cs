using System;
using System.Collections;
using System.Data;
using System.IO;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Hipatia;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CompanyInfo : ReadOnlyBaseEx<CompanyInfo>, moleQule.Library.ISchemaInfo, IAgenteHipatia
    {
        #region IAgenteHipatia

        public string NombreHipatia { get { return Name; } }
        public string IDHipatia { get { return Code; } }
		public Type TipoEntidad { get { return typeof(Company); } }
        public string ObservacionesHipatia { get { return string.Empty; } }

        #endregion

        #region ISchemaInfo

		public string Code { get { return _base._code; } set { _base._code = value; } }
		public string Name { get { return _base._name; } }
		public bool Principal { get { return _base._principal; } set { _base._principal = value; } }

		public virtual string SchemaCode { get { return _base.SchemaCode; } }

        #endregion

        #region Attributes

		public CompanyBase _base = new CompanyBase();

        private ContactoEmpresaList _contactos = null;

        #endregion

        #region Properties

        public long Serial { get { return _base.Record.Serial; } }
		public long Status { get { return _base.Record.Status; } }
		public string VatNumber { get { return _base.Record.VatNumber.ToUpper(); } }
		public long TipoID { get { return _base.Record.TipoId; } }
		public string CtaCotizacion { get { return _base.Record.CtaCotizacion.ToUpper(); } }
		public string Direccion { get { return _base.Record.Direccion; } }
		public string Municipio { get { return _base.Record.Municipio; } }
		public string CodPostal { get { return _base.Record.CodPostal; } }
		public string Provincia { get { return _base.Record.Provincia; } }
		public string CountryIso2 { get { return _base.Record.CountryIso2; } }
		public string CurrencyIso { get { return _base.Record.CurrencyIso; } }
		public string Telefonos { get { return _base.Record.Telefonos; } }
		public string Fax { get { return _base.Record.Fax; } }
		public string Url { get { return _base.Record.Url; } }
		public string Email { get { return _base.Record.Email; } }
		public string Responsable { get { return _base.Record.Responsable; } }
		public string Logo { get { return _base.Record.Logo; } }
		public string CuentaBancaria { get { return _base.Record.CuentaBancaria; } }
		public Decimal PIRPF { get { return _base.Record.PIrpf; } }
        public bool UseDefaultReports { get { return _base.Record.UseDefaultReports; } }

        public virtual ContactoEmpresaList Contactos { get { return _contactos; } }

		//UNMAPPED
		public virtual EEstado EStatus { get { return _base.EStatus; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		public virtual string Country { get { return _base.Country; } set { _base.Country = value; } }
		public virtual string Currency { get { return _base.Currency; } set { _base.Currency = value; } }

        #endregion

        #region Business Methods

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_base.CopyValues(source);
		}
		protected void CopyValues(Company source)
        {
            if (source == null) return;

            Oid = source.Oid;
			_base.CopyValues(source);
        }

        public void CopyFrom(Company source) { CopyValues(source); }

        public System.Byte[] GetImage()
        {
            System.Byte[] _logo_emp = null;

            string path = Properties.Settings.Default.LOGO_EMPRESA_PATH + Logo;

            // Cargamos la imagen en el buffer
            if (File.Exists(path))
            {
                //Declaramos fs para poder abrir la imagen.
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

                // Declaramos un lector binario para pasar la imagen a bytes
                BinaryReader br = new BinaryReader(fs);
                _logo_emp = new byte[(int)fs.Length];
                br.Read(_logo_emp, 0, (int)fs.Length);
                br.Close();
                fs.Close();
            }

            return _logo_emp;
        }

        public static string GetLogoPath(long schema)
        {
			return ModuleController.LOGOS_EMPRESAS_PATH + schema.ToString("00") + ".bmp";
        }

		public void SetCurrency() { _base.SetCurrency(); }

        #endregion

        #region Factory Methods

        private CompanyInfo() { /* require use of factory methods */ }
        private CompanyInfo(int sessionCode, IDataReader source, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(source);
        }
		internal CompanyInfo(Company item, bool copy_childs)
		{
			CopyValues(item);
			
			if (copy_childs)
			{
				_contactos = (item.Contactos != null) ? ContactoEmpresaList.GetChildList(item.Contactos) : null;
			}
		}

        public static CompanyInfo Get(long oid) { return Get(oid, false); }

        public static CompanyInfo Get(long oid, bool childs)
        {
            CriteriaEx criteria = Company.GetCriteria(Company.OpenSession());
            criteria.Childs = childs;

			criteria.Query = SELECT(oid);
			
			CompanyInfo obj = DataPortal.Fetch<CompanyInfo>(criteria);
            Company.CloseSession(criteria.SessionCode);
            return obj;
        }

        public static CompanyInfo GetByCode(string code)
        {
            CriteriaEx criteria = Company.GetCriteria(Company.OpenSession());

			QueryConditions conditions = new QueryConditions { Schema = CompanyInfo.New() };
            conditions.Schema.Oid = 0;
			conditions.Schema.Code = code;

			criteria.Query = SELECT(conditions);
			
			CompanyInfo obj = DataPortal.Fetch<CompanyInfo>(criteria);
            Company.CloseSession(criteria.SessionCode);
            return obj;
        }

        public static CompanyInfo Get(int sessionCode, IDataReader reader, bool childs)
        {
            return new CompanyInfo(sessionCode, reader, childs);
        }

        public static ISchemaInfo GetISchemaInfo(long oid)
        {
            return (ISchemaInfo)CompanyInfo.Get(oid);
        }
        public static ISchema GetISchema(long oid)
        {
            return (ISchema)Company.Get(oid);
        }

        public static CompanyInfo New(long oid = 0)
        {
            return new CompanyInfo() { Oid = oid };
        }

        #endregion

        #region ISchemaInfo

        public ISchemaInfo IGet(long oid)
        {
            return (ISchemaInfo)CompanyInfo.Get(oid);
        }

        public ISchema IGetSchema(long oid)
        {
            return (ISchema)Company.Get(oid);
        }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                _base.Record.Oid = 0;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

				IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

				if (reader.Read())
					CopyValues(reader);

                if (Childs)
                {
                    criteria = ContactoEmpresa.GetCriteria(Session());
                    criteria.AddEq("OidEmpresa", this.Oid);
                    _contactos = ContactoEmpresaList.GetChildList(criteria.List<ContactoEmpresa>());
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                CopyValues(source);

                if (Childs)
                {
                    string query = ContactoEmpresaList.SELECT(this);
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _contactos = ContactoEmpresaList.GetChildList(reader);
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        #endregion

		#region SQL

		public static string SELECT(long oid) { return Company.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Company.SELECT(conditions, false); }
		
		#endregion
	}
}