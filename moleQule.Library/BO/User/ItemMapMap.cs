using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library
{
	[Serializable()]
    public class ItemMapMap : ClassMapping<ItemMapRecord>
    {
        public ItemMapMap()
        {
            Table("`ItemMap`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`ItemMap_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidItem, map => { map.Column("`OID_ITEM`"); });
			Property(x => x.Privilege, map => { map.Column("`PRIVILEGE`"); });
			Property(x => x.OidAssociateItem, map => { map.Column("`OID_ASSOCIATE_ITEM`"); });
			Property(x => x.AssociatePrivilege, map => { map.Column("`ASSOCIATE_PRIVILEGE`"); });
        }
    }
}
