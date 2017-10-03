using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class IRPFMap : ClassMapping<IRPFRecord>
    {
        public IRPFMap()
        {
            Table("`CMIrpf`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMIrpf_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
            Property(x => x.Porcentaje, map => { map.Column("`PORCENTAJE`"); map.NotNullable(false); map.Length(32768); });
            Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}
