/* UPDATE 5.5.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.5.0.0' WHERE "NAME" = 'MOLEQULE_DB_VERSION';

ALTER TABLE "User" ADD COLUMN "PIN" varchar(255) DEFAULT 0;

SET SEARCH_PATH = "0001";