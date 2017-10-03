using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class BankAccountMap : ClassMapping<BankAccountRecord>
	{
		public BankAccountMap()
		{
			Table("`CMBankAccount`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`CMBankAccount_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Valor, map => { map.Column("`VALOR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Swift, map => { map.Column("`SWIFT`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CuentaContable, map => { map.Column("`CUENTA_CONTABLE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CuentaContableGastos, map => { map.Column("`CUENTA_CONTABLE_GASTOS`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.SaldoInicial, map => { map.Column("`SALDO_INICIAL`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Estado, map => { map.Column("`ESTADO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidAsociada, map => { map.Column("`OID_ASOCIADA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Entidad, map => { map.Column("`ENTIDAD`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.FechaFirma, map => { map.Column("`FECHA_FIRMA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.DuracionPoliza, map => { map.Column("`DURACION_POLIZA`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Comision, map => { map.Column("`COMISION`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.TipoInteres, map => { map.Column("`TIPO_INTERES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.PagoGastosInicio, map => { map.Column("`PAGO_GASTOS_INICIO`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.DiasCredito, map => { map.Column("`DIAS_CREDITO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

