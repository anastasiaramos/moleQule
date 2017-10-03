using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class CurrencyExchangeRecord : RecordBase
	{
		#region Attributes

		private string _from_currency = string.Empty;
		private string _to_currency = string.Empty;
		private Decimal _rate;
		private string _comments = string.Empty;
  
		#endregion
		
		#region Properties
		
		public virtual string FromCurrencyIso { get { return _from_currency; } set { _from_currency = value; } }
		public virtual string ToCurrencyIso { get { return _to_currency; } set { _to_currency = value; } }
		public virtual Decimal Rate { get { return _rate; } set { _rate = value; } }
		public virtual string Comments { get { return _comments; } set { _comments = value; } }

		#endregion
		
		#region Business Methods
		
		public CurrencyExchangeRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_from_currency = Format.DataReader.GetString(source, "FROM_CURRENCY");
			_to_currency = Format.DataReader.GetString(source, "TO_CURRENCY");
			_rate = Format.DataReader.GetDecimal(source, "RELATION");
			_comments = Format.DataReader.GetString(source, "COMMENTS");

		}		
		public virtual void CopyValues(CurrencyExchangeRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_from_currency = source.FromCurrencyIso;
			_to_currency = source.ToCurrencyIso;
			_rate = source.Rate;
			_comments = source.Comments;
		}
		
		#endregion	
	}

    [Serializable()]
	public class CurrencyExchangeBase 
	{	 
		#region Attributes
		
		private CurrencyExchangeRecord _record = new CurrencyExchangeRecord();
		
		#endregion
		
		#region Properties
		
		public CurrencyExchangeRecord Record { get { return _record; } }

		public string FromCurrency
		{
			get { return Library.Currency.Find(_record.FromCurrencyIso).Name; }
			set { _record.FromCurrencyIso = Library.Currency.FindByName(value).ISOCode; }
		}
		public string ToCurrency
		{
			get { return Library.Currency.Find(_record.ToCurrencyIso).Name; }
			set { _record.ToCurrencyIso = Library.Currency.FindByName(value).ISOCode; }
		}

		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);
		}		
		public void CopyValues(CurrencyExchange source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}
		public void CopyValues(CurrencyExchangeInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}

		internal static decimal GetCurrencyRate(string fromCurrencyIso, string toCurrencyIso)
		{
			CurrencyExchangeList list = CurrencyExchangeList.GetList(false);
			CurrencyExchangeInfo currency = list.GetItem(fromCurrencyIso, toCurrencyIso);

			return (currency != null) ? currency.Rate : 0;
		}

		#endregion	
	}
		
	/// <summary>
	/// </summary>	
    [Serializable()]
	public class CurrencyExchange : BusinessBaseEx<CurrencyExchange>
	{	 
		#region Attributes
		
		protected CurrencyExchangeBase _base = new CurrencyExchangeBase();
		

		#endregion
		
		#region Properties
		
		public CurrencyExchangeBase Base { get { return _base; } }
		
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
				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
					//PropertyHasChanged();
				}
			}
		}
		public virtual string FromCurrencyIso
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FromCurrencyIso;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.FromCurrencyIso.Equals(value))
				{
					_base.Record.FromCurrencyIso = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string ToCurrencyIso
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ToCurrencyIso;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.ToCurrencyIso.Equals(value))
				{
					_base.Record.ToCurrencyIso = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Rate
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Rate;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Rate.Equals(value))
				{
					_base.Record.Rate = value;
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

		public virtual string FromCurrency { get { return _base.FromCurrency; } set { _base.FromCurrency = value; } }
		public virtual string ToCurrency { get { return _base.ToCurrency; } set { _base.ToCurrency = value; } }
		
		#endregion
		
		#region Business Methods
		
		protected virtual void CopyFrom(CurrencyExchangeInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			FromCurrencyIso = source.FromCurrency;
			ToCurrency = source.ToCurrency;
			Rate = source.Rate;
			Comments = source.Comments;
		}

		public static decimal GetCurrencyRate(string fromCurrencyIso, string toCurrencyIso) { return CurrencyExchangeBase.GetCurrencyRate(fromCurrencyIso, toCurrencyIso); }

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
		/// Debe ser public para que funcionen los DataGridView
		/// </summary>
		protected CurrencyExchange ()
		{
			// Si se necesita constructor público para este objeto hay que añadir el MarkAsChild() debido a la interfaz Child
			// y el código que está en el DataPortal_Create debería ir aquí
		
		}
				
		private CurrencyExchange(CurrencyExchange source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private CurrencyExchange(int sessionCode, IDataReader source, bool childs)
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
		public static CurrencyExchange NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CurrencyExchange obj = DataPortal.Create<CurrencyExchange>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">CurrencyExchange con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(CurrencyExchange source, bool childs)
		/// <remarks/>
		internal static CurrencyExchange GetChild(CurrencyExchange source) { return new CurrencyExchange(source, false); }
		internal static CurrencyExchange GetChild(CurrencyExchange source, bool childs) { return new CurrencyExchange(source, childs); }
        internal static CurrencyExchange GetChild(int sessionCode, IDataReader source) { return new CurrencyExchange(sessionCode, source, false); }
        internal static CurrencyExchange GetChild(int sessionCode, IDataReader source, bool childs) { return new CurrencyExchange(sessionCode, source, childs); }

		public virtual CurrencyExchangeInfo GetInfo (bool childs = true) { return new CurrencyExchangeInfo(this, childs); }
		
		#endregion				
		
		#region Common Data Access
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria) {}
		
		private void Fetch(CurrencyExchange source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);			 

			MarkOld();
		}
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
		internal void Insert(CurrencyExchanges parent)
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
		internal void Update(CurrencyExchanges parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			CurrencyExchangeRecord obj = Session().Get<CurrencyExchangeRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(CurrencyExchanges parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<CurrencyExchangeRecord>(Oid));
		
			MarkNew(); 
		}

		#endregion		
				
        #region SQL

		internal enum EQueryType { GENERAL = 0, CLUSTERED = 1 }
		
		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() {};
        }
		
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string SELECT_FIELDS(EQueryType queryType, QueryConditions conditions)
        {            	
            string query = @"
			SELECT " + (long)queryType + @" AS ""QUERY_TYPE""";

			switch (queryType)
			{
				case EQueryType.GENERAL:

					query += @"
						,CU.*";

					break;
			}

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
            string cu = nHManager.Instance.GetSQLTable(typeof(CurrencyExchangeRecord));

			string query;

            query = @"
			FROM " + cu + @" AS CU";
				
			return query + " " + conditions.ExtraJoin;
		}
		
		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;
		
			string query;

            query = @" 
			WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "CU", ForeignFields());
				
			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "CU");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "CU");
			
			return query + " " + conditions.ExtraWhere;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
			string query = 
			SELECT_FIELDS(EQueryType.GENERAL, conditions) + 
			JOIN(conditions) +
			WHERE(conditions);

            if (conditions != null) 
			{
				query += ORDER(conditions.Orders, "CU", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}				

			query += Common.EntityBase.LOCK("CU", lockTable);

            return query;
        }
		
		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions, lockTable);
		}
		
		#endregion
	}
}
