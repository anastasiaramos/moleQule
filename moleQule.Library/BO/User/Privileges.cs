using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	/// <summary>
	/// Editable Child Collection
	/// </summary>
	[Serializable()]
	public class Privileges : BusinessListBaseEx<Privileges, Privilege>
	{	
		#region Business Methods

		public Privilege NewItem(User parent) 
		{
			this.Add(Privilege.NewChild(parent));
			return this[Count - 1];
		}

		public Privilege GetItemBySecureItem(long oidItem)
		{
			return this.FirstOrDefault(x => x.OidItem == oidItem);
		}

		#endregion

		#region Factory Methods

		private Privileges()
		{
			MarkAsChild();
		}
		private Privileges(IList<Privilege> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private Privileges(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

		public static Privileges NewChildList() { return new Privileges(); }
	    
        public static Privileges GetChildList(IList<Privilege> lista) { return new Privileges(lista); }
        public static Privileges GetChildList(int sessionCode, IDataReader reader, bool childs) { return new Privileges(sessionCode, reader, childs); }
        public static Privileges GetChildList(User parent, bool childs)
        {
            CriteriaEx criteria = Privilege.GetCriteria(parent.SessionCode);

			criteria.Query = Privileges.SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<Privileges>(criteria);
        }

        public static Privileges CreatePerms(User usuario)
        {
            Privileges mapa = Privileges.NewChildList();
            SecureItemList elementos = SecureItemList.GetList();
            foreach (SecureItemInfo item in elementos)
            {
                Privilege permiso = Privilege.NewChild(usuario);
                permiso.OidItem = item.Oid;
                permiso.Item = item.Tipo;
                permiso.Create = false;
                permiso.Modify = false;
                permiso.Remove = false;
                permiso.Read = false;
                permiso.AssociatedItems = ItemMapList.GetAssociatedItemsList(item.Oid);
                permiso.IsAssociatedItem = ItemMapList.GetIsAssociatedItemsList(item.Oid);
                mapa.Add(permiso);
            }
            return mapa;
        }

        public static void AssociatePerms(User usuario)
        {
            foreach (Privilege privilegio in usuario.Licences)
                usuario.Licences.SetPrivilegios(privilegio);
        }

        public static void AssociatePerms(User usuario, Privilege privilegio, EPrivilege permiso)
        {
            usuario.Licences.SetPrivilegios(privilegio, permiso);
        }

        public static bool CheckPerms(User usuario, Privilege privilegio, EPrivilege permiso)
        {
            return usuario.Licences.CheckPrivilegios(privilegio, permiso);
        }

		public void SetPrivilegios(ESecureItem secureItem, EPrivilege privilege)
		{
			Privilege item = this.First(m => m.ESecureItem == secureItem);			
			if (item != null) SetPrivilegios(item, privilege);
		}
        public void SetPrivilegios(Privilege item)
        {
            foreach (ItemMapInfo itemMap in item.AssociatedItems)
            {
                List<Privilege> sublist = GetSubList(new FCriteria<long>("OidItem", itemMap.OidAssociateItem, Operation.Equal));

                foreach (Privilege itemLicenses in sublist)
                {
                    if (itemLicenses.OidItem != itemMap.OidAssociateItem)
                        continue;

                    if ((itemMap.Privilege == (long)EPrivilege.Read && item.Read) ||
                        (itemMap.Privilege == (long)EPrivilege.Create && item.Create) ||
                        (itemMap.Privilege == (long)EPrivilege.Modify && item.Modify) ||
                        (itemMap.Privilege == (long)EPrivilege.Delete && item.Remove))
                    {
                        switch (itemMap.AssociatePrivilege)
                        {
                            case (long)EPrivilege.Read:
                                if (!itemLicenses.Read)
                                {
                                    itemLicenses.Read = true;
                                    SetPrivilegios(itemLicenses, EPrivilege.Read);
                                }
                                break;

                            case (long)EPrivilege.Create:
                                if (!itemLicenses.Create)
                                {
                                    itemLicenses.Create = true;
                                    SetPrivilegios(itemLicenses, EPrivilege.Create);
                                }
                                break;

                            case (long)EPrivilege.Modify:
                                if (!itemLicenses.Modify)
                                {
                                    itemLicenses.Modify = true;
                                    SetPrivilegios(itemLicenses, EPrivilege.Modify);
                                }
                                break;

                            case (long)EPrivilege.Delete:
                                if (!itemLicenses.Remove)
                                {
                                    itemLicenses.Remove = true;
                                    SetPrivilegios(itemLicenses, EPrivilege.Delete);
                                }
                                break;
                        }
                    }
                }
            }
        }
        public void SetPrivilegios(Privilege item, EPrivilege privilege)
        {
            switch (privilege)
            {
                case EPrivilege.Create:
                    item.Create = true;
                    break;

                case EPrivilege.Read:
                    item.Read = true;
                    break;

                case EPrivilege.Modify:
                    item.Modify = true;
                    break;

                case EPrivilege.Delete:
                    item.Remove = true;
                    break;

                case EPrivilege.All:
                    item.Create = true;
                    item.Read = true;
                    item.Modify = true;
                    item.Remove = true;
                    break;
            }

            foreach (ItemMapInfo itemMap in item.AssociatedItems)
            {
                List<Privilege> sublist = GetSubList(new FCriteria<long>("OidItem", itemMap.OidAssociateItem, Operation.Equal));

                foreach (Privilege itemLicenses in sublist)
                {
                    if (itemLicenses.OidItem != itemMap.OidAssociateItem) continue;
					
					if (privilege == (long)EPrivilege.All)
					{					
						if (!itemLicenses.All)
						{
							itemLicenses.Create = true;
							itemLicenses.Read = true;
							itemLicenses.Modify = true;
							itemLicenses.Remove = true;
							SetPrivilegios(itemLicenses, EPrivilege.All);
						}									
						break;
					}
                    else if (itemMap.TipoPrivilegio == privilege)
                    {
                        switch (itemMap.AssociatePrivilege)
                        {
                            case (long)EPrivilege.Read:
                                if (!itemLicenses.Read)
                                {
                                    itemLicenses.Read = true;
                                    SetPrivilegios(itemLicenses, EPrivilege.Read);
                                }
                                break;

                            case (long)EPrivilege.Create:
                                if (!itemLicenses.Create)
                                {
                                    itemLicenses.Create = true;
                                    SetPrivilegios(itemLicenses, EPrivilege.Create);
                                }
                                break;

                            case (long)EPrivilege.Modify:
                                if (!itemLicenses.Modify)
                                {
                                    itemLicenses.Modify = true;
                                    SetPrivilegios(itemLicenses, EPrivilege.Modify);
                                }
                                break;

                            case (long)EPrivilege.Delete:
                                if (!itemLicenses.Remove)
                                {
                                    itemLicenses.Remove = true;
                                    SetPrivilegios(itemLicenses, EPrivilege.Delete);
                                }
                                break;
                        }
                    }
                }
            }
        }

        public bool CheckPrivilegios(Privilege privilegio, EPrivilege permiso_modificado)
        {
            foreach (ItemMapInfo item in privilegio.IsAssociatedItem)
            {
                List<Privilege> sublist = GetSubList(new FCriteria<long>("OidItem", item.OidItem, Operation.Equal));

                foreach (Privilege permiso in sublist)
                {
                    if (permiso.OidItem != item.OidItem)
                        continue;

                    if (item.TipoPrivilegioAsociado == permiso_modificado)
                    {
                        switch (item.Privilege)
                        {
                            case (long)EPrivilege.Read:
                                if (permiso.Read)
                                    return false;
                                break;
                            case (long)EPrivilege.Create:
                                if (permiso.Create)
                                    return false; 
                                break;
                            case (long)EPrivilege.Modify:
                                if (permiso.Modify)
                                    return false;
                                break;
                            case (long)EPrivilege.Delete:
                                if (permiso.Remove)
                                    return false;
                                break;
                        }
                    }
                }
            }

            return true;
        }

		#endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            try
            {
                this.RaiseListChangedEvents = false;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    Privilege.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Privilege.GetChild(SessionCode, reader));
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
		private void Fetch(IList<Privilege> lista)
		{
			this.RaiseListChangedEvents = false;
            
			foreach (Privilege item in lista)
                this.AddItem(Privilege.GetChild(item));            

			this.RaiseListChangedEvents = true;
		}

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(Privilege.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(User usuario)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
            foreach (Privilege obj in DeletedList)
                obj.DeleteSelf(usuario);
			
			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
            foreach (Privilege obj in this)
			{
				if (obj.IsNew)
                    obj.Insert(usuario);
				else
                    obj.Update(usuario);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion	

        #region SQL

        internal static string SELECT(QueryConditions conditions)  { return Privilege.SELECT(conditions, true); }
		internal static string SELECT(User source) { return SELECT(new QueryConditions { User = source.GetInfo() }); }

        #endregion
    }
}

