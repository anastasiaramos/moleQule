using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{	
    [Serializable()]
    public class RegistryMap : ClassMapping<RegistryRecord>
    {
        public RegistryMap()
        {
			Table("`CMRegistry`");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMRegistry_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidUsuario, map => { map.Column("`OID_USUARIO`"); map.Length(32768); });
			Property(x => x.TipoRegistro, map => { map.Column("`TIPO_REGISTRO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoExportacion, map => { map.Column("`TIPO_EXPORTACION`"); map.NotNullable(false); map.Length(32768); });
        }
    }
}
