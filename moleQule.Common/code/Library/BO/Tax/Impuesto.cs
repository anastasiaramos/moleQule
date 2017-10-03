using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;
using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class TaxRecord : RecordBase
    {
        #region Attributes

        private string _nombre = string.Empty;
        private Decimal _porcentaje;
        private string _cuenta_contable_repercutido = string.Empty;
        private string _cuenta_contable_soportado = string.Empty;
        private string _observaciones = string.Empty;
        private long _oid_subtipo_factura_emitida;
        private long _oid_subtipo_factura_recibida;

        #endregion

        #region Properties
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual Decimal Porcentaje { get { return _porcentaje; } set { _porcentaje = value; } }
        public virtual string CuentaContableRepercutido { get { return _cuenta_contable_repercutido; } set { _cuenta_contable_repercutido = value; } }
        public virtual string CuentaContableSoportado { get { return _cuenta_contable_soportado; } set { _cuenta_contable_soportado = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual long OidSubtipoFacturaEmitida { get { return _oid_subtipo_factura_emitida; } set { _oid_subtipo_factura_emitida = value; } }
        public virtual long OidSubtipoFacturaRecibida { get { return _oid_subtipo_factura_recibida; } set { _oid_subtipo_factura_recibida = value; } }

        #endregion

        #region Business Methods

        public TaxRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _porcentaje = Format.DataReader.GetDecimal(source, "PORCENTAJE");
            _cuenta_contable_repercutido = Format.DataReader.GetString(source, "CUENTA_CONTABLE_REPERCUTIDO");
            _cuenta_contable_soportado = Format.DataReader.GetString(source, "CUENTA_CONTABLE_SOPORTADO");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _oid_subtipo_factura_emitida = Format.DataReader.GetInt64(source, "OID_SUBTIPO_FACTURA_EMITIDA");
            _oid_subtipo_factura_recibida = Format.DataReader.GetInt64(source, "OID_SUBTIPO_FACTURA_RECIBIDA");

        }

        public virtual void CopyValues(TaxRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _nombre = source.Nombre;
            _porcentaje = source.Porcentaje;
            _cuenta_contable_repercutido = source.CuentaContableRepercutido;
            _cuenta_contable_soportado = source.CuentaContableSoportado;
            _observaciones = source.Observaciones;
            _oid_subtipo_factura_emitida = source.OidSubtipoFacturaEmitida;
            _oid_subtipo_factura_recibida = source.OidSubtipoFacturaRecibida;
        }
        #endregion
    }

    [Serializable()]
    public class TaxBase
    {
        #region Attributes

        private TaxRecord _record = new TaxRecord();

        private string _codigo_impuestoA3_emitida = string.Empty;
        private string _codigo_impuestoA3_recibida = string.Empty;

        public TaxRecord Record { get { return _record; } }

        #endregion

        #region Properties

        public virtual string CodigoImpuestoA3Emitida { get { return _codigo_impuestoA3_emitida; } set { _codigo_impuestoA3_emitida = value; } }
        public virtual string CodigoImpuestoA3Recibida { get { return _codigo_impuestoA3_recibida; } set { _codigo_impuestoA3_recibida = value; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _codigo_impuestoA3_emitida = Format.DataReader.GetString(source, "CODIGO_IMPUESTO_A3_EMITIDA");
            _codigo_impuestoA3_recibida = Format.DataReader.GetString(source, "CODIGO_IMPUESTO_A3_RECIBIDA");
        }

        internal void CopyValues(Impuesto source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _codigo_impuestoA3_emitida = source.CodigoImpuestoA3Emitida;
            _codigo_impuestoA3_recibida = source.CodigoImpuestoA3Recibida;
        }
        internal void CopyValues(ImpuestoInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _codigo_impuestoA3_emitida = source.CodigoImpuestoA3Emitida;
            _codigo_impuestoA3_recibida = source.CodigoImpuestoA3Recibida;
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
	[Serializable()]
	public class Impuesto : BusinessBaseEx<Impuesto>
	{
		#region Attributes

        public TaxBase _base = new TaxBase();

		#endregion

		#region Properties

		public TaxBase Base { get { return _base; } }

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
		public virtual string Nombre
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Nombre;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
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
		public virtual Decimal Porcentaje
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Porcentaje;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Porcentaje.Equals(value))
				{
					_base.Record.Porcentaje = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaContableRepercutido
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaContableRepercutido;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CuentaContableRepercutido.Equals(value))
				{
					_base.Record.CuentaContableRepercutido = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaContableSoportado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.CuentaContableSoportado;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.CuentaContableSoportado.Equals(value))
				{
					_base.Record.CuentaContableSoportado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Observaciones
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Observaciones;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Observaciones.Equals(value))
				{
					_base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long OidSubtipoFacturaEmitida
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidSubtipoFacturaEmitida;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidSubtipoFacturaEmitida.Equals(value))
                {
                    _base.Record.OidSubtipoFacturaEmitida = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long OidSubtipoFacturaRecibida
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidSubtipoFacturaRecibida;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidSubtipoFacturaRecibida.Equals(value))
                {
                    _base.Record.OidSubtipoFacturaRecibida = value;
                    PropertyHasChanged();
                }
            }
        }

        public virtual string CodigoImpuestoA3Emitida { get { return _base.CodigoImpuestoA3Emitida; } set { _base.CodigoImpuestoA3Emitida = value; } }
        public virtual string CodigoImpuestoA3Recibida { get { return _base.CodigoImpuestoA3Recibida; } set { _base.CodigoImpuestoA3Recibida = value; } }

		#endregion

		#region Business Methods

		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>
		public virtual Impuesto CloneAsNew()
		{
			Impuesto clon = base.Clone();

			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();

			clon.SessionCode = Impuesto.OpenSession();
			Impuesto.BeginTransaction(clon.SessionCode);

			clon.MarkNew();

			return clon;
		}

		#endregion

        #region Validation Rules

        /// <summary>
        /// Añade las reglas de validación necesarias para el objeto
        /// </summary>
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            ////OidSubtipoFacturaEmitida
            //if (OidSubtipoFacturaEmitida <= 0)
            //{
            //    e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "OidSubtipoFacturaEmitida");
            //    throw new iQValidationException(e.Description, string.Empty);
            //}

            ////OidSubtipoFacturaRecibida
            //if (OidSubtipoFacturaRecibida <= 0)
            //{
            //    e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "OidSubtipoFacturaRecibida");
            //    throw new iQValidationException(e.Description, string.Empty);
            //}
            return true;
        }	

		#endregion

		#region Autorization Rules

		public static bool CanAddObject()
		{
			return AutorizationRulesControler.CanAddObject(Resources.SecureItems.AUXILIARES);
		}

		public static bool CanGetObject()
		{
			return AutorizationRulesControler.CanGetObject(Resources.SecureItems.AUXILIARES);
		}

		public static bool CanDeleteObject()
		{
			return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.AUXILIARES);
		}

		public static bool CanEditObject()
		{
			return AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUXILIARES);
		}

		#endregion

		#region Common Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Impuesto() { }

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private Impuesto(Impuesto source, bool retrieve_childs)
		{
			MarkAsChild();
			Childs = retrieve_childs;
			Fetch(source);
		}

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private Impuesto(IDataReader source, bool retrieve_childs)
		{
			MarkAsChild();
			Childs = retrieve_childs;
			Fetch(source);
		}

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static Impuesto NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			Impuesto obj = DataPortal.Create<Impuesto>(new CriteriaCs(-1));
			obj.MarkAsChild();
			return obj;
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Impuesto con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Impuesto source, bool retrieve_childs)
		/// <remarks/>
		internal static Impuesto GetChild(Impuesto source)
		{
			return new Impuesto(source, false);
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Impuesto con los datos para el objeto</param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
		internal static Impuesto GetChild(Impuesto source, bool retrieve_childs)
		{
			return new Impuesto(source, retrieve_childs);
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="reader">DataReader con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(IDataReader source, bool retrieve_childs)
		/// <remarks/>
		internal static Impuesto GetChild(IDataReader source)
		{
			return new Impuesto(source, false);
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">IDataReader con los datos para el objeto</param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
		internal static Impuesto GetChild(IDataReader source, bool retrieve_childs)
		{
			return new Impuesto(source, retrieve_childs);
		}

		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// También copia los datos de los hijos del objeto.
		/// </summary>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual ImpuestoInfo GetInfo()
		{
			return GetInfo(true);
		}

		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual ImpuestoInfo GetInfo(bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			return new ImpuestoInfo(this, get_childs);
		}

		#endregion

		#region Root Factory Methods

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static Impuesto New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			return DataPortal.Create<Impuesto>(new CriteriaCs(-1));
		}

		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <returns>Objeto con los valores del registro</returns>
		public static Impuesto Get(long oid)
		{
			return Get(oid, true);
		}

		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto con los valores del registro</returns>
		public static Impuesto Get(long oid, bool retrieve_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Impuesto.GetCriteria(Impuesto.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Impuesto.SELECT(oid);

			Impuesto.BeginTransaction(criteria.Session);

			return DataPortal.Fetch<Impuesto>(criteria);
		}

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

		/// <summary>
		/// Elimina todos los Impuesto. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Impuesto.OpenSession();
			ISession sess = Impuesto.Session(sessCode);
			ITransaction trans = Impuesto.BeginTransaction(sessCode);

			try
			{
				sess.Delete("from TaxRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Impuesto.CloseSession(sessCode);
			}
		}

		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Impuesto Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(
				moleQule.Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

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

				Transaction().Commit();

				return this;
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return null;
			}
			finally
			{
				if (CloseSessions) CloseSession(); 
				else BeginTransaction();
			}
		}

		#endregion

		#region Common Data Access

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			// El código va al constructor porque los DataGrid no llamana al DataPortal sino directamente al constructor			
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Impuesto source)
		{
			try
			{
				SessionCode = source.SessionCode;

				_base.CopyValues(source);

			}
			catch (Exception ex) { throw ex; }

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
			}
			catch (Exception ex) { throw ex; }

			MarkOld();
		}

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Impuestos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex) { throw ex; }

			MarkOld();
		}

		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(Impuestos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				TaxRecord obj = Session().Get<TaxRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkOld();
		}

		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Impuestos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<TaxRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkNew();
		}

		#endregion

		#region Root Data Access

		/// <summary>
		/// Obtiene un registro de la base de datos
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>Lo llama el DataPortal tras generar el objeto</remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					Impuesto.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					if (reader.Read())
						_base.CopyValues(reader);
				}

				MarkOld();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
		}

		/// <summary>
		/// Inserta un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isNew</remarks>
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
				//Borrar si no hay código

				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}

		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					TaxRecord obj = Session().Get<TaxRecord>(Oid);
					obj.CopyValues(this._base.Record);
					Session().Update(obj);
					MarkOld();
				}
				catch (Exception ex)
				{
					throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
				}
			}
		}

		/// <summary>
		/// Borrado aplazado, no se ejecuta hasta que se llama al Save
		/// </summary>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}

		/// <summary>
		/// Elimina un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal</remarks>
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();

				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((TaxRecord)(criterio.UniqueResult()));
				Transaction().Commit();
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

		#region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		public static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT I.*" +
                    "   , COALESCE(SFE.\"CODIGO\", '') AS \"CODIGO_IMPUESTO_A3_EMITIDA\"" +
                    "   , COALESCE(SFR.\"CODIGO\", '') AS \"CODIGO_IMPUESTO_A3_RECIBIDA\"";

			return query;
		}

		internal static string SELECT(long oid, bool lock_table)
		{
            string i = nHManager.Instance.GetSQLTable(typeof(TaxRecord));
            string sf = nHManager.Instance.GetSQLTable(typeof(InvoiceSubtypeRecord));
			string query;

            query = SELECT_FIELDS() +
                    " FROM " + i + " AS I" +
                    " LEFT JOIN " + sf + " AS SFE ON SFE.\"OID\" = I.\"OID_SUBTIPO_FACTURA_EMITIDA\" AND SFE.\"TIPO\" = " + (long)ESubtipoFactura.Emitida +
                    " LEFT JOIN " + sf + " AS SFR ON SFR.\"OID\" = I.\"OID_SUBTIPO_FACTURA_RECIBIDA\" AND SFR.\"TIPO\" = " + (long)ESubtipoFactura.Recibida;

			if (oid > 0)
				query += " WHERE I.\"OID\" = " + oid.ToString();

			if (lock_table)
				query += " FOR UPDATE OF I NOWAIT;";

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lock_table)
		{
			string query;

			query = SELECT(0, lock_table);

			//if (conditions.Expediente != null) query += " AND ST.\"OID_EXPEDIENTE\" = " + conditions.Expediente.Oid.ToString();
			//if (lock_table) query += " FOR UPDATE OF PP NOWAIT";

			return query;
		}

		#endregion
	}

	[Serializable()]
	public class ImpuestoResumen
	{
		private decimal _importe;
		private decimal _base;

		public string Nombre { get; set; }
		public long OidImpuesto { get; set; }
		public decimal Importe { get { return Decimal.Round(_importe, 2); } set { _importe = value; } }
		public decimal BaseImponible { get { return Decimal.Round(_base, 2); } set { _base = value; } }
        public decimal Total { get { return BaseImponible + Importe; } }
        public string SubtipoFacturaEmitida { get; set; }
        public string SubtipoFacturaRecibida { get; set; }
        public decimal Porcentaje { get; set; }
        
		#region Factory Methods

		public ImpuestoResumen() { }
		public ImpuestoResumen(SerializationInfo info, StreamingContext context) { }

		#endregion
	}

	[Serializable()]
	public class ImpuestoResumenList : Hashtable, IDisposable
	{
		#region Factory Methods

		public ImpuestoResumenList() { }
		public ImpuestoResumenList(SerializationInfo info, StreamingContext context) { }

		public void Dispose()
		{
			Cache.Instance.Remove(typeof(ImpuestoList));
		}

		#endregion

		#region Business Methods

		public void Insert(ImpuestoResumen item)
		{
			ImpuestoList impuestos = ImpuestoList.GetList(false, true);

			ImpuestoResumen cr = (ImpuestoResumen)this[item.OidImpuesto];

			if (cr == null)
			{
				ImpuestoInfo impuesto = impuestos.GetItem(item.OidImpuesto);

				this.Add(item.OidImpuesto, new ImpuestoResumen
				{
					Nombre = impuesto.Nombre,
					OidImpuesto = item.OidImpuesto,
					BaseImponible = item.BaseImponible,
                    Importe = item.Importe
				});
			}
			else
			{
				cr.BaseImponible += item.BaseImponible;
                cr.Importe += item.Importe;
			}
		}

		public decimal TotalizeImpuestos()
		{
			ImpuestoList impuestos = ImpuestoList.GetList(false, true);

			decimal total = 0;

			//Calculo del impuesto para evitar errores de redondeo
			foreach (DictionaryEntry item in this)
			{
                ImpuestoResumen cr = (ImpuestoResumen)item.Value;
                
                total += cr.Importe;
			}

			return total;
		}

		#endregion
	}
}
