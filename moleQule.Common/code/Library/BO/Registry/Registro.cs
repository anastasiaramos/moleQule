using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;
using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class RegistryRecord : RecordBase
    {
        #region Attributes

        private long _oid_usuario;
        private long _tipo_registro;
        private long _serial;
        private string _codigo = string.Empty;
        private long _estado;
        private string _nombre = string.Empty;
        private DateTime _fecha;
        private string _observaciones = string.Empty;
        private long _tipo_exportacion;

        #endregion

        #region Properties
        public virtual long OidUsuario { get { return _oid_usuario; } set { _oid_usuario = value; } }
        public virtual long TipoRegistro { get { return _tipo_registro; } set { _tipo_registro = value; } }
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual long TipoExportacion { get { return _tipo_exportacion; } set { _tipo_exportacion = value; } }

        #endregion

        #region Business Methods

        public RegistryRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_usuario = Format.DataReader.GetInt64(source, "OID_USUARIO");
            _tipo_registro = Format.DataReader.GetInt64(source, "TIPO_REGISTRO");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _tipo_exportacion = Format.DataReader.GetInt64(source, "TIPO_EXPORTACION");

        }

        public virtual void CopyValues(RegistryRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_usuario = source.OidUsuario;
            _tipo_registro = source.TipoRegistro;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _estado = source.Estado;
            _nombre = source.Nombre;
            _fecha = source.Fecha;
            _observaciones = source.Observaciones;
            _tipo_exportacion = source.TipoExportacion;
        }
        #endregion
    }

    [Serializable()]
	public class RegistryBase
    {
        #region Attributes

        private RegistryRecord _record = new RegistryRecord();
        //NO ENLAZADAS
        private string _usuario = string.Empty;

        public RegistryRecord Record { get { return _record; } }

        #endregion

        #region Properties

        //NO ENLAZADAS
        public virtual string Usuario { get { return _usuario; } set { _usuario = value; } }
        public virtual ETipoRegistro ETipoRegistro { get { return (ETipoRegistro)_record.TipoRegistro; } set { _record.TipoRegistro = (long)value; } }
        public virtual string TipoRegistroLabel { get { return Library.Common.EnumText<ETipoRegistro>.GetLabel(ETipoRegistro); } }
        public virtual EEstado EEstado { get { return (EEstado)_record.Estado; } set { _record.Estado = (long)value; } }
        public virtual string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
        public virtual ETipoExportacion ETipoExportacion { get { return (ETipoExportacion)_record.TipoExportacion; } set { _record.TipoExportacion = (long)value; } }
        public virtual string TipoExportacionLabel { get { return Library.Common.EnumText<ETipoExportacion>.GetLabel(ETipoExportacion); } }
		

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _usuario = Format.DataReader.GetString(source, "USUARIO");
        }

        internal void CopyValues(Registro source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _usuario = source.Usuario;
        }
        internal void CopyValues(RegistroInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _usuario = source.Usuario;
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>	
    [Serializable()]
	public class Registro : BusinessBaseEx<Registro>
	{	 
		#region Attributes

		public RegistryBase _base = new RegistryBase();

		private RegistryLines _lineas = RegistryLines.NewChildList();

		List<TEntidadRegistroList> _entities_list = new List<TEntidadRegistroList>();

		#endregion
		
		#region Properties

		public RegistryBase Base { get { return _base; } }

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
		public virtual long OidUsuario
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidUsuario;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.OidUsuario.Equals(value))
				{
                    _base.Record.OidUsuario = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long TipoRegistro
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.TipoRegistro;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.TipoRegistro.Equals(value))
				{
					_base.Record.TipoRegistro = value;
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
        public virtual long TipoExportacion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.TipoExportacion;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.TipoExportacion.Equals(value))
                {
                    _base.Record.TipoExportacion = value;
                    PropertyHasChanged();
                }
            }
        }
		
		public virtual RegistryLines LineaRegistros
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _lineas;
			}
		}
			
		//NO ENLAZADAS
        public virtual string Usuario { get { return _base.Usuario; } set { _base.Usuario = value; } }
		public virtual ETipoRegistro ETipoRegistro { get { return (ETipoRegistro)_base.Record.TipoRegistro; } set { TipoRegistro = (long)value; } }
		public virtual string TipoRegistroLabel { get { return Library.Common.EnumText<ETipoRegistro>.GetLabel(ETipoRegistro); } }
		public virtual EEstado EEstado { get { return (EEstado)_base.Record.Estado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
        public virtual ETipoExportacion ETipoExportacion { get { return (ETipoExportacion)_base.Record.TipoExportacion; } set { TipoExportacion = (long)value; } }
        public virtual string TipoExportacionLabel { get { return Library.Common.EnumText<ETipoExportacion>.GetLabel(ETipoExportacion); } }
		
		/// <summary>
        /// Indica si el objeto está validado
        /// </summary>
		/// <remarks>Para añadir una lista: && _lista.IsValid<remarks/>
		public override bool IsValid
		{
			get { return base.IsValid
						 && _lineas.IsValid ; }
		}
		
        /// <summary>
        /// Indica si el objeto está "sucio" (se ha modificado) y se debe actualizar en la base de datos
        /// </summary>
		/// <remarks>Para añadir una lista: || _lista.IsDirty<remarks/>
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _lineas.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods
		
		public virtual Registro CloneAsNew()
		{
			Registro clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();

			clon.OidUsuario = AppContext.User.Oid;
			clon.Usuario = AppContext.User.Name;
			clon.Fecha = DateTime.Now;
			clon.EEstado = EEstado.Abierto;
			clon.ETipoRegistro = ETipoRegistro;
			clon.GetNewCode(ETipoRegistro);
			
			clon.SessionCode = Registro.OpenSession();
			Registro.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.LineaRegistros.MarkAsNew();
			
			return clon;
		}

		protected virtual void CopyFrom(RegistroInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			_base.Record.TipoRegistro = source.TipoRegistro;
			_base.Record.Serial = source.Serial;
			_base.Record.Codigo = source.Codigo;
			_base.Record.Estado = source.Estado;
			_base.Record.Fecha = source.Fecha;
			_base.Record.Observaciones = source.Observaciones;
            _base.Record.TipoExportacion = source.TipoExportacion;
		}		
		
        public virtual void GetNewCode(ETipoRegistro tipo)
        {
            Serial = SerialRegistroInfo.GetNext(tipo);
            Codigo = Serial.ToString(Resources.Defaults.REGISTRO_CODE_FORMAT);
        }

		public static Registro ChangeEstado(long oid, EEstado estado)
		{
			Registro item = Registro.Get(oid, false);
			item.BeginEdit();
			item.EEstado = estado;
			item.ApplyEdit();
			item.Save();
			item.CloseSession();

			return item;
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


            //Propiedad
            /*if (Propiedad <= 0)
            {
                e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Propiedad");
                throw new iQValidationException(e.Description, string.Empty);
            }*/

            return true;
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
		/// </summary>
		protected Registro () {}
		private Registro(Registro source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Registro(int sessionCode, IDataReader source, bool childs)
        {
			SessionCode = sessionCode;
            MarkAsChild();
			Childs = childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static Registro NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Registro obj = DataPortal.Create<Registro>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Informe con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Informe source, bool retrieve_childs)
		/// <remarks/>
		internal static Registro GetChild(Registro source, bool childs = false)
		{
			return new Registro(source, childs);
		}
        internal static Registro GetChild(int sessionCode, IDataReader source, bool childs = false)
        {
			return new Registro(sessionCode, source, childs);
        }
		
		public virtual RegistroInfo GetInfo (bool childs = true) { return new RegistroInfo(this, childs); }
		
		#endregion
		
		#region Root Factory Methods
		
		public static Registro New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Registro>(new CriteriaCs(-1));
		}
		public static Registro New(ETipoRegistro tipo)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			Registro obj = DataPortal.Create<Registro>(new CriteriaCs(-1));
			obj.ETipoRegistro = tipo;
			obj.GetNewCode(tipo);

			return obj;
		}

		public static Registro Get(long oid) { return Get(oid, true); }		
		public static Registro Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Registro.GetCriteria(Registro.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Registro.SELECT(oid);
				
			Registro.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Registro>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Informe. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Registro.OpenSession();
			ISession sess = Registro.Session(sessCode);
			ITransaction trans = Registro.BeginTransaction(sessCode);
			
			try
			{	
				sess.Delete("from RegistryRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Registro.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Registro Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);			
		
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

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

				BuildLists();
#if TRACE				
				Library.Timer.Instance.Record("Registro.BuildLists()");
#endif
				_lineas.Update(this, _entities_list);
#if TRACE
				Library.Timer.Instance.Record("LineaRegistros.Update()");
#endif
				SaveLists();
#if TRACE
				Library.Timer.Instance.Record("Registro::SaveLists()");
#endif				
				Transaction().Commit();
#if TRACE				
				Library.Timer.Instance.Record("Registro::Commit()");
#endif
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
				if (CloseSessions) CloseSession(); 
				else BeginTransaction();
			}
		}

		private void BuildLists()
		{
			TEntidadRegistroList list;
			bool created = false;

			foreach (LineaRegistro item in _lineas)
			{
				created = false;

				foreach (TEntidadRegistroList entity_list in _entities_list)
					if (entity_list.ETipoEntidad == item.ETipoEntidad)
					{
						entity_list.Oids.Add(item.OidEntidad);
						created = true;
						continue;
					}

				if (!created)
				{
					list = new TEntidadRegistroList
					{
						ETipoEntidad = item.ETipoEntidad,
						ListType = ModuleController.Instance.GetEntidad(item.ETipoEntidad).ListType,
						Oids = new List<long>()
					};
					list.Oids.Add(item.OidEntidad);
					_entities_list.Add(list);
				}
			}

			foreach (TEntidadRegistroList item in _entities_list)
			{
				item.List = (IEntidadRegistroList)item.ListType.InvokeMember("GetChildList", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new Object[3] { SessionCode, item.Oids, false });
			}
		}

		private void SaveLists()
		{
			foreach (TEntidadRegistroList item in _entities_list)
				item.List.Update(this);
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
			Oid = (long)(new Random()).Next();
			OidUsuario = AppContext.User.Oid;
			Usuario = AppContext.User.Name;
			Fecha = DateTime.Now;
			EEstado = EEstado.Abierto;
			ETipoRegistro = ETipoRegistro.Todos;
			GetNewCode(ETipoRegistro);
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Registro source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{					
					LineaRegistro.DoLOCK(Session());
					string query = RegistryLines.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_lineas = RegistryLines.GetChildList(SessionCode, reader, false);					
				}
			} 

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

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{
					
					LineaRegistro.DoLOCK(Session());
					string query = RegistryLines.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_lineas = RegistryLines.GetChildList(SessionCode, reader, false);					
				}
			}   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Registries parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode(ETipoRegistro);
		
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
		internal void Update(Registries parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			RegistryRecord obj = Session().Get<RegistryRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Registries parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<RegistryRecord>(Oid));
		
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
					Registro.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						LineaRegistro.DoLOCK(Session());
						query = RegistryLines.SELECT(this);
						reader = nHMng.SQLNativeSelect(query);
						_lineas = RegistryLines.GetChildList(SessionCode, reader, Childs);						
 					} 
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
            if (!SharedTransaction)
            {
                if (SessionCode < 0) SessionCode = OpenSession();
                BeginTransaction();
            }
			
			GetNewCode(ETipoRegistro);

			Session().Save(Base.Record);
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;
			
			RegistryRecord obj = Session().Get<RegistryRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			MarkOld();			
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
				Session().Delete((RegistryRecord)(criterio.UniqueResult()));
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
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT RG.*" +
					"		,US.\"NAME\" AS \"USUARIO\"";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (RG.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			if (conditions.Registro != null) query += " AND RG.\"OID\" = " + conditions.Registro.Oid;
			if (conditions.Estado != EEstado.Todos) query += " AND RG.\"ESTADO\" = " + (long)conditions.Estado;
			if (conditions.TipoRegistro != ETipoRegistro.Todos) query += " AND RG.\"TIPO_REGISTRO\" = " + (long)conditions.TipoRegistro;

			return query;
		}

	    internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
            string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));

            string query;

			query = SELECT_FIELDS() +
					" FROM " + rg + " AS RG" +
					" LEFT JOIN " + us + " AS US ON US.\"OID\" = RG.\"OID_USUARIO\"" +
					WHERE(conditions);

			query += " ORDER BY RG.\"FECHA\" DESC";//RG.\"CODIGO\", RG.\"FECHA\" DESC";

			//if (lock_table) query += " FOR UPDATE OF I NOWAIT";

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
		
		#endregion
	}
}
