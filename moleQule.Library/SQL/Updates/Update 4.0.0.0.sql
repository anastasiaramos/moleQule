/* UPDATE 4.0.0.0*/

SET SEARCH_PATH = "COMMON";

ALTER TABLE "COMMON"."SecureItem" ADD COLUMN "TIPO" varchar(255);

UPDATE "COMMON"."SecureItem" SET "TIPO" = 'GENERICOS' WHERE "NAME" = 'Genericos';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'AUXILIARES' WHERE "NAME" = 'Tablas Auxiliares';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'ALUMNO' WHERE "NAME" = 'Alumnos';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'CURSO' WHERE "NAME" = 'Cursos';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'CURSO_FORMACION' WHERE "NAME" = 'Cursos de Formación';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'EXAMEN' WHERE "NAME" = 'Exámenes';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'HORARIO' WHERE "NAME" = 'Horarios';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'INSTRUCTOR' WHERE "NAME" = 'Instructores';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'MATERIAL_DOCENTE' WHERE "NAME" = 'Material Docente';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'MODULO' WHERE "NAME" = 'Módulos';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'PARTE_ASISTENCIA' WHERE "NAME" = 'Partes de Asistencia';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'PLAN_ESTUDIOS' WHERE "NAME" = 'Planes de Estudios';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'PLAN_EXTRA' WHERE "NAME" = 'Planes Extra';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'PROMOCION' WHERE "NAME" = 'Promociones';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'PROVEEDOR' WHERE "NAME" = 'Proveedores';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'PLAN_ANUAL' WHERE "NAME" = 'Planes Anuales';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'EMPLEADO' WHERE "NAME" = 'Empleados';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'DISCREPANCIA' WHERE "NAME" = 'Discrepancias';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'CLASE_AUDITORIA' WHERE "NAME" = 'Clases de Auditorías';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'AUDITORIA' WHERE "NAME" = 'Auditorías';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'AREA' WHERE "NAME" = 'Areas';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'AMPLIACION' WHERE "NAME" = 'Ampliaciones';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'ACTA_COMITE' WHERE "NAME" = 'Actas del Comité de Calidad';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'ACCION_CORRECTORA' WHERE "NAME" = 'Acciones Correctoras';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'CLIENTE' WHERE "NAME" = 'Clientes';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'FACTURA' WHERE "NAME" = 'Facturas';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'INCIDENCIA' WHERE "NAME" = 'Incidencias';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'ALMACEN' WHERE "NAME" = 'Almacenes';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'DOCUMENTO' WHERE "NAME" = 'Documento';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'EXPEDIENTE' WHERE "NAME" = 'Expedientes';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'PRODUCTO' WHERE "NAME" = 'Productos';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'TRANSPORTISTA' WHERE "NAME" = 'Transportistas';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'ALBARAN' WHERE "NAME" = 'Albaranes';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'CAJA' WHERE "NAME" = 'Cajas';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'PROFORMA' WHERE "NAME" = 'Proformas';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'FACTURA_PROVEEDOR' WHERE "NAME" = 'Facturas Proveedores';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'MOVIMIENTO_BANCO' WHERE "NAME" = 'Movimientos a Bancos';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'REGISTRO' WHERE "NAME" = 'Registro';
UPDATE "COMMON"."SecureItem" SET "TIPO" = 'NOMINA' WHERE "NAME" = 'Nominas';