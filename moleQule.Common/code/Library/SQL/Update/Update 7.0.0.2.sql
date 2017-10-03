/* UPDATE 7.0.0.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.0.0.2' WHERE "NAME" = 'COMMON_DB_VERSION';

ALTER TABLE "CARGO" RENAME TO "CMJob";
ALTER TABLE "ContactoEmpresa" RENAME TO "CMCompanyContact";
--ALTER TABLE "Country" RENAME TO "CMCountry";
ALTER TABLE "Empresa" RENAME TO "CMCompany";
ALTER TABLE "MUNICIPIO" RENAME TO "CMLocality";
ALTER TABLE "SubtipoFactura" RENAME TO "CMInvoiceSubtype";

ALTER TABLE "CARGO_OID_seq" RENAME TO "CMJob_OID_seq";
ALTER TABLE "ContactoEmpresa_OID_seq" RENAME TO "CMCompanyContact_OID_seq";
--ALTER TABLE "Country_OID_seq" RENAME TO "CMCountry_OID_seq";
ALTER TABLE "Empresa_OID_seq" RENAME TO "CMCompany_OID_seq";
ALTER TABLE "MUNICIPIO_OID_seq" RENAME TO "CMLocality_OID_seq";
ALTER TABLE "SubtipoFactura_OID_seq" RENAME TO "CMInvoiceSubtype_Ticket_OID_seq";

SET SEARCH_PATH = "0001";

ALTER TABLE "Ayuda" RENAME TO "CMGrant";
ALTER TABLE "AyudaPeriodo" RENAME TO "CMGrantPeriod";
ALTER TABLE "CuentaBancaria" RENAME TO "CMBankAccount";
ALTER TABLE "Impuesto" RENAME TO "CMTax";
ALTER TABLE "IRPF" RENAME TO "CMIrpf";
ALTER TABLE "LineaRegistro" RENAME TO "CMRegistryLine";
ALTER TABLE "Pesaje" RENAME TO "CMWeighing";
ALTER TABLE "Registro" RENAME TO "CMRegistry";
ALTER TABLE "TarjetaCredito" RENAME TO "CMCreditCard";
ALTER TABLE "TPV" RENAME TO "CMTpv";

ALTER TABLE "Ayuda_OID_seq" RENAME TO "CMGrant_OID_seq";
ALTER TABLE "AyudaPeriodo_OID_seq" RENAME TO "CMGrantPeriod_OID_seq";
ALTER TABLE "CuentaBancaria_OID_seq" RENAME TO "CMBankAccount_OID_seq";
ALTER TABLE "Impuesto_OID_seq" RENAME TO "CMTax_OID_seq";
ALTER TABLE "IRPF_OID_seq" RENAME TO "CMIrpf_OID_seq";
ALTER TABLE "LineaRegistro_OID_seq" RENAME TO "CMRegistryLine_OID_seq";
ALTER TABLE "Pesaje_OID_seq" RENAME TO "CMWeighing_OID_seq";
ALTER TABLE "Registro_OID_seq" RENAME TO "CMRegistry_OID_seq";
ALTER TABLE "TarjetaCredito_OID_seq" RENAME TO "CMCreditCard_OID_seq";
ALTER TABLE "TPV_OID_seq" RENAME TO "CMTpv_OID_seq";

ALTER TABLE "CMGrant"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMGrant_OID_seq"'::text)::regclass);
ALTER TABLE "CMGrantPeriod"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMGrantPeriod_OID_seq"'::text)::regclass);
ALTER TABLE "CMBankAccount"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMBankAccount_OID_seq"'::text)::regclass);
ALTER TABLE "CMTax"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMTax_OID_seq"'::text)::regclass);
ALTER TABLE "CMIrpf"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMIrpf_OID_seq"'::text)::regclass);
ALTER TABLE "CMRegistryLine"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMRegistryLine_OID_seq"'::text)::regclass);
ALTER TABLE "CMWeighing"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMWeighing_OID_seq"'::text)::regclass);
ALTER TABLE "CMRegistry"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMRegistry_OID_seq"'::text)::regclass);
ALTER TABLE "CMCreditCard"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMCreditCard_OID_seq"'::text)::regclass);
ALTER TABLE "CMTpv"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."CMTpv_OID_seq"'::text)::regclass);