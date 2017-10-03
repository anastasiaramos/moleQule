using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

using moleQule.Library;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class GrantPeriodRecord : RecordBase
	{
		#region Attributes

		private long _oid_ayuda;
		private long _estado;
		private long _tipo_descuento;
		private Decimal _porcentaje;
		private Decimal _cantidad;
		private DateTime _fecha_ini;
		private DateTime _fecha_fin;
		private string _observaciones = string.Empty;
  
		#endregion
		
		#region Properties
		public virtual long OidAyuda { get { return _oid_ayuda; } set { _oid_ayuda = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual long TipoDescuento { get { return _tipo_descuento; } set { _tipo_descuento = value; } }
		public virtual Decimal Porcentaje { get { return _porcentaje; } set { _porcentaje = value; } }
		public virtual Decimal Cantidad { get { return _cantidad; } set { _cantidad = value; } }
		public virtual DateTime FechaIni { get { return _fecha_ini; } set { _fecha_ini = value; } }
		public virtual DateTime FechaFin { get { return _fecha_fin; } set { _fecha_fin = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		
		#endregion
		
		#region Business Methods
		
		public GrantPeriodRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_ayuda = Format.DataReader.GetInt64(source, "OID_AYUDA");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_tipo_descuento = Format.DataReader.GetInt64(source, "TIPO_DESCUENTO");
			_porcentaje = Format.DataReader.GetDecimal(source, "PORCENTAJE");
			_cantidad = Format.DataReader.GetDecimal(source, "CANTIDAD");
			_fecha_ini = Format.DataReader.GetDateTime(source, "FECHA_INI");
			_fecha_fin = Format.DataReader.GetDateTime(source, "FECHA_FIN");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

		}
		
		public virtual void CopyValues(GrantPeriodRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_ayuda = source.OidAyuda;
			_estado = source.Estado;
			_tipo_descuento = source.TipoDescuento;
			_porcentaje = source.Porcentaje;
			_cantidad = source.Cantidad;
			_fecha_ini = source.FechaIni;
			_fecha_fin = source.FechaFin;
			_observaciones = source.Observaciones;
		}
		#endregion	
	}

    [Serializable()]
	public class GrantPeriodoBase 
	{	 
		#region Attributes
		
		private GrantPeriodRecord _record = new GrantPeriodRecord();
		
		public GrantPeriodRecord Record { get { return _record; } }
		
		#endregion

        #region Properties

        //NO ENLAZADOS	
        internal virtual EEstado EEstado { get { return (EEstado)_record.Estado; } }
        internal virtual string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
        internal virtual ETipoDescuento ETipoDescuento { get { return (ETipoDescuento)_record.TipoDescuento; } set { _record.TipoDescuento = (long)value; } }
        internal virtual string TipoDescuentoLabel { get { return EnumText<ETipoDescuento>.GetLabel(ETipoDescuento); } }
		
		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);
		}
		
		internal void CopyValues(AyudaPeriodo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source._base.Record);
		}
		internal void CopyValues(AyudaPeriodoInfo source)
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
	public class AyudaPeriodo : BusinessBaseEx<AyudaPeriodo>
	{	 
		#region Attributes
		
		public GrantPeriodoBase _base = new GrantPeriodoBase();		

		#endregion
		
		#region Properties

		public GrantPeriodoBase Base { get { return _base; } }

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
        public virtual long OidAyuda
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidAyuda;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.OidAyuda.Equals(value))
                {
                    _base.Record.OidAyuda = value;
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
        public virtual long TipoDescuento
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.TipoDescuento;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.TipoDescuento.Equals(value))
                {
                    _base.Record.TipoDescuento = value;
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
        public virtual Decimal Cantidad
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Cantidad;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Cantidad.Equals(value))
                {
                    _base.Record.Cantidad = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaIni
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaIni;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaIni.Equals(value))
                {
                    _base.Record.FechaIni = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime FechaFin
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FechaFin;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FechaFin.Equals(value))
                {
                    _base.Record.FechaFin = value;
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

		//NO ENLAZADOS	
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual ETipoDescuento ETipoDescuento { get { return _base.ETipoDescuento; } set { TipoDescuento = (long)value; } }
		public virtual string TipoDescuentoLabel { get { return _base.TipoDescuentoLabel; } }	

		#endregion
		
		#region Business Methods
		
		public virtual AyudaPeriodo CloneAsNew()
		{
			AyudaPeriodo clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.SessionCode = AyudaPeriodo.OpenSession();
			AyudaPeriodo.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
		
		protected void CopyValues(AyudaPeriodoInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
		}
		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_base.CopyValues(source);
		}
		
		protected virtual void CopyFrom(AyudaPeriodoInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			OidAyuda = source.OidAyuda;
			Estado = source.Estado;
			TipoDescuento = source.TipoDescuento;
			Porcentaje = source.Porcentaje;
			Cantidad = source.Cantidad;
			FechaIni = source.FechaIni;
			FechaFin = source.FechaFin;
			Observaciones = source.Observaciones;
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
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.EMPRESA);
        }

        public static bool CanGetObject()
        {
			return AutorizationRulesControler.CanGetObject(Resources.SecureItems.EMPRESA);
        }

        public static bool CanDeleteObject()
        {
			return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.EMPRESA);
        }

        public static bool CanEditObject()
        {
			return AutorizationRulesControler.CanEditObject(Resources.SecureItems.EMPRESA);
        }

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		protected AyudaPeriodo ()
		{
			// Si se necesita constructor público para este objeto hay que añadir el MarkAsChild() debido a la interfaz Child
			// y el código que está en el DataPortal_Create debería ir aquí		
		}		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private AyudaPeriodo(AyudaPeriodo source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private AyudaPeriodo(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static AyudaPeriodo NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			AyudaPeriodo obj = DataPortal.Create<AyudaPeriodo>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">AyudaPeriodo con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(AyudaPeriodo source, bool childs)
		/// <remarks/>
		internal static AyudaPeriodo GetChild(AyudaPeriodo source) { return new AyudaPeriodo(source, false); }
		internal static AyudaPeriodo GetChild(AyudaPeriodo source, bool childs) { return new AyudaPeriodo(source, childs); }
        internal static AyudaPeriodo GetChild(int sessionCode, IDataReader source) { return new AyudaPeriodo(sessionCode, source, false); }
        internal static AyudaPeriodo GetChild(int sessionCode, IDataReader source, bool childs) { return new AyudaPeriodo(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual AyudaPeriodoInfo GetInfo() { return GetInfo(true); }	
		public virtual AyudaPeriodoInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new AyudaPeriodoInfo(this, childs);
		}
		
		#endregion				
		
		#region Child Factory Methods
		
		/// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LO UTILIZA LA FUNCION DE CREACION DE LA LISTA DEL PADRE
        /// </summary>
        private AyudaPeriodo(Ayuda parent)
        {
			_base.Record.Oid = (long)(new Random()).Next();
            OidAyuda = parent.Oid;
			EEstado = EEstado.Active;
			ETipoDescuento = ETipoDescuento.Porcentaje;
			FechaIni = DateTime.Now;
			FechaFin = DateTime.Now;
            MarkAsChild();
        }
		
		/// <summary>
		/// Crea un nuevo objeto hijo
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <returns>Nuevo objeto creado</returns>
		internal static AyudaPeriodo NewChild(Ayuda parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new AyudaPeriodo(parent);
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
		public override AyudaPeriodo Save()
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
			_base.Record.Oid = (long)(new Random()).Next();	
			// El código va al constructor porque los DataGrid no llamana al DataPortal sino directamente al constructor			
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(AyudaPeriodo source)
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
			CopyValues(source);			   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(AyudaPeriodos parent)
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
		internal void Update(AyudaPeriodos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			GrantPeriodRecord obj = Session().Get<GrantPeriodRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(AyudaPeriodos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<GrantPeriodRecord>(Oid));
		
			MarkNew(); 
		}

		#endregion
		
		#region Child Data Access

		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(Ayuda parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidAyuda = parent.Oid;	
			
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
		internal void Update(Ayuda parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidAyuda = parent.Oid;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			GrantPeriodRecord obj = parent.Session().Get<GrantPeriodRecord>(Oid);
			obj.CopyValues(this._base.Record);
			parent.Session().Update(obj);			

			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(Ayuda parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<GrantPeriodRecord>(Oid));

			MarkNew();
		}
		
		#endregion		
		
        #region SQL

        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }		
		public static string SELECT(Ayuda item) 
		{ 
			Library.Common.QueryConditions conditions = new Library.Common.QueryConditions { Ayuda = item.GetInfo(false) };
			return SELECT(conditions, false); 
		}			
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT AP.*";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (AP.\"FECHA_INI\" >= '" + conditions.FechaIniLabel + "' AND AP.\"FECHA_FIN\" <= '" + conditions.FechaFinLabel + "')";
 
            if (conditions.AyudaPeriodo != null)
		       if (conditions.AyudaPeriodo.Oid != 0)
                   query += " AND AP.\"OID\" = " + conditions.AyudaPeriodo.Oid;				
			
            if (conditions.Ayuda != null) query += " AND AP.\"OID_AYUDA\" = " + conditions.Ayuda.Oid;  

			return query;
		}
		
        internal static string SELECT(long oid, bool lock_table)
        {			
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { AyudaPeriodo = AyudaPeriodo.NewChild().GetInfo(false) };
			conditions.AyudaPeriodo.Oid = oid;

			query = SELECT(conditions, lock_table);

			return query;
        }
	
	    internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
            string a = nHManager.Instance.GetSQLTable(typeof(GrantPeriodRecord));
            
			string query;

            query = SELECT_FIELDS() +
                    " FROM " + a + " AS AP";
					
			query += WHERE(conditions);	
		
			//if (lock_table) query += " FOR UPDATE OF A NOWAIT";

            return query;
        }
		
		#endregion

	}
}
