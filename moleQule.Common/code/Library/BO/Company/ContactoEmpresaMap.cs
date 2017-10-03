using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
    public class CompanyContactMap : ClassMapping<CompanyContactRecord>
    {
        public CompanyContactMap()
        {
			Table("`CMCompanyContact`");
			Schema("\"COMMON\"");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMCompanyContact_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidEmpresa, map => { map.Column("`OID_EMPRESA`"); map.Length(32768); });
			Property(x => x.Cargo, map => { map.Column("`CARGO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Dni, map => { map.Column("`DNI`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Direccion, map => { map.Column("`DIRECCION`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.CodPostal, map => { map.Column("`COD_POSTAL`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Municipio, map => { map.Column("`MUNICIPIO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Provincia, map => { map.Column("`PROVINCIA`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Telefonos, map => { map.Column("`TELEFONOS`"); map.NotNullable(false); map.Length(255); });
        }
    }
}

