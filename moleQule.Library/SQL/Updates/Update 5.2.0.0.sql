/* UPDATE 5.2.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.2.0.0' WHERE "NAME" = 'MOLEQULE_DB_VERSION';

--ALTER TABLE "User" ADD COLUMN "IS_PARTNER" boolean DEFAULT FALSE;

UPDATE "User" SET "IS_PARTNER" = FALSE;

SET SEARCH_PATH = "0001";