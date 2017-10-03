SET search_path TO "0001";
-- MOLEQULE DETAIL SCHEMA SCRIPT

DROP TABLE IF EXISTS "Privilege" CASCADE;
CREATE TABLE "Privilege"
(
	"OID" bigserial NOT NULL,
	"OID_USER" bigint NOT NULL,
    "OID_ITEM" bigint NOT NULL,
    "READ" boolean NOT NULL,
    "CREATE" boolean DEFAULT false,
    "MODIFY" boolean DEFAULT false,
    "DELETE" boolean DEFAULT false,
	CONSTRAINT "PRIVILEGE_PK" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Privilege" OWNER TO moladmin;
GRANT ALL ON TABLE "Privilege" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "Setting" CASCADE;
CREATE TABLE "Setting"
(
	"OID" bigserial NOT NULL,
	"NAME" character varying(255) DEFAULT 0 NOT NULL,
    "VALUE" character varying NOT NULL,
	CONSTRAINT "Setting_PK" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Setting" OWNER TO moladmin;
GRANT ALL ON TABLE "Setting" TO GROUP "MOLEQULE_ADMINISTRATOR";

-- FOREIGN KEYS--------------------------------------------------------------------------------------------------------------------------------------

ALTER TABLE ONLY "Setting"
    ADD CONSTRAINT "Setting_NAME_key" UNIQUE ("NAME");

ALTER TABLE ONLY "Privilege"
    ADD CONSTRAINT "Privilege_OID_ITEM_fkey" FOREIGN KEY ("OID_ITEM") REFERENCES "COMMON"."SecureItem"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "Privilege"
    ADD CONSTRAINT "Privilege_OID_USER_fkey" FOREIGN KEY ("OID_USER") REFERENCES "COMMON"."User"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
