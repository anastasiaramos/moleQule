using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx;
using NHibernate;
using moleQule.Library;

namespace moleQule.Library.Common
{
    public enum ETipoQuery { GENERAL = 0, FOMENTO = 1 };

    [Serializable()]
    public class RegistryLineRecord : RecordBase
    {
        #region Attributes

        private long _oid_registro;
        private long _oid_entidad;
        private long _tipo_entidad;
        private long _serial;
        private string _codigo = string.Empty;
        private long _estado;
        private DateTime _fecha;
        private string _descripcion = string.Empty;
        private string _id_exportacion = string.Empty;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties
        public virtual long OidRegistro { get { return _oid_registro; } set { _oid_registro = value; } }
        public virtual long OidEntidad { get { return _oid_entidad; } set { _oid_entidad = value; } }
        public virtual long TipoEntidad { get { return _tipo_entidad; } set { _tipo_entidad = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public virtual string IdExportacion { get { return _id_exportacion; } set { _id_exportacion = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public RegistryLineRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_registro = Format.DataReader.GetInt64(source, "OID_REGISTRO");
            _oid_entidad = Format.DataReader.GetInt64(source, "OID_ENTIDAD");
            _tipo_entidad = Format.DataReader.GetInt64(source, "TIPO_ENTIDAD");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
            _id_exportacion = Format.DataReader.GetString(source, "ID_EXPORTACION");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }

        public virtual void CopyValues(RegistryLineRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_registro = source.OidRegistro;
            _oid_entidad = source.OidEntidad;
            _tipo_entidad = source.TipoEntidad;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _estado = source.Estado;
            _fecha = source.Fecha;
            _descripcion = source.Descripcion;
            _id_exportacion = source.IdExportacion;
            _observaciones = source.Observaciones;
        }
        #endregion
    }

	[Serializable()]
	public class LineaRegistroBase
	{
		#region Attributes

        private RegistryLineRecord _record = new RegistryLineRecord();

		//NO ENLAZADAS
		public long _tipo_registro;
		public string _codigo_registro = string.Empty;
		public string _codigo_entidad = string.Empty;
		public long _estado_entidad;

        public string _expediente = string.Empty;
        public string _producto = string.Empty;
        public string _linea_fomento = string.Empty;
        public DateTime _fecha_conocimiento;
        public decimal _subvencion;

		#endregion

		#region Properties

        public RegistryLineRecord Record { get { return _record; } }

		//NO ENLAZADAS
		public ETipoRegistro ETipoRegistro { get { return (ETipoRegistro)_tipo_registro; } }
		public string TipoRegistroLabel { get { return Library.Common.EnumText<ETipoRegistro>.GetLabel(ETipoRegistro); } }
		public string IDCompuesto { get { return _codigo_registro + "/" + _record.Codigo; } }
		public ETipoEntidad ETipoEntidad { get { return (ETipoEntidad)_record.TipoEntidad; } }
		public string TipoEntidadLabel { get { return Library.Common.EnumText<ETipoEntidad>.GetLabel(ETipoEntidad); } }
		public EEstado EEstado { get { return (EEstado)_record.Estado; } }
		public string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
		public EEstado EEstadoEntidad { get { return (EEstado)_estado_entidad; } }
		public string EstadoEntidadLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstadoEntidad); } }

		#endregion

		#region Business Methods

		public void CopyValues(IDataReader source)
		{
			if (source == null) return;

            int tipo_query = Format.DataReader.GetInt32(source, "TIPO_QUERY");

            _record.CopyValues(source);

			_record.Fecha = Format.DataReader.GetDateTime(source, "FECHA_REGISTRO");
			_codigo_registro = Format.DataReader.GetString(source, "CODIGO_REGISTRO");
			_tipo_registro = Format.DataReader.GetInt64(source, "TIPO_REGISTRO");
			_estado_entidad = Format.DataReader.GetInt64(source, "ESTADO_ENTIDAD");
			_codigo_entidad = Format.DataReader.GetString(source, "CODIGO_ENTIDAD");

            switch ((ETipoQuery)tipo_query)
            {
                case ETipoQuery.FOMENTO:
                    {
                        _expediente = Format.DataReader.GetString(source, "EXPEDIENTE");
                        _producto = Format.DataReader.GetString(source, "PRODUCTO");
                        _linea_fomento = Format.DataReader.GetString(source, "LINEA_FOMENTO");
                        _fecha_conocimiento = Format.DataReader.GetDateTime(source, "CONOCIMIENTO");
                        _subvencion = Format.DataReader.GetDecimal(source, "SUBVENCION");
                    }
                    break;
            }
		}
		public void CopyValues(LineaRegistro source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_tipo_registro = source.TipoRegistro;
			_codigo_entidad = source.CodigoEntidad;
			_codigo_registro = source.CodigoRegistro;
			_estado_entidad = source.EstadoEntidad;
		}
		public void CopyValues(LineaRegistroInfo source)
		{
			if (source == null) return;

            _record.CopyValues(source._base.Record);

			_tipo_registro = source.TipoRegistro;
			_codigo_entidad = source.CodigoEntidad;
			_codigo_registro = source.CodigoRegistro;
			_estado_entidad = source.EstadoEntidad;
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class LineaRegistro : BusinessBaseEx<LineaRegistro>
	{	 
		#region Attributes

		public LineaRegistroBase _base = new LineaRegistroBase();

		#endregion
		
		#region Properties

		public LineaRegistroBase Base { get { return _base; } }

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
		public virtual long OidRegistro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidRegistro;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidRegistro.Equals(value))
				{
					_base.Record.OidRegistro = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidEntidad
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidEntidad;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.OidEntidad.Equals(value))
				{
                    _base.Record.OidEntidad = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long TipoEntidad
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoEntidad;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.TipoEntidad.Equals(value))
				{
                    _base.Record.TipoEntidad = value;
					PropertyHasChanged();
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
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.Serial.Equals(value))
				{
                    _base.Record.Serial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Codigo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Codigo;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

                if (!_base.Record.Codigo.Equals(value))
				{
                    _base.Record.Codigo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Estado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Estado;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.Estado.Equals(value))
				{
                    _base.Record.Estado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Fecha
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fecha;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.Fecha.Equals(value))
				{
                    _base.Record.Fecha = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Descripcion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Descripcion;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

                if (!_base.Record.Descripcion.Equals(value))
				{
                    _base.Record.Descripcion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string SerialExportacion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.IdExportacion;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

                if (!_base.Record.IdExportacion.Equals(value))
				{
                    _base.Record.IdExportacion = value;
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

		//NO ENLAZADAS
		public virtual long TipoRegistro { get { return _base._tipo_registro; } }
		public virtual ETipoRegistro ETipoRegistro { get { return _base.ETipoRegistro; } set { _base._tipo_registro = (long)value; } }
		public virtual string TipoRegistroLabel { get { return Library.Common.EnumText<ETipoRegistro>.GetLabel(ETipoRegistro); } }
		public virtual string IDExportacion { get { return _base.Record.IdExportacion; } }
		public virtual string CodigoRegistro { get { return _base._codigo_registro; } set { _base._codigo_registro = value; } }
		public virtual string IDCompuesto { get { return _base.IDCompuesto; } }
		public virtual ETipoEntidad ETipoEntidad { get { return _base.ETipoEntidad; } set { TipoEntidad = (long)value; } }
		public virtual string TipoEntidadLabel { get { return _base.TipoEntidadLabel; } }
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual string CodigoEntidad { get { return _base._codigo_entidad; } set { _base._codigo_entidad = value; } }
		public virtual long EstadoEntidad { get { return _base._estado_entidad; } }
		public virtual EEstado EEstadoEntidad { get { return _base.EEstadoEntidad; } set { _base._estado_entidad = (long)value; } }
		public virtual string EstadoEntidadLabel { get { return _base.EstadoEntidadLabel; } }

		#endregion
		
		#region Business Methods
		
		public virtual LineaRegistro CloneAsNew()
		{
			LineaRegistro clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.Fecha = DateTime.Now;
			clon.EEstado = EEstado.Abierto;
			
			clon.SessionCode = LineaRegistro.OpenSession();
			LineaRegistro.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
        
		protected virtual void CopyFrom(LineaRegistroInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			_base.CopyValues(source);
		}
		protected virtual void CopyFrom(IEntidadRegistroInfo source)
		{
			if (source == null) return;

			OidEntidad = source.Oid;
			ETipoEntidad = source.ETipoEntidad;
			Descripcion = source.DescripcionRegistro;

			CodigoEntidad = source.Codigo;
		}

        public virtual void GetNewCode(Registro parent)
        {
            Serial = SerialLineaRegistroInfo.GetNext(parent.ETipoRegistro, DateTime.Today.Year);
            Codigo = Serial.ToString(Resources.Defaults.LINEA_REGISTRO_CODE_FORMAT);

			if (parent.ETipoRegistro == ETipoRegistro.Contabilidad)
			{
				long sExportacion = SerialLineaRegistroInfo.GetNextIDExportacion(parent.ETipoRegistro, DateTime.Today.Year);
				SerialExportacion = sExportacion.ToString(Resources.Defaults.ID_EXPORTACION_CODE_FORMAT);
			}
        }				
			
		#endregion
		 
	    #region Validation Rules

		/// <summary>
		/// Añade las reglas de validación necesarias para el objeto
		/// </summary>
		protected override void AddBusinessRules()
		{
			
			//Código para valores requeridos o que haya que validar
			
		}
		 
		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.REGISTRO);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.REGISTRO);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.REGISTRO);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.REGISTRO);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		protected LineaRegistro ()
		{
			// Si se necesita constructor público para este objeto hay que añadir el MarkAsChild() debido a la interfaz Child
			// y el código que está en el DataPortal_Create debería ir aquí		
		}		
		private LineaRegistro(LineaRegistro source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
		private LineaRegistro(IDataReader source, bool childs)
        {
            MarkAsChild();
			Childs = childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static LineaRegistro NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			LineaRegistro obj = DataPortal.Create<LineaRegistro>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		internal static LineaRegistro GetChild(LineaRegistro source)
		{
			return new LineaRegistro(source, false);
		}
		internal static LineaRegistro GetChild(LineaRegistro source, bool childs)
		{
			return new LineaRegistro(source, childs);
		}
        internal static LineaRegistro GetChild(IDataReader source)
        {
            return new LineaRegistro(source, false);
        }
		internal static LineaRegistro GetChild(IDataReader source, bool childs)
        {
			return new LineaRegistro(source, childs);
        }
		
		public virtual LineaRegistroInfo GetInfo() { return GetInfo(true); }
		public virtual LineaRegistroInfo GetInfo(bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new LineaRegistroInfo(this, childs);
		}
		
		#endregion				
		
		#region Child Factory Methods
		
		/// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LO UTILIZA LA FUNCION DE CREACION DE LA LISTA DEL PADRE
        /// </summary>
        private LineaRegistro(Registro parent)
        {
            OidRegistro = parent.Oid;
			Fecha = DateTime.Now;
            MarkAsChild();
        }
		
		/// <summary>
		/// Crea un nuevo objeto hijo
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <returns>Nuevo objeto creado</returns>
		internal static LineaRegistro NewChild(Registro parent, IEntidadRegistroInfo source)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			LineaRegistro obj = new LineaRegistro(parent);
			obj.CopyFrom(source);
			obj.EEstado = EEstado.Abierto;
			obj.CodigoRegistro = parent.Codigo;

			return obj;
		}
				
		/// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre, que
		/// debe utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override LineaRegistro Save()
		{
			throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
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
		private void Fetch(LineaRegistro source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);			 

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
			_base.CopyValues(source); 

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(RegistryLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			
		
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(RegistryLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			RegistryLineRecord obj = Session().Get<RegistryLineRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(RegistryLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<RegistryLineRecord>(Oid));
		
			MarkNew(); 
		}

		#endregion
		
		#region Child Data Access
		
		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidRegistro = parent.Oid;	
			
			ValidationRules.CheckRules();
			
			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);				

			MarkOld();
		}

		/// <summary>
		/// Actualiza un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Update(Registro parent, List<TEntidadRegistroList> lists)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidRegistro = parent.Oid;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			RegistryLineRecord obj = parent.Session().Get<RegistryLineRecord>(Oid);
			obj.CopyValues(this._base.Record);
			parent.Session().Update(obj);

			SaveEntidad(parent, lists);	
		
			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(Registro parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<RegistryLineRecord>(Oid));

			MarkNew();
		}

		internal void SaveEntidad(Registro parent, List<TEntidadRegistroList> lists)
		{ 
			IEntidadRegistro obj = null;

			if (EEstado == EEstado.Anulado) return;

			switch (parent.ETipoRegistro)
			{
				case ETipoRegistro.Contabilidad:
					{
						foreach (TEntidadRegistroList item in lists)
						{
							if (item.ETipoEntidad == ETipoEntidad)
							{
								obj = item.List.IGetItem(OidEntidad);

								if ((obj != null))
								{
									//if ((obj.EEstado != EEstadoEntidad) && (EEstadoEntidad != EEstado.Anulado))
										obj.EEstado = EEstadoEntidad;

									obj = null;
								}

								return;
							}
						}
					}
					break;

				case ETipoRegistro.Fomento:
					{
						foreach (TEntidadRegistroList item in lists)
						{
							if (item.ETipoEntidad == ETipoEntidad)
							{
								obj = item.List.IGetItem(OidEntidad);

								if ((obj != null))
								{
									if ((obj.EEstado != EEstadoEntidad) && (EEstadoEntidad != EEstado.Anulado))
										obj.EEstado = EEstadoEntidad;

									obj = null;
								}

								return;
							}
						}
					}
					break;
			}

		}

		#endregion
				
        #region SQL

		public delegate string SelectLocalCaller(QueryConditions conditions, ETipoEntidad tipo);
		public static SelectLocalCaller local_caller_SELECT = new SelectLocalCaller(SELECT_BASE);

        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }		
		public static string SELECT(Registro item)
		{
			Library.Common.QueryConditions conditions = new Library.Common.QueryConditions { Registro = item.GetInfo(false) };
			return SELECT(conditions, false); 
		}			
		
        internal static string SELECT_FIELDS_FOMENTO()
        {
            string query;

            query = "SELECT 1 AS \"TIPO_QUERY\"" +
                    "       ,LR.*" +
                    "		,CASE WHEN (COALESCE(AP.\"TIPO_AYUDA\", 2) = " + (long)ETipoDescuento.Porcentaje + ")" +
                    "			THEN (\"TOTAL\" * COALESCE(AP.\"PORCENTAJE\", 0) / 100)" +
                    "			ELSE COALESCE(AP.\"CANTIDAD\", 0)" +
                    "			END AS \"SUBVENCION\"" +
                    "		,RG.\"FECHA\" AS \"FECHA_REGISTRO\"" +
                    "		,RG.\"CODIGO\" AS \"CODIGO_REGISTRO\"" +
                    "		,RG.\"TIPO_REGISTRO\" AS \"TIPO_REGISTRO\"" +
                    "		,COALESCE(EN.\"CODIGO\", '') AS \"CODIGO_ENTIDAD\"" +
                    "		,COALESCE(EN.\"ESTADO\", 0) AS \"ESTADO_ENTIDAD\"" +
	                "       ,COALESCE(EX.\"CODIGO\", '') AS \"EXPEDIENTE\"" +
	                "       ,COALESCE(PA.\"TIPO_MERCANCIA\", '') AS \"PRODUCTO\"" +
	                "       ,EN.\"FECHA_CONOCIMIENTO\" AS \"CONOCIMIENTO\"" +
	                "       ,EN.\"CODIGO\" AS \"LINEA_FOMENTO\"";

            return query;
        }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT 0 AS \"TIPO_QUERY\"" +
                    "       ,LR.*" +
                    "		,RG.\"FECHA\" AS \"FECHA_REGISTRO\"" +
                    "		,RG.\"CODIGO\" AS \"CODIGO_REGISTRO\"" +
                    "		,RG.\"TIPO_REGISTRO\" AS \"TIPO_REGISTRO\"" +
                    "		,COALESCE(EN.\"CODIGO\", '') AS \"CODIGO_ENTIDAD\"" +
                    "		,COALESCE(EN.\"ESTADO\", 0) AS \"ESTADO_ENTIDAD\"";

            return query;
        }

		internal static string TABLE(ETipoEntidad tipo)
		{
			return ModuleController.Instance.ActiveEntidades[tipo].Table;
		}

		internal static string SELECT_BASE(QueryConditions conditions, ETipoEntidad tipo)
		{
            string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
            string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));

            EEstado estado = conditions.Estado;
            conditions.Estado = EEstado.Todos;

			string query;

            switch (conditions.TipoRegistro)
            {
                case ETipoRegistro.Fomento:

                    Assembly assembly = Assembly.Load("moleQule.Library.Store");
                    Type type = assembly.GetType("moleQule.Library.Store.LineaFomento");

                    query = (string)type.InvokeMember("SELECT_LINEAS_FOMENTO", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[2] { conditions, tipo });
                    
                    break;

                default:
			query = 
				SELECT_FIELDS() +
			"	FROM " + lr + " AS LR" +
			"	INNER JOIN " + rg + " AS RG ON RG.\"OID\" = LR.\"OID_REGISTRO\"" +
			"	INNER JOIN " + TABLE(tipo) + " AS EN ON EN.\"OID\" = LR.\"OID_ENTIDAD\" AND LR.\"TIPO_ENTIDAD\" = " + (long)tipo +
				WHERE(conditions);
            break;
        }

			//if (lock_table) query += " FOR UPDATE OF L NOWAIT";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (RG.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			if (conditions.Registro != null) query += " AND LR.\"OID_REGISTRO\" = " + conditions.Registro.Oid;
            if (conditions.OidEntity != 0) query += " AND LR.\"OID_ENTIDAD\" = " + conditions.OidEntity;

			return query;
		}

	    internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
			string query;

			query = SELECT_BUILDER(local_caller_SELECT, conditions);

            query += " ORDER BY \"CODIGO\"";

            if (conditions.Order == ListSortDirection.Descending)
                query += " DESC";

			//if (lock_table) query += " FOR UPDATE OF L NOWAIT";

            return query;
        }

		internal static string SELECT(long oid, bool lock_table)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Registro = Registro.New().GetInfo(false) };
			conditions.Registro.Oid = oid;

			query = SELECT(conditions, lock_table);

			return query;
		}

		internal static string SELECT_BUILDER(SelectLocalCaller local_SELECT, QueryConditions conditions)
		{
			string query = string.Empty;
			string union = " UNION ";

			foreach (KeyValuePair<ETipoEntidad, TEntidadRegistroBase> entity in ModuleController.Instance.ActiveEntidades)
			{			
				if (!entity.Value.Active) continue;

				switch (conditions.TipoRegistro)
				{
					case ETipoRegistro.Contabilidad:
						{
							switch (entity.Key)
							{
								case ETipoEntidad.Cobro:
								case ETipoEntidad.Pago:
								case ETipoEntidad.FacturaEmitida:
								case ETipoEntidad.FacturaRecibida:
								case ETipoEntidad.Nomina:
								//case ETipoEntidad.LineaFomento:
								case ETipoEntidad.ExpedienteREA:
                                case ETipoEntidad.Traspaso:
                                case ETipoEntidad.Prestamo:
									query += union + local_SELECT(conditions, entity.Key);
									break;
							}
						}
						break;

					case ETipoRegistro.Email:
						{
							switch (entity.Key)
							{
								case ETipoEntidad.Cliente:
									query += union + local_SELECT(conditions, entity.Key);
									break;
							}
						}
						break;

					case ETipoRegistro.Fomento:
						{
							switch (entity.Key)
							{
								case ETipoEntidad.LineaFomento:
									query += union + local_SELECT(conditions, entity.Key);
									break;
							}
						}
						break;

					case ETipoRegistro.Todos:
						{
							switch (entity.Key)
							{
								case ETipoEntidad.Cliente:
								case ETipoEntidad.Cobro:
								case ETipoEntidad.Pago:
								case ETipoEntidad.FacturaEmitida:
								case ETipoEntidad.FacturaRecibida:
								case ETipoEntidad.Nomina:
								case ETipoEntidad.LineaFomento:
								case ETipoEntidad.ExpedienteREA:
                                case ETipoEntidad.Traspaso:
                                case ETipoEntidad.Prestamo:
									query += union + local_SELECT(conditions, entity.Key);
									break;
							}
						}
						break;
				}					
			}

			query = query.Substring(union.Length);

			return query;
		}

		#endregion
	}
}
