/* UPDATE 5.3.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.3.0.0' WHERE "NAME" = 'MOLEQULE_DB_VERSION';

ALTER TABLE "User" ADD COLUMN "OID_CLIENT" int8 DEFAULT 0;
ALTER TABLE "User" ADD COLUMN "OID_PARTNER" int8 DEFAULT 0;
ALTER TABLE "User" ADD COLUMN "SERIAL" int8 DEFAULT 0;
ALTER TABLE "User" ADD COLUMN "ID" character varying(255);

UPDATE "User" SET "OID_CLIENT" = 0;
UPDATE "User" SET "OID_PARTNER" = 0;

UPDATE "User" SET "SERIAL" = C."NROW", "ID" = trim(to_char(C."NROW", '000000'))
FROM (SELECT "OID", ROW_NUMBER() OVER (ORDER BY "OID") AS "NROW"
     FROM "User" 
     ORDER BY "OID") AS C
WHERE "User"."OID" = C."OID";

ALTER TABLE "User" ALTER COLUMN "ID" SET NOT NULL;

SET SEARCH_PATH = "0001";
