using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

using moleQule.Library.CslaEx;
using moleQule.Library;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class EntityBase
	{
		#region Business Methods

		public static bool CheckChangeState(EEstado estado, EEstado newEstado)
		{
			switch (estado)
			{
				case EEstado.Anulado:
					if (newEstado == EEstado.Contabilizado)
					{
						throw new iQException(Resources.Messages.NULL_CONTABILIZADO_NOT_ALLOWED);
					}
					break;
			}

			return true;
		}

		public static bool CheckEditLockedEstado(EEstado estado, EEstado lockedEstado)
		{
			if ((estado == lockedEstado) || (estado == EEstado.Anulado))
			{
				throw new iQException(string.Format(Resources.Messages.EDIT_NOT_ALLOWED_BY_ESTADO, EnumText<EEstado>.GetLabel(estado)));
			}

			return true;
		}

		public static bool CheckEditAllowedEstado(EEstado estado, EEstado allowedEstado)
		{
			if ((estado != allowedEstado) || (estado == EEstado.Anulado))
			{
				throw new iQException(string.Format(Resources.Messages.EDIT_NOT_ALLOWED_BY_ESTADO, EnumText<EEstado>.GetLabel(estado)));
			}

			return true;
		}

		public static bool UpdateByYear(IEntityBase oldItem, IEntityBase newItem, object parent)
		{
            if (oldItem.FechaReferencia.Year != newItem.FechaReferencia.Year)
            {
                newItem.DifferentYearChecks();
                IEntityBase entity = newItem.ICloneAsNew();
                entity.IEntityBaseSave(parent);
                newItem.EEstado = EEstado.Anulado;
                oldItem.EEstado = EEstado.Anulado;
                newItem.DifferentYearTasks(oldItem);
                return true;
            }
            else
            {
                oldItem.SameYearTasks(newItem);
                return false;
            }
		}

		#endregion

		#region SQL

		public static string SELECT_BUILDER(SelectCaller local_caller, Common.QueryConditions conditions)
		{
			string query = string.Empty;
			string union = " UNION ";
			string copy = string.Empty;

			foreach (KeyValuePair<ETipoEntidad, TEntidadRegistroBase> entity in ModuleController.Instance.ActiveEntidades)
			{			
				if (!entity.Value.Active) continue;

				copy = query;
				conditions.TipoEntidad = entity.Key;
				query += union + local_caller(conditions);

				if (query == (copy + union)) query = copy;
			}

			query = query.Substring(union.Length);

			return query;
		}

		public static string SELECT_BUILDER(SelectCaller local_caller, ETipoEntidad[] list, Common.QueryConditions conditions)
		{
			string query = string.Empty;
			string union = @" 
				UNION ";
			string copy = string.Empty;

			/*foreach (KeyValuePair<ETipoEntidad, TEntidadRegistroBase> entity in ModuleController.Instance.ActiveEntidades)
				if (entity.Value.Active)
				{
					foreach (ETipoEntidad item in list)
						if (item == entity.Value.ETipoEntidad)
						{
							copy = query;
							conditions.TipoEntidad = item;
							query += union + local_caller(conditions);

							if (query == (copy + union)) query = copy;
						}
				}*/

			TEntidadRegistroBase entity;

			foreach (ETipoEntidad item in list)
			{
				if (!ModuleController.Instance.ActiveEntidades.ContainsKey(item)) continue;
				
				entity = ModuleController.Instance.ActiveEntidades[item];
				
				if (!entity.Active) continue;

				copy = query;
				conditions.TipoEntidad = item;
				query += union + local_caller(conditions);

				if (query == (copy + union)) query = copy;
			}

			query = query.Substring(union.Length);

			return query;
		}

		public static string GET_FILTERS(FilterList filters, string tableAlias)
		{
			return FilterMng.GET_FILTERS_SQL(filters, tableAlias);
		}

		public static string GET_IN_STRING(List<long> oidList)
		{
            string IN_pattern = "({0})";

            if (oidList == null || oidList.Count == 0)
                return String.Format(IN_pattern, "0");
            else
                return String.Format(IN_pattern, String.Join(", ", oidList));
		}

        public static string GET_IN_STRING(List<EMedioPago> EList)
        {
            string list = "(";

            foreach (EMedioPago item in EList)
            {
                list += (long)item +",";
            }

            return list.Substring(0, list.Length - 1) + ")";
        }

		public static string GET_IN_LIST_CONDITION(string oidList, string tableAlias)
		{
			if (oidList == string.Empty) oidList = "0";

			return @"
				 AND " + tableAlias + @".""OID"" IN (" + oidList + ")";
		}
		public static string GET_IN_LIST_CONDITION(List<long> oidList, string tableAlias)
		{
			if (oidList == null) return string.Empty;

			string list = string.Empty;

			if (oidList.Count == 0)
				list = "(0)";
			else
				list = GET_IN_STRING(oidList);

			return @"
				AND " + tableAlias + @".""OID"" IN " + list;
		}
		public static string GET_IN_LIST_CONDITION(HashOidList oidList, string tableAlias)
		{
			if (oidList == null) return string.Empty;

			string list = string.Empty;

			if (oidList.List.Count == 0)
				list = "(0)";
			else
				list = GET_IN_STRING(oidList.ToList());

			return @"
				AND " + tableAlias + @".""OID"" IN " + list;
		}
        public static string GET_IN_LIST_CONDITION(List<EMedioPago> EList, string tableAlias, string tableField)
        {
            if (EList == null) return string.Empty;

            string list = string.Empty;

            if (EList.Count == 0)
                list = "(0)";
            else
                list = GET_IN_STRING(EList);

            return @"
				AND " + tableAlias + ".\"" + tableField + "\" IN " + list;
        }

		public static string GET_IN_PARTNERS_LIST_CONDITION(HashOidList oidList, string tableAlias)
		{
			if (oidList == null) return string.Empty;

			string list = string.Empty;

			if (oidList.List.Count == 0)
				list = "(0)";
			else
				list = GET_IN_STRING(oidList.ToList());

			return @" 
				AND " + tableAlias + @".""OID_PARTNER"" IN " + list;
		}
		public static string GET_IN_BRANCHES_LIST_CONDITION(HashOidList oidList, string tableAlias)
		{
			if (oidList == null) return string.Empty;

			string list = string.Empty;

			if (oidList.List.Count == 0)
				list = "(0)";
			else
				list = GET_IN_STRING(oidList.ToList());

			return @" 
				AND " + tableAlias + @".""OID_BRANCH"" IN " + list;
		}

		public static string LOCK(string tableAlias, bool lockTable)
		{
            lockTable = false;
			return (lockTable)
					? @" 
						FOR UPDATE OF " + tableAlias + " NOWAIT;"
					: ";";
		}
		public static string NO_NULL_RECORDS_CONDITION(string tableAlias)
		{
			bool nulls = SettingsMng.Instance.GetShowNullRecordsSetting();

			return (nulls) ? string.Empty : ESTADO_CONDITION(EEstado.NoAnulado, tableAlias);
		}

		public static string ESTADO_CONDITION(EEstado estado, string tableAlias)
		{
			string query = string.Empty;

			switch (estado)
			{
				case EEstado.Todos:
					break;

				case EEstado.NoAnulado:
					query += " AND " + tableAlias + ".\"ESTADO\" != " + (long)EEstado.Anulado;
					break;

				case EEstado.NoOculto:
					query += " AND " + tableAlias + ".\"ESTADO\" != " + (long)EEstado.Oculto;
					break;

				default:
					query += " AND " + tableAlias + ".\"ESTADO\" = " + (long)estado;
					break;
			}

			return @"" +
				query;
		}
		public static string STATUS_CONDITION(EEstado[] status, string tableAlias)
		{
			if (status == null) return string.Empty;
			if (status[1] == (long)EEstado.Todos) return string.Empty;

			string query = " AND " + tableAlias + ".\"ESTADO\" IN (";

            foreach (EEstadoItem item in status)
                query += (long)item + ",";

			query = query.Substring(0, query.Length - 2) + ")";

			return @"" +
				query;
		}
		public static string STATUS_LIST_CONDITION(EEstado[] status, string tableAlias, string columnName = "STATUS")
		{
            if (status == null) return string.Empty;
            if (status.Length == 0) return string.Empty;
            if (status[0] == EEstado.Todos) return string.Empty;

 			string query = @"
                AND " + tableAlias + @".""" + columnName + @""" IN (0";

			foreach (EEstado item in status)
				query += "," + (long)item;

			return @"" + query + ")";
		}

		#endregion
	}

	public class NotifyEntity
	{
        public long Oid { get; set; }
		public ETipoNotificacion ETipoNotificacion { get; set; }
		public ETipoEntidad ETipoEntidad { get; set; }
		public int Count { get; set; }
		public decimal Total { get; set; }
		public string Title { get; set; }
		public object List { get; set; }
		public bool Checked { get; set; }
		public object Tag { get; set; }
		public int Level { get; set; }
		public bool SetTotal { get; set; }

		public long TipoNotificacion { get { return (long)ETipoNotificacion; } }

		public string FullTitle { get { return SetTotal ? Title + " (" + Total.ToString("C2") + ")" : Title; } }

        public NotifyEntity()
            : this(ETipoNotificacion.Node) { }

		public NotifyEntity(ETipoNotificacion tipo)
			: this(tipo, ETipoEntidad.Todos, 0, 0) { }

		public NotifyEntity(ETipoNotificacion tipo, string title)
			: this(tipo, ETipoEntidad.Todos, 0, 0, title) { }

		public NotifyEntity(ETipoNotificacion tipo, string title, bool checkedItem)
			: this(tipo, ETipoEntidad.Todos, 0, 0, title, null, checkedItem) { }

		public NotifyEntity(ETipoNotificacion tipo, string title, bool checkedItem, bool setTotal)
			: this(tipo, ETipoEntidad.Todos, 0, 0, title, null, checkedItem, setTotal) { }

		public NotifyEntity(ETipoNotificacion tipo, ETipoEntidad tipoEntidad, int count, decimal total)
			: this(tipo, tipoEntidad, count, total, string.Empty) { }

		public NotifyEntity(ETipoNotificacion tipo, ETipoEntidad tipoEntidad, int count, decimal total, string title)
			: this(tipo, tipoEntidad, count, total, title, null) { }

		public NotifyEntity(ETipoNotificacion tipo, ETipoEntidad tipoEntidad, int count, decimal total, string title, object list)
			: this(tipo, tipoEntidad, count, total, title, list, true) { }

		public NotifyEntity(ETipoNotificacion tipo, ETipoEntidad tipoEntidad, string title)
			: this(tipo, tipoEntidad, 0, 0, title, null, true) { }

		public NotifyEntity(ETipoNotificacion tipo, ETipoEntidad tipoEntidad, int count, decimal total, string title, object list, bool checkedItem)
			: this(tipo, tipoEntidad, count, total, title, list, checkedItem, true) { }

		public NotifyEntity(ETipoNotificacion tipo, ETipoEntidad tipoEntidad, int count, decimal total, string title, object list, bool checkedItem, bool setTotal)
		{
			ETipoNotificacion = tipo;
			ETipoEntidad = tipoEntidad;
			Count = count;
			Total = total;
			Title = title;
			List = list;
			Checked = checkedItem;
			SetTotal = setTotal;
		}
	}

	public class NotifyEntityList : List<NotifyEntity> { }
}
