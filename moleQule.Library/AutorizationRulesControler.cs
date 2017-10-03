using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;

using moleQule.Library;
using moleQule.Library.CslaEx; 
using moleQule.Library.Resources;

namespace moleQule.Library
{
	[Serializable()]
	public class AutorizationRulesControler
    {
        #region Business Methods

        //public static void CheckAssociatedList(ItemLicences licence,
        //                                        SecureItemList secureItems)
        //{ 
        //    ItemMapList lista_asociados = ItemMapList.GetAssociatedItemsList(licence.OidItem);

        //    foreach (ItemMapInfo item in lista_asociados)
        //    {
        //        bool allowed = true;

        //        ItemLicences checked_licence = AppContext.User.CheckedLicences.GetItem(new FCriteria<long>("OidItem", item.OidAssociateItem, Operation.Equal));
        //        if (checked_licence != null)
        //            continue;

        //        switch (item.TipoPrivilegioAsociado)
        //        { 
        //            case EPrivilege.Read:
        //                if (!CanGetObject(item.OidAssociateItem, secureItems))
        //                    allowed = false;
        //                break;
        //            case EPrivilege.Create:
        //                if (!CanAddObject(item.OidAssociateItem, secureItems))
        //                    allowed = false;
        //                break;
        //            case EPrivilege.Modify:
        //                if (!CanEditObject(item.OidAssociateItem, secureItems))
        //                    allowed = false;
        //                break;
        //            case EPrivilege.Delete:
        //                if (!CanDeleteObject(item.OidAssociateItem, secureItems))
        //                    allowed = false;
        //                break;
        //        }

        //        if (!allowed)
        //        {
        //            switch (item.TipoPrivilegio)
        //            {
        //                case EPrivilege.Read:
        //                    if (licence.Read)
        //                    {
        //                        licence.Read = false;
        //                        UpdateAssociatedItems(licence.OidItem, EPrivilege.Read);
        //                    }
        //                    break;
        //                case EPrivilege.Create:
        //                    if (licence.Create)
        //                    {
        //                        licence.Create = false;
        //                        UpdateAssociatedItems(licence.OidItem, EPrivilege.Create);
        //                    }
        //                    break;
        //                case EPrivilege.Modify:
        //                    if (licence.Modify)
        //                    {
        //                        licence.Modify = false;
        //                        UpdateAssociatedItems(licence.OidItem, EPrivilege.Modify);
        //                    }
        //                    break;
        //                case EPrivilege.Delete:
        //                    if (licence.Remove)
        //                    {
        //                        licence.Remove = false;
        //                        UpdateAssociatedItems(licence.OidItem, EPrivilege.Delete);
        //                    }
        //                    break;
        //            }

        //            throw new iQAuthorizationException(string.Format("Secure Item {0} must has {1} privilege heritage from Secure Item {2} and {3} privilege"
        //                                                ,item.OidAssociateItem, item.TipoPrivilegioAsociadoLabel
        //                                                ,item.OidItem, item.TipoPrivilegioLabel));
        //        }
        //    }
        //}

        //public static ItemLicences AddVerifiedItems(long oidSecureItem, SecureItemList secureItems)
        //{
        //    ItemLicences licence = AppContext.User.Licences.GetItem(new FCriteria<long>("OidItem", oidSecureItem, Operation.Equal));

        //    if (licence != null)
        //    {
        //        ItemLicences nuevo_check = ItemLicences.NewChild(AppContext.User);
        //        nuevo_check.OidItem = oidSecureItem;
        //        nuevo_check.Read = true;
        //        nuevo_check.Create = true;
        //        nuevo_check.Modify = true;
        //        nuevo_check.Remove = true;

        //        AppContext.User.CheckedLicences.AddItem(nuevo_check);

        //        ItemLicences nuevo = ItemLicences.NewChild(AppContext.User);
        //        nuevo.OidItem = oidSecureItem;
        //        nuevo.Read = licence.Read;
        //        nuevo.Create = licence.Create;
        //        nuevo.Modify = licence.Modify;
        //        nuevo.Remove = licence.Remove;

        //        AppContext.User.VerifiedLicences.AddItem(nuevo);

        //        CheckAssociatedList(nuevo, secureItems);

        //        return nuevo;
        //    }

        //    return null;
        //}

        //public static void UpdateAssociatedItems(long oidSecureItem, EPrivilege privilege)
        //{
        //    ItemMapList lista_asociados = ItemMapList.GetIsAssociatedItemsList(oidSecureItem);

        //    foreach (ItemMapInfo item in lista_asociados)
        //    {
        //        ItemLicences verified = AppContext.User.VerifiedLicences.GetItem(new FCriteria<long>("OidItem", item.OidItem, Operation.Equal));
        //        if (verified == null || item.TipoPrivilegioAsociado != privilege)
        //            continue;

        //        switch (item.TipoPrivilegio)
        //        {
        //            case EPrivilege.Read:
        //                if (verified.Read)
        //                {
        //                    verified.Read = false;
        //                    UpdateAssociatedItems(verified.OidItem, EPrivilege.Read);
        //                }
        //                break;
        //            case EPrivilege.Create:
        //                if (verified.Create)
        //                {
        //                    verified.Create = false;
        //                    UpdateAssociatedItems(verified.OidItem, EPrivilege.Create);
        //                }
        //                break;
        //            case EPrivilege.Modify:
        //                if (verified.Modify)
        //                {
        //                    verified.Modify = false;
        //                    UpdateAssociatedItems(verified.OidItem, EPrivilege.Modify);
        //                }
        //                break;
        //            case EPrivilege.Delete:
        //                if (verified.Remove)
        //                {
        //                    verified.Remove = false;
        //                    UpdateAssociatedItems(verified.OidItem, EPrivilege.Delete);
        //                }
        //                break;
        //        }
        //    }

        //}

        public static bool CanGetObject(string secureItem)
        {
			if (AppContext.User == null) return false;
            if (AppContext.User.IsAdmin) return true;

            //SecureItemList elementos_seguros = SecureItemList.GetList();

			SecureItemInfo item = AppContext.Principal.SecureItems.GetItemByTipo(Convert.ToInt64(secureItem));
            Privilege privilege = AppContext.User.Licences.GetItemBySecureItem(item.Oid);

            return privilege != null ? privilege.Read : false;

            //AppContext.User.CheckedLicences = LicenceMap.NewChildList();

            //return CanGetObject(item.Oid, elementos_seguros);
        }
        //public static bool CanGetObject(long oidSecureItem, SecureItemList secureItems)
        //{
        //    ItemLicences item = AppContext.User.VerifiedLicences.GetItem(new FCriteria<long>("OidItem", oidSecureItem, Operation.Equal));

        //    if (item != null)
        //        return AppContext.User.VerifiedLicences.GetItem(new FCriteria<long>("OidItem", oidSecureItem, Operation.Equal)).Read;

        //    ItemLicences licence = AddVerifiedItems(oidSecureItem, secureItems);

        //    return licence != null ? licence.Read : false;
        //}

        public static bool CanAddObject(string secureItem)
        {
			if (AppContext.User == null) return false;
			if (AppContext.User.IsAdmin) return true;

            //SecureItemList elementos_seguros = SecureItemList.GetList();

			SecureItemInfo item = AppContext.Principal.SecureItems.GetItemByTipo(Convert.ToInt64(secureItem));
			Privilege privilege = AppContext.User.Licences.GetItemBySecureItem(item.Oid);

            return privilege != null ? privilege.Create : false;

            //AppContext.User.CheckedLicences = LicenceMap.NewChildList();

            //return CanAddObject(item.Oid, elementos_seguros);
        }
        //public static bool CanAddObject(long oidSecureItem, SecureItemList secureItems)
        //{
        //    ItemLicences item = AppContext.User.VerifiedLicences.GetItem(new FCriteria<long>("OidItem", oidSecureItem, Operation.Equal));

        //    if (item != null)
        //        return AppContext.User.VerifiedLicences.GetItem(new FCriteria<long>("OidItem", oidSecureItem, Operation.Equal)).Create;

        //    ItemLicences licence = AddVerifiedItems(oidSecureItem, secureItems);

        //    return licence != null ? licence.Create : false;
        //}

        public static bool CanEditObject(string secureItem)
        {
			if (AppContext.User == null) return false;
			if (AppContext.User.IsAdmin) return true;

            //SecureItemList elementos_seguros = SecureItemList.GetList();

			SecureItemInfo item = AppContext.Principal.SecureItems.GetItemByTipo(Convert.ToInt64(secureItem));
			Privilege privilege = AppContext.User.Licences.GetItemBySecureItem(item.Oid);

            return privilege != null ? privilege.Modify : false;

            //AppContext.User.CheckedLicences = LicenceMap.NewChildList();

            //return CanEditObject(item.Oid, elementos_seguros);
        }
        //public static bool CanEditObject(long oidSecureItem, SecureItemList secureItems)
        //{
        //    ItemLicences item = AppContext.User.VerifiedLicences.GetItem(new FCriteria<long>("OidItem", oidSecureItem, Operation.Equal));

        //    if (item != null)
        //        return AppContext.User.VerifiedLicences.GetItem(new FCriteria<long>("OidItem", oidSecureItem, Operation.Equal)).Modify;

        //    ItemLicences licence = AddVerifiedItems(oidSecureItem, secureItems);

        //    return licence != null ? licence.Modify : false;

        //}

		public static bool CanDeleteObject(string secureItem)
        {
			if (AppContext.User == null) return false;
			if (AppContext.User.IsAdmin) return true;

            //SecureItemList elementos_seguros = SecureItemList.GetList();

            SecureItemInfo item = AppContext.Principal.SecureItems.GetItemByTipo(Convert.ToInt64(secureItem));
			Privilege privilege = AppContext.User.Licences.GetItemBySecureItem(item.Oid);

            return privilege != null ? privilege.Remove : false;

            //AppContext.User.CheckedLicences = LicenceMap.NewChildList();

            //return CanDeleteObject(item.Oid, elementos_seguros);
        }
        //public static bool CanDeleteObject(long oidSecureItem, SecureItemList secureItems)
        //{
        //    ItemLicences item = AppContext.User.VerifiedLicences.GetItem(new FCriteria<long>("OidItem", oidSecureItem, Operation.Equal));

        //    if (item != null)
        //        return AppContext.User.VerifiedLicences.GetItem(new FCriteria<long>("OidItem", oidSecureItem, Operation.Equal)).Remove;

        //    ItemLicences licence = AddVerifiedItems(oidSecureItem, secureItems);

        //    return licence != null ? licence.Remove : false;
        //}

		#endregion
	}
}
