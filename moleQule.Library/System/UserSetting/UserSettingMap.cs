using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library
{
	[Serializable()]
	public class UserSettingMap : ClassMapping<UserSettingRecord>
    {
        public UserSettingMap()
        {
            Table("`UserSetting`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`UserSetting_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Name, map => { map.Column("`NAME`"); map.Length(255); map.NotNullable(false); });
			Property(x => x.Value, map => { map.Column("`VALUE`"); map.NotNullable(false); });                        
            Property(x => x.OidUser, map => { map.Column("`OID_USER`"); });
            Property(x => x.OidSetting, map => { map.Column("`OID_SETTING`"); });
        }
    }
}