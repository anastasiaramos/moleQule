SET search_path TO "COMMON";
-- MOLEQULE COMMON SCHEMA SCRIPT

DROP TABLE IF EXISTS "ItemMap" CASCADE;
CREATE TABLE "ItemMap"
(
	"OID" bigserial NOT NULL UNIQUE,
    "OID_ITEM" bigint NOT NULL,
    "PRIVILEGE" bigint NOT NULL,
    "OID_ASSOCIATE_ITEM" bigint NOT NULL,
    "ASSOCIATE_PRIVILEGE" bigint NOT NULL,
	CONSTRAINT "PK_ItemMap" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "ItemMap" OWNER TO moladmin;
GRANT ALL ON TABLE "ItemMap" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "SecureItem" CASCADE;
CREATE TABLE "SecureItem"
(
	"OID" bigserial NOT NULL UNIQUE,
	"NAME" character varying(255),
    "TIPO" character varying(255),
    "DESCRIPTOR" character varying(255),
	CONSTRAINT "PK_SecureItem" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "SecureItem" OWNER TO moladmin;
GRANT ALL ON TABLE "SecureItem" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "Setting" CASCADE;
CREATE TABLE "Setting"
(
	"OID" bigserial NOT NULL UNIQUE,
	"NAME" character varying(255) NOT NULL,
    "COPY" boolean DEFAULT true,
    "COMMENTS" character varying,
	CONSTRAINT "PK_Setting" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Setting" OWNER TO moladmin;
GRANT ALL ON TABLE "Setting" TO GROUP "MOLEQULE_ADMINISTRATOR";

/*DROP TABLE IF EXISTS "Schema" CASCADE;
CREATE TABLE "Schema"
(
	"OID" bigserial NOT NULL UNIQUE,
	"SERIAL" bigint DEFAULT 0 NOT NULL,
    "CODE" character varying(255) NOT NULL,
    "NAME" character varying(255),
	CONSTRAINT "PK_Schema" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Schema" OWNER TO moladmin;
GRANT ALL ON TABLE "Schema" TO GROUP "MOLEQULE_ADMINISTRATOR";*/

DROP TABLE IF EXISTS "SchemaUser" CASCADE;
CREATE TABLE "SchemaUser"
(
	"OID" bigserial NOT NULL UNIQUE,
	"OID_USER" bigint NOT NULL,
    "OID_SCHEMA" bigint NOT NULL,
	CONSTRAINT "PK_SchemaUser" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "SchemaUser" OWNER TO moladmin;
GRANT ALL ON TABLE "SchemaUser" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "Variable" CASCADE;
CREATE TABLE "Variable"
(
	"OID" bigserial NOT NULL,
	"NAME" character varying(255) DEFAULT 0 NOT NULL,
    "VALUE" character varying NOT NULL,
	CONSTRAINT "PK_Variable" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Variable" OWNER TO moladmin;
GRANT ALL ON TABLE "Variable" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "User" CASCADE;
CREATE TABLE "User"
(
	"OID" bigserial NOT NULL UNIQUE,
	"NAME" character varying(255) NOT NULL,
    "PASSWORD" character varying(255) DEFAULT ''::character varying NOT NULL,
    "ADMOR" boolean DEFAULT false NOT NULL,
    "MAIN" boolean DEFAULT false,
    "ESTADO" bigint DEFAULT 1,
    "IS_PARTNER" boolean DEFAULT false,
	"ENTITY_TYPE" int8 DEFAULT 0,
    "OID_ENTITY" bigint DEFAULT 0,
    "SERIAL" bigint DEFAULT 0,
    "ID" character varying(255) NOT NULL,
    "PIN" character varying(255) DEFAULT 0,
    "EMAIL" character varying(255),
    "CREATION_DATE" timestamp without time zone,
    "LAST_LOGIN_DATE" timestamp without time zone,
    "LAST_PASSWORD_DATE" timestamp without time zone,
    "LAST_LOCK_OUT_DATE" timestamp without time zone,
    "LAST_ACTIVITY_DATE" timestamp without time zone,
    "BIRTH_DATE" timestamp without time zone,
    "PASSWORD_QUESTION" character varying(255),
    "PASSWORD_RESPONSE" character varying(255),
    "IS_SERVICE" boolean DEFAULT false,	
	CONSTRAINT "PK_User" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "User" OWNER TO moladmin;
GRANT ALL ON TABLE "User" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "UserSetting" CASCADE;
CREATE TABLE "UserSetting"
(
	"OID" bigserial NOT NULL,
	"OID_USER" bigint NOT NULL,
    "NAME" character varying(255) DEFAULT 0 NOT NULL,
    "VALUE" character varying NOT NULL,
    "OID_SETTING" bigint DEFAULT 0,
	CONSTRAINT "PK_UserSetting" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "UserSetting" OWNER TO moladmin;
GRANT ALL ON TABLE "UserSetting" TO GROUP "MOLEQULE_ADMINISTRATOR";
-----------------------------------------------------------------------------------------------------------------------------------------------------
ALTER TABLE ONLY "User"
    ADD CONSTRAINT "User_NAME_key" UNIQUE ("NAME");

ALTER TABLE ONLY "Variable"
    ADD CONSTRAINT "Variable_NOMBRE_key" UNIQUE ("NAME");

ALTER TABLE ONLY "ItemMap"
    ADD CONSTRAINT "UQ_ItemMap" UNIQUE ("OID_ITEM", "PRIVILEGE", "OID_ASSOCIATE_ITEM", "ASSOCIATE_PRIVILEGE");

/*ALTER TABLE ONLY "Schema"
    ADD CONSTRAINT "Schema_CODE_key" UNIQUE ("CODE");

ALTER TABLE ONLY "Schema"
    ADD CONSTRAINT "Schema_SERIAL_key" UNIQUE ("SERIAL");*/

ALTER TABLE ONLY "SchemaUser"
    ADD CONSTRAINT "SchemaUser_OID_USER_fkey" FOREIGN KEY ("OID_USER") REFERENCES "User"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "UserSetting"
    ADD CONSTRAINT "UserSetting_OID_SETTING_fkey" FOREIGN KEY ("OID_SETTING") REFERENCES "Setting"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "UserSetting"
    ADD CONSTRAINT "UserSetting_OID_USER_fkey" FOREIGN KEY ("OID_USER") REFERENCES "User"("OID") ON UPDATE CASCADE ON DELETE CASCADE;
