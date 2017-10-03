using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
    public class GrantMap : ClassMapping<GrantRecord>
    {
        public GrantMap()
        {
			Table("`CMGrant`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMGrant_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CuentaContable, map => { map.Column("`CUENTA_CONTABLE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}
