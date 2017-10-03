using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class RelationInfo : ReadOnlyBaseEx<RelationInfo, Relation>
	{	
		#region Attributes

		protected RelationBase _base = new RelationBase();

		
		#endregion
		
		#region Properties
		
		public RelationBase Base { get { return _base; } }
		
		
		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidParent { get { return _base.Record.OidParent; } set { _base.Record.OidParent = value; } }
        public long ParentType { get { return _base.Record.ParentType; } set { _base.Record.ParentType = value; } }
		public long OidChild { get { return _base.Record.OidChild; } }
		public long ChildType { get { return _base.Record.ChildType; } }
		
		
		
		#endregion
		
		#region Business Methods
						
		public void CopyFrom(Relation source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected RelationInfo() { /* require use of factory methods */ }
		private RelationInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal RelationInfo(Relation item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
		
		public static RelationInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new RelationInfo(sessionCode, reader, childs);
		}
		
		public static RelationInfo New(long oid = 0) { return new RelationInfo(){ Oid = oid}; }
		
 		#endregion
		
		#region Root Factory Methods
	
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static RelationInfo Get(long oid, bool childs = false) 
		{ 
            if (!Relation.CanGetObject()) throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			return Get(Relation.SELECT(oid, false), childs); 
		}
		
		#endregion
					
		#region Common Data Access
								
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
		#region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
			{
				Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);
					
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex, new object[] { criteria.Query }); }
		}
		
		#endregion			
	}
}
