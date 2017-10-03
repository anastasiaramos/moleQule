/* UPDATE 6.4.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.4.0.1' WHERE "NAME" = 'COMMON_DB_VERSION';

SET SEARCH_PATH = "0001";

CREATE TABLE "IRPF" 
( 
	"OID" bigserial NOT NULL,
	"NOMBRE" character varying(255),
    "PORCENTAJE" numeric(10,2) DEFAULT 0,
    "OBSERVACIONES" text,
	CONSTRAINT "PK_IRPF" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "IRPF" OWNER TO moladmin;
GRANT ALL ON TABLE "IRPF" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "CMMonitor" CASCADE;
CREATE TABLE "CMMonitor"
(
  "OID" bigserial,
  "STATUS" bigint DEFAULT 0,
  "COMPONENT_TYPE" character varying(255),
  "COMPONENT_SERIAL" text,
  "COMPONENT_NAME" text,
  "COMPONENT_IP" text,
  "COMPONENT_INTERVAL" text,
  "COMPONENT_STATUS" bigint DEFAULT 0,
  "ERROR_TYPE" bigint DEFAULT 0,
  "ERROR_LEVEL" bigint DEFAULT 0,
  "DESCRIPTION" bigint DEFAULT 0,
  "LAST_UPDATE" timestamp without time zone,
  CONSTRAINT "PK_CMMonitor" PRIMARY KEY ("OID")
)WITHOUT OIDS;

ALTER TABLE "CMMonitor" OWNER TO moladmin;
GRANT ALL ON TABLE "CMMonitor" TO GROUP "MOLEQULE_ADMINISTRATOR";




