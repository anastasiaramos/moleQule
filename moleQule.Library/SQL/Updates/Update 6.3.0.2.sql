/* UPDATE 6.3.0.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.3.0.2' WHERE "NAME" = 'MOLEQULE_DB_VERSION';
	
SET SEARCH_PATH = "0001";

INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('ADMIN_MOBILE_PHONE', '');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('CLIENTS_MOBILE_PHONE', '');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SUPPORT_MOBILE_PHONE', '');