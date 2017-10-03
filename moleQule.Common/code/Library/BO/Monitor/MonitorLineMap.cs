using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class MonitorLineMap : ClassMapping<MonitorLineRecord>
	{	
		public MonitorLineMap()
		{
			Table("`CMMonitorLine`");
			Lazy(true);	
			
			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMMonitorLine_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidMonitor, map => { map.Column("`OID_MONITOR`"); map.NotNullable(false);  });
			Property(x => x.Date, map => { map.Column("`DATE`"); map.NotNullable(false); });
			Property(x => x.ComponentIP, map => { map.Column("`COMPONENT_IP`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.ComponentInterval, map => { map.Column("`COMPONENT_INTERVAL`"); map.NotNullable(false); });
			Property(x => x.ComponentStatus, map => { map.Column("`COMPONENT_STATUS`"); map.NotNullable(false); });
			Property(x => x.Status, map => { map.Column("`STATUS`"); map.NotNullable(false); });
			Property(x => x.ErrorLevel, map => { map.Column("`ERROR_LEVEL`"); map.NotNullable(false); });
			Property(x => x.Description, map => { map.Column("`DESCRIPTION`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}
