using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library
{
	[Serializable()]
    public class SettingItemMap : ClassMapping<SettingItemRecord>
    {
        public SettingItemMap()
        {
            Table("`Setting`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`Setting_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Name, map => { map.Column("`NAME`"); map.Length(255); map.NotNullable(false); });
			Property(x => x.Copyable, map => { map.Column("`COPY`"); });
			Property(x => x.Comments, map => { map.Column("`COMMENTS`"); map.NotNullable(false); });
        }
    }
}