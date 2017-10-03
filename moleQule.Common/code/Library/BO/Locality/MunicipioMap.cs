using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class LocalityMap : ClassMapping<LocalityRecord>
    {
        public LocalityMap()
        {
			Table("`CMLocality`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMLocality_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Valor, map => { map.Column("`VALOR`"); map.Length(255); });
			Property(x => x.Provincia, map => { map.Column("`PROVINCIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CodPostal, map => { map.Column("`COD_POSTAL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Localidad, map => { map.Column("`LOCALIDAD`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Pais, map => { map.Column("`PAIS`"); map.NotNullable(false); map.Length(255); });
        }
    }
}
