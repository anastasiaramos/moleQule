using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CreditCardStatementRecord : RecordBase
    {
        #region Attributes

        private long _oid_credit_card;
        private DateTime _from;
        private DateTime _till;
        private Decimal _amount;
        private DateTime _due_date;
        private string _comments = string.Empty;

        #endregion

        #region Properties

        public virtual long OidCreditCard { get { return _oid_credit_card; } set { _oid_credit_card = value; } }
        public virtual DateTime From { get { return _from; } set { _from = value; } }
        public virtual DateTime Till { get { return _till; } set { _till = value; } }
        public virtual DateTime DueDate { get { return _due_date; } set { _due_date = value; } }
        public virtual Decimal Amount { get { return _amount; } set { _amount = value; } }
        public virtual string Comments { get { return _comments; } set { _comments = value; } }

        #endregion

        #region Business Methods

        public CreditCardStatementRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_credit_card = Format.DataReader.GetInt64(source, "OID_CREDIT_CARD");
            _from = Format.DataReader.GetDateTime(source, "FROM");
            _till = Format.DataReader.GetDateTime(source, "TILL");
            _due_date = Format.DataReader.GetDateTime(source, "DUE_DATE");
            _amount = Format.DataReader.GetDecimal(source, "AMOUNT");
            _comments = Format.DataReader.GetString(source, "COMMENTS");
        }
        public virtual void CopyValues(CreditCardStatementRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_credit_card = source.OidCreditCard;
            _from = source.From;
            _till = source.Till;
            _due_date = source.DueDate;
            _amount = source.Amount;
            _comments = source.Comments;
        }

        #endregion
    }

    [Serializable()]
    public class CreditCardStatementBase
    {
        #region Attributes

        private CreditCardStatementRecord _record = new CreditCardStatementRecord();

        private long _status;
        private string _credit_card = string.Empty;
        private string _bank_account = string.Empty;
        private decimal _payed;
        private decimal _pending;
        private decimal _allocated;
        private decimal _deallocated;
        private decimal _aggregate;
        private DateTime _allocation_date;
        private string _linked = Resources.Labels.SET_PAGO;
        private decimal _cash_amount;

        public CreditCardStatementRecord Record { get { return _record; } }

        #endregion

        #region Properties

        public string BankAccount { get { return _bank_account; } set { _bank_account = value; } }
        public string CreditCard { get { return _credit_card; } set { _credit_card = value; } }

        public long Status { get { return _status; } set { _status = value; } }
        public virtual EEstado EStatus { get { return (EEstado)Status; } set { Status = (long)value; } }
        public virtual string StatusLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EStatus); } }
        public decimal Allocated { get { return _allocated; } set { _allocated = value; } }
        public decimal Payed { get { return _payed; } set { _payed = value; } }
        public decimal Pending { get { return _pending; } set { _pending = value; } }
        public decimal Deallocated { get { return Math.Min(_pending, _deallocated); } set { _deallocated = value; } }
        public string AllocationDate { get { return (_allocation_date != DateTime.MinValue) ? _allocation_date.ToShortDateString() : "---"; } set { _allocation_date = DateTime.Parse(value); } }
        public string Linked { get { return _linked; } set { _linked = value; } }
        public decimal Aggregate { get { return _aggregate; } set { _aggregate = value; } }
        public ETipoAcreedor ETipoAcreedor { get { return ETipoAcreedor.Todos; } set { } }
        public decimal CashAmount { get { return _cash_amount; } set { _cash_amount = value; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _bank_account = Format.DataReader.GetString(source, "BANK_ACCOUNT");
            _credit_card = Format.DataReader.GetString(source, "CREDIT_CARD");

            _allocated = Format.DataReader.GetDecimal(source, "ASIGNADO_PAGO");
            _pending = Format.DataReader.GetDecimal(source, "PENDIENTE");
            _deallocated = Format.DataReader.GetDecimal(source, "PENDIENTE_ASIGNAR");
            _payed = Format.DataReader.GetDecimal(source, "TOTAL_PAGADO");

            _deallocated = Math.Min(_pending, _deallocated);
            _linked = (_allocated == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
            _status = (_pending != 0) ? (long)EEstado.Abierto : (long)EEstado.Pagado;

            _cash_amount = Format.DataReader.GetDecimal(source, "CASH_AMOUNT");
        }
        internal void CopyValues(CreditCardStatement source)
        {
            if (source == null) return;

            _record.CopyValues(source.Base.Record);

            _bank_account = source.BankAccount;
            _credit_card = source.CreditCard;
            _pending = source.Base.Pending;
            _deallocated = source.Base.Deallocated;
            _status = source.Base.Status;
            _cash_amount = source.CashAmount;
        }
        internal void CopyValues(CreditCardStatementInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source.Base.Record);

            _bank_account = source.BankAccount;
            _credit_card = source.CreditCard;
            _pending = source.Base.Pending;
            _deallocated = source.Base.Deallocated;
            _status = source.Base.Status;
            _cash_amount = source.CashAmount;
        }

        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class CreditCardStatement : BusinessBaseEx<CreditCardStatement>, ITransactionPayment
	{	 
		#region Attributes

        public CreditCardStatementBase _base = new CreditCardStatementBase();

		#endregion

        #region ITransactionPayment

        public Decimal Total { get { return _base.Record.Amount; } }
        public decimal Asignado { get { return _base.Allocated; } set { _base.Allocated = value; } }
        public decimal TotalPagado { get { return _base.Payed; } set { _base.Payed = value; } }
        public decimal Pendiente { get { return _base.Pending; } set { _base.Pending = value; } }
        public decimal PendienteAsignar { get { return _base.Deallocated; } set { _base.Deallocated = value; } }
        public decimal Acumulado { get { return _base.Aggregate; } set { _base.Aggregate = value; } }
        public string FechaAsignacion { get { return _base.AllocationDate; } set { _base.AllocationDate = value; } }
        public string Vinculado { get { return _base.Linked; } set { _base.Linked = value; } }
        public string NFactura { get { return string.Empty; } set { } }
        public long OidExpediente { get { return 0; } }
        public ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { } }

        #endregion

		#region Properties

        public CreditCardStatementBase Base { get { return _base; } }

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
        public virtual long OidCreditCard
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.OidCreditCard;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);


                if (!_base.Record.OidCreditCard.Equals(value))
                {
                    _base.Record.OidCreditCard = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime From
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.From;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.From.Equals(value))
				{
                    _base.Record.From = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Till
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Till;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.Till.Equals(value))
				{
                    _base.Record.Till = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual DateTime DueDate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.DueDate;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.DueDate.Equals(value))
                {
                    _base.Record.DueDate = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Amount
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                ////CanReadProperty(true);
                return _base.Record.Amount;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                ////CanWriteProperty(true);

                if (!_base.Record.Amount.Equals(value))
                {
                    _base.Record.Amount = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual string Comments
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Comments;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

                if (!_base.Record.Comments.Equals(value))
				{
                    _base.Record.Comments = value;
					PropertyHasChanged();
				}
			}
		}

        public EEstado EStatus { get { return _base.EStatus; } set { _base.EStatus = value; } }
        public string StatusLabel { get { return _base.StatusLabel; } }
        public string BankAccount { get { return _base.BankAccount; } set { _base.BankAccount = value; } }
        public string CreditCard { get { return _base.CreditCard; } set { _base.CreditCard = value; } }
        public decimal CashAmount { get { return _base.CashAmount; } set { _base.CashAmount = value; } }

        #endregion
		
		#region Business Methods

        public virtual CreditCardStatement CloneAsNew()
		{
            CreditCardStatement clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();

            clon.SessionCode = CreditCardStatement.OpenSession();
            CreditCardStatement.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}

        protected virtual void CopyFrom(CreditCardStatementInfo source)
		{
            _base.CopyValues(source);
		}
        protected virtual void CopyFrom(CreditCard source)
        {
            if (source == null) return;

            OidCreditCard = source.Oid;
            CreditCard = source.Numeracion;
            BankAccount = source.CuentaBancaria;
        }

		#endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            if (OidCreditCard == 0)
            {
                e.Description = Resources.Messages.NO_CREDIT_CARD_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "OidCreditCard");
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
		protected CreditCardStatement()
        {
            Oid = (long)(new Random()).Next();
        }
	
		public virtual CreditCardStatementInfo GetInfo (bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			return new CreditCardStatementInfo(this, childs);
		}
		
		#endregion
		
        #region Child Factory Methods
	
		private CreditCardStatement(CreditCardStatement source, bool childs)
		{
			MarkAsChild();
            Childs = childs;
			Fetch(source);
		}
        private CreditCardStatement(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();
            SessionCode = sessionCode;
            Childs = childs;
            Fetch(source);
        }
		
		public static CreditCardStatement NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CreditCardStatement obj = DataPortal.Create<CreditCardStatement>(new CriteriaCs(-1));
            obj.MarkAsChild();
            return obj;
		}
        public static CreditCardStatement NewChild(CreditCard source)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            CreditCardStatement obj = DataPortal.Create<CreditCardStatement>(new CriteriaCs(-1));
            obj.MarkAsChild();
            obj.CopyFrom(source);
            return obj;
        }

        internal static CreditCardStatement GetChild(int sessionCode, IDataReader source, bool childs = false)
        {
            return new CreditCardStatement(sessionCode, source, childs);
        }
		internal static CreditCardStatement GetChild(CreditCardStatement source, bool childs = false)
		{
			return new CreditCardStatement(source, childs);
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
			
		#endregion

		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static CreditCardStatement New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<CreditCardStatement>(new CriteriaCs(-1));
		}
        public static CreditCardStatement New(CreditCardInfo creditCard, DateTime baseDate, decimal amount, int sessionCode = -1)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            CreditCardStatement obj = DataPortal.Create<CreditCardStatement>(new CriteriaCs(-1));

            obj.SetSharedSession(sessionCode);
            obj.OidCreditCard = creditCard.Oid;
            obj.From = StatementDatesFromOperationDueDate.GetStatementFromDate(creditCard, baseDate);
            obj.Till = StatementDatesFromOperationDueDate.GetStatementTillDate(creditCard, baseDate);
            obj.DueDate = StatementDatesFromOperationDueDate.GetStatementDueDate(creditCard, baseDate);

            return obj;
        }

        public new static CreditCardStatement Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return BusinessBaseEx<CreditCardStatement>.Get(query, childs, -1);
		}
		public static CreditCardStatement Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }
		
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
			int sessCode = CreditCardStatement.OpenSession();
			ISession sess = CreditCardStatement.Session(sessCode);
			ITransaction trans = CreditCardStatement.BeginTransaction(sessCode);
			
			try
			{	
				sess.Delete("from CreditCardStatementRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				CreditCardStatement.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override CreditCardStatement Save()
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

                if (!SharedTransaction) Transaction().Commit();
                return this;
            }
            catch (Exception ex)
            {
                if (!SharedTransaction && Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!SharedTransaction)
                {
                    if (CloseSessions) CloseSession();
                    else BeginTransaction();
                }
            }

            return this;
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
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
        private void Fetch(CreditCardStatement source)
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
        internal void Insert(CreditCardStatements parent)
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
		internal void Update(CreditCardStatements parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			

			ValidationRules.CheckRules();

            if (!IsValid)
                throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			CreditCardStatementRecord obj = Session().Get<CreditCardStatementRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(CreditCardStatements parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<CreditCardStatementRecord>(Oid));
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
					CreditCardStatement.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);					 
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
					CreditCardStatementRecord obj = Session().Get<CreditCardStatementRecord>(Oid);
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
				Session().Delete((CreditCardStatementRecord)(criterio.UniqueResult()));
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

        #region Child Data Access

        internal void Insert(CreditCard parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            OidCreditCard = parent.Oid;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(_base.Record);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void Update(CreditCard parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            OidCreditCard = parent.Oid;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
                CreditCardStatementRecord obj = Session().Get<CreditCardStatementRecord>(Oid);
                obj.CopyValues(this._base.Record);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void DeleteSelf(CreditCard parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<CreditCardStatementRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

        #endregion
		
        #region SQL

        public new static string SELECT(long oid) { return SELECT(oid, true); }
        internal static string SELECT(long oid, bool lockTable)
        {
            QueryConditions conditions = new QueryConditions { CreditCardStatement = CreditCardStatementInfo.New(oid) };

            string query = SELECT(conditions, lockTable);

            return query;
        }
        public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }        

        internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() 
			{
			};
        }

        internal static string FIELDS(QueryConditions conditions)
        {
            string query = @"
            SELECT CCS.*
                    ,CC.""NUMERACION"" AS ""CREDIT_CARD""
                    ,BK.""VALOR"" AS ""BANK_ACCOUNT""
                    ,COALESCE(PF1.""TOTAL_PAGADO"", 0) AS ""TOTAL_PAGADO""
                    ,COALESCE(CCS.""AMOUNT"" - (PF1.""TOTAL_PAGADO"" - COALESCE(PF2.""ASIGNADO_PAGO"", 0)), CCS.""AMOUNT"") AS ""PENDIENTE""
                    ,COALESCE(PF2.""ASIGNADO_PAGO"", 0) AS ""ASIGNADO_PAGO""
                    ,COALESCE(CCS.""AMOUNT"", 0) - COALESCE(PF1.""TOTAL_PAGADO"", 0) AS ""PENDIENTE_ASIGNAR""
                    ,COALESCE(CAL.""CASH_AMOUNT"", 0) AS ""CASH_AMOUNT""";

            return query;
        }

        internal static string JOIN(QueryConditions conditions)
		{
            string ccs = nHManager.Instance.GetSQLTable(typeof(CreditCardStatementRecord));
            string cc = nHManager.Instance.GetSQLTable(typeof(CreditCardRecord));
            string bk = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));

			string query = @"
			FROM " + ccs + @" AS CCS
            LEFT JOIN " + cc + @" AS CC ON CC.""OID"" = CCS.""OID_CREDIT_CARD""
			LEFT JOIN " + bk + @" AS BK ON BK.""OID"" = CC.""OID_CUENTA_BANCARIA""";

            Assembly assembly = Assembly.Load("moleQule.Library.Store");
            Type type = assembly.GetType("moleQule.Library.Store.TransactionPayment");

            long[] types = new long[] { 6 }; //ETipoPago.ExtractoTarjeta
            query += (string)type.InvokeMember("JOIN_PAYMENTS", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[3] { conditions, types, "CCS" });
            
            assembly = Assembly.Load("moleQule.Library.Invoice");
            type = assembly.GetType("moleQule.Library.Invoice.CashLine");

            query += (string)type.InvokeMember("JOIN_CREDIT_CARD_STATEMENTS", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[2] { conditions, "CCS" });                

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query += @"
            WHERE (CCS.""FROM"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

            if (conditions.CreditCardStatement != null) 
                query += @"
                AND CCS.""OID"" = " + conditions.CreditCardStatement.Oid;

			if (conditions.CreditCard != null) 
                query += @"
                AND CCS.""OID_CREDIT_CARD"" = " + conditions.CreditCard.Oid;

			if (conditions.BankAccount != null) 
                query += @"
                AND CC.""OID_CUENTA_BANCARIA"" = " + conditions.BankAccount.Oid;

			if (conditions.TipoTarjeta != ETipoTarjeta.Todos) 
                query += @"
                AND CC.""TIPO"" = " + (long)conditions.TipoTarjeta;

			return query + " " + conditions.ExtraWhere;
		}

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query = 
            FIELDS(conditions) +
            JOIN(conditions) +
			WHERE(conditions);

            if (conditions != null)
            {
                if (conditions.Step != EStepGraph.None)
                {
                    query += @"
					GROUP BY ""STEP""
					ORDER BY ""STEP""";
                }
                else
                {
                    if (conditions.Orders == null)
                    {
                        conditions.Orders = new OrderList();
                        conditions.Orders.Add(FilterMng.BuildOrderItem("Till", ListSortDirection.Descending, typeof(CreditCardStatement)));
                    }

                    query += ORDER(conditions.Orders, "CCS", ForeignFields());
                    query += LIMIT(conditions.PagingInfo);
                }
            }

            query += EntityBase.LOCK("CCS", lockTable);

            return query;
        }

        internal static string SELECT_BY_PAYMENT(QueryConditions conditions, bool lockTable)
        {
            conditions.ExtraWhere = @"
				AND (PA.""OID"" = " + conditions.OidEntity + ")";

            conditions.Orders = new OrderList();
            conditions.Orders.Add(FilterMng.BuildOrderItem("Till", ListSortDirection.Ascending, typeof(CreditCardStatement)));

            string query =
            SELECT(conditions, lockTable);

            return query;
        }

        internal static string SELECT_UNPAID(QueryConditions conditions, bool lockTable)
        {
            conditions.ExtraWhere = @"
				AND (PF1.""TOTAL_PAGADO"" != CCS.""AMOUNT"" OR PF1.""TOTAL_PAGADO"" IS NULL)
				AND (CCS.""DUE_DATE"" BETWEEN '" + conditions.FechaAuxIniLabel + "' AND '" + conditions.FechaAuxFinLabel + @"')
				AND COALESCE(CCS.""AMOUNT"" - PF1.""TOTAL_PAGADO"", CCS.""AMOUNT"") != 0";

            if (conditions.Orders == null)
                conditions.Orders = new OrderList() 
                { 
                    FilterMng.BuildOrderItem("DueDate", ListSortDirection.Ascending, typeof(CreditCardStatement)) 
                };

            string query =
            SELECT(conditions, lockTable);

            return query;
        }

		#endregion
	}
}