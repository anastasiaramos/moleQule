using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CreditCardMap : ClassMapping<CreditCardRecord>
    {
        public CreditCardMap()
        {
			Table("`CMCreditCard`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMCreditCard_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidCuentaBancaria, map => { map.Column("`OID_CUENTA_BANCARIA`"); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Numeracion, map => { map.Column("`NUMERACION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CuentaContable, map => { map.Column("`CUENTA_CONTABLE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.FormaPago, map => { map.Column("`FORMA_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.DiasPago, map => { map.Column("`DIAS_PAGO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.DiaExtracto, map => { map.Column("`DIA_EXTRACTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PComision, map => { map.Column("`P_COMISION`"); map.NotNullable(false); });
        }
    }
}
