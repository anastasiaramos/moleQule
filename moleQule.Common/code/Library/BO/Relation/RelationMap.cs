using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class RelationMap : ClassMapping<RelationRecord>
	{	
		public RelationMap()
		{
			Table("`CMRelation`");
			Lazy(true);	
			
			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMRelation_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidParent, map => { map.Column("`OID_PARENT`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.ParentType, map => { map.Column("`PARENT_TYPE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidChild, map => { map.Column("`OID_CHILD`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.ChildType, map => { map.Column("`CHILD_TYPE`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}
