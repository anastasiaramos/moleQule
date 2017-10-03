using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class ContactoEmpresas : BusinessListBaseEx<ContactoEmpresas, ContactoEmpresa>
    {
        #region Business Methods

        public ContactoEmpresa NewItem(Company parent)
        {
            this.NewItem(ContactoEmpresa.NewChild(parent));
            return this[Count - 1];
        }

        #endregion

        #region Factory Methods

        private ContactoEmpresas()
        {
            MarkAsChild();
        }

        private ContactoEmpresas(IList<ContactoEmpresa> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }

        private ContactoEmpresas(IDataReader reader)
        {
            MarkAsChild();
            Fetch(reader);
        }

        public static ContactoEmpresas NewChildList() { return new ContactoEmpresas(); }

        public static ContactoEmpresas GetChildList(IList<ContactoEmpresa> lista) { return new ContactoEmpresas(lista); }

        public static ContactoEmpresas GetChildList(IDataReader reader) { return new ContactoEmpresas(reader); }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<ContactoEmpresa> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (ContactoEmpresa item in lista)
                this.AddItem(ContactoEmpresa.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(ContactoEmpresa.GetChild(reader));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(Company parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (ContactoEmpresa obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (ContactoEmpresa obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

		#region SQL

		public static string SELECT() { return ContactoEmpresa.SELECT(new QueryConditions(), true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions); }
		public static string SELECT(Company parent) { return SELECT(new QueryConditions { Schema = parent.GetInfo(false) }); }

		#endregion
    }
}
