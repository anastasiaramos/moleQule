using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library
{
	[Serializable()]
    public class ApplicationSettingMap : ClassMapping<ApplicationSettingRecord>
    {
        public ApplicationSettingMap()
        {
            Table("`Variable`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`Variable_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Name, map => { map.Column("`NAME`"); map.Length(255); map.NotNullable(false); });
			Property(x => x.Value, map => { map.Column("`VALUE`"); map.NotNullable(false); });
        }
    }
}