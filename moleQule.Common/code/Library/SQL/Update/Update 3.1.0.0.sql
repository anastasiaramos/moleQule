/* UPDATE 3.1.0.0*/

SET SEARCH_PATH = "COMMON";

ALTER TABLE "MUNICIPIO" ADD COLUMN "PAIS" varchar(255);

UPDATE "MUNICIPIO" SET "PAIS" = 'ESPA�A'; 

SET SEARCH_PATH = "0001";