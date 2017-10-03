/* UPDATE 5.0.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '5.0.0.1' WHERE "NAME" = 'COMMON_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "CuentaBancaria" ADD COLUMN "FECHA_FIRMA" timestamp without time zone;
ALTER TABLE "CuentaBancaria" ADD COLUMN "DURACION_POLIZA" timestamp without time zone;