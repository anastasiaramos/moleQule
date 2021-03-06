/* UPDATE 7.4.0.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.4.0.2' WHERE "NAME" = 'COMMON_DB_VERSION';

SET SEARCH_PATH = "0001";

DROP TABLE IF EXISTS "CMRelation" CASCADE;
CREATE TABLE "CMRelation"
(
  "OID" bigserial,
  "OID_PARENT" bigint DEFAULT 0,
  "PARENT_TYPE" bigint,
  "OID_CHILD" bigint,
  "CHILD_TYPE" bigint,
  CONSTRAINT "PK_CMRelation" PRIMARY KEY ("OID")
)WITHOUT OIDS;

ALTER TABLE "CMRelation" OWNER TO moladmin;
GRANT ALL ON TABLE "CMRelation" TO GROUP "MOLEQULE_ADMINISTRATOR";