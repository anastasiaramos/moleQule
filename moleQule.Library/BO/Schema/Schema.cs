using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx;  
using NHibernate;

namespace moleQule.Library
{
	/// <summary>
	/// Editable MAIN Root Business Object
	/// Clon this object in order to defining your own main root object for the application.
	/// </summary>
    [Serializable()]
    public class Schema : BusinessBaseEx<Schema> , ISchema
    {
        #region ISchema

        private string _code = string.Empty;
        private string _name = string.Empty;
        private bool _principal = false;
        private bool _use_default_reports = false;

        public virtual string Code
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _code;
            }

            set
            {
                CanWriteProperty(true);

                if (_code != value)
                {
                    _code = value;
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
                return _name;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_name != value)
                {
                    _name = value;
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
                return _principal;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (_principal != value)
                {
                    _principal = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool UseDefaultReports
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _use_default_reports;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (_use_default_reports != value)
                {
                    _use_default_reports = value;
                    PropertyHasChanged();
                }
            }
        }

		public virtual string SchemaCode { get { return Convert.ToInt64(_code).ToString("0000"); } }

        #endregion

        #region Business Methods

        private long _serial;

        public virtual long Serial
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _serial;
            }

            set
            {
                CanWriteProperty(true);
                _serial = value;
            }
        }

        /// <summary>
        /// Clona la entidad y sus subentidades y las marca como nuevas
        /// </summary>
        /// <returns>Una entidad clon</returns>
        public virtual Schema CloneAsNew()
        {
            Schema clon = base.Clone();

            // Se definen el Oid y el Codigo como nueva entidad
            Random rd = new Random();
            clon.Oid = rd.Next();
            clon.Code = (0).ToString(Resources.Defaults.SCHEMA_CODE_FORMAT);

            clon.MarkNew();
            return clon;
        }

		protected void CopyValues(Schema source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_code = source.Code;
			_name = source.Name;
			_principal = source.Principal;
            _use_default_reports = source.UseDefaultReports;

			_serial = source.Serial;
		}

        /// <summary>
        /// Devuelve el siguiente código de Empresa.
        /// </summary>
        /// <returns>Código de 9 cifras</returns>
        public static string GetNewCode()
        {
            Int64 lastcode = Schema.GetNewSerial();

            // Devolvemos el siguiente codigo de empresa 
            return lastcode.ToString(Resources.Defaults.SCHEMA_CODE_FORMAT);
        }

        /// <summary>
        /// Devuelve el siguiente Serial de Empresa.
        /// </summary>
        /// <returns>Código de 9 cifras</returns>
        protected static Int64 GetNewSerial()
        {
            // Obtenemos la lista de empresas ordenadas por serial
            SortedBindingList<SchemaInfo> emps = SchemaList.GetSortedList("Serial", ListSortDirection.Ascending);

            // Obtenemos el último código de empresa
            Int64 lastcode = 0;

            if (emps.Count > 0)
            {
                for (int i = 1; i < 11; i++)
                {
                    if (emps.Find("Serial", i) == -1)
                    {
                        lastcode = i;
                        return i;
                    }
                }
            }
            else
                lastcode = 1;

            return lastcode;
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //Code
            if (Code == string.Empty)
            {
                e.Description = Resources.Messages.NO_ID_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "Code");
            }

            //Name
            if (Name == string.Empty)
            {
                e.Description = Resources.Messages.NO_FIELD_FILLED;
                throw new iQValidationException(e.Description, string.Empty, "Name");
            }

            return true;
        }

        #endregion

		#region Authorization Rules

		public static bool CanAddObject() { return (AppContext.User == null) || AppContext.User.IsAdmin; }

		public static bool CanGetObject() { return true; }

		public static bool CanDeleteObject() { return AppContext.User.IsAdmin; }

		public static bool CanEditObject() { return AppContext.User.IsAdmin; }

		#endregion

        #region Factory Methods

		public static Schema New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            
			return DataPortal.Create<Schema>(new CriteriaCs(-1));
        }

		public static Schema Get(long oid)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Schema.GetCriteria(Schema.OpenSession());
			criteria.AddOidSearch(oid);
			Schema.BeginTransaction(criteria.Session);
			return DataPortal.Fetch<Schema>(criteria);
        }

        public virtual SchemaInfo GetInfo()
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            return new SchemaInfo(this);
        }

        /// <summary>
        /// Borrado inmediato, no cabe "undo"
        /// (La función debe ser "estática")
        /// </summary>
        /// <param name="oid"></param>
        public static void Delete(long oid)
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            
			DataPortal.Delete(new CriteriaCs(oid));
        }

        public override Schema Save()
        {
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

            if (IsDeleted && !CanDeleteObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            else if (IsNew && !CanAddObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            else if (!CanEditObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

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

                Transaction().Commit();

				if (isNew) PrincipalBase.NewSchema(Oid);

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

		#region ISchema

		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// </summary>
		/// <param name="oid"></param>
		public void IDelete(long oid)
		{
			CloseDBObject();
			Schema.Delete(oid);
		}

		public ISchema IGet(long oid)
		{
			return (ISchema)Schema.Get(oid);
		}

		#endregion

		#region Common Data Access

		[RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
			//_base.Record.Oid = (long)(new Random()).Next();
			_code = (0).ToString(Resources.Defaults.SCHEMA_CODE_FORMAT);
		}


		#endregion

		#region Root Data Access

		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                Oid = 0;
				SessionCode = criteria.SessionCode;

				CopyValues((Schema)(criteria.UniqueResult()));

                Session().Lock(Session().Get<Schema>(Oid), LockMode.UpgradeNoWait);
            }
			catch (NHibernate.ADOException)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQLockException(Resources.Messages.LOCK_ERROR);
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
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
				_code = GetNewCode();
				_serial = GetNewSerial();
				Session().Save(this);
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
					Schema obj = Session().Get<Schema>(Oid);
					obj.CopyValues(this);
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
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((Schema)(criterio.UniqueResult()));

				Transaction().Commit();

				//Borramos los usuarios que únicamente tenían acceso a esta empresa
				Users usuarios = Users.GetNoSchemaList();

				foreach (User usr in usuarios)
					User.Delete(usr.Oid);

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
            private string _codigo;
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
                CriteriaEx criteria = Schema.GetCriteria(Schema.OpenSession());
				criteria.AddCodeSearch(_codigo);
				SchemaList lista = SchemaList.GetList(criteria);
				_exists = !(lista.Count == 0);
		    }
        }

        #endregion

    }

    public class SchemaMap : ClassMapping<Schema>
    {
        public SchemaMap()
        {
            Table("`Schema`");
            Schema("\"COMMON\"");

            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`Schema_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.Name, map =>
            {
                map.Column("NAME");
                map.Length(255);
            });

            Property(x => x.Serial, map =>
            {
                map.Column("SERIAL");
                map.NotNullable(false);
                map.Unique(true);
            });

            Property(x => x.Code, map =>
            {
                map.Column("CODE");
                map.Length(255);
                map.NotNullable(false);
                map.Unique(true);
            });

        }
    }
}
