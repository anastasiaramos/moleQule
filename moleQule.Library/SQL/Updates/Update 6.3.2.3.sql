/* UPDATE 6.3.2.3*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.3.2.3' WHERE "NAME" = 'MOLEQULE_DB_VERSION';

--CREATE USER "molservice"
--	ENCRYPTED PASSWORD 'md556cbe7ab63d0e1c056f7a153e5811b7b'
	--IN ROLE "MOLEQULE_ADMINISTRATOR";
	
ALTER TABLE "User" ADD COLUMN "IS_SERVICE" boolean DEFAULT false;

INSERT INTO "User" ("SERIAL", "ID", "NAME", "PASSWORD", "ADMOR", "MAIN", "IS_SERVICE", "ESTADO", "PIN") VALUES (2, '000002', 'molservice', 'fcabbe1bfb630c6e6a6c07d174275e45', FALSE, FALSE, TRUE, 10, '0000');

INSERT INTO "SchemaUser" ("OID_USER", "OID_SCHEMA") 
	(	SELECT U."OID", E."OID" 
		FROM "User" AS U,"CMCompany" as E
		WHERE U."NAME" = 'molservice');

INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") 
	(	SELECT U."OID", US."OID_SETTING", US."NAME", US."VALUE" 
		FROM "UserSetting" AS US
		INNER JOIN "User" AS U ON U."NAME" = 'molservice'
		WHERE US."OID_USER" = 1);
	
SET SEARCH_PATH = "0001";

INSERT INTO "Privilege" ("OID_USER", "OID_ITEM", "READ", "CREATE", "MODIFY", "DELETE") 
	SELECT u."OID", i."OID", FALSE, FALSE, FALSE, FALSE 
	FROM "COMMON"."User" AS u, "COMMON"."SecureItem" AS i
	WHERE (u."OID", i."OID") NOT IN (SELECT "OID_USER", "OID_ITEM" FROM "Privilege");