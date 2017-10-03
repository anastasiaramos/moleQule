using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
    public class InvoiceSubtypeMap : ClassMapping<InvoiceSubtypeRecord>
    {
        public InvoiceSubtypeMap()
        {
			Table("`CMInvoiceSubtype`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMInvoiceSubtype_seq`" })); map.Column("`OID`"); });
            Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(true); });
            Property(x => x.Descripcion, map => { map.Column("`DESCRIPCION`"); map.NotNullable(true); });
            Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(true); });
        }
    }
}
