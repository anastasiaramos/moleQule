/* UPDATE 6.3.3.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.3.3.2' WHERE "NAME" = 'MOLEQULE_DB_VERSION';
	
SET SEARCH_PATH = "0001";

INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMS_GATEWAY_NAME', 'Esendex');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMS_GATEWAY_CODE', '1');	
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMS_GATEWAY_USER', 'soporte@iqingenieros.com');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMS_GATEWAY_PWD', 'XAvHA7Tdf433');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMS_GATEWAY_ACCOUNT', 'EX0111208');
