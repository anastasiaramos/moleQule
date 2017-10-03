using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class RelationRecord : RecordBase
	{
		#region Attributes

		private long _oid_parent;
		private long _parent_type;
		private long _oid_child;
		private long _child_type;
  
		#endregion
		
		#region Properties
		
		public virtual long OidParent { get { return _oid_parent; } set { _oid_parent = value; } }
		public virtual long ParentType { get { return _parent_type; } set { _parent_type = value; } }
		public virtual long OidChild { get { return _oid_child; } set { _oid_child = value; } }
		public virtual long ChildType { get { return _child_type; } set { _child_type = value; } }

		#endregion
		
		#region Business Methods
		
		public RelationRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_parent = Format.DataReader.GetInt64(source, "OID_PARENT");
			_parent_type = Format.DataReader.GetInt64(source, "PARENT_TYPE");
			_oid_child = Format.DataReader.GetInt64(source, "OID_CHILD");
			_child_type = Format.DataReader.GetInt64(source, "CHILD_TYPE");

		}		
		public virtual void CopyValues(RelationRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_parent = source.OidParent;
			_parent_type = source.ParentType;
			_oid_child = source.OidChild;
			_child_type = source.ChildType;
		}
		
		#endregion	
	}

    [Serializable()]
	public class RelationBase 
	{	 
		#region Attributes
		
		private RelationRecord _record = new RelationRecord();
		
		#endregion
		
		#region Properties
		
		public RelationRecord Record { get { return _record; } }
		
		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);
		}		
		public void CopyValues(Relation source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}
		public void CopyValues(RelationInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}
		
		#endregion	
	}
		
	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class Relation : BusinessBaseEx<Relation>
	{	 
		#region Attributes
		
		protected RelationBase _base = new RelationBase();		

		#endregion
		
		#region Properties
		
		public RelationBase Base { get { return _base; } }
		
		public override long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Oid;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
					//PropertyHasChanged();
				}
			}
		}
		public virtual long OidParent
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidParent;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidParent.Equals(value))
				{
					_base.Record.OidParent = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long ParentType
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ParentType;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.ParentType.Equals(value))
				{
					_base.Record.ParentType = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidChild
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidChild;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidChild.Equals(value))
				{
					_base.Record.OidChild = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long ChildType
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ChildType;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.ChildType.Equals(value))
				{
					_base.Record.ChildType = value;
					PropertyHasChanged();
				}
			}
		}
	
		
		
		#endregion
		
		#region Business Methods
		
		public static Relation CloneAsNew(RelationInfo source)
		{
			Relation clon = Relation.New();;
			clon.Base.CopyValues(source);
			
			clon.Oid = (new Random()).Next();
			
			
			clon.MarkNew();
			
			
			return clon;
		}
		
		protected virtual void CopyFrom(RelationInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			OidParent = source.OidParent;
			ParentType = source.ParentType;
			OidChild = source.OidChild;
			ChildType = source.ChildType;
		}
		
			
		#endregion
		 
	    #region Validation Rules

		/// <summary>
		/// Añade las reglas de validación necesarias para el objeto
		/// </summary>
		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
						
			
			//Propiedad
			/*if (Propiedad <= 0)
			{
				e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Propiedad");
				throw new iQValidationException(e.Description, string.Empty);
			}*/

			return true;
		}	
		 
		#endregion
		 
		#region Autorization Rules
				
		public static bool CanAddObject()
        {
            return true;
        }
        public static bool CanGetObject()
        {
            return true;
        }
        public static bool CanDeleteObject()
        {
            return true;
        }
        public static bool CanEditObject()
        {
            return true;
        }

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				Relation = RelationInfo.New(oid),
			};

		}
		
		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Relation() 
		{
			
		}				
		private Relation(Relation source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Relation(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

		public static Relation NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Relation obj = DataPortal.Create<Relation>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		internal static Relation GetChild(Relation source, bool childs = false) { return new Relation(source, childs); }
        internal static Relation GetChild(int sessionCode, IDataReader source, bool childs = false) { return new Relation(sessionCode, source, childs); }

		public virtual RelationInfo GetInfo (bool childs = true) { return new RelationInfo(this, childs); }
		
		#endregion
		
		#region Child Factory Methods
	
		private Relation(Relation source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private Relation(int sessionCode, IDataReader reader)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Fetch(reader);
		}
		
		internal static Relation NewChild(IEntity parent, IEntity child) 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			Relation obj = new Relation();
			obj.MarkAsChild();

			obj.OidParent = parent.Oid;
			obj.ParentType = parent.EntityType;
			obj.OidChild = child.Oid;
			obj.ChildType = child.EntityType;

			return obj;
		}
		
		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);			
				
			MarkDeleted();
		}
			
		#endregion

		#region Root Factory Methods
		
		public static Relation New(int sessionCode = -1)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Relation obj = DataPortal.Create<Relation>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			return obj;
		}
		
		public new static Relation Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<Relation>.Get(query, childs, -1);
		}
		
		public static Relation Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }
		
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			IsPosibleDelete(oid);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los RElation. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Relation.OpenSession();
			ISession sess = Relation.Session(sessCode);
			ITransaction trans = Relation.BeginTransaction(sessCode);
			
			try
			{	
				sess.Delete("from RelationRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Relation.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Relation Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);			
		
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			try
			{
				ValidationRules.CheckRules();
			}
			catch (iQValidationException ex)
			{
				iQExceptionHandler.TreatException(ex);
				return this;
			}

			try
			{	
				base.Save();				
				
				if (!SharedTransaction) Transaction().Commit();
				return this;
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return this;
			}
			finally
			{
				if (!SharedTransaction)
				{
					if (CloseSessions) CloseSession(); 
					else BeginTransaction();
				}
			}
		}
				
		#endregion				
		
		#region Common Data Access
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			Oid = (long)(new Random()).Next();
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Relation source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);		 

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
			_base.CopyValues(source);

			   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Relations parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(Relations parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			RelationRecord obj = Session().Get<RelationRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Relations parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<RelationRecord>(Oid));
		
			MarkNew(); 
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
					//RElation.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);					
					
				}

				MarkOld();
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			if (!SharedTransaction)
			{
				SessionCode = OpenSession();
				BeginTransaction();
			}			
			
			Session().Save(_base.Record);
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;
			
			RelationRecord obj = Session().Get<RelationRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			MarkOld();
			
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();
					
				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((RelationRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				CloseSession();
			}
		}		
		
		#endregion		

		#region Child Data Access

		internal void Insert(IEntity parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidParent = parent.Oid;

			try
			{
				parent.Session().Save(_base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Update(IEntity parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			OidParent = parent.Oid;

			try
			{
				SessionCode = parent.SessionCode;
				RelationRecord obj = Session().Get<RelationRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void DeleteSelf(IEntity parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<RelationRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}

		#endregion
				
        #region SQL

		internal enum EQueryType { GENERAL = 0 }
		
		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() {};
        }
		
        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string SELECT_FIELDS(EQueryType queryType, QueryConditions conditions)
        {            	
            string query = @"
			SELECT " + (long)queryType + @" AS ""QUERY_TYPE""";

			switch (queryType)
			{
				case EQueryType.GENERAL:

					query += @"
							,RE.*";

					break;
			}

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
            string re = nHManager.Instance.GetSQLTable(typeof(RelationRecord));

			string query;

            query = @"
			FROM " + re + @" AS RE";
				
			return query + " " + conditions.ExtraJoin;
		}
		
		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;
		
			string query;

            query = @" 
			WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "RE", ForeignFields());
				
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "RE");
			            
            if (conditions.Relation != null)
            {
                if (conditions.Relation.OidParent != 0)
                    query += @"
					    AND RE.""OID_PARENT"" = " + conditions.Relation.OidParent + @"
					    AND RE.""PARENT_TYPE"" = " + conditions.Relation.ParentType;
                
                if (conditions.Relation.OidChild != 0)
                    query += @"
					    AND RE.""OID_CHILD"" = " + conditions.Relation.OidChild + @"
					    AND RE.""CHILD_TYPE"" = " + conditions.Relation.ChildType;

                if ((conditions.Relation.OidParent == 0) && (conditions.Relation.OidChild == 0))
                    query += @"
					    AND RE.""OID"" = " + conditions.Relation.Oid;
            }
			
			return query + " " + conditions.ExtraWhere;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
			string query = 
				SELECT_FIELDS(EQueryType.GENERAL, conditions) + 
				JOIN(conditions) +
				WHERE(conditions);

            if (conditions != null) 
			{
				query += ORDER(conditions.Orders, "RE", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}				

			query += Common.EntityBase.LOCK("RE", lockTable);

            return query;
        }
		
		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions, lockTable);
		}
		
		internal static string SELECT(long oid, bool lockTable)
        {			
			return SELECT(new QueryConditions { Relation = RelationInfo.New(oid) }, lockTable);
        }
		
		#endregion
	}
}
