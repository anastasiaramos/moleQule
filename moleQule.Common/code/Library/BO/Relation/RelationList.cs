using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class RelationList : ReadOnlyListBaseEx<RelationList, RelationInfo, Relation>
	{	
		#region Business Methods
			
		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private RelationList() {}
		private RelationList(IList<Relation> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private RelationList(IList<RelationInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }

        public List<long> ToChildsOidList()
        {
            List<long> oid_list = new List<long>(
                                        from st in Items
                                        select st.OidChild
                                    );

            return oid_list;
        }

		#endregion
		
		#region Root Factory Methods
		
		public static RelationList NewList() { return new RelationList(); }
		
		private static RelationList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Relation.GetCriteria(Relation.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			RelationList list = DataPortal.Fetch<RelationList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}	
		public static RelationList GetList(QueryConditions conditions, bool childs) {	return GetList(SELECT(conditions), childs); }				
		public static RelationList GetList(bool childs = true) { return GetList(SELECT(), childs); }
		
        public static RelationList GetList(IList<Relation> list) { return new RelationList(list,false); }
        public static RelationList GetList(IList<RelationInfo> list) { return new RelationList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<RelationInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<RelationInfo> sortedList = new SortedBindingList<RelationInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<RelationInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<RelationInfo> sortedList = new SortedBindingList<RelationInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion

        #region Child Factory Methods

        public static RelationList GetChildList(IList<Relation> list) { return new RelationList(list, false); }

        #endregion

        #region Common Data Access

		private void Fetch(IList<Relation> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Relation item in lista)
				this.AddItem(item.GetInfo(Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(RelationInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion

		#region Root Data Access
		 
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
		protected override void Fetch(CriteriaEx criteria)
		{
			this.RaiseListChangedEvents = false;
			
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{					
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session()); 
					
					IsReadOnly = false;
					
					while (reader.Read())
						this.AddItem(RelationInfo.GetChild(SessionCode, reader, Childs));

                    if (criteria.PagingInfo != null)
                    {
                        reader = nHManager.Instance.SQLNativeSelect(Relation.SELECT_COUNT(criteria), criteria.Session);
                        if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
                    }
					
					IsReadOnly = true;
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
			
			this.RaiseListChangedEvents = true;
		}
				
		#endregion

        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Relation.SELECT(conditions, false); }
        public static string SELECT(IEntity parent)
        {
            QueryConditions conditions = new QueryConditions { Relation = RelationInfo.New() };
            conditions.Relation.OidParent = parent.Oid;
            conditions.Relation.ParentType = parent.EntityType;

            return Relation.SELECT(conditions, true);
        }

		#endregion		
	}
}
