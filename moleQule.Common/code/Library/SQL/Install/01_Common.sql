SET search_path TO "COMMON";
-- COMMON MODULE COMMON SCHEMA SCRIPT

DROP TABLE IF EXISTS "CMCompany" CASCADE;
CREATE TABLE "CMCompany"
(
	"OID" bigserial NOT NULL UNIQUE,
	"SERIAL" bigint NOT NULL UNIQUE,
    "CODIGO" character varying(255) NOT NULL UNIQUE,
	"STATUS" bigint NOT NULL DEFAULT 10,
    "NOMBRE" character varying(255),
    "VAT_NUMBER" character varying(255),
    "TIPO_ID" bigint NOT NULL,
    "CTA_COTIZACION" character varying(255),
    "DIRECCION" character varying(255),
    "MUNICIPIO" character varying(255),
    "COD_POSTAL" character varying(255),
    "PROVINCIA" character varying(255),
    "TELEFONOS" character varying(255),
    "FAX" character varying(255),
    "URL" character varying(255),
    "EMAIL" character varying(255),
    "RESPONSABLE" character varying(255),
    "LOGO" character varying(255),
    "CUENTA_BANCARIA" character varying(255),
    "P_IRPF" numeric(10,2),
	"COUNTRY" varchar(255) DEFAULT 'ES',
	"CURRENCY" varchar(255) DEFAULT '€',
	CONSTRAINT "PK_CMCompany" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "CMCompany" OWNER TO moladmin;
GRANT ALL ON TABLE "CMCompany" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "CMCompanyContact" CASCADE;
CREATE TABLE "CMCompanyContact"
(
	"OID" bigserial NOT NULL UNIQUE,
	"OID_EMPRESA" bigint NOT NULL,
    "CARGO" character varying(255),
    "NOMBRE" character varying(255),
    "DNI" character varying(255),
    "DIRECCION" character varying(255),
    "COD_POSTAL" character varying(255),
    "MUNICIPIO" character varying(255),
    "PROVINCIA" character varying(255),
    "TELEFONOS" character varying(255),
	"USE_DEFAULT_REPORTS" boolean DEFAULT FALSE,
	CONSTRAINT "PK_CMCompanyContact" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "CMCompanyContact" OWNER TO moladmin;
GRANT ALL ON TABLE "CMCompanyContact" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "CMCountry" CASCADE;
CREATE TABLE "CMCountry" 
( 
	"OID" bigserial NOT NULL,
	"LOCALE" character varying(255) UNIQUE,
    CONSTRAINT "PK_CMCountry" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "CMCountry" OWNER TO moladmin;
GRANT ALL ON TABLE "CMCountry" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "CMCurrencyExchange" CASCADE;
CREATE TABLE "CMCurrencyExchange" 
( 
	"OID" bigserial NOT NULL,
	"FROM_CURRENCY" character varying(255),
	"TO_CURRENCY" character varying(255),
    "RELATION" numeric(10,5),
	"COMMENTS" text,
	CONSTRAINT "PK_CMCurrencyExchange" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "CMCurrencyExchange" OWNER TO moladmin;
GRANT ALL ON TABLE "CMCurrencyExchange" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "CMInvoiceSubtype" CASCADE;
CREATE TABLE "CMInvoiceSubtype" 
(
    "OID" bigserial,
    "CODIGO" character varying(255),
    "DESCRIPCION" text,
    "TIPO" bigint,	
	CONSTRAINT "PK_CMInvoiceSubtype" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "CMInvoiceSubtype" OWNER TO moladmin;
GRANT ALL ON TABLE "CMInvoiceSubtype" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "CMJob" CASCADE;
CREATE TABLE "CMJob"
(
  "OID" bigserial NOT NULL,
  "VALOR" character varying(255) NOT NULL UNIQUE,
  CONSTRAINT "PK_CMJob" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "CMJob" OWNER TO moladmin;
GRANT ALL ON TABLE "CMJob" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "CMLocality" CASCADE;
CREATE TABLE "CMLocality"
(
	"OID" bigserial NOT NULL,
	"VALOR" character varying(255) NOT NULL,
    "PROVINCIA" character varying(255),
    "COD_POSTAL" character varying(255),
    "LOCALIDAD" character varying(255),
    "PAIS" character varying(255),
	CONSTRAINT "PK_CMLocality" PRIMARY KEY ("OID")
)WITHOUT OIDS;

ALTER TABLE "CMLocality" OWNER TO moladmin;
GRANT ALL ON TABLE "CMLocality" TO GROUP "MOLEQULE_ADMINISTRATOR";

-- FOREIGN KEYS

ALTER TABLE ONLY "SchemaUser"
    ADD CONSTRAINT "FK_SchemaUser_OID_SCHEMA" FOREIGN KEY ("OID_SCHEMA") REFERENCES "CMCompany"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "CMCompanyContact"
    ADD CONSTRAINT "FK_CMCompanyContact_CARGO" FOREIGN KEY ("CARGO") REFERENCES "CMJob"("VALOR") ON UPDATE CASCADE ON DELETE RESTRICT;

ALTER TABLE ONLY "CMCompanyContact"
    ADD CONSTRAINT "FK_CMCompanyContact_OID_EMPRESA" FOREIGN KEY ("OID_EMPRESA") REFERENCES "CMCompany"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
