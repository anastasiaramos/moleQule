using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;
using moleQule.Library;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class CompanyContactRecord : RecordBase
	{
		#region Attributes

		private long _oid_empresa;
		private string _cargo = string.Empty;
		private string _nombre = string.Empty;
		private string _dni = string.Empty;
		private string _direccion = string.Empty;
		private string _cod_postal = string.Empty;
		private string _municipio = string.Empty;
		private string _provincia = string.Empty;
		private string _telefonos = string.Empty;
  
		#endregion
		
		#region Properties
		public virtual long OidEmpresa { get { return _oid_empresa; } set { _oid_empresa = value; } }
		public virtual string Cargo { get { return _cargo; } set { _cargo = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual string Dni { get { return _dni; } set { _dni = value; } }
		public virtual string Direccion { get { return _direccion; } set { _direccion = value; } }
		public virtual string CodPostal { get { return _cod_postal; } set { _cod_postal = value; } }
		public virtual string Municipio { get { return _municipio; } set { _municipio = value; } }
		public virtual string Provincia { get { return _provincia; } set { _provincia = value; } }
		public virtual string Telefonos { get { return _telefonos; } set { _telefonos = value; } }
		
		#endregion
		
		#region Business Methods
		
		public CompanyContactRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_empresa = Format.DataReader.GetInt64(source, "OID_EMPRESA");
			_cargo = Format.DataReader.GetString(source, "CARGO");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_dni = Format.DataReader.GetString(source, "DNI");
			_direccion = Format.DataReader.GetString(source, "DIRECCION");
			_cod_postal = Format.DataReader.GetString(source, "COD_POSTAL");
			_municipio = Format.DataReader.GetString(source, "MUNICIPIO");
			_provincia = Format.DataReader.GetString(source, "PROVINCIA");
			_telefonos = Format.DataReader.GetString(source, "TELEFONOS");

		}
		
		public virtual void CopyValues(CompanyContactRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_empresa = source.OidEmpresa;
			_cargo = source.Cargo;
			_nombre = source.Nombre;
			_dni = source.Dni;
			_direccion = source.Direccion;
			_cod_postal = source.CodPostal;
			_municipio = source.Municipio;
			_provincia = source.Provincia;
			_telefonos = source.Telefonos;
		}
		#endregion	
	}

    [Serializable()]
	public class CompanyContactBase 
	{	 
		#region Attributes
		
		private CompanyContactRecord _record = new CompanyContactRecord();
		
		public CompanyContactRecord Record { get { return _record; } }
		
		#endregion
		
		#region Properties
		
		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);
		}
		
		internal void CopyValues(ContactoEmpresa source)
		{
			if (source == null) return;
			
			_record.CopyValues(source._base.Record);
		}
		internal void CopyValues(ContactoEmpresaInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source._base.Record);
		}
		#endregion	
	}
		
    /// <summary>
    /// Editable Child Business Object
    /// </summary>
    [Serializable()]
    public class ContactoEmpresa : BusinessBaseEx<ContactoEmpresa>
    {
        #region Attributes

		public CompanyContactBase _base = new CompanyContactBase();

		#endregion

		#region Properties

		public CompanyContactBase Base { get { return _base; } }

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
				_base.Record.Oid = value;
			}
		}
        public virtual long OidEmpresa
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidEmpresa;
            }

            set
            {
                //CanWriteProperty(true);
                _base.Record.OidEmpresa = value;
                PropertyHasChanged();
            }
        }
        public virtual string Nombre
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Nombre;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_base.Record.Nombre.Equals(value))
                {
                    _base.Record.Nombre = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Cargo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Cargo;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_base.Record.Cargo.Equals(value))
                {
                    _base.Record.Cargo = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Dni
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Dni;
            }

            set
            {
                CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (!_base.Record.Dni.Equals(value))
                {
                    _base.Record.Dni = value;
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
                if (!_base.Record.Direccion.Equals(value))
                {
                    _base.Record.Direccion = value;
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
                if (!_base.Record.CodPostal.Equals(value))
                {
                    _base.Record.CodPostal = value;
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
                if (!_base.Record.Municipio.Equals(value))
                {
                    _base.Record.Municipio = value;
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
                if (!_base.Record.Provincia.Equals(value))
                {
                    _base.Record.Provincia = value;
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
                if (!_base.Record.Telefonos.Equals(value))
                {
                    _base.Record.Telefonos = value;
                    PropertyHasChanged();
                }
            }
        }
        
        public override bool IsValid
        {
            get { return base.IsValid; }
        }

        public override bool IsDirty
        {
            get { return base.IsDirty; }
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(
                Csla.Validation.CommonRules.StringRequired, "Nombre");
        }

        #endregion

        #region Authorization Rules

        protected override void AddAuthorizationRules()
        {
        }

        public static bool CanAddObject()
        {
            return AppContext.User.IsAdmin;
        }

        public static bool CanGetObject()
        {
            return true;
        }

        public static bool CanDeleteObject()
        {
            return AppContext.User.IsAdmin;
        }

        public static bool CanEditObject()
        {
            return AppContext.User.IsAdmin;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public ContactoEmpresa()
        {
            MarkAsChild();
            Random r = new Random();
            Oid = (long)r.Next();
        }

        private ContactoEmpresa(ContactoEmpresa source)
        {
            MarkAsChild();
            Fetch(source);
        }

        private ContactoEmpresa(IDataReader reader)
        {
            MarkAsChild();
            Fetch(reader);
        }

        public static ContactoEmpresa NewChild(Company parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(
                    moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            ContactoEmpresa obj = new ContactoEmpresa();
            obj.OidEmpresa = parent.Oid;
            return obj;
        }

        internal static ContactoEmpresa GetChild(ContactoEmpresa source)
        {
            return new ContactoEmpresa(source);
        }

        internal static ContactoEmpresa GetChild(IDataReader reader)
        {
            return new ContactoEmpresa(reader);
        }

        public virtual ContactoEmpresaInfo GetInfo()
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(
                  moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            return new ContactoEmpresaInfo(Oid, OidEmpresa, Nombre, Cargo, Dni, Direccion,
                                     CodPostal, Municipio, Provincia, Telefonos);

        }

        /// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(
                    moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }

        /// <summary>
        /// No se debe utilizar esta función para guardar. Hace falta el padre.
        /// Utilizar Insert o Update en sustitución de Save.
        /// </summary>
        /// <returns></returns>
        public override ContactoEmpresa Save()
        {
            throw new iQException(moleQule.Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }

        #endregion

        #region Child Data Access

        private void Fetch(ContactoEmpresa source)
        {
			_base.CopyValues(source);
            MarkOld();
        }

        private void Fetch(IDataReader source)
        {
            _base.CopyValues(source);
            MarkOld();
        }

        internal void Insert(Company parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            _base.Record.OidEmpresa = parent.Oid;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void Update(Company parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            _base.Record.OidEmpresa = parent.Oid;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                CompanyContactRecord obj = parent.Session().Get<CompanyContactRecord>(Oid);
                obj.CopyValues(this._base.Record);
                parent.Session().Update(obj);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void DeleteSelf(Company parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                parent.Session().Delete(parent.Session().Get<CompanyContactRecord>(Oid));
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkNew();
        }

        #endregion

        #region Commands

        public static bool Exists(long oid)
        {
            ExistsCmd result;
            result = DataPortal.Execute<ExistsCmd>(new ExistsCmd(oid));
            return result.Exists;
        }

        [Serializable()]
        private class ExistsCmd : CommandBase
        {
            private long _codigo;
            private bool _exists = false;

            public bool Exists
            {
                get { return _exists; }
            }

            public ExistsCmd(long oid)
            {
                _codigo = oid;
            }

            protected override void DataPortal_Execute()
            {
                // Buscar por Oid
                CriteriaEx criteria = ContactoEmpresa.GetCriteria(ContactoEmpresa.OpenSession());
                criteria.AddOidSearch(_codigo);
                ContactoEmpresaList list = ContactoEmpresaList.GetList(criteria);
                _exists = (list.Count > 0);
            }
        }

        #endregion

		#region SQL

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT CE.*";

			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
            string ce = nHManager.Instance.GetSQLTable(typeof(CompanyContactRecord));
			string query;

			query = SELECT_FIELDS() +
					" FROM " + ce + " AS CE";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query = " WHERE TRUE";
			if (conditions.Schema != null) query += " AND CE.\"OID_EMPRESA\" = " + conditions.Schema.Oid.ToString();

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query;

			query = SELECT_BASE(conditions);

			query += WHERE(conditions);

			//if (lock_table) query += " FOR UPDATE OF SU NOWAIT";

			return query;
		}

        public static string SELECT(Company empresa, bool lockTable = false)
        { 
            QueryConditions conditions = new QueryConditions
            {
                Schema = empresa.GetInfo()
            };

            return SELECT(conditions, lockTable);
        }

		#endregion
    }
}

