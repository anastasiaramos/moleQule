/* UPDATE 5.0.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.0.0.0' WHERE "NAME" = 'COMMON_DB_VERSION';

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "Pesaje" CASCADE;
CREATE TABLE "Pesaje" 
( 
	"OID" bigserial NOT NULL,
	"SERIAL" int8,
	"CODIGO" varchar(255),
	"ESTADO" int8 DEFAULT 1,
	"FECHA" timestamp without time zone,
	"DESCRIPCION" text,
	"BRUTO" numeric(10,2),
	"NETO" numeric(10,2),
	"TARA" numeric(10,2),
	"OBSERVACIONES" text,	
	CONSTRAINT "PK_Pesaje" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Pesaje" OWNER TO moladmin;
GRANT ALL ON TABLE "Pesaje" TO GROUP "MOLEQULE_ADMINISTRATOR";