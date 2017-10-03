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
	/// Editable Business Object Root Collection
	/// </summary>
    [Serializable()]
    public class Pesajes : BusinessListBaseEx<Pesajes, Pesaje>
    {
		#region Business Methods
		
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public Pesaje NewItem()
        {
			Pesaje item = Pesaje.NewChild();
			SetNextCode(item);
			this.AddItem(item);
            return item;
        }

		public void SetMaxSerial()
		{
			foreach (Pesaje item in this)
				if (item.Serial > _max_serial) MaxSerial = item.Serial;
		}

		public void SetNextCode(Pesaje item)
		{
			int index = this.IndexOf(item);

			if (index == 0)
			{
				item.GetNewCode();
				MaxSerial = item.Serial;
			}
			else
			{
				item.Serial = MaxSerial + 1;
				item.Codigo = item.Serial.ToString(Resources.Defaults.PESAJE_CODE_FORMAT);
				MaxSerial++;
			}
		}

		#endregion

		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private Pesajes() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Pesajes NewList() 
		{ 	
			if (!Pesaje.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new Pesajes(); 
		}

        public static Pesajes GetList() { return GetList(true); }		
		public static Pesajes GetList(bool childs) 
		{
			return GetList(Pesajes.SELECT(), childs);
		}
		public static Pesajes GetList(QueryConditions conditions, bool childs)
		{
			return GetList(Pesajes.SELECT(conditions), childs);
		}
		
		private static Pesajes GetList(string query, bool childs)
		{
            if (!Pesaje.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Pesaje.GetCriteria(Pesaje.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;
				
			Pesaje.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Pesajes>(criteria);
		}
		
        #endregion
		
		#region Root Data Access

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        /// <remarks>LA UTILIZA EL DATAPORTAL</remarks>
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        private void Fetch(CriteriaEx criteria)
        {
            try
            {
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
                {
                    Pesaje.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Pesaje.GetChild(SessionCode, reader, Childs));
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Pesaje obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (Pesaje obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
						{
							SetNextCode(obj);
							obj.Insert(this);
						}
						else
							obj.Update(this);
					}
				}

                if (!SaveAsChildList) Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (!SaveAsChildList) if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!SaveAsChildList) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion		
			
        #region SQL

        public static string SELECT() { return Pesaje.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Pesaje.SELECT(conditions, true); }
			
		#endregion
    }
}

