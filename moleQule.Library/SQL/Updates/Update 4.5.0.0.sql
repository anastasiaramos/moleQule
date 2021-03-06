/* UPDATE 4.5.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '4.5.0.0' WHERE "NAME" = 'MOLEQULE_DB_VERSION';

INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'SMTP_ENABLE_SSL', 'FALSE' FROM "User" AS U);

INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_FACTURAS_RECIBIDAS', 'TRUE' FROM "User" AS U);
INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_FACTURAS_EMITIDAS', 'TRUE' FROM "User" AS U);
INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_COBROS', 'TRUE' FROM "User" AS U);
INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_PAGOS', 'TRUE' FROM "User" AS U);
INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_GASTOS', 'TRUE' FROM "User" AS U);

INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_PLAZO_FACTURAS_RECIBIDAS', '7' FROM "User" AS U);
INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_PLAZO_FACTURAS_EMITIDAS', '7' FROM "User" AS U);
INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_PLAZO_COBROS', '7' FROM "User" AS U);
INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_PLAZO_PAGOS', '7' FROM "User" AS U);
INSERT INTO "UserSetting" ("OID_USER", "NAME", "VALUE") (SELECT U."OID", 'NOTIFY_PLAZO_GASTOS', '7' FROM "User" AS U);

DELETE FROM "UserSetting" WHERE "NAME" = 'PLAZO_AVISO_VENCIMIENTO_PAGOS';

SET SEARCH_PATH = "0001";





