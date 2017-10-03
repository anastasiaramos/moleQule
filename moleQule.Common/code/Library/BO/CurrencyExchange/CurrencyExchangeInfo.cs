using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class CurrencyExchangeInfo : ReadOnlyBaseEx<CurrencyExchangeInfo, CurrencyExchange>
	{	
		#region Attributes

		protected CurrencyExchangeBase _base = new CurrencyExchangeBase();

		
		#endregion
		
		#region Properties
		
		public CurrencyExchangeBase Base { get { return _base; } }
		
		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; }}
		public string FromCurrencyIso { get { return _base.Record.FromCurrencyIso; } }
		public string ToCurrencyIso { get { return _base.Record.ToCurrencyIso; } }
		public Decimal Rate { get { return _base.Record.Rate; } }
		public string Comments { get { return _base.Record.Comments; } }

		public string FromCurrency { get { return _base.FromCurrency; } set { _base.FromCurrency = value; } }
		public string ToCurrency { get { return _base.ToCurrency; } set { _base.ToCurrency = value; } }
		
		#endregion
		
		#region Business Methods
						
		public void CopyFrom(CurrencyExchange source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected CurrencyExchangeInfo() { /* require use of factory methods */ }
		private CurrencyExchangeInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal CurrencyExchangeInfo(CurrencyExchange item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
		
		public static CurrencyExchangeInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new CurrencyExchangeInfo(sessionCode, reader, childs);
		}
		
		public static CurrencyExchangeInfo New(long oid = 0) { return new CurrencyExchangeInfo(){ Oid = oid}; }
		
 		#endregion
					
		#region Common Data Access
								
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion			
	}
}
