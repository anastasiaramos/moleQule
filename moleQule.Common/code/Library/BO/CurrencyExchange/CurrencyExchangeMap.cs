using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class CurrencyExchangeMap : ClassMapping<CurrencyExchangeRecord>
	{	
		public CurrencyExchangeMap()
		{
			Table("`CMCurrencyExchange`");
			Schema("\"COMMON\"");
			Lazy(true);	
			
			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMCurrencyExchange_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.FromCurrencyIso, map => { map.Column("`FROM_CURRENCY`"); map.NotNullable(false); map.Length(255);  });
			Property(x => x.ToCurrencyIso, map => { map.Column("`TO_CURRENCY`"); map.NotNullable(false); map.Length(255);  });
			Property(x => x.Rate, map => { map.Column("`RELATION`"); map.NotNullable(false); });
			Property(x => x.Comments, map => { map.Column("`COMMENTS`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}
