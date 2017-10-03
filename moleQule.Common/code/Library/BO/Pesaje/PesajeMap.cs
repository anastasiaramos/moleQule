using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class WeighingMap : ClassMapping<WeighingRecord>
    {
        public WeighingMap()
        {
			Table("`CMWeighing`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMWeighing_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descripcion, map => { map.Column("`DESCRIPCION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Bruto, map => { map.Column("`BRUTO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Neto, map => { map.Column("`NETO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Tara, map => { map.Column("`TARA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}
