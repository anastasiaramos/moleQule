using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CompanyRecord : RecordBase
    {
        #region Attributes

        private long _serial;
        private string _code = string.Empty;
		private long _status;
        private string _name = string.Empty;
        private string _vat_number = string.Empty;
        private long _tipo_id;
        private string _cta_cotizacion = string.Empty;
        private string _direccion = string.Empty;
        private string _municipio = string.Empty;
        private string _cod_postal = string.Empty;
        private string _provincia = string.Empty;
		private string _country_iso2 = string.Empty;
		private string _currency_iso = string.Empty;
        private string _telefonos = string.Empty;
        private string _fax = string.Empty;
        private string _url = string.Empty;
        private string _email = string.Empty;
        private string _responsable = string.Empty;
        private string _logo = string.Empty;
        private string _cuenta_bancaria = string.Empty;
        private Decimal _p_irpf;
        private bool _use_default_reports = false;

        #endregion

        #region Properties

        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Code { get { return _code; } set { _code = value; } }
		public virtual long Status { get { return _status; } set { _status = value; } }
        public virtual string Name { get { return _name; } set { _name = value; } }
        public virtual string VatNumber { get { return _vat_number; } set { _vat_number = value; } }
        public virtual long TipoId { get { return _tipo_id; } set { _tipo_id = value; } }
        public virtual string CtaCotizacion { get { return _cta_cotizacion; } set { _cta_cotizacion = value; } }
        public virtual string Direccion { get { return _direccion; } set { _direccion = value; } }
        public virtual string Municipio { get { return _municipio; } set { _municipio = value; } }
        public virtual string CodPostal { get { return _cod_postal; } set { _cod_postal = value; } }
        public virtual string Provincia { get { return _provincia; } set { _provincia = value; } }
		public virtual string CountryIso2 { get { return _country_iso2; } set { _country_iso2 = value; } }
		public virtual string CurrencyIso { get { return _currency_iso; } set { _currency_iso = value; } }
        public virtual string Telefonos { get { return _telefonos; } set { _telefonos = value; } }
        public virtual string Fax { get { return _fax; } set { _fax = value; } }
        public virtual string Url { get { return _url; } set { _url = value; } }
        public virtual string Email { get { return _email; } set { _email = value; } }
        public virtual string Responsable { get { return _responsable; } set { _responsable = value; } }
        public virtual string Logo { get { return _logo; } set { _logo = value; } }
        public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }
        public virtual Decimal PIrpf { get { return _p_irpf; } set { _p_irpf = value; } }
        public virtual bool UseDefaultReports { get { return _use_default_reports; } set { _use_default_reports = value; } }

        #endregion

        #region Business Methods

        public CompanyRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _code = Format.DataReader.GetString(source, "CODIGO");
			_status = Format.DataReader.GetInt64(source, "STATUS");
            _name = Format.DataReader.GetString(source, "NOMBRE");
            _vat_number = Format.DataReader.GetString(source, "VAT_NUMBER");
            _tipo_id = Format.DataReader.GetInt64(source, "TIPO_ID");
            _cta_cotizacion = Format.DataReader.GetString(source, "CTA_COTIZACION");
            _direccion = Format.DataReader.GetString(source, "DIRECCION");
            _municipio = Format.DataReader.GetString(source, "MUNICIPIO");
            _cod_postal = Format.DataReader.GetString(source, "COD_POSTAL");
            _provincia = Format.DataReader.GetString(source, "PROVINCIA");
			_country_iso2 = Format.DataReader.GetString(source, "COUNTRY");
			_currency_iso = Format.DataReader.GetString(source, "CURRENCY");
            _telefonos = Format.DataReader.GetString(source, "TELEFONOS");
            _fax = Format.DataReader.GetString(source, "FAX");
            _url = Format.DataReader.GetString(source, "URL");
            _email = Format.DataReader.GetString(source, "EMAIL");
            _responsable = Format.DataReader.GetString(source, "RESPONSABLE");
            _logo = Format.DataReader.GetString(source, "LOGO");
            _cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
            _p_irpf = Format.DataReader.GetDecimal(source, "P_IRPF");
            _use_default_reports = Format.DataReader.GetBool(source, "USE_DEFAULT_REPORTS");

        }
        public virtual void CopyValues(CompanyRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _serial = source.Serial;
            _code = source.Code;
			_status = source.Status;
            _name = source.Name;
            _vat_number = source.VatNumber;
            _tipo_id = source.TipoId;
            _cta_cotizacion = source.CtaCotizacion;
            _direccion = source.Direccion;
            _municipio = source.Municipio;
            _cod_postal = source.CodPostal;
            _provincia = source.Provincia;
			_country_iso2 = source.CountryIso2;
			_currency_iso = source.CurrencyIso;
            _telefonos = source.Telefonos;
            _fax = source.Fax;
            _url = source.Url;
            _email = source.Email;
            _responsable = source.Responsable;
            _logo = source.Logo;
            _cuenta_bancaria = source.CuentaBancaria;
            _p_irpf = source.PIrpf;
            _use_default_reports = source.UseDefaultReports;
        }
        
		#endregion
    }

    [Serializable()]
	public class CompanyBase
    {
        #region ISchema

        internal string _code = string.Empty;
        internal string _name = string.Empty;
        internal bool _principal = false;

        internal string SchemaCode { get { return (string.IsNullOrEmpty(_code)) ? "0000" : Convert.ToInt64(_code).ToString("0000"); } }

        #endregion

        #region Attributes

        private CompanyRecord _record = new CompanyRecord();

        #endregion

        #region Properties

		public CompanyRecord Record { get { return _record; } }

		public EEstado EStatus { get { return (EEstado)_record.Status; } }
		public string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
		public string Country 
		{ 
			get { return Library.Country.Find(_record.CountryIso2).Name; }
			set { _record.CountryIso2 = Library.Country.FindByName(value).Iso2; }
		}
		public string Currency
		{
			get { return Library.Currency.Find(_record.CurrencyIso).Name; }
			set { _record.CurrencyIso = Library.Currency.FindByName(value).ISOCode; }
		}

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _name = _record.Name;
            _code = _record.Code;
            _principal = Format.DataReader.GetBool(source, "DEFAULT_SCHEMA");
        }

        public void CopyValues(Company source)
        {
            if (source == null) return;

			_record.CopyValues(source._base.Record);

            _name = _record.Name;
            _code = _record.Code;
        }
        public void CopyValues(CompanyInfo source)
        {
            if (source == null) return;

			_record.CopyValues(source._base.Record);

            _name = _record.Name;
            _code = _record.Code;            
        }

		public void SetCurrency()
		{
			Library.Currency currency = Library.Currency.Find(_record.CurrencyIso);
			AppControllerBase.Culture.NumberFormat.CurrencySymbol = currency.Symbol;
			CultureInfo culture = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(c => c.Name.EndsWith(currency.RegionInfo.TwoLetterISORegionName)).FirstOrDefault();
			if (culture != null)
				AppControllerBase.Culture.NumberFormat.CurrencyDecimalDigits = culture.NumberFormat.CurrencyDecimalDigits;
		}

        #endregion
    }

	[Serializable()]
	public class Schema : Company { }

    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Company : BusinessBaseEx<Company>, moleQule.Library.ISchema
    {
        #region ISchema

        public virtual string Code
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Code;
            }

            set
            {
                CanWriteProperty(true);

				if (_base._code != value)
                {
					_base.Record.Code = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Name
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
				return _base.Record.Name;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Name != value)
                {
					_base.Record.Name = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Principal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
				return _base._principal;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
				if (_base._principal != value)
                {
					_base._principal = value;
                    PropertyHasChanged();
                }
            }
        }

		public virtual string SchemaCode { get { return _base.SchemaCode; } }

        #endregion

        #region Attributes

		public CompanyBase _base = new CompanyBase();

        private ContactoEmpresas _contactos = ContactoEmpresas.NewChildList();

        #endregion

        #region Properties

        public CompanyBase Base { get { return _base; } }

        public override long Oid
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Oid;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Oid.Equals(value))
                {
                    _base.Record.Oid = value;
                    //PropertyHasChanged();
                }
            }
        }
        public virtual long Serial
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Serial;
            }

            set
            {
                //CanWriteProperty(true);
                _base.Record.Serial = value;
            }
        }
		public virtual long Status
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Status;
			}

			set
			{
				//CanWriteProperty(true);
				_base.Record.Status = value;
			}
		}
		public virtual string VatNumber

        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.VatNumber.ToUpper();
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_base.Record.VatNumber != value)
                {
                    _base.Record.VatNumber = value.ToUpper();
                    PropertyHasChanged();
                }
            }
        }
        public virtual long TipoID
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.TipoId;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
				if (!_base.Record.TipoId.Equals(value))
                {
					_base.Record.TipoId = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string CtaCotizacion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.CtaCotizacion.ToUpper();
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.CtaCotizacion != value)
                {
					_base.Record.CtaCotizacion = value.ToUpper();
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Direccion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Direccion;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Direccion != value)
                {
					_base.Record.Direccion = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Municipio
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Municipio;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Municipio != value)
                {
					_base.Record.Municipio = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string CodPostal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.CodPostal;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.CodPostal != value)
                {
					_base.Record.CodPostal = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Provincia
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Provincia;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Provincia != value)
                {
					_base.Record.Provincia = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual string CountryIso2
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CountryIso2;
			}

			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (_base.Record.CountryIso2 != value)
				{
					_base.Record.CountryIso2 = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CurrencyIso
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CurrencyIso;
			}

			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (_base.Record.CurrencyIso != value)
				{
					_base.Record.CurrencyIso = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string Telefonos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Telefonos;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Telefonos != value)
                {
					_base.Record.Telefonos = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Fax
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Fax;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Fax != value)
                {
					_base.Record.Fax = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Url
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Url;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Url != value)
                {
					_base.Record.Url = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Email
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Email;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Email != value)
                {
					_base.Record.Email = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Responsable
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Responsable;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Responsable != value)
                {
					_base.Record.Responsable = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Logo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Logo;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Logo != value)
                {
					_base.Record.Logo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string CuentaBancaria
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.CuentaBancaria;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

				if (!_base.Record.CuentaBancaria.Equals(value))
                {
					_base.Record.CuentaBancaria = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual Decimal PIRPF
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.PIrpf;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.PIrpf.Equals(value))
				{
					_base.Record.PIrpf = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual bool UseDefaultReports
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.UseDefaultReports;
            }

            set
            {
                //CanWriteProperty(true);
                _base.Record.UseDefaultReports = value;
                PropertyHasChanged();
            }
        }

        public virtual ContactoEmpresas Contactos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _contactos;
            }
        }

		//UNMAPPED
		public virtual EEstado EStatus { get { return _base.EStatus; } set { Status = (long)value; PropertyHasChanged(); } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		public virtual string Country { get { return _base.Country; } set { _base.Country = value; } }
		public virtual string Currency { get { return _base.Currency; } set { _base.Currency = value; } }

        public override bool IsValid
        {
            get { return base.IsValid && _contactos.IsValid; }
        }
        public override bool IsDirty
        {
            get { return base.IsDirty || _contactos.IsDirty; }
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Clona la entidad y sus subentidades y las marca como nuevas
        /// </summary>
        /// <returns>Una entidad clon</returns>
        public virtual Company CloneAsNew()
        {
            Company clon = null;
            clon = base.Clone();

            // Se definen el Oid y el Codigo como nueva entidad
            Random rd = new Random();
            clon.Oid = rd.Next();
            clon.GetNewCode();

            clon.SessionCode = Company.OpenSession();
            Company.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.Contactos.MarkAsNew();

            return clon;
        }

        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(Company));
            Code = Serial.ToString(Properties.Settings.Default.EMPRESA_CODE_FORMAT);
        }

		public void SetCurrency() { _base.SetCurrency(); }
		public void SetCurrency(string ISOCode)
		{
			CurrencyIso = ISOCode;
			SetCurrency();
		}

        #endregion

        #region Validation Rules

		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
			//Codigo
			if (Code == string.Empty)
			{
				e.Description = Resources.Messages.NO_ID_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			//Nombre
			if (Name == string.Empty)
			{
				e.Description = String.Format(Resources.Messages.NO_FIELD_FILLED, "Nombre");
				throw new iQValidationException(e.Description, string.Empty);
			}

			//ID
			AgenteBase.ValidateInput((ETipoID)TipoID, "ID", VatNumber);

			return true;
		}	

        #endregion

        #region Authorization Rules

        public static bool CanAddObject() { return AppContext.User.IsSuperUser; }

        public static bool CanGetObject() { return true; }

		public static bool CanDeleteObject() { return AppContext.User.IsSuperUser; }

		public static bool CanEditObject() { return AutorizationRulesControler.CanEditObject(Resources.SecureItems.EMPRESA); }

        #endregion

        #region Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USA LA FUNCION New
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero es protected por exigencia de NHibernate.
        /// </summary>
        public Company() { }

        public static Company New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<Company>(new CriteriaCs(-1));
        }

        public static Company Get(long oid)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Company.GetCriteria(Company.OpenSession());
            criteria.Query = Company.SELECT(oid);

            Company.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<Company>(criteria);
        }

        public virtual CompanyInfo GetInfo(bool childs = false) { return new CompanyInfo(this, childs); }

        /// <summary>
        /// Borrado inmediato, no cabe "undo"
        /// (La función debe ser "estática")
        /// </summary>
        /// <param name="oid"></param>
        public static void Delete(long oid)
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            DataPortal.Delete(new CriteriaCs(oid));
        }

        public override Company Save()
        {
            // Por la posible doble interfaz Root/Child
            if (IsChild) throw new iQException(moleQule.Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
            
            if (IsDeleted && !CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
            else if (IsNew && !CanAddObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
            else if (!CanEditObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            // Guardamos si es nuevo pq tras el Save la propiedad IsNew es false
            // y necesitamos saber si es un Insert para crear el eschema de usuarios
            // tras guardar la empresa
            Boolean isNew = IsNew;

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                base.Save();

                _contactos.Update(this);

                Transaction().Commit();

                // Si es una inserción de una nueva empresa tenemos que crear 
                // el mapa de permisos de los usuarios
                if (isNew) PrincipalBase.NewSchema(this.Oid);

                return this;
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
                if (CloseSessions && (this.IsNew || Transaction().WasCommitted)) CloseSession();
                else BeginTransaction();
            }
        }

        #endregion

        #region ISchema

        /// <summary>
        /// Borrado inmediato, no cabe "undo"
        /// </summary>
        /// <param name="oid"></param>
        public void IDelete(long oid)
        {
            CloseDBObject();
            Company.Delete(oid);
        }

        public ISchema IGet(long oid)
        {
            return (ISchema)Company.Get(oid);
        }

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
			_base.Record.Oid = (long)(new Random()).Next();
            GetNewCode();

            _contactos = ContactoEmpresas.NewChildList();
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                _base.Record.Oid = 0;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
					//Empresa.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
						_base.CopyValues(reader);

                    if (Childs)
                    {
						//ContactoEmpresa.DoLOCK(Session());
                        string query = ContactoEmpresa.SELECT(this); ;
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _contactos = ContactoEmpresas.GetChildList(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Insert()
        {
            try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
                GetNewCode();
                Session().Save(Base.Record);
            }
            catch (ValidationException ex)
            {
                throw new iQValidationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Update()
        {
            if (IsDirty)
            {
                try
                {
                    CompanyRecord obj = Session().Get<CompanyRecord>(Oid);
                    obj.CopyValues(this._base.Record);
                    Session().Update(obj);
                }
                catch (Exception ex)
                {
                    throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
                }
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new CriteriaCs(Oid));
        }

        [Transactional(TransactionalTypes.Manual)]
        private void DataPortal_Delete(CriteriaCs criteria)
        {
            // Iniciamos la conexion y la transaccion
            SessionCode = OpenSession();
            BeginTransaction();

            try
            {
                Session().Delete(Session().Get<CompanyRecord>(criteria.Oid));

                Transaction().Commit();

                //Borramos los usuarios que únicamente tenían acceso a esta empresa
                Users usuarios = Users.GetNoSchemaList();

                foreach (User usr in usuarios)
                    User.Delete(usr.Oid);

				//DROP SCHEMA
				//CREATE SCHEMA
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                CloseSession();
            }
        }

        #endregion

        #region Commands

        public static bool Exists(string codigo)
        {
            ExistsCmd result;
            result = DataPortal.Execute<ExistsCmd>(new ExistsCmd(codigo));
            return result.Exists;
        }

        [Serializable()]
        private class ExistsCmd : CommandBase
        {
            private string _codigo = string.Empty;
            private bool _exists = false;

            public bool Exists
            {
                get { return _exists; }
            }

            public ExistsCmd(string codigo)
            {
                _codigo = codigo;
            }

            protected override void DataPortal_Execute()
            {
                // Buscar por codigo
                CriteriaEx criteria = Company.GetCriteria(Company.OpenSession());
                criteria.AddEq("Code", _codigo);
                CompanyList lista = CompanyList.GetList(criteria);
                _exists = !(lista.Count == 0);
            }
        }

        #endregion

		#region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>()
            {
            };
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

        internal static string SELECT_FIELDS(QueryConditions conditions)
		{
			string query;

            query = "SELECT EM.*";

            if (conditions.User != null)
            {
                query += @"
                	,CASE WHEN (COALESCE(US.""VALUE"", '0') = CAST(EM.""OID"" AS text)) THEN TRUE ELSE FALSE END AS ""DEFAULT_SCHEMA""";
            }
            else
            {
                query += @"
					,FALSE AS ""DEFAULT_SCHEMA""";
            }

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string em = nHManager.Instance.GetSQLTable(typeof(CompanyRecord));
			string su = nHManager.Instance.GetSQLTable(typeof(SchemaUserRecord));
			string us = nHManager.Instance.GetSQLTable(typeof(UserSettingRecord));

			string query;

			query = @"
			   FROM " + em + " AS EM";

			if (conditions.User != null)
			{
				query += @"
				   INNER JOIN " + su + @" AS SU ON SU.""OID_SCHEMA"" = EM.""OID"" 
				   INNER JOIN " + us + @" AS US ON US.""OID_USER"" = SU.""OID_USER"" 
				   AND US.""NAME"" = '" + PrincipalBase.DefaultSchemaSettingName + "'";
			}

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

            query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "EM", ForeignFields());

			if (conditions.Schema != null)
			{
				if (conditions.Schema.Oid != 0) 
					query += @"
						AND EM.""OID"" = " + conditions.Schema.Oid;
				if (conditions.Schema.Code != string.Empty) 
					query += @"
						AND EM.""CODIGO"" = '" + conditions.Schema.Code + "'";
			}

            if (conditions.User != null && !conditions.User.IsAdmin) query += " AND SU.\"OID_USER\" = " + conditions.User.Oid;

            if (conditions.User != null && conditions.User.IsAdmin) query += " AND SU.\"OID_USER\" = 1";

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
            string em = nHManager.Instance.GetSQLTable(typeof(CompanyRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(SchemaUserRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserSettingRecord));

			string query;

            query =
                SELECT_FIELDS(conditions) +
				JOIN(conditions) +
                WHERE(conditions);

			if (conditions != null)
			{
				if (conditions.Orders == null)
				{
					conditions.Orders = new OrderList();
					conditions.Orders.Add(FilterMng.BuildOrderItem("Code", ListSortDirection.Ascending, typeof(Company)));
				}

				query += ORDER(conditions.Orders, "EM", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			//if (lock_table) query += " FOR UPDATE OF I NOWAIT";

			return query;
		}

		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions, lockTable);
		}

		public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
		public static string SELECT_COUNT(QueryConditions conditions)
		{
			string query;

			query = @"
                SELECT COUNT(*) AS ""TOTAL_ROWS""" +
				SELECT(conditions) +
				WHERE(conditions);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { Schema = CompanyInfo.New(oid) }, lockTable);
		}

		#endregion
    }
}
