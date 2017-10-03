using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CreditCardRecord : RecordBase
    {
        #region Attributes

        private long _oid_cuenta_bancaria;
        private long _tipo;
        private string _nombre = string.Empty;
        private string _numeracion = string.Empty;
        private string _cuenta_contable = string.Empty;
        private long _forma_pago;
        private long _dias_pago;
        private long _dia_extracto;
        private string _observaciones = string.Empty;
        private Decimal _p_comision;

        #endregion

        #region Properties

        public virtual long OidCuentaBancaria { get { return _oid_cuenta_bancaria; } set { _oid_cuenta_bancaria = value; } }
        public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual string Numeracion { get { return _numeracion; } set { _numeracion = value; } }
        public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }
        public virtual long FormaPago { get { return _forma_pago; } set { _forma_pago = value; } }
        public virtual long DiasPago { get { return _dias_pago; } set { _dias_pago = value; } }
        public virtual long DiaExtracto { get { return _dia_extracto; } set { _dia_extracto = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual Decimal PComision { get { return _p_comision; } set { _p_comision = value; } }

        #endregion

        #region Business Methods

        public CreditCardRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_cuenta_bancaria = Format.DataReader.GetInt64(source, "OID_CUENTA_BANCARIA");
            _tipo = Format.DataReader.GetInt64(source, "TIPO");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _numeracion = Format.DataReader.GetString(source, "NUMERACION");
            _cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
            _forma_pago = Format.DataReader.GetInt64(source, "FORMA_PAGO");
            _dias_pago = Format.DataReader.GetInt64(source, "DIAS_PAGO");
            _dia_extracto = Format.DataReader.GetInt64(source, "DIA_EXTRACTO");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _p_comision = Format.DataReader.GetDecimal(source, "P_COMISION");

        }

        public virtual void CopyValues(CreditCardRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_cuenta_bancaria = source.OidCuentaBancaria;
            _tipo = source.Tipo;
            _nombre = source.Nombre;
            _numeracion = source.Numeracion;
            _cuenta_contable = source.CuentaContable;
            _forma_pago = source.FormaPago;
            _dias_pago = source.DiasPago;
            _dia_extracto = source.DiaExtracto;
            _observaciones = source.Observaciones;
            _p_comision = source.PComision;
        }
        #endregion
    }

    [Serializable()]
    public class CreditCardBase
    {
        #region Attributes

        private CreditCardRecord _record = new CreditCardRecord();

        private string _bank_account = string.Empty;

        #endregion

        #region Properties

        public CreditCardRecord Record { get { return _record; } }

        public string CuentaBancaria { get { return _bank_account; } set { _bank_account = value; } }
        public ETipoTarjeta ETipoTarjeta { get { return (ETipoTarjeta)_record.Tipo; } set { _record.Tipo = (long)value; } }
        public string TipoTarjetaLabel { get { return EnumText<ETipoTarjeta>.GetLabel(ETipoTarjeta); } }
        public EFormaPago EFormaPago { get { return (EFormaPago)_record.FormaPago; } set { _record.FormaPago = (long)value; } }
        public string FormaPagoLabel { get { return EnumText<EFormaPago>.GetLabel(EFormaPago); ; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _bank_account = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
        }
        internal void CopyValues(CreditCard source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _bank_account = source.CuentaBancaria;
        }
        internal void CopyValues(CreditCardInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _bank_account = source.CuentaBancaria;
        }

        public int StatementLimitDay(DateTime baseDate)
        {
            switch (Record.DiaExtracto)
            {
                case 31:
                    return DateAndTime.LastDay(baseDate.Month, baseDate.Year).Day;

                case 29:
                case 30:
                    if (baseDate.Month == 2)
                        return DateAndTime.LastDay(baseDate.Month, baseDate.Year).Day;
                    else
                        return (int)Record.DiaExtracto;

                case 0:
                    return 1;

                default:
                    return (int)Record.DiaExtracto;
            }
        }
        public int StatementPayDay(DateTime baseDate)
        {
            switch (Record.DiasPago)
            {
                case 31:
                    return DateAndTime.LastDay(baseDate.Month, baseDate.Year).Day;

                case 29:
                case 30:
                    if (baseDate.Month == 2)
                        return DateAndTime.LastDay(baseDate.Month, baseDate.Year).Day;
                    else
                        return (int)Record.DiasPago;

                case 0:
                    return 1;

                default:
                    return (int)Record.DiasPago;
            }
        }

        #endregion
    }

    /// <summary>
    /// Editable Root Business Object
    /// </summary>	
    [Serializable()]
    public class CreditCard : BusinessBaseEx<CreditCard>
    {
        #region Attributes

        public CreditCardBase _base = new CreditCardBase();

        private CreditCardStatements _statements = CreditCardStatements.NewChildList();

        #endregion

        #region Properties

        public CreditCardBase Base { get { return _base; } }

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
        public virtual long OidCuentaBancaria
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidCuentaBancaria;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.OidCuentaBancaria.Equals(value))
                {
                    _base.Record.OidCuentaBancaria = value;
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
        public virtual long Tipo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Tipo;
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
        public virtual string Numeracion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Numeracion;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Numeracion.Equals(value))
                {
                    _base.Record.Numeracion = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string CuentaContable
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CuentaContable;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.CuentaContable.Equals(value))
                {
                    _base.Record.CuentaContable = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long FormaPago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.FormaPago;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.FormaPago.Equals(value))
                {
                    _base.Record.FormaPago = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long DiasPago
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.DiasPago;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.DiasPago.Equals(value))
                {
                    _base.Record.DiasPago = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long DiaExtracto
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.DiaExtracto;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.DiaExtracto.Equals(value))
                {
                    _base.Record.DiaExtracto = value;
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
        public virtual Decimal PComision
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                ////CanReadProperty(true);
                return _base.Record.PComision;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                ////CanWriteProperty(true);

                if (!_base.Record.PComision.Equals(value))
                {
                    _base.Record.PComision = value;
                    PropertyHasChanged();
                }
            }
        }

        public virtual string CuentaBancaria { get { return _base.CuentaBancaria; } set { _base.CuentaBancaria = value; } }
        public virtual ETipoTarjeta ETipoTarjeta { get { return (ETipoTarjeta)Tipo; } set { Tipo = (long)value; } }
        public virtual string TipoTarjetaLabel { get { return EnumText<ETipoTarjeta>.GetLabel(ETipoTarjeta); } }
        public virtual EFormaPago EFormaPago { get { return (EFormaPago)FormaPago; } set { FormaPago = (long)value; } }
        public virtual string FormaPagoLabel { get { return EnumText<EFormaPago>.GetLabel(EFormaPago); ; } }

        public virtual CreditCardStatements Statements
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _statements;
            }
        }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                          && _statements.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                          || _statements.IsDirty;
            }
        }

        #endregion

        #region Business Methods

        public virtual CreditCard CloneAsNew()
        {
            CreditCard clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
            Random rd = new Random();
            clon.Oid = rd.Next();

            clon.SessionCode = CreditCard.OpenSession();
            CreditCard.BeginTransaction(clon.SessionCode);

            clon.MarkNew();

            return clon;
        }

        protected virtual void CopyFrom(CreditCardInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _base.Record.OidCuentaBancaria = source.OidCuentaBancaria;
            _base.Record.Nombre = source.Nombre;
            _base.Record.Tipo = source.Tipo;
            _base.Record.Numeracion = source.Numeracion;
            _base.Record.CuentaContable = source.CuentaContable;
            _base.Record.FormaPago = source.FormaPago;
            _base.Record.DiasPago = source.DiasPago;
            _base.Record.DiaExtracto = source.DiaExtracto;
            _base.Record.Observaciones = source.Observaciones;
            _base.Record.PComision = source.PComision;

            _base.CuentaBancaria = source.CuentaBancaria;
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //OidCuentaBancaria
            if (OidCuentaBancaria == 0)
            {
                e.Description = Resources.Messages.NO_CUENTA_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "OidCuentaBancaria");
            }

            //Nombre
            if (Nombre == string.Empty)
            {
                e.Description = Resources.Messages.NO_FIELD_FILLED;
                throw new iQValidationException(e.Description, string.Empty, "Nombre");
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
        protected CreditCard() { }
        private CreditCard(CreditCard source, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            Fetch(source);
        }
        private CreditCard(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();
            SessionCode = sessionCode;
            Childs = childs;
            Fetch(source);
        }

        /// <summary>
        /// Crea un nuevo objeto
        /// </summary>
        /// <returns>Nuevo objeto creado</returns>
        /// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
        public static CreditCard NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            CreditCard obj = DataPortal.Create<CreditCard>(new CriteriaCs(-1));
            obj.MarkAsChild();
            return obj;
        }

        /// <summary>
        /// Crea un objeto
        /// </summary>
        /// <param name="source">TarjetaCredito con los datos para el objeto</param>
        /// <returns>Objeto creado</returns>
        /// <remarks>
        /// La utiliza la BusinessListBaseEx correspondiente para montar la lista
        /// NO OBTIENE los hijos. Para ello utilice GetChild(TarjetaCredito source, bool retrieve_childs)
        /// <remarks/>
        internal static CreditCard GetChild(CreditCard source, bool childs = false)
        {
            return new CreditCard(source, childs);
        }
        internal static CreditCard GetChild(int sessionCode, IDataReader source, bool childs = false)
        {
            return new CreditCard(sessionCode, source, childs);
        }

        /// <summary>
        /// Construye y devuelve un objeto de solo lectura copia de si mismo.
        /// También copia los datos de los hijos del objeto.
        /// </summary>
        /// <returns>Réplica de solo lectura del objeto</returns>
        public virtual CreditCardInfo GetInfo(bool childs = true)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            return new CreditCardInfo(this, childs);
        }

        public virtual void LoadChilds(Type type, bool childs)
        {
            if (type.Equals(typeof(CreditCardStatement)))
            {
                _statements = CreditCardStatements.GetChildList(this, childs);
            }
        }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Crea un nuevo objeto
        /// </summary>
        /// <returns>Nuevo objeto creado</returns>
        public static CreditCard New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<CreditCard>(new CriteriaCs(-1));
        }

        /// <summary>
        /// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
        /// </summary>
        /// <param name="oid"></param>
        /// <returns>Objeto con los valores del registro</returns>
        public static CreditCard Get(long oid, bool childs = true)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = CreditCard.GetCriteria(CreditCard.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = CreditCard.SELECT(oid);
            else
                criteria.AddOidSearch(oid);

            CreditCard.BeginTransaction(criteria.Session);

            return DataPortal.Fetch<CreditCard>(criteria);
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
        /// Elimina todos los TarjetaCredito. 
        /// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = CreditCard.OpenSession();
            ISession sess = CreditCard.Session(sessCode);
            ITransaction trans = CreditCard.BeginTransaction(sessCode);

            try
            {
                sess.Delete("from CreditCardRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                CreditCard.CloseSession(sessCode);
            }
        }

        /// <summary>
        /// Guarda en la base de datos todos los cambios del objeto.
        /// También guarda los cambios de los hijos si los tiene
        /// </summary>
        /// <returns>Objeto actualizado y con los flags reseteados</returns>
        public override CreditCard Save()
        {
            // Por la posible doble interfaz Root/Child
            if (IsChild)
                throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

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

                _statements.Update(this);

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
            Tipo = (long)ETipoTarjeta.Credito;
            EFormaPago = EFormaPago.XDiasMes;
        }

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="source">Objeto fuente</param>
        private void Fetch(CreditCard source)
        {
            try
            {
                SessionCode = source.SessionCode;

                _base.CopyValues(source);

                if (Childs)
                {
                    if (nHMng.UseDirectSQL)
                    {
                        CreditCardStatement.DoLOCK(Session());
                        string query = CreditCardStatements.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query);
                        _statements = CreditCardStatements.GetChildList(SessionCode, reader);
                    }
                }
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

                if (Childs)
                {
                    if (nHMng.UseDirectSQL)
                    {
                        CreditCardStatement.DoLOCK(Session());
                        string query = CreditCardStatements.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query);
                        _statements = CreditCardStatements.GetChildList(SessionCode, reader);
                    }
                }
            }
            catch (Exception ex) { throw ex; }

            MarkOld();
        }

        /// <summary>
        /// Inserta el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
        internal void Insert(CreditCards parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            parent.Session().Save(Base.Record);

            _statements.Update(this);

            MarkOld();
        }

        /// <summary>
        /// Actualiza el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
        internal void Update(CreditCards parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

            SessionCode = parent.SessionCode;
            CreditCardRecord obj = Session().Get<CreditCardRecord>(Oid);
            obj.CopyValues(this._base.Record);
            Session().Update(obj);

            _statements.Update(this);

            MarkOld();
        }

        /// <summary>
        /// Borra el registro de la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
        internal void DeleteSelf(CreditCards parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<CreditCardRecord>(Oid));
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
                    CreditCard.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        query = CreditCardStatements.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query);
                        _statements = CreditCardStatements.GetChildList(SessionCode, reader);
                    }
                }

                MarkOld();
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
                    CreditCardRecord obj = Session().Get<CreditCardRecord>(Oid);
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
                Session().Delete((CreditCardRecord)(criterio.UniqueResult()));
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

            query = "SELECT TC.*," +
                    "       CB.\"VALOR\" AS \"CUENTA_BANCARIA\"";

            return query;
        }

        internal static string WHERE(QueryConditions conditions)
        {
            string query = string.Empty;

            query += " WHERE TRUE";

            if (conditions.CreditCard != null) query += " AND TC.\"OID\" = " + conditions.CreditCard.Oid.ToString();
            if (conditions.BankAccount != null) query += " AND TC.\"OID_CUENTA_BANCARIA\" = " + conditions.BankAccount.Oid.ToString();
            if (conditions.TipoTarjeta != ETipoTarjeta.Todos) query += " AND TC.\"TIPO\" = " + ((long)conditions.TipoTarjeta).ToString();

            return query;
        }

        internal static string SELECT_BASE(QueryConditions conditions)
        {
            string t = nHManager.Instance.GetSQLTable(typeof(CreditCardRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));

            string query;

            query = SELECT_FIELDS() +
                    " FROM " + t + " AS TC" +
                    " LEFT JOIN " + cb + " AS CB ON TC.\"OID_CUENTA_BANCARIA\" = CB.\"OID\"";

            return query;
        }

        internal static string SELECT(long oid, bool lock_table)
        {
            string query;

            QueryConditions conditions = new QueryConditions { CreditCard = CreditCard.New().GetInfo(false) };
            conditions.CreditCard.Oid = oid;

            query = SELECT(conditions, lock_table);

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
            string query;

            query = SELECT_BASE(conditions) +
                    WHERE(conditions);

            if (lock_table) query += " FOR UPDATE OF TC NOWAIT";

            return query;
        }

        #endregion
    }

    public class CreditCardService
    {
        public static CreditCardStatement GetOrCreateStatementFromOperationDate(long oidCreditCard, DateTime baseDate, int sessionCode = -1)
        {
            CreditCardInfo credit_card = CreditCardInfo.Get(oidCreditCard, false);

            if (credit_card.ETipoTarjeta != ETipoTarjeta.Credito) return CreditCardStatement.New();

            if (credit_card.Statements == null) credit_card.LoadChilds(typeof(CreditCardStatement), false);
            CreditCardStatementInfo statement = credit_card.Statements.GetByDueDateItem(StatementDatesFromOperationDate.GetStatementDueDate(credit_card, baseDate));

            if (statement == null)
            {
                CreditCardStatement obj = CreditCardStatement.New(credit_card, baseDate, sessionCode);
                obj.From = StatementDatesFromOperationDate.GetStatementFromDate(credit_card, baseDate);
                obj.Till = StatementDatesFromOperationDate.GetStatementTillDate(credit_card, baseDate);
                obj.DueDate = StatementDatesFromOperationDate.GetStatementDueDate(credit_card, baseDate);
                return obj;
            }
            else
                return CreditCardStatement.Get(statement.Oid, false, sessionCode);
        }
        public static CreditCardStatement GetOrCreateStatementFromOperationDueDate(long oidCreditCard, DateTime dueDate, int sessionCode = -1)
        {
            CreditCardInfo credit_card = CreditCardInfo.Get(oidCreditCard, false);

            if (credit_card.ETipoTarjeta != ETipoTarjeta.Credito) return CreditCardStatement.New();

            if (credit_card.Statements == null) credit_card.LoadChilds(typeof(CreditCardStatement), false);
            CreditCardStatementInfo statement = credit_card.Statements.GetByDueDateItem(StatementDatesFromOperationDueDate.GetStatementDueDate(credit_card, dueDate));

            if (statement == null)
            {
                CreditCardStatement obj = CreditCardStatement.New(credit_card, dueDate, sessionCode);
                obj.From = StatementDatesFromOperationDueDate.GetStatementFromDate(credit_card, dueDate);
                obj.Till = StatementDatesFromOperationDueDate.GetStatementTillDate(credit_card, dueDate);
                obj.DueDate = StatementDatesFromOperationDueDate.GetStatementDueDate(credit_card, dueDate);
                return obj;
            }
            else
                return CreditCardStatement.Get(statement.Oid, false, sessionCode);
        }
    }

    public class StatementDatesFromOperationDate
    {
        internal static DateTime GetStatementDueDate(CreditCard obj, DateTime baseDate)
        {
            return GetStatementDueDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementDueDate(CreditCardInfo obj, DateTime baseDate)
        {
            return GetStatementDueDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementDueDate(CreditCardBase objBase, DateTime baseDate)
        {
            if (objBase.ETipoTarjeta != ETipoTarjeta.Credito) return DateTime.MinValue;

            DateTime due_date =  baseDate.AddMonths(1);

            int statement_limit_day = objBase.StatementLimitDay(due_date);
            int statement_pay_day = objBase.StatementPayDay(due_date);

            switch (objBase.EFormaPago)
            {
                case EFormaPago.MesVencido:
                    
                    due_date = new DateTime(due_date.Year, due_date.Month, statement_pay_day);

                    if (baseDate.Day <= statement_limit_day)
                        return due_date;
                    else
                        return due_date.AddMonths(1);

                case EFormaPago.XDiasMes:

                    return new DateTime(due_date.Year, due_date.Month, statement_pay_day);

                //Caso imposible
                default:
                    return DateTime.MinValue;
            }
        }

        internal static DateTime GetStatementFromDate(CreditCard obj, DateTime baseDate)
        {
            return GetStatementFromDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementFromDate(CreditCardInfo obj, DateTime baseDate)
        {
            return GetStatementFromDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementFromDate(CreditCardBase objBase, DateTime baseDate)
        {
            if (objBase.ETipoTarjeta != ETipoTarjeta.Credito) return DateTime.MinValue;

            switch (objBase.EFormaPago)
            {
                case EFormaPago.MesVencido:

                    int statement_limit_day = objBase.StatementLimitDay(baseDate);
                    return (baseDate.Day <= statement_limit_day)
                                ? new DateTime(baseDate.AddMonths(-1).Year, baseDate.AddMonths(-1).Month, statement_limit_day).AddDays(1)
                                : new DateTime(baseDate.Year, baseDate.Month, statement_limit_day).AddDays(1);

                case EFormaPago.XDiasMes:

                    return new DateTime(baseDate.Year, baseDate.Month, 1);

                //Caso imposible
                default:
                    return DateTime.MinValue;
            }
        }

        internal static DateTime GetStatementTillDate(CreditCard obj, DateTime baseDate)
        {
            return GetStatementTillDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementTillDate(CreditCardInfo obj, DateTime baseDate)
        {
            return GetStatementTillDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementTillDate(CreditCardBase objBase, DateTime baseDate)
        {
            if (objBase.ETipoTarjeta != ETipoTarjeta.Credito) return DateTime.MinValue;

            switch (objBase.EFormaPago)
            {
                case EFormaPago.MesVencido:

                    int statement_limit_day = objBase.StatementLimitDay(baseDate);
                    return (baseDate.Day <= statement_limit_day)
                                ? new DateTime(baseDate.Year, baseDate.Month, statement_limit_day)
                                : new DateTime(baseDate.AddMonths(1).Year, baseDate.AddMonths(1).Month, statement_limit_day);

                case EFormaPago.XDiasMes:

                    return new DateTime(baseDate.Year, baseDate.Month, DateAndTime.LastDay(baseDate.Month, baseDate.Year).Day);

                //Caso imposible
                default:
                    return DateTime.MinValue;
            }
        }        
    }

    public class StatementDatesFromOperationDueDate
    {     
        internal static DateTime GetStatementDueDate(CreditCard obj, DateTime baseDate)
        {
            return GetStatementDueDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementDueDate(CreditCardInfo obj, DateTime baseDate)
        {
            return GetStatementDueDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementDueDate(CreditCardBase objBase, DateTime dueDate)
        {
            if (objBase.ETipoTarjeta != ETipoTarjeta.Credito) return DateTime.MinValue;

            return new DateTime(dueDate.Year, dueDate.Month, dueDate.Day);
        }

        internal static DateTime GetStatementFromDate(CreditCard obj, DateTime baseDate)
        {
            return GetStatementFromDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementFromDate(CreditCardInfo obj, DateTime baseDate)
        {
            return GetStatementFromDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementFromDate(CreditCardBase objBase, DateTime dueDate)
        {
            if (objBase.ETipoTarjeta != ETipoTarjeta.Credito) return DateTime.MinValue;

            DateTime from_date = dueDate.AddMonths(-1);

            switch (objBase.EFormaPago)
            {
                case EFormaPago.MesVencido:
                    int statement_limit_day = objBase.StatementLimitDay(from_date);
                    return new DateTime(from_date.Year, from_date.Month, statement_limit_day).AddMonths(-1).AddDays(1);

                case EFormaPago.XDiasMes:
                    return new DateTime(from_date.Year, from_date.Month, 1);

                //Caso imposible
                default:
                    return DateTime.MinValue;
            }
        }

        internal static DateTime GetStatementTillDate(CreditCard obj, DateTime baseDate)
        {
            return GetStatementTillDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementTillDate(CreditCardInfo obj, DateTime baseDate)
        {
            return GetStatementTillDate(obj.Base, baseDate);
        }
        internal static DateTime GetStatementTillDate(CreditCardBase objBase, DateTime dueDate)
        {
            if (objBase.ETipoTarjeta != ETipoTarjeta.Credito) return DateTime.MinValue;

            DateTime till_date = dueDate.AddMonths(-1);

            switch (objBase.EFormaPago)
            {
                case EFormaPago.MesVencido:

                    int statement_limit_day = objBase.StatementLimitDay(till_date);
                    return new DateTime(till_date.Year, till_date.Month, statement_limit_day);

                case EFormaPago.XDiasMes:
                    return new DateTime(till_date.Year, till_date.Month, DateAndTime.LastDay(till_date.Month, till_date.Year).Day);

                //Caso imposible
                default:
                    return DateTime.MinValue;
            }
        }
    }
}