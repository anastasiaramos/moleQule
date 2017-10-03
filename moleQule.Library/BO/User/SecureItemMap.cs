using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library
{
	[Serializable()]
    public class SecureItemMap : ClassMapping<SecureItemRecord>
    {
        public SecureItemMap()
        {
            Table("`SecureItem`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`SecureItem_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Name, map => { map.Column("`DESCRIPTOR`"); map.Length(255); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); });
        }
    }
}
