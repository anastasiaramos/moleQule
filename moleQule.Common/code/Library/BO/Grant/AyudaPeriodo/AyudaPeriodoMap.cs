using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
    public class GrantPeriodMap : ClassMapping<GrantPeriodRecord>
    {
        public GrantPeriodMap()
        {
			Table("`CMGrantPeriod`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMGrantPeriod_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidAyuda, map => { map.Column("`OID_AYUDA`"); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoDescuento, map => { map.Column("`TIPO_DESCUENTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Porcentaje, map => { map.Column("`PORCENTAJE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Cantidad, map => { map.Column("`CANTIDAD`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaIni, map => { map.Column("`FECHA_INI`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.FechaFin, map => { map.Column("`FECHA_FIN`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}
