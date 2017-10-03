/* UPDATE 6.2.0.0*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '6.2.0.0' WHERE "NAME" = 'COMMON_DB_VERSION';

CREATE TABLE "SubtipoFactura" (
    "OID" bigserial,
    "CODIGO" character varying(255),
    "DESCRIPCION" text,
    "TIPO" bigint,	
	CONSTRAINT "PK_SubtipoFactura" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "SubtipoFactura" OWNER TO moladmin;
GRANT ALL ON TABLE "SubtipoFactura" TO GROUP "MOLEQULE_ADMINISTRATOR";

INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('01', 'Operaciones interiores sujetas a IVA', 1);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('02', 'Operaciones exentas sin derecho a deducción',	1);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('03', 'Entregas intracomunitarias', 1);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('04', 'Entregas intracomunitarias Ops. Triangulares', 1);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('05', 'Operaciones con Canarias, Ceuta y Melilla', 1);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('06', 'Exportaciones', 1);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('07', 'Otras operaciones no sujetas a IVA', 1);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('08', 'Otras operaciones no sujetas o inversión del sujeto pasivo con derecho a devolución', 1);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('09', 'Otras operaciones exentas con derecho a deducción', 1);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('01', 'Operaciones interiores IVA deducible', 2);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('02', 'Compensaciones agrarias',	2);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('03', 'Adquisiciones intracomunitarias',	2);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('04', 'Inversión del Sujeto Pasivo',	2);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('06', 'Importaciones', 2);
INSERT INTO "SubtipoFactura" ("CODIGO", "DESCRIPCION", "TIPO") VALUES ('07', 'IVA no deducible', 2);

SET SEARCH_PATH = "0001";

ALTER TABLE "Impuesto" ADD COLUMN "OID_SUBTIPO_FACTURA_EMITIDA" bigint NOT NULL DEFAULT 1;
ALTER TABLE "Impuesto" ADD COLUMN "OID_SUBTIPO_FACTURA_RECIBIDA" bigint NOT NULL DEFAULT 10;




