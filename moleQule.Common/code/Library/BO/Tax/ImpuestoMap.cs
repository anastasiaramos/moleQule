using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{	
    [Serializable()]
    public class TaxMap : ClassMapping<TaxRecord>
    {
        public TaxMap()
        {
            Table("`CMTax`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMTax_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Porcentaje, map => { map.Column("`PORCENTAJE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CuentaContableRepercutido, map => { map.Column("`CUENTA_CONTABLE_REPERCUTIDO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CuentaContableSoportado, map => { map.Column("`CUENTA_CONTABLE_SOPORTADO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidSubtipoFacturaEmitida, map => { map.Column("`OID_SUBTIPO_FACTURA_EMITIDA`"); map.Length(32768); });
			Property(x => x.OidSubtipoFacturaRecibida, map => { map.Column("`OID_SUBTIPO_FACTURA_RECIBIDA`"); map.Length(32768); });
        }
    }
}
