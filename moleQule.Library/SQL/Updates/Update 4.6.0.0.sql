/* UPDATE 4.6.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '4.6.0.0' WHERE "NAME" = 'MOLEQULE_DB_VERSION';

ALTER TABLE "User" ADD COLUMN "DEFAULT_SCHEMA" bigint DEFAULT 1;

SET SEARCH_PATH = "0001";




