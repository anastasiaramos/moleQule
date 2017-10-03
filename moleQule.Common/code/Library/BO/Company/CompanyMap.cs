using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
    public class CompanyMap : ClassMapping<CompanyRecord>
    {
        public CompanyMap()
        {
            Table("`CMCompany`");
            Schema("\"COMMON\"");
            Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMCompany_OID_seq`" })); map.Column("`OID`"); });
            Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.NotNullable(true); map.Unique(true); });
            Property(x => x.Code, map => { map.Column("`CODIGO`"); map.Unique(true); map.NotNullable(true); map.Length(255); });
			Property(x => x.Status, map => { map.Column("`STATUS`"); map.NotNullable(true); });
            Property(x => x.Name, map => { map.Column("`NOMBRE`"); map.Length(255); });
            Property(x => x.VatNumber, map => { map.Column("`VAT_NUMBER`"); map.Length(255); });
            Property(x => x.TipoId, map => { map.Column("`TIPO_ID`"); });
            Property(x => x.CtaCotizacion, map => { map.Column("`CTA_COTIZACION`"); map.Length(255); });
            Property(x => x.Direccion, map => { map.Column("`DIRECCION`"); map.Length(255); });
            Property(x => x.Municipio, map => { map.Column("`MUNICIPIO`"); map.Length(255); });
            Property(x => x.CodPostal, map => { map.Column("`COD_POSTAL`"); map.Length(255); });
            Property(x => x.Provincia, map => { map.Column("`PROVINCIA`"); map.Length(255); });
			Property(x => x.CountryIso2, map => { map.Column("`COUNTRY`"); map.Length(255); });
			Property(x => x.CurrencyIso, map => { map.Column("`CURRENCY`"); map.Length(255); });
            Property(x => x.Telefonos, map => { map.Column("`TELEFONOS`"); map.Length(255); });
            Property(x => x.Fax, map => { map.Column("`FAX`"); map.Length(255); });
            Property(x => x.Url, map => { map.Column("`URL`"); map.Length(255); });
            Property(x => x.Email, map => { map.Column("`EMAIL`"); map.Length(255); });
            Property(x => x.Responsable, map => { map.Column("`RESPONSABLE`"); map.Length(255); });
            Property(x => x.Logo, map => { map.Column("`LOGO`"); map.Length(255); });
            Property(x => x.CuentaBancaria, map => { map.Column("`CUENTA_BANCARIA`"); map.Length(255); });
            Property(x => x.PIrpf, map => { map.Column("`P_IRPF`"); });
            Property(x => x.UseDefaultReports, map => { map.Column("`USE_DEFAULT_REPORTS`"); });
        }
    }
}
