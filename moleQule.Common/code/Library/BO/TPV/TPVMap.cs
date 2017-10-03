using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class TPVMap : ClassMapping<TPVRecord>
    {
        public TPVMap()
        {
			Table("`CMTpv`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMTpv_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidCuentaBancaria, map => { map.Column("`OID_CUENTA_BANCARIA`"); map.Length(32768); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CuentaContable, map => { map.Column("`CUENTA_CONTABLE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PComision, map => { map.Column("`P_COMISION`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}
