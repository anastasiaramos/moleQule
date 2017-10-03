/* UPDATE 7.4.3.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.4.3.2' WHERE "NAME" = 'COMMON_DB_VERSION';

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

SET SEARCH_PATH = "0001";
