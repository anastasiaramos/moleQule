/* UPDATE 6.3.1.4*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.3.1.4' WHERE "NAME" = 'MOLEQULE_DB_VERSION';
	
SET SEARCH_PATH = "0001";

INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMTP_HOST', 'smtp.iqingenieros.com');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMTP_PORT', '25');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMTP_USER', 'iq_tech');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMTP_PWD', 'iQi1998');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMTP_EMAIL', 'tech@iqingenieros.com');
INSERT INTO "Setting" ("NAME", "VALUE") VALUES ('SMTP_ENABLE_SSL', FALSE);