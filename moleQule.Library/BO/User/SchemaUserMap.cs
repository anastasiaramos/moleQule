using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library
{
	[Serializable()]
    public class SchemaUserMap : ClassMapping<SchemaUserRecord>
    {
        public SchemaUserMap()
        {
            Table("`SchemaUser`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`SchemaUser_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidSchema, map => { map.Column("`OID_SCHEMA`"); });
			Property(x => x.OidUser, map => { map.Column("`OID_USER`"); });
        }
    }
}
