using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Globalization;

using NHibernate.Mapping;
using moleQule.Library;

namespace moleQule.Library.CslaEx
{
    public enum IFilterType
    {
        None = 0,
        Filter = 1,
        Search = 2,
        FilterBack = 3
    }

    public enum IFilterProperty
    {
        All = 0,
        ByParamenter = 1,
        ByList = 2
    }

    [Serializable()]
    public class FilterItem
    {
        public bool Active = true;
        public IFilterProperty FilterProperty = IFilterProperty.ByParamenter;
        public CslaEx.Operation Operation;
        public string Column;
        public string ColumnTitle;
        public Type EntityType { get; set; }
		public Type Type { get; set; }
        public string Property;
        public Column TableColumn { get; set; }
        public object Value;        

        public string Text { get { return ColumnTitle.ToUpper() + " " + CslaEx.EnumText.GetString(Operation) + " '" + ValueToString + "'; "; } }
        public string ValueToString
        {
            get
            {
                if (Value != null)
                {
                    return ((Value is DateTime)) ? ((DateTime)Value).ToShortDateString() : Value.ToString();
                }
                else
                    return string.Empty;
            }
        }
		public object ValueToSQL
		{
			get
			{
				if (Value != null)
				{
					if ((Value is DateTime))
						return QueryConditions.GetFechaLabel(Convert.ToDateTime(Value));
					else
						return Value.ToString();
				}
				else
					return Value.ToString();
			}
		}
    }

    [Serializable()]
    public class FilterList : List<FilterItem> 
    {
        public void NewFilter(object value,
                                string propertyName,
                                IFilterProperty filterProperty,
                                Operation operation,
                                Type entityType, String propLabel)
        { 
            this.Add(FilterMng.BuildFilterItem(value,
                                                  propertyName,
                                                  filterProperty,
                                                  operation,
                                                  entityType, propLabel));
        }
    }

    [Serializable()]
    public class FilterGraphItem
    {
        #region Attributes

        private DateTime _dateRangeFrom = DateTime.Now.AddMonths(-1);
        private DateTime _dateRangeTo = DateTime.Now;
        private EStepGraph _step = EStepGraph.Day;

		#endregion

		#region Properties

        public DateTime DateRangeFrom { get { return _dateRangeFrom; } set { _dateRangeFrom = value; } }
        public DateTime DateRangeTo { get { return _dateRangeTo; } set { _dateRangeTo = value; } }
        public EStepGraph Step { get { return _step; } set { _step = value; } }

        #endregion
    }

    [Serializable()]
    public class FilterMng
    {
        public static FilterItem BuildFilterItem(object value, 
                                                    string propertyName, 
                                                    Type propertytype, 
                                                    IFilterProperty filterProperty, 
                                                    Operation operation)
        {
            FilterItem fItem = new FilterItem();
            fItem.Column = propertyName;
            fItem.Property = propertyName;
            fItem.FilterProperty = filterProperty;
            fItem.Operation = operation;

            if (propertytype == typeof(DateTime))
            {
                if (fItem.Operation == Operation.Contains) fItem.Operation = Operation.Equal;

                switch (fItem.Operation)
                {
                    case Operation.Equal:
                    case Operation.Distinct:
                        fItem.Value = ((DateTime)value).ToShortDateString() + " 0:00:00";
                        break;

                    case Operation.Greater:
                    case Operation.LessOrEqual:
                        fItem.Value = ((DateTime)value).ToShortDateString() + " 23:59:59";
                        break;

                    case Operation.Less:
                    case Operation.GreaterOrEqual:
                        fItem.Value = ((DateTime)value).ToShortDateString() + " 0:00:00";
                        break;
                }
            }
            else
                fItem.Value = value;

            return fItem;
        }

        public static FilterItem BuildFilterItem(object value,
                                                  string propertyName,
                                                  IFilterProperty filterProperty,
                                                  Operation operation,
                                                  Type entityType, String propLabel)
        {
            FilterItem fItem = new FilterItem();
            fItem.Column = propertyName;
            fItem.Property = propertyName;
            fItem.FilterProperty = filterProperty;
            fItem.Operation = operation;
            fItem.EntityType = entityType;

			Type entityRecordType = AppControllerBase.AppControler.RecordEntities[fItem.EntityType];

            try
            {
                fItem.TableColumn = (filterProperty == IFilterProperty.ByParamenter) ? nHManager.Instance.GetTableColumn(entityRecordType, propertyName) : null;
            }
            catch { /* no existe la propiedad en esta tabla por lo que es de un join */ }
            fItem.ColumnTitle = propLabel;

            if ((filterProperty == IFilterProperty.ByParamenter) && (fItem.TableColumn != null))
            {
                if (fItem.TableColumn.Value.Type is NHibernate.Type.DateTimeType)
                {
                    if (fItem.Operation == Operation.Contains) fItem.Operation = Operation.Equal;

                    switch (fItem.Operation)
                    {
                        case Operation.Equal:
                        case Operation.Distinct:
                            fItem.Value = (value is DateTime) 
											? DateTime.Parse(((DateTime)value).ToShortDateString() + " 0:00:00")
											: DateTime.Parse(DateTime.Parse((string)value).ToShortDateString() + " 0:00:00");
                            break;

                        case Operation.Greater:
                        case Operation.LessOrEqual:
							fItem.Value = (value is DateTime)
											? DateTime.Parse(((DateTime)value).ToShortDateString() + " 23:59:59")
											: DateTime.Parse(DateTime.Parse((string)value).ToShortDateString() + " 23:59:59");

                            break;

                        case Operation.Less:
                        case Operation.GreaterOrEqual:
							fItem.Value = (value is DateTime)
											? DateTime.Parse(((DateTime)value).ToShortDateString() + " 0:00:00")
											: DateTime.Parse(DateTime.Parse((string)value).ToShortDateString() + " 0:00:00");
                            break;
                    }
                }
                else
                    fItem.Value = value;
            }
            else
                fItem.Value = value;

            return fItem;
        }

		public static GroupItem BuildGroupItem(string propertyName,
												Type entityType)
		{
			GroupItem gItem = new GroupItem();
			gItem.Property = propertyName;
			gItem.EntityType = entityType;

			Type entityRecordType = AppControllerBase.AppControler.RecordEntities[gItem.EntityType];

			try
			{
				if (propertyName == "Oid")
					gItem.TableColumn = nHManager.Instance.GetTableIDColumn(entityRecordType);
				else
					gItem.TableColumn = nHManager.Instance.GetTableColumn(entityRecordType, propertyName);
			}
			catch { }

			return gItem;
		}

		public static OrderItem BuildOrderItem(string propertyName,
                                                ListSortDirection direction,
                                                Type entityType)
        {
            OrderItem oItem = new OrderItem();
            oItem.Property = propertyName;
            oItem.Direction = direction;
            oItem.EntityType = entityType;

			Type entityRecordType = AppControllerBase.AppControler.RecordEntities[oItem.EntityType];

            try
            {
                if (propertyName == "Oid")
					oItem.TableColumn = nHManager.Instance.GetTableIDColumn(entityRecordType);
                else
					oItem.TableColumn = nHManager.Instance.GetTableColumn(entityRecordType, propertyName);
            }
            catch { }
           
            return oItem;
        }

		public static Column GetColumn(FilterItem filterItem, Dictionary<String, ForeignField> foreignFields)
		{
			if (foreignFields != null && foreignFields.ContainsKey(filterItem.Property))
				return foreignFields[filterItem.Property].Column;
			else
				return nHManager.Instance.GetTableColumn(AppControllerBase.AppControler.RecordEntities[filterItem.EntityType], filterItem.Property);
		}

        public static string GET_FILTERS_SQL(FilterList filters, string tableAlias, Dictionary<String, ForeignField> foreignFields = null)
        {
            return GET_FILTERS_SQL(filters, tableAlias, CultureInfo.CurrentCulture, foreignFields);
        }

        public static string GET_FILTERS_SQL(FilterList filters, string tableAlias, CultureInfo cultureInfo, Dictionary<String, ForeignField> foreignFields = null)
        {
            string queryPattern = "(TRUE {0})";
            string stringPattern = @"{0}.""{1}"" {2} '{3}'";
            string intPattern = @"{0}.""{1}"" {2} {3}";
            string likePattern = @"{0}.""{1}"" {2} '%{3}%'";
            string likeIntPattern = @"TRIM(TO_CHAR({0}.""{1}"", '999999999999')) {2} '%{3}%'";
            string likeDatePattern = @"TO_CHAR({0}.""{1}"", '" + cultureInfo.DateTimeFormat.ShortDatePattern + "') {2} '%{3}%'";
            string StartPattern = @"{0}.""{1}"" {2} '{3}%'";
            string StartIntPattern = @"TRIM(TO_CHAR({0}.""{1}"", '999999999999')) {2} '{3}%'";
            string StartDatePattern = @"TO_CHAR({0}.""{1}"", '" + cultureInfo.DateTimeFormat.ShortDatePattern + "') {2} '{3}%'";
            string betweenPattern = @"{0}.""{1}"" {2} '{3}' AND '{4}'";
            string betweenIntPattern = @"{0}.""{1}"" {2} {3} AND {4}";
            string query = string.Empty;

            if (filters != null)
            {
                foreach (FilterItem item in filters)
                {
					Type entityRecordType = AppControllerBase.AppControler.RecordEntities[item.EntityType];

                    switch (item.FilterProperty)
                    {
                        case IFilterProperty.All:
                            {
                                query += @" 
									AND (FALSE ";

								ICollection<Column> cols = nHManager.Instance.GetTableColumns(entityRecordType);
								foreach (Column col in nHManager.Instance.GetTableColumns(entityRecordType))
                                {
                                    if (col.Value.Type.Name == "String")
                                    {
                                        query += " OR ";
                                        query += String.Format(likePattern, tableAlias, col.Name, GET_OPERATOR(item.Operation), item.ValueToString.ToLower());
                                    }
                                    else if ((col.Value.Type.Name == "DateTime") ||
                                            (col.Value.Type.Name == "Date"))
                                    {
                                        query += " OR ";
										query += String.Format(likeDatePattern, tableAlias, col.Name, GET_OPERATOR(item.Operation), item.ValueToSQL.ToString().ToLower());
                                    }
                                    else if ((col.Value.Type.Name == "Int32") ||
                                            (col.Value.Type.Name == "Int64"))
                                    {
                                        query += " OR ";
										query += String.Format(likeIntPattern, tableAlias, col.Name, GET_OPERATOR(item.Operation), item.ValueToString.ToString().ToLower());
                                    }
                                }

                                if (foreignFields != null)
                                {
                                    NHibernate.Mapping.Column fcol;
                                    foreach (KeyValuePair<String, ForeignField> field in foreignFields)
                                    {
                                        fcol = field.Value.Column;
                                        if (fcol != null)
                                        {
                                            if (fcol.Value.Type.Name == "String")
                                            {
                                                query += " OR ";
                                                query += String.Format(likePattern, field.Value.TableAlias, fcol.Name, GET_OPERATOR(item.Operation), item.ValueToString.ToLower());
                                            }
                                            else if ((fcol.Value.Type.Name == "DateTime") ||
                                                    (fcol.Value.Type.Name == "Date"))
                                            {
                                                query += " OR ";
												query += String.Format(likeDatePattern, field.Value.TableAlias, fcol.Name, GET_OPERATOR(item.Operation), item.ValueToSQL);
                                            }
                                            else if ((fcol.Value.Type.Name == "Int32") ||
                                                    (fcol.Value.Type.Name == "Int64"))
                                            {
                                                query += " OR ";
												query += String.Format(likeIntPattern, field.Value.TableAlias, fcol.Name, GET_OPERATOR(item.Operation), item.ValueToString.ToString().ToLower());
                                            }
                                        }
                                    }
                                }
                                query += ")";
                            }
                            break;

                        case IFilterProperty.ByParamenter:
                            {
								Column col = GetColumn(item, foreignFields);
								string tablealias = (foreignFields != null && foreignFields.ContainsKey(item.Property))
														? foreignFields[item.Property].TableAlias
														: tableAlias;

                                switch (item.Operation)
                                {
                                    case Operation.Contains:

                                        query += " AND ";

                                        if (col.Value.Type.Name == "String")
                                        {
											query += String.Format(likePattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.ValueToString.ToLower());
                                        }
                                        else if ((col.Value.Type.Name == "DateTime") ||
                                                (col.Value.Type.Name == "Date"))
                                        {
											query += String.Format(likeDatePattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.ValueToSQL);
                                        }
                                        else if ((col.Value.Type.Name == "Int32") ||
                                                (col.Value.Type.Name == "Int64"))
                                        {
											query += String.Format(likeIntPattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.ValueToString.ToLower());
                                        }

                                        break;

                                    case Operation.StartsWith:

                                        query += " AND ";

                                        if (col.Value.Type.Name == "String")
                                        {
											query += String.Format(StartPattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.ValueToString.ToLower());
                                        }
                                        else if ((col.Value.Type.Name == "DateTime") ||
                                                (col.Value.Type.Name == "Date"))
                                        {
											query += String.Format(StartDatePattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.ValueToSQL);
                                        }
                                        else if ((col.Value.Type.Name == "Int32") ||
                                                (col.Value.Type.Name == "Int64"))
                                        {
											query += String.Format(StartIntPattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.ValueToString.ToLower());
                                        }

                                        break;

                                    case Operation.Between:

                                        query += " AND ";

                                        // TODO : Aggregate second item value to the filter
                                        // Necessary second item value in order to this work
   										switch (item.TableColumn.Value.Type.Name)
										{
											case "DateTime":
											case "Date":
											case "String":
												query += String.Format(betweenPattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.ValueToSQL, item.ValueToSQL);
												break;

											default:
												query += String.Format(betweenIntPattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.Value, item.Value);
												break;
										}

                                        break;

                                    default:

                                        query += " AND ";

										switch (item.TableColumn.Value.Type.Name)
										{
											case "DateTime":
											case "Date":
											case "String":
												query += String.Format(stringPattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.ValueToSQL);
												break;

											default:
												query += String.Format(intPattern, tablealias, col.Name, GET_OPERATOR(item.Operation), item.Value);
												break;
										}

                                        break;
                                }
                            }
                            break;
                    }
                }
            }

            return String.Format(queryPattern, query);
        }

		public static string GET_GROUPBY_SQL(GroupList groups, string tableAlias, Dictionary<String, ForeignField> foreignFields = null)
		{
			String query = @"
				GROUP BY";

			if ((groups == null) || (groups.Count == 0))
			{
				return string.Empty;
			}

			foreach (GroupItem group in groups)
			{
				if (foreignFields != null && foreignFields.ContainsKey(group.Property))
				{
					//Es un campo calculado. No tiene tabla asociada
					if (foreignFields[group.Property].Column == null)
					{
						query += GET_GROUPBY_FIELD(foreignFields[group.Property].TableAlias,
												foreignFields[group.Property].Property);
					}
					else
					{
						query += GET_GROUPBY_FIELD(foreignFields[group.Property].TableAlias,
												foreignFields[group.Property].Column.Name);
					}
				}
				else
				{
					query += GET_GROUPBY_FIELD(string.Empty, group.TableColumn.Name);
				}

				query += ",";
			}

			return query.Substring(0, query.Length - 1);
		}

		private static string GET_GROUPBY_FIELD(string tableAlias, string tableColumn)
		{
			String pattern = " {0}.\"{1}\"";
			String pattern2 = " \"{0}\"";

			if (string.IsNullOrEmpty(tableAlias))
			{
				return String.Format(pattern2,
									tableColumn);
			}
			else
			{
				return String.Format(pattern,
									tableAlias,
									tableColumn);
			}
		}

        public static string GET_ORDERS_SQL(OrderList orders, string tableAlias, Dictionary<String, ForeignField> foreignFields = null)
        {
            String query = @"
				ORDER BY";

            if ((orders == null) || (orders.Count == 0))
            {
				return query + GET_ORDER_FIELD(tableAlias, "OID", ListSortDirection.Ascending);
            }            

            foreach (OrderItem order in orders)
            {
                if (foreignFields != null && foreignFields.ContainsKey(order.Property))
                {
                    if (foreignFields[order.Property].Column == null)
                    {
						query += GET_ORDER_FIELD(foreignFields[order.Property].TableAlias, 
												foreignFields[order.Property].Property,
                                                order.Direction);
                    }
                    else
					{
						query += GET_ORDER_FIELD(foreignFields[order.Property].TableAlias, 
												foreignFields[order.Property].Column.Name,
                                                order.Direction);
					}
                }
                else
                {
                    query += GET_ORDER_FIELD(tableAlias, order.TableColumn.Name, order.Direction);
                }

                query += ",";
            }

            return query.Substring(0, query.Length - 1);
        }

		private static string GET_ORDER_FIELD(string tableAlias, string tableColumn, ListSortDirection direction)
		{
			String pattern = " {0}.\"{1}\" {2}";
			String pattern2 = " \"{0}\" {1}";

			if (string.IsNullOrEmpty(tableAlias))
			{
				return String.Format(pattern2,
									tableColumn,
									((direction == ListSortDirection.Ascending) ? "ASC" : "DESC"));
			}
			else
			{
				return String.Format(pattern,
									tableAlias,
									tableColumn,
									((direction == ListSortDirection.Ascending) ? "ASC" : "DESC"));
			}
		}

        public static string GET_OPERATOR(Operation operation)
        {
            switch (operation)
            {
                case Operation.Equal: return "=";
                case Operation.Contains: return "ILIKE";
                case Operation.StartsWith: return "ILIKE";
                case Operation.Less: return "<";
                case Operation.LessOrEqual: return "<=";
                case Operation.GreaterOrEqual: return ">=";
                case Operation.Greater: return ">";
                case Operation.Between: return "BETWEEN";
                case Operation.Distinct: return "!=";
                default: return "=";
            }
        }
    }

    public class ForeignField
    {
        public string Property { get; set; }
        public string TableAlias { get; set; }
        public NHibernate.Mapping.Column Column { get; set; }
    }

	[Serializable()]
	public class GroupItem
	{
		public Type EntityType { get; set; }
		public string Property { get; set; }
		public Column TableColumn { get; set; }
	}

	[Serializable()]
	public class GroupList : List<GroupItem>
	{
		public void NewGroup(string propertyName, Type entityType)
		{
			this.Add(FilterMng.BuildGroupItem(propertyName, entityType));
		}

        public bool Contains(string propertyName)
        {
            return this.FirstOrDefault(x => x.Property == propertyName) != null; 
        }
	}

	[Serializable()]
	public class OrderItem
	{
		public Type EntityType { get; set; }
		public string Property { get; set; }
		public Column TableColumn { get; set; }
		public ListSortDirection Direction { get; set; }
	}

	[Serializable()]
	public class OrderList : List<OrderItem>
	{
		public void NewOrder(string propertyName,
								ListSortDirection direction,
								Type entityType)
		{
			this.Add(FilterMng.BuildOrderItem(propertyName,
												  direction,
												  entityType));
		}

		public void EditOrder(int orderItemIndex, string propertyName,
								ListSortDirection direction,
								Type entityType)
		{
			this[orderItemIndex].Direction = direction;
			this[orderItemIndex].Property = propertyName;
			this[orderItemIndex].EntityType = entityType;

			Type entityRecordType = AppControllerBase.AppControler.RecordEntities[entityType];

			try
			{
				if (propertyName == "Oid")
					this[orderItemIndex].TableColumn = nHManager.Instance.GetTableIDColumn(entityRecordType);
				else
					this[orderItemIndex].TableColumn = nHManager.Instance.GetTableColumn(entityRecordType, propertyName);
			}
			catch { }
		}
	}
}
