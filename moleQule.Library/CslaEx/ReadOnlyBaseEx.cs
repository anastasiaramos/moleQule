using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Core;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping;

namespace moleQule.Library.CslaEx
{
    [Serializable()]
	public abstract class ReadOnlyBaseEx<T> : ReadOnlyBase<T> 
        where T : ReadOnlyBaseEx<T>
    {
		#region Business Methods

		protected int _sessCode;
		private long _oid;
        protected bool _childs = false;
		protected bool _selected;

		[System.ComponentModel.DataObjectField(true)]
		public virtual long Oid
		{
			get { return _oid; }
			set { _oid = value; }
		}

		public int SessionCode
		{
			get { return _sessCode; }
			set { _sessCode = value; }
		}

		/// <summary>
		/// Indica si se quiere guardar como parte de una transaccion externa
		/// </summary>
		public virtual bool SharedTransaction { get; set; }

        /// <summary>
        /// Indica si se quiere que el objeto cargue los hijos
        /// </summary>
        public bool Childs
        {
            get { return _childs; }
            set { _childs = value; }
        }

		public bool IsSelected
		{
			get { return _selected; }
			set { _selected = value; }
		}

        /// <summary>
		/// Manejador del motor de persistencia
		/// </summary>
		/// <returns></returns>
		public virtual nHManager nHMng { get { return nHManager.Instance; } }

		protected override object GetIdValue() { return Oid; }

        /// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un registro de la base de datos
        /// </summary>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static T Get(CriteriaEx criteria)
		{
			T obj = DataPortal.Fetch<T>(criteria);
			CloseSession(criteria.SessionCode);
			return obj;
		}

        /// <summary>
        /// Devuelve el tipo de una propiedad a partir de su nombre
        /// </summary>
        /// <param name="name">Nombre de la propiedad</param>
        /// <returns></returns>
        public Type GetPropertyType(string name)
        {
            Type type = typeof(T);
            System.Reflection.PropertyInfo prop = type.GetProperty(name);

            return prop.PropertyType;
        }

		protected virtual void SetSharedSession(int sessionCode)
		{
			SessionCode = sessionCode;
			SharedTransaction = (sessionCode != -1);
		}

 		#endregion

        #region SQL

		public static string LIMIT(PagingInfo pagingInfo)
		{
			if (pagingInfo == null) return string.Empty;

			return @"
				LIMIT " + pagingInfo.ItemsPerPage + " OFFSET " + pagingInfo.CurrentPage * pagingInfo.ItemsPerPage;
		}

        /// <summary>
        /// Construye el SELECT de la lista y lo ejecuta
        /// </summary>
        /// <param name="type"></param>
        /// <param name="schema"></param>
        /// <param name="sesion"></param>
        /// <returns></returns>
        public static string SELECT(Type type, long oid)
        {
            return nHManager.Instance.SELECT(type, null, false, "Oid", oid, null);
        }

        /// <summary>
        /// Construye un SELECT para el esquema dado
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="sesion">sesión abierta para la transacción</param>
        /// <returns></returns>
        public static string SELECT(Type type, string schema, long oid)
        {
            return nHManager.Instance.SELECT(type, schema, false, "Oid", oid, null);
        }

		public static string ORDER(OrderList orders, string tableAlias, Dictionary<String, ForeignField> foreignFields)
		{
			if (orders == null) return string.Empty;

			return FilterMng.GET_ORDERS_SQL(orders, tableAlias, foreignFields);
		}

        #endregion

        #region NHibernate Default Interface

        public virtual ITransaction BeginTransaction()
		{
			return BeginTransaction(SessionCode);
		}

		public virtual ISession Session()
		{
			return Session(SessionCode);
		}

		public virtual ITransaction Transaction()
		{
			return Transaction(_sessCode);
		}

		public virtual void CloseSession()
		{
			CloseSession(_sessCode);
		}

		#endregion

		#region NHibernate By Code Interface

		/// <summary>
		/// Abre una nueva sesión 
		/// </summary>
		/// <returns></returns>
		public static int OpenSession() { return nHManager.Instance.OpenSession(); }

		/// <summary>
		/// Inicia una transacción para la sessión actual
		/// </summary>
		/// <returns></returns>
		public static ITransaction BeginTransaction(int sessionCode)
		{
			return nHManager.Instance.BeginTransaction(sessionCode);
		}

		/// <summary>
		/// Devuelve la sesión correspondiente a este objeto
		/// </summary>
		/// <returns></returns>
		public static ISession Session(int sessionCode) { return nHManager.Instance.GetSession(sessionCode); }

		/// <summary>
		/// Devuelve la transacción correspondiente a este objeto
		/// </summary>
		/// <returns></returns>
		public static ITransaction Transaction(int sessionCode)
		{
			return nHManager.Instance.GetTransaction(sessionCode);
		}

		/// <summary>
		/// Cierra la sesión que se creó para el objeto
		/// </summary>
		/// <returns></returns>
		public static void CloseSession(int sessionCode) { nHManager.Instance.CloseSession(sessionCode); }

		#endregion
    }

	[Serializable()]
	public abstract class ReadOnlyBaseEx<T, C> : ReadOnlyBaseEx<T>
		where T : ReadOnlyBaseEx<T>
		where C : BusinessBaseEx<C>
	{
		#region Factory Methods

		public static T Get(string query, bool childs = false)
		{
			Type type = typeof(BusinessBaseEx<C>);

			CriteriaEx criteria = GetCriteria(OpenSession());

			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			T obj = DataPortal.Fetch<T>(criteria);
			CloseSession(criteria.SessionCode);
			return (obj.Oid == 0) ? null : obj;
		}

		#endregion

		#region NHibernate By Code Interface

		public static CriteriaEx GetCriteria(int sessionCode) { return nHManager.Instance.GetCriteria(typeof(C), sessionCode); }

		#endregion
	}
}
