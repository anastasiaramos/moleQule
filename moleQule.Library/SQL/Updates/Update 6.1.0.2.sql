/* UPDATE 6.1.0.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.1.0.2' WHERE "NAME" = 'MOLEQULE_DB_VERSION';

ALTER TABLE "User" ADD COLUMN "EMAIL" character varying(255);
ALTER TABLE "User" ADD COLUMN "CREATION_DATE" timestamp without time zone;
ALTER TABLE "User" ADD COLUMN "LAST_LOGIN_DATE" timestamp without time zone;
ALTER TABLE "User" ADD COLUMN "LAST_PASSWORD_DATE" timestamp without time zone;
ALTER TABLE "User" ADD COLUMN "LAST_LOCK_OUT_DATE" timestamp without time zone;
ALTER TABLE "User" ADD COLUMN "LAST_ACTIVITY_DATE" timestamp without time zone;
ALTER TABLE "User" ADD COLUMN "BIRTH_DATE" timestamp without time zone;
ALTER TABLE "User" ADD COLUMN "PASSWORD_QUESTION" character varying(255);
ALTER TABLE "User" ADD COLUMN "PASSWORD_RESPONSE" character varying(255);
	
SET SEARCH_PATH = "0001";
