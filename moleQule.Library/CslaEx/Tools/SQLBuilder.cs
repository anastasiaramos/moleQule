using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace moleQule.Library.CslaEx
{
    public interface ISQLBuilder
    {
        
    }
    
    /// <summary>
    /// Clase base para generación de consultas
    /// </summary>
    public class SQLBuilder
    {
        #region Pagination

        public static string SELECT_COUNT(CriteriaEx criteria)
        {
            criteria.Select = @"SELECT COUNT(*) AS ""TOTAL_ROWS""";
            criteria.From = criteria.Query.Substring(criteria.Query.IndexOf("FROM"));

            int unionPos = criteria.From.IndexOf("UNION");

            int trimPos = 0;

            //FALTA RECORRER LOS UNION PARA INCLUIRLOS
            //AHORA MISMO SE QUEDA SOLO CON EL PRIMER SELECT DEL UNION
            if (unionPos >= 0)
                trimPos = unionPos;
            else
                trimPos = criteria.From.IndexOf("ORDER BY ");

            trimPos = (trimPos < 0) ? 0 : trimPos;

            criteria.From = criteria.From.Substring(0, trimPos);

            return criteria.Query;
        }

        #endregion

        #region COMMANDS

        protected static string GROUPBY(GroupList groups, string tableAlias, Dictionary<String, ForeignField> foreignFields)
        {
            if (groups == null || groups.Count == 0) return string.Empty;

            return FilterMng.GET_GROUPBY_SQL(groups, tableAlias, foreignFields);
        }

        protected static string LIMIT(PagingInfo pagingInfo)
        {
            if (pagingInfo == null) return string.Empty;

            return @"
			LIMIT " + pagingInfo.ItemsPerPage + " OFFSET " + pagingInfo.CurrentPage * pagingInfo.ItemsPerPage;
        }

        protected static string LOCK(string tableAlias, bool lockTable = false)
        {
			return 
            (lockTable)
			? @" 
				FOR UPDATE OF " + tableAlias + " NOWAIT;"
			: ";";
        }

        protected static string ORDER(OrderList orders, string tableAlias, Dictionary<String, ForeignField> foreignFields)
        {
            if (orders == null || orders.Count == 0) return string.Empty;

            return FilterMng.GET_ORDERS_SQL(orders, tableAlias, foreignFields);
        }

        #endregion

        #region OPERATORS

        protected static string BETWEEN(string from, string till)
        {
            string BETWEEN_pattern = " BETWEEN '{0}' AND '{1}'";

            return String.Format(BETWEEN_pattern, from, till);
        }

        protected static string IN(List<long> oidList)
        {
            string IN_pattern = " IN ({0})";

            if (oidList == null || oidList.Count == 0)
                return String.Format(IN_pattern, "0");
            else
                return String.Format(IN_pattern, String.Join(", ", oidList));
        }

        #endregion
    }
}