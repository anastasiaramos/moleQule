SET search_path TO "COMMON";

UPDATE "Variable" SET "VALUE" = '6.0.0.0' WHERE "NAME" = 'MOLEQULE_DB_VERSION';

INSERT INTO "User" ("NAME", "PASSWORD", "ADMOR", "MAIN", "IS_PARTNER", "ESTADO", "SERIAL", "ID") 
	(	SELECT 'moladmin'
			, 'fcabbe1bfb630c6e6a6c07d174275e45'
			, TRUE
			, TRUE
			, FALSE
			, 10
			, MAX("SERIAL") + 1
			, overlay('000000' placing cast(MAX("SERIAL") + 1 as varchar) from 7 - length(cast(MAX("SERIAL")+1 as varchar)))
		FROM "User");

INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") 
	(	SELECT U."OID", US."OID_SETTING", US."NAME", US."VALUE" 
		FROM "UserSetting" AS US
		INNER JOIN "User" AS U ON U."NAME" = 'moladmin'
		WHERE US."OID_USER" = 1);


INSERT INTO "SchemaUser" ("OID_USER", "OID_SCHEMA") 
	(	SELECT U."OID", E."OID" 
		FROM "User" AS U,"Empresa" as E
		WHERE U."NAME" = 'moladmin');


SET search_path TO "0001";

INSERT INTO "Privilege" ("OID_USER", "OID_ITEM", "READ", "CREATE", "MODIFY", "DELETE") 
	SELECT u."OID", i."OID", TRUE, TRUE, TRUE, TRUE 
	FROM "COMMON"."User" AS u, "COMMON"."SecureItem" AS i
	WHERE (u."OID", i."OID") NOT IN (SELECT "OID_USER", "OID_ITEM" FROM "Privilege") AND u."NAME" = 'moladmin';

