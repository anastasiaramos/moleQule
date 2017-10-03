using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class RegistryLineMap : ClassMapping<RegistryLineRecord>
    {
        public RegistryLineMap()
        {
			Table("`CMRegistryLine`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMRegistryLine_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidRegistro, map => { map.Column("`OID_REGISTRO`"); map.Length(32768); });
			Property(x => x.OidEntidad, map => { map.Column("`OID_ENTIDAD`"); map.Length(32768); });
			Property(x => x.TipoEntidad, map => { map.Column("`TIPO_ENTIDAD`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Descripcion, map => { map.Column("`DESCRIPCION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.IdExportacion, map => { map.Column("`ID_EXPORTACION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}
