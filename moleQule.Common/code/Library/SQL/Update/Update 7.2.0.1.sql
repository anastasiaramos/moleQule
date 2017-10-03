/* UPDATE 7.2.0.1*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '7.2.0.1' WHERE "NAME" = 'COMMON_DB_VERSION';

ALTER TABLE "CMCompany"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"COMMON"."CMCompany_OID_seq"'::text)::regclass);
ALTER TABLE "CMCompanyContact"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"COMMON"."CMCompanyContact_OID_seq"'::text)::regclass);
ALTER TABLE "CMJob"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"COMMON"."CMJob_OID_seq"'::text)::regclass);
ALTER TABLE "CMLocality"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"COMMON"."CMLocality_OID_seq"'::text)::regclass);

SET SEARCH_PATH = "0001";