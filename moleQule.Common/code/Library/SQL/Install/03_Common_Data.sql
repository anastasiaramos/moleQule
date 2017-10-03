-- COMMON MODULE COMMON DATA SCRIPT

-- INSERTS

INSERT INTO "Variable" ("NAME", "VALUE") VALUES ('COMMON_DB_VERSION', '7.4.4.2');

INSERT INTO "CMCompany" ("OID", "SERIAL", "CODIGO" , "NOMBRE", "TIPO_ID") VALUES (1, 1, '01', 'Cambie este nombre', 0);

INSERT INTO "SchemaUser" ("OID_USER", "OID_SCHEMA") (SELECT U."OID", E."OID" FROM "User" AS U,"CMCompany" as E WHERE U."NAME" IN ('moladmin', 'molservice', 'Admin'));

INSERT INTO "Setting" ("NAME", "COPY") VALUES ('USE_ACTIVE_YEAR', TRUE);
INSERT INTO "Setting" ("NAME", "COPY") VALUES ('ACTIVE_YEAR', TRUE);

INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", 'TRUE' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'USE_ACTIVE_YEAR');
INSERT INTO "UserSetting" ("OID_USER", "OID_SETTING", "NAME", "VALUE") (SELECT U."OID", S."OID", S."NAME", '01/01/2013' FROM "User" AS U, "Setting" AS S WHERE S."NAME" = 'ACTIVE_YEAR');

INSERT INTO "COMMON"."CMJob" ("VALOR") VALUES ('');
INSERT INTO "COMMON"."CMJob" ("VALOR") VALUES ('Administrador');
INSERT INTO "COMMON"."CMJob" ("VALOR") VALUES ('Director');
INSERT INTO "COMMON"."CMJob" ("VALOR") VALUES ('Gerente');
INSERT INTO "COMMON"."CMJob" ("VALOR") VALUES ('Jefe de Compras');
INSERT INTO "COMMON"."CMJob" ("VALOR") VALUES ('Jefe de Obra');
INSERT INTO "COMMON"."CMJob" ("VALOR") VALUES ('Presidente');

INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('', '', '');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Agaete', 'Las Palmas', '35480');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Aguimes', 'Las Palmas', '35260');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Artenara', 'Las Palmas', '35350');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Arucas', 'Las Palmas', '35400');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Firgas', 'Las Palmas', '35430');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Galdar', 'Las Palmas', '35460');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Guia', 'Las Palmas', '35450');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Ingenio', 'Las Palmas', '35250');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('La Aldea de San Nicolas', 'Las Palmas', '35470');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Las Palmas de G.C.', 'Las Palmas', '');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Mogan', 'Las Palmas', '35140');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Moya', 'Las Palmas', '35420');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('San Bartolome de Tirajana', 'Las Palmas', '35290');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('San Mateo', 'Las Palmas', '35320');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Santa Brigida', 'Las Palmas', '35300');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Santa Lucia', 'Las Palmas', '35280');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Tejeda', 'Las Palmas', '35360');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Telde', 'Las Palmas', '');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Teror', 'Las Palmas', '35330');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Valsequillo', 'Las Palmas', '35217');
INSERT INTO "COMMON"."CMLocality" ("VALOR", "PROVINCIA", "COD_POSTAL") VALUES ('Valleseco', 'Las Palmas', '35340');

INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('01', 'Operaciones interiores sujetas a IVA', 1);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('02', 'Operaciones exentas sin derecho a deducci�n',	1);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('03', 'Entregas intracomunitarias', 1);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('04', 'Entregas intracomunitarias Ops. Triangulares', 1);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('05', 'Operaciones con Canarias, Ceuta y Melilla', 1);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('06', 'Exportaciones', 1);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('07', 'Otras operaciones no sujetas a IVA', 1);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('08', 'Otras operaciones no sujetas o inversi�n del sujeto pasivo con derecho a devoluci�n', 1);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('09', 'Otras operaciones exentas con derecho a deducci�n', 1);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('01', 'Operaciones interiores IVA deducible', 2);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('02', 'Compensaciones agrarias',	2);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('03', 'Adquisiciones intracomunitarias',	2);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('04', 'Inversi�n del Sujeto Pasivo',	2);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('06', 'Importaciones', 2);
INSERT INTO "CMInvoiceSubtype" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('07', 'IVA no deducible', 2);

-- HIPATIA ENTITIES INSERTS

INSERT INTO "COMMON"."HPEntityType" ("VALOR", "USER_CREATED", "COMMON_SCHEMA") VALUES ('Empresa', FALSE, TRUE);
INSERT INTO "COMMON"."HPEntityType" ("VALOR", "USER_CREATED", "COMMON_SCHEMA") VALUES ('CuentaBancaria', FALSE, FALSE);

INSERT INTO "COMMON"."HPEntity" ("TIPO", "OBSERVACIONES") VALUES ('Empresa', 'Empresa');
INSERT INTO "COMMON"."HPEntity" ("TIPO", "OBSERVACIONES") VALUES ('CuentaBancaria', 'Cuentas Bancarias');

-- SECURE ITEMS INSERTS

INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Tablas Auxiliares', '001', 'AUXILIARES');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Empresas', '002', 'EMPRESA');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Registro', '003', 'REGISTRO');
INSERT INTO "COMMON"."SecureItem" ("NAME", "TIPO", "DESCRIPTOR") VALUES ('Estado', '004', 'ESTADO');

-- ITEM MAP INSERTS

-- AUXILIARES	-> VARIABLE
--				-> EMPRESA

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'EMPRESA'	;
	
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'AUXILIARES' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'REGISTRO' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'REGISTRO' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'REGISTRO' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'REGISTRO' AND ASI."DESCRIPTOR" = 'VARIABLE'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '1', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'REGISTRO' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '2', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'REGISTRO' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '3', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'REGISTRO' AND ASI."DESCRIPTOR" = 'EMPRESA'	;

INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE")  
	SELECT SI."OID", '4', ASI."OID", '1' 
	FROM "COMMON"."SecureItem" AS SI
	INNER JOIN "COMMON"."SecureItem" AS ASI ON SI."DESCRIPTOR" = 'REGISTRO' AND ASI."DESCRIPTOR" = 'EMPRESA'	;