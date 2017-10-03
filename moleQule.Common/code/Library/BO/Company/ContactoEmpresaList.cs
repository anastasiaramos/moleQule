using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

namespace moleQule.Library.Common
{
    /// <summary>
    /// Read Only Child Collection of Business Objects
    /// </summary>
    [Serializable()]
    public class ContactoEmpresaList : ReadOnlyListBaseEx<ContactoEmpresaList, ContactoEmpresaInfo>
    {
        #region Factory Methods

        private ContactoEmpresaList() { }

        private ContactoEmpresaList(IList<ContactoEmpresa> lista)
        {
            Fetch(lista);
        }

        private ContactoEmpresaList(IDataReader reader)
        {
            Fetch(reader);
        }

        /// <summary>
        /// Builds a ContactoEmpresaList from IList<!--<ContactoEmpresa>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>ContactoEmpresaList</returns>
        public static ContactoEmpresaList GetChildList(IList<ContactoEmpresa> lista) { return new ContactoEmpresaList(lista); }

        public static ContactoEmpresaList GetChildList(IDataReader reader) { return new ContactoEmpresaList(reader); }

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static ContactoEmpresaList GetList(CriteriaEx criteria)
        {
            return ContactoEmpresaList.RetrieveList(typeof(ContactoEmpresa), AppContext.ActiveSchema.Code, criteria);
        }

        #endregion

        #region Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;

            try
            {
                if (nHMng.UseDirectSQL)
                {
					ContactoEmpresa.DoLOCK("COMMON", Session());

                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session()); ;

                    IsReadOnly = false;

                    while (reader.Read())
                        this.AddItem(ContactoEmpresaInfo.Get(reader, Childs));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            this.RaiseListChangedEvents = true;
        }

        // called to load data from list
        private void Fetch(IList<ContactoEmpresa> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;
            foreach (ContactoEmpresa item in lista)
                this.AddItem(item.GetInfo());
            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to load data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;
            while (reader.Read())
                this.AddItem(ContactoEmpresa.GetChild(reader).GetInfo());
            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        #endregion

		#region SQL

		public static string SELECT() { return ContactoEmpresa.SELECT(new QueryConditions(), false); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions); }
		public static string SELECT(CompanyInfo parent) { return SELECT(new QueryConditions { Schema = parent }); }

		#endregion
    }
}
