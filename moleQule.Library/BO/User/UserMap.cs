using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library
{
	[Serializable()]
    public class UserMap : ClassMapping<UserRecord>
    {
        public UserMap()
        {
            Table("`User`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`User_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.Name, map => { map.Column("`NAME`"); map.Length(255); });
            Property(x => x.Password, map => { map.Column("`PASSWORD`"); map.Length(255); });
            Property(x => x.IsAdmin, map => { map.Column("`ADMOR`"); }); 
			Property(x => x.IsSuperUser, map => { map.Column("`MAIN`"); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); });
            Property(x => x.IsPartner, map => { map.Column("`IS_PARTNER`"); });
			Property(x => x.IsService, map => { map.Column("`IS_SERVICE`"); });
            Property(x => x.EntityType, map => { map.Column("`ENTITY_TYPE`"); });
            Property(x => x.OidEntity, map => { map.Column("`OID_ENTITY`"); });
            Property(x => x.Serial, map => { map.Column("`SERIAL`"); });
            Property(x => x.Code, map => { map.Column("`ID`"); map.Length(255); map.NotNullable(false); });
            Property(x => x.Pin, map => { map.Column("`PIN`"); map.Length(255); }); 
			Property(x => x.Email, map => { map.Column("`EMAIL`"); map.Length(255); });
            Property(x => x.CreationDate, map => { map.Column("`CREATION_DATE`"); map.NotNullable(false); });
            Property(x => x.LastLoginDate, map => { map.Column("`LAST_LOGIN_DATE`"); map.NotNullable(false); });
            Property(x => x.LastPasswordDate, map => { map.Column("`LAST_PASSWORD_DATE`"); map.NotNullable(false); });
            Property(x => x.LastLockedOutDate, map => { map.Column("`LAST_LOCK_OUT_DATE`"); map.NotNullable(false); });
            Property(x => x.LastActivityDate, map => { map.Column("`LAST_ACTIVITY_DATE`"); map.NotNullable(false); });
            Property(x => x.BirthDate, map => { map.Column("`BIRTH_DATE`"); map.NotNullable(false); });
            Property(x => x.PasswordQuestion, map => { map.Column("`PASSWORD_QUESTION`"); map.Length(255); map.NotNullable(false); });
            Property(x => x.PasswordResponse, map => { map.Column("`PASSWORD_RESPONSE`"); map.Length(255); map.NotNullable(false); });
        }
    }
}
