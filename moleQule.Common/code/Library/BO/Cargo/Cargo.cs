using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using moleQule.Library;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class JobRecord : RecordBase
	{
		#region Attributes

		private string _valor = string.Empty;
  
		#endregion
		
		#region Properties
		public virtual string Valor { get { return _valor; } set { _valor = value; } }
		
		#endregion
		
		#region Business Methods
		
		public JobRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_valor = Format.DataReader.GetString(source, "VALOR");

		}
		
		public virtual void CopyValues(JobRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_valor = source.Valor;
		}
		#endregion	
	}

    [Serializable()]
	public class JobBase 
	{	 
		#region Attributes
		
		private JobRecord _record = new JobRecord();
		
		public JobRecord Record { get { return _record; } }
		
		#endregion
		
		#region Properties
		
		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);
		}
		
		internal void CopyValues(Cargo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source._base.Record);
		}
		internal void CopyValues(CargoInfo source)
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
    public class Cargo : BusinessBaseEx<Cargo>
    {
        #region Attributes

        public JobBase _base = new JobBase();

		#endregion

		#region Properties

		public JobBase Base { get { return _base; } }

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
        public virtual string Valor
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Valor;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_base.Record.Valor != value)
                {
                    _base.Record.Valor = value;
                    PropertyHasChanged();
                }
            }
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.StringRequired, "Valor");
        }

        #endregion

        #region Authorization Rules

        protected override void AddAuthorizationRules()
        {
        }

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.AUXILIARES)
                    || AppContext.User.IsAdmin;
        }

        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.AUXILIARES)
					|| AppContext.User.IsAdmin;
        }

        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.AUXILIARES)
					|| AppContext.User.IsAdmin;
        }

        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUXILIARES)
					|| AppContext.User.IsAdmin;
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public Cargo()
        {
            MarkAsChild();
            Random r = new Random();
            Oid = (long)r.Next();
        }
		private Cargo(Cargo source)
		{
			MarkAsChild();
			Fetch(source);
		}
		private Cargo(int sessionCode, IDataReader reader, bool childs)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		
		internal static Cargo NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			return new Cargo();
		}

		internal static Cargo GetChild(Cargo source) { return new Cargo(source); }
		internal static Cargo GetChild(int sessionCode, IDataReader reader, bool childs = false) { return new Cargo(sessionCode, reader, childs); }
		
        public virtual CargoInfo GetInfo() { return new CargoInfo(this); }

        /// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }

        /// <summary>
        /// No se debe utilizar esta función para guardar. Hace falta el padre.
        /// Utilizar Insert o Update en sustitución de Save.
        /// </summary>
        /// <returns></returns>
        public override Cargo Save()
        {
            throw new iQException(moleQule.Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
        }

        #endregion

        #region Child Data Access

        private void Fetch(Cargo source)
        {
			_base.CopyValues(source);
            MarkOld();
        }

        private void Fetch(IDataReader reader)
        {
            _base.CopyValues(reader);
            MarkOld();
        }

        internal void Insert(Cargos parent)
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
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void Update(Cargos parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            SessionCode = parent.SessionCode;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                JobRecord obj = Session().Get<JobRecord>(Oid);
                obj.CopyValues(this._base.Record);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void DeleteSelf(Cargos parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<JobRecord>(Oid));
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkNew();
        }

        #endregion

        #region Commands

        public static bool Exists(string valor)
        {
            ExistsCmd result;
            result = DataPortal.Execute<ExistsCmd>(new ExistsCmd(valor));
            return result.Exists;
        }

        [Serializable()]
        private class ExistsCmd : CommandBase
        {
            private string _valor;
            private bool _exists = false;

            public bool Exists
            {
                get { return _exists; }
            }

            public ExistsCmd(string valor)
            {
                _valor = valor;
            }

            protected override void DataPortal_Execute()
            {
                // Buscar por valor
                CriteriaEx criteria = Cargo.GetCriteria(Cargo.OpenSession());
                criteria.AddEq("Valor", _valor);
                CargoList list = CargoList.GetList(criteria);
                _exists = !(list.Count == 0);
            }
        }

        #endregion

		#region SQL

		//public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT CG.*";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE TRUE";

			/*if (conditions.Cargo != null)
				if (conditions.Cargo.Oid != 0)
					query += " AND CG.\"OID\" = " + conditions.Cargo.Oid;*/

			return query;
		}

		/*internal static string SELECT(long oid, bool lock_table)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { cARG = Pesaje.New().GetInfo(false) };
			conditions.Pesaje.Oid = oid;

			query = SELECT(conditions, lock_table);

			return query;
		}*/

		internal static string SELECT(QueryConditions conditions, bool lock_table)
		{
			string cg = nHManager.Instance.GetSQLTable(typeof(JobRecord));

			string query;

			query = SELECT_FIELDS() +
					" FROM " + cg + " AS CG";

			query += WHERE(conditions);

			return query;
		}

		#endregion
    }
}
