/* UPDATE 6.2.0.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.2.0.2' WHERE "NAME" = 'MOLEQULE_DB_VERSION';
	
SET SEARCH_PATH = "0001";

INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('ADMIN_CONTACT', '');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('ADMIN_EMAIL', '');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('CLIENTS_CONTACT', '');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('CLIENTS_EMAIL', '');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SUPPORT_CONTACT', '');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SUPPORT_EMAIL', '');