using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class JobMap : ClassMapping<JobRecord>
    {
        public JobMap()
        {
			Table("`CMJob`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMJob_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.Valor, map => { map.Column("`VALOR`"); map.Length(255); });
		}
    }
}
