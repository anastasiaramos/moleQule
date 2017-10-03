SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.4.4.2' WHERE "NAME" = 'COMMON_DB_VERSION';

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "CMCreditCardStatement" CASCADE;
CREATE TABLE "CMCreditCardStatement" 
( 
	"OID" bigserial NOT NULL,
	"OID_CREDIT_CARD" bigint NOT NULL,
	"FROM" timestamp without time zone,
	"TILL" timestamp without time zone,
	"DUE_DATE" timestamp without time zone,
	"AMOUNT" numeric(10,2),
	"COMMENTS" text,
	CONSTRAINT "PK_CMCreditCardStatement" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "CMCreditCardStatement" OWNER TO moladmin;
GRANT ALL ON TABLE "CMCreditCardStatement" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE ONLY "CMCreditCardStatement"
    ADD CONSTRAINT "FK_CMCreditCardStatement_CMCreditCard" FOREIGN KEY ("OID_CREDIT_CARD") REFERENCES "CMCreditCard"("OID") ON UPDATE CASCADE ON DELETE RESTRICT;

