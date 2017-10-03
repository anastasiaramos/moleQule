using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class MonitorMap : ClassMapping<MonitorRecord>
	{
		public MonitorMap()
		{
			Table("`CMMonitor`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMMonitor_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Status, map => { map.Column("`STATUS`"); map.NotNullable(false); });
			Property(x => x.ComponentType, map => { map.Column("`COMPONENT_TYPE`"); map.NotNullable(false); });
			Property(x => x.ComponentSerial, map => { map.Column("`COMPONENT_SERIAL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.ComponentName, map => { map.Column("`COMPONENT_NAME`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.ComponentIP, map => { map.Column("`COMPONENT_IP`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.ComponentInterval, map => { map.Column("`COMPONENT_INTERVAL`"); map.NotNullable(false); });
			Property(x => x.ComponentStatus, map => { map.Column("`COMPONENT_STATUS`"); map.NotNullable(false); });
			Property(x => x.ErrorType, map => { map.Column("`ERROR_TYPE`"); map.NotNullable(false); });
			Property(x => x.ErrorLevel, map => { map.Column("`ERROR_LEVEL`"); map.NotNullable(false); });
			Property(x => x.Description, map => { map.Column("`DESCRIPTION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.LastUpdate, map => { map.Column("`LAST_UPDATE`"); map.NotNullable(false);  });
			Property(x => x.ErrorCount, map => { map.Column("`ERROR_COUNT`"); map.NotNullable(false); });
			Property(x => x.WarningCount, map => { map.Column("`WARNING_COUNT`"); map.NotNullable(false); });
			Property(x => x.Notify, map => { map.Column("`NOTIFY`"); map.NotNullable(false); });
		}
	}
}
