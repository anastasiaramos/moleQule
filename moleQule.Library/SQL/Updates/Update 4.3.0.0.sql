/* UPDATE 4.3.0.0*/

SET SEARCH_PATH = "COMMON";

--ALTER TABLE "User" ADD COLUMN "MAIN" BOOLEAN DEFAULT FALSE;
--UPDATE "User" SET "MAIN" = TRUE WHERE "OID" = 1;

INSERT INTO "COMMON"."Variable" ("NAME", "VALUE") VALUES ('MOLEQULE_DB_VERSION', '4.3.0.0');

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "Setting" CASCADE;
CREATE TABLE "Setting"
(
	"OID" bigserial NOT NULL,
	"NAME" varchar(255) NOT NULL UNIQUE DEFAULT 0,
	"VALUE" varchar NOT NULL,
	CONSTRAINT "Setting_PK" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Setting" OWNER TO moladmin;
GRANT ALL ON TABLE "Setting" TO GROUP "MOLEQULE_ADMINISTRATOR";

INSERT INTO "Setting" ("NAME", "VALUE") (SELECT "NAME", "VALUE" FROM "COMMON"."Variable" WHERE "NAME" NOT LIKE '%DB_VERSION');
DELETE FROM "COMMON"."Variable" WHERE "NAME" NOT LIKE '%DB_VERSION';