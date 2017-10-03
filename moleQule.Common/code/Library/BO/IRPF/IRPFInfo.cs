using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class IRPFInfo : ReadOnlyBaseEx<IRPFInfo>
	{
		#region Attributes

        public IRPFBase _base = new IRPFBase();

		#endregion

		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public Decimal Porcentaje { get { return _base.Record.Porcentaje; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }

		#endregion

		#region Business Methods

        
		public void CopyFrom(IRPF source) { _base.CopyValues(source); }

		#endregion

		#region Common Factory Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
		/// </remarks>
		protected IRPFInfo() { /* require use of factory methods */ }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="reader"><see cref="IDataReader"/> origen de los datos</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
		/// <remarks>
		///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
		/// </remarks>
		private IRPFInfo(IDataReader reader, bool retrieve_childs)
		{
			Childs = retrieve_childs;
			Fetch(reader);
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="reader"><see cref="BusinessBaseEx"/> origen</param>
		/// <param name="copy_childs">Flag para copiar los hijos</param>
		/// <remarks>
		///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
		/// </remarks>
		internal IRPFInfo(IRPF item, bool copy_childs)
		{
			_base.CopyValues(item);

			if (copy_childs)
			{

			}
		}

		/// <summary>
		/// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
		/// </summary>
		/// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		/// <remarks>
		/// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool retrieve_childs)
		/// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
		/// <remarks/>
		public static IRPFInfo GetChild(IDataReader reader)
		{
			return GetChild(reader, false);
		}

		/// <summary>
		/// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
		/// </summary>
		/// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		/// <remarks>La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista<remarks/>
		public static IRPFInfo GetChild(IDataReader reader, bool retrieve_childs)
		{
			return new IRPFInfo(reader, retrieve_childs);
		}

		#endregion

		#region Root Factory Methods

		public static IRPFInfo Get(long oid) { return Get(oid, false); }

		/// <summary>
		/// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
		/// </summary>
		/// <param name="oid">Oid del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static IRPFInfo Get(long oid, bool retrieve_childs)
		{
			CriteriaEx criteria = IRPF.GetCriteria(IRPF.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = IRPFInfo.SELECT(oid);
			else
				criteria.AddOidSearch(oid);

			IRPFInfo obj = DataPortal.Fetch<IRPFInfo>(criteria);
			IRPF.CloseSession(criteria.SessionCode);
			return obj;
		}

		#endregion

		#region Root Data Access

		/// <summary>
		/// Obtiene un registro de la base de datos
		/// </summary>
		/// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
		/// <remarks>
		/// La llama el DataPortal
		/// </remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					if (reader.Read())
						_base.CopyValues(reader);

				}
				else
				{
					_base.CopyValues((IRPF)(criteria.UniqueResult()));

				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

		#region Child Data Access

		//called to copy data from IDataReader
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}

		#endregion

		#region SQL

		public static string SELECT(long oid) { return IRPF.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return IRPF.SELECT(conditions, false); }

		#endregion
	}

}
