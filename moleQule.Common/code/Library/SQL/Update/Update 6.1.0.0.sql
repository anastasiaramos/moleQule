/* UPDATE 6.1.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.1.0.0' WHERE "NAME" = 'COMMON_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "CuentaBancaria" ADD COLUMN "DIAS_CREDITO" bigint;