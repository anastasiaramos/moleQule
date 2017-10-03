/* UPDATE 3.8.0.0*/

SET SEARCH_PATH = "COMMON";

SET SEARCH_PATH = "0001";

ALTER TABLE "Privilege" ADD COLUMN "CREATE" boolean DEFAULT false;
ALTER TABLE "Privilege" ADD COLUMN "MODIFY" boolean DEFAULT false;
ALTER TABLE "Privilege" ADD COLUMN "DELETE" boolean DEFAULT false;

UPDATE "Privilege" SET "CREATE" = true WHERE "WRITE" = true;
UPDATE "Privilege" SET "MODIFY" = true WHERE "WRITE" = true;
UPDATE "Privilege" SET "DELETE" = true WHERE "WRITE" = true;

ALTER TABLE "Privilege" DROP COLUMN "WRITE";


