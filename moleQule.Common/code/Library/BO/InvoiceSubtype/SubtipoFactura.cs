using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;
using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class InvoiceSubtypeRecord : RecordBase
    {
        #region Attributes

        private string _codigo = string.Empty;
        private string _descripcion = string.Empty;
        private long _tipo;

        #endregion

        #region Properties
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }

        #endregion

        #region Business Methods

        public InvoiceSubtypeRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
            _tipo = Format.DataReader.GetInt64(source, "TIPO");

        }

        public virtual void CopyValues(InvoiceSubtypeRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _codigo = source.Codigo;
            _descripcion = source.Descripcion;
            _tipo = source.Tipo;
        }
        #endregion
    }

    [Serializable()]
	public class InvoiceSubtypeBase
    {
        #region Attributes

        private InvoiceSubtypeRecord _record = new InvoiceSubtypeRecord();

        public InvoiceSubtypeRecord Record { get { return _record; } }

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(SubtipoFactura source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(SubtipoFacturaInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        #endregion
    }
		
    /// <summary>
    /// Editable Root Business Object
    /// </summary>	
    [Serializable()]
    public class SubtipoFactura : BusinessBaseEx<SubtipoFactura>
    {
        #region Attributes

		public InvoiceSubtypeBase _base = new InvoiceSubtypeBase();
        
        #endregion

        #region Properties

		public InvoiceSubtypeBase Base { get { return _base; } }

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
        public virtual long Tipo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Tipo; ;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.Tipo.Equals(value))
                {
                    _base.Record.Tipo = value;
                    PropertyHasChanged();
                }
            }
        }

        public virtual ESubtipoFactura ETipo { get { return (ESubtipoFactura)_base.Record.Tipo; } }
        public virtual string ETipoLabel { get { return EnumText<ESubtipoFactura>.GetLabel(ETipo); } }


        #endregion

        #region Business Methods

        public virtual SubtipoFactura CloneAsNew()
        {
            SubtipoFactura clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            Random rd = new Random();
            clon.Oid = rd.Next();

            clon.SessionCode = SubtipoFactura.OpenSession();
            SubtipoFactura.BeginTransaction(clon.SessionCode);

            clon.MarkNew();

            return clon;
        }

        protected void CopyValues(SubtipoFacturaInfo source)
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

        protected virtual void CopyFrom(SubtipoFacturaInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
            Codigo = source.Codigo;
            Descripcion = source.Descripcion;
            Tipo = source.Tipo;
        }


        #endregion

        #region Validation Rules

        /// <summary>
        /// Añade las reglas de validación necesarias para el objeto
        /// </summary>
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
            ValidationRules.AddRule(CheckValidation, "Tipo");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {

            //if (Valor == string.Empty)
            //{
            //    e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Propiedad");
            //    throw new iQValidationException(e.Description, string.Empty);
            //}	

            //Tipo
            if (Tipo <= 0)
            {
                e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Tipo");
                throw new iQValidationException(e.Description, string.Empty);
            }

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
        protected SubtipoFactura() 
        {
            Tipo = (long)ESubtipoFactura.Todas;
        }

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
        /// </summary>
        private SubtipoFactura(SubtipoFactura source, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            Fetch(source);
        }
        private SubtipoFactura(int sessionCode, IDataReader source, bool childs)
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
        public static SubtipoFactura NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            SubtipoFactura obj = DataPortal.Create<SubtipoFactura>(new CriteriaCs(-1));
            obj.MarkAsChild();
            return obj;
        }

        /// <summary>
        /// Crea un objeto
        /// </summary>
        /// <param name="source">SubtipoFactura con los datos para el objeto</param>
        /// <returns>Objeto creado</returns>
        /// <remarks>
        /// La utiliza la BusinessListBaseEx correspondiente para montar la lista
        /// NO OBTIENE los hijos. Para ello utilice GetChild(SubtipoFactura source, bool childs)
        /// <remarks/>
        internal static SubtipoFactura GetChild(SubtipoFactura source) { return new SubtipoFactura(source, false); }
        internal static SubtipoFactura GetChild(SubtipoFactura source, bool childs) { return new SubtipoFactura(source, childs); }
        internal static SubtipoFactura GetChild(int sessionCode, IDataReader source) { return new SubtipoFactura(sessionCode, source, false); }
        internal static SubtipoFactura GetChild(int sessionCode, IDataReader source, bool childs) { return new SubtipoFactura(sessionCode, source, childs); }

        /// <summary>
        /// Construye y devuelve un objeto de solo lectura copia de si mismo.
        /// </summary>
        /// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
        /// <returns>Réplica de solo lectura del objeto</returns>
        public virtual SubtipoFacturaInfo GetInfo() { return GetInfo(true); }
        public virtual SubtipoFacturaInfo GetInfo(bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return new SubtipoFacturaInfo(this, childs);
        }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Crea un nuevo objeto
        /// </summary>
        /// <returns>Nuevo objeto creado</returns>
        public static SubtipoFactura New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<SubtipoFactura>(new CriteriaCs(-1));
        }

        public static SubtipoFactura Get(long oid) { return Get(oid, true); }
        public static SubtipoFactura Get(long oid, bool childs)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = GetCriteria(OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT(oid);

            BeginTransaction(criteria.Session);

            return DataPortal.Fetch<SubtipoFactura>(criteria);
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
        /// Elimina todos los SubtipoFactura. 
        /// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = SubtipoFactura.OpenSession();
            ISession sess = SubtipoFactura.Session(sessCode);
            ITransaction trans = SubtipoFactura.BeginTransaction(sessCode);

            try
            {
                sess.Delete("from InvoiceSubtypeRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                SubtipoFactura.CloseSession(sessCode);
            }
        }

        /// <summary>
        /// Guarda en la base de datos todos los cambios del objeto.
        /// También guarda los cambios de los hijos si los tiene
        /// </summary>
        /// <returns>Objeto actualizado y con los flags reseteados</returns>
        public override SubtipoFactura Save()
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

                Transaction().Commit();
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
        private void Fetch(SubtipoFactura source)
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
        internal void Insert(SubtipoFacturas parent)
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
        internal void Update(SubtipoFacturas parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;
            InvoiceSubtypeRecord obj = Session().Get<InvoiceSubtypeRecord>(Oid);
            obj.CopyValues(this._base.Record);
            Session().Update(obj);

            MarkOld();
        }

        /// <summary>
        /// Borra el registro de la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
        internal void DeleteSelf(SubtipoFacturas parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;
            Session().Delete(Session().Get<InvoiceSubtypeRecord>(Oid));

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
                    SubtipoFactura.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        CopyValues(reader);
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

            InvoiceSubtypeRecord obj = Session().Get<InvoiceSubtypeRecord>(Oid);
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
                Session().Delete((InvoiceSubtypeRecord)(criterio.UniqueResult()));
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

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>() { };
		}

        public new static string SELECT(long oid) { return SELECT(oid, true); }
        public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

        internal static string SELECT_FIELDS()
        {
            string query;

			query = "SELECT SF.*";

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
			string t = nHManager.Instance.GetSQLTable(typeof(InvoiceSubtypeRecord));

			string query = @"
					FROM " + t + @" AS SF";

			return query + " " + conditions.ExtraJoin;
		}

        internal static string WHERE(QueryConditions conditions)
        {
            string query = " WHERE TRUE";

            //query = " WHERE (T.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            if (conditions.SubtipoFactura != ESubtipoFactura.Todas)
				query += " AND SF.\"TIPO\" = " + (long)conditions.SubtipoFactura;

            return query;
        }

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query =
				SELECT_FIELDS() +
				JOIN(conditions) +
				WHERE(conditions) +
				ORDER((conditions != null) ? conditions.Orders : null, "SF", ForeignFields());

			if (conditions.PagingInfo != null) query += LIMIT(conditions.PagingInfo);

			query += Common.EntityBase.LOCK("SF", lockTable);

			return query;
		}

		internal static string SELECT(long oid, bool lock_table)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { /*SubtipoFactura = SubtipoFactura.New().GetInfo(false) */};
			//conditions.SubtipoFactura.Oid = oid;

			query = SELECT(conditions, lock_table);

			return query;
		}

        #endregion
    }
}
