using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CreditCardStatementMap : ClassMapping<CreditCardStatementRecord>
    {
        public CreditCardStatementMap()
        {
            Table("`CMCreditCardStatement`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMCreditCardStatement_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidCreditCard, map => { map.Column("`OID_CREDIT_CARD`"); });
			Property(x => x.From, map => { map.Column("`FROM`"); map.NotNullable(false); });
			Property(x => x.Till, map => { map.Column("`TILL`"); map.NotNullable(false); });
            Property(x => x.DueDate, map => { map.Column("`DUE_DATE`"); map.NotNullable(false); });
			Property(x => x.Amount, map => { map.Column("`AMOUNT`"); map.NotNullable(false); });
			Property(x => x.Comments, map => { map.Column("`COMMENTS`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}