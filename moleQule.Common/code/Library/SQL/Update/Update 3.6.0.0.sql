/* UPDATE 3.6.0.0*/

SET SEARCH_PATH = "COMMON";

INSERT INTO "COMMON"."SecureItem" ("NAME") VALUES ('Registros');

SET SEARCH_PATH = "0001";

CREATE TABLE "Registro" 
( 
	"OID" bigserial NOT NULL,
	"TIPO_REGISTRO" int8,
	"SERIAL" int8 DEFAULT 0 NOT NULL,
	"CODIGO" varchar(255),
	"ESTADO" int8 DEFAULT 1,
	"NOMBRE" varchar(255),
	"FECHA" timestamp without time zone,
	"OBSERVACIONES" text,
	CONSTRAINT "PK_Registro" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "Registro" OWNER TO moladmin;
GRANT ALL ON TABLE "Registro" TO moladmin;
GRANT ALL ON TABLE "Registro" TO GROUP "MOLEQULE_ADMINISTRATOR";

CREATE TABLE "LineaRegistro" 
( 
	"OID" bigserial NOT NULL,
	"OID_REGISTRO" bigint NOT NULL,
	"OID_ENTIDAD" bigint NOT NULL,
	"TIPO_ENTIDAD" int8,
	"SERIAL" int8 DEFAULT 0 NOT NULL,
	"CODIGO" varchar(255),
	"ESTADO" int8 DEFAULT 1,
	"FECHA" timestamp without time zone,
	"DESCRIPCION" text,
	"OBSERVACIONES" text,
	CONSTRAINT "PK_LineaRegistro" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "LineaRegistro" OWNER TO moladmin;
GRANT ALL ON TABLE "LineaRegistro" TO moladmin;
GRANT ALL ON TABLE "LineaRegistro" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE "LineaRegistro" ADD CONSTRAINT "FK_LineaRegistro_Registro" FOREIGN KEY ("OID_REGISTRO") REFERENCES "Registro" ("OID") ON UPDATE CASCADE ON DELETE CASCADE;


