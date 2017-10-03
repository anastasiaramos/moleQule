using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library
{
	[Serializable()]
    public class PrivilegeMap : ClassMapping<PrivilegeRecord>
    {
        public PrivilegeMap()
        {
            Table("`Privilege`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`Privilege_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidUser, map => { map.Column("`OID_USER`"); });
			Property(x => x.OidItem, map => { map.Column("`OID_ITEM`"); });
			Property(x => x.Read, map => { map.Column("`READ`"); });
			Property(x => x.Create, map => { map.Column("`CREATE`"); });
			Property(x => x.Modify, map => { map.Column("`MODIFY`"); });
			Property(x => x.Remove, map => { map.Column("`DELETE`"); });
        }
    }
}
