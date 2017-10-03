using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

using CslaEx;
using moleQule.Library;
using moleQule.Library.Security;
using moleQule.Library.Common.Resources;

namespace moleQule.Library.Common
{

	[Serializable()]
    public class AutorizationRulesControler
    {
        #region Business Methods

        public static bool CanGetObject(string secure_item)
        {
            List<ItemLicences> permisos_comprobados = new List<ItemLicences>();

            return CanGetObject(secure_item, permisos_comprobados);
        }

        public static bool CanAddObject(string secure_item)
        {
            List<ItemLicences> permisos_comprobados = new List<ItemLicences>();

            return CanAddObject(secure_item, permisos_comprobados);
        }

        public static bool CanEditObject(string secure_item)
        {
            List<ItemLicences> permisos_comprobados = new List<ItemLicences>();

            return CanEditObject(secure_item, permisos_comprobados);
        }

        public static bool CanDeleteObject(string secure_item)
        {
            List<ItemLicences> permisos_comprobados = new List<ItemLicences>();

            return CanDeleteObject(secure_item, permisos_comprobados);
        }

        public new static bool CanGetObject(string secure_item, List<ItemLicences> permisos_comprobados)
        {
            bool creado = false;

            for (int i = 0; i < permisos_comprobados.Count; i++)
            {
                if (permisos_comprobados[i].Item == Convert.ToInt64(secure_item))
                {
                    if (permisos_comprobados[i].Read)
                        return true;
                    else
                        permisos_comprobados[i].Read = true;

                    creado = true;
                    break;
                }
            }

            if (!creado)
            {
                ItemLicences nuevo = new ItemLicences();
                nuevo.Item = Convert.ToInt64(secure_item);
                nuevo.Read = true;
                permisos_comprobados.Add(nuevo);
            }

            switch (secure_item)
            {           
                //Resources.ElementosSeguros.AUXILIARES:              
                case "001":
                    return (ApplicationContextEx.User.CanReadObject(Convert.ToInt64(secure_item))
                        && CanGetObject(Resources.ElementosSeguros.EMPRESA, permisos_comprobados)
                        && moleQule.Library.AutorizationRulesControler.CanGetObject(moleQule.Library.Resources.SecureItems.VARIABLE, permisos_comprobados));
                //Resources.ElementosSeguros.EMPRESA:              
                case "002":
                    return ApplicationContextEx.User.CanReadObject(Convert.ToInt64(secure_item));
                //Resources.ElementosSeguros.REGISTRO:              
                case "003":
                    return (ApplicationContextEx.User.CanReadObject(Convert.ToInt64(secure_item))
                        && CanGetObject(Resources.ElementosSeguros.EMPRESA, permisos_comprobados)
                        && moleQule.Library.AutorizationRulesControler.CanGetObject(moleQule.Library.Resources.SecureItems.VARIABLE, permisos_comprobados));
            }

            return false;

        }

        public new static bool CanAddObject(string secure_item, List<ItemLicences> permisos_comprobados)
        {
            bool creado = false;

            for (int i = 0; i < permisos_comprobados.Count; i++)
            {
                if (permisos_comprobados[i].Item == Convert.ToInt64(secure_item))
                {
                    if (permisos_comprobados[i].Create)
                        return true;
                    else
                        permisos_comprobados[i].Create = true;

                    creado = true;
                    break;
                }
            }

            if (!creado)
            {
                ItemLicences nuevo = new ItemLicences();
                nuevo.Item = Convert.ToInt64(secure_item);
                nuevo.Create = true;
                permisos_comprobados.Add(nuevo);
            }

            switch (secure_item)
            {
                //Resources.ElementosSeguros.AUXILIARES:              
                case "001":
                    return (ApplicationContextEx.User.CanCreateObject(Convert.ToInt64(secure_item))
                        && CanGetObject(Resources.ElementosSeguros.EMPRESA, permisos_comprobados)
                        && moleQule.Library.AutorizationRulesControler.CanGetObject(moleQule.Library.Resources.SecureItems.VARIABLE, permisos_comprobados));
                //Resources.ElementosSeguros.EMPRESA:              
                case "002":
                    return ApplicationContextEx.User.CanCreateObject(Convert.ToInt64(secure_item));
                //Resources.ElementosSeguros.REGISTRO:              
                case "003":
                    return (ApplicationContextEx.User.CanCreateObject(Convert.ToInt64(secure_item))
                        && CanGetObject(Resources.ElementosSeguros.EMPRESA, permisos_comprobados)
                        && moleQule.Library.AutorizationRulesControler.CanGetObject(moleQule.Library.Resources.SecureItems.VARIABLE, permisos_comprobados));
            }

            return false;

        }

        public new static bool CanEditObject(string secure_item, List<ItemLicences> permisos_comprobados)
        {
            bool creado = false;

            for (int i = 0; i < permisos_comprobados.Count; i++)
            {
                if (permisos_comprobados[i].Item == Convert.ToInt64(secure_item))
                {
                    if (permisos_comprobados[i].Modify)
                        return true;
                    else
                        permisos_comprobados[i].Modify = true;

                    creado = true;
                    break;
                }
            }

            if (!creado)
            {
                ItemLicences nuevo = new ItemLicences();
                nuevo.Item = Convert.ToInt64(secure_item);
                nuevo.Modify = true;
                permisos_comprobados.Add(nuevo);
            }

            switch (secure_item)
            {
                //Resources.ElementosSeguros.AUXILIARES:              
                case "001":
                    return (ApplicationContextEx.User.CanModifyObject(Convert.ToInt64(secure_item))
                        && CanGetObject(Resources.ElementosSeguros.EMPRESA, permisos_comprobados)
                        && moleQule.Library.AutorizationRulesControler.CanGetObject(moleQule.Library.Resources.SecureItems.VARIABLE, permisos_comprobados));
                //Resources.ElementosSeguros.EMPRESA:              
                case "002":
                    return ApplicationContextEx.User.CanModifyObject(Convert.ToInt64(secure_item));
                //Resources.ElementosSeguros.REGISTRO:              
                case "003":
                    return (ApplicationContextEx.User.CanModifyObject(Convert.ToInt64(secure_item))
                        && CanGetObject(Resources.ElementosSeguros.EMPRESA, permisos_comprobados)
                        && moleQule.Library.AutorizationRulesControler.CanGetObject(moleQule.Library.Resources.SecureItems.VARIABLE, permisos_comprobados));
            }

            return false;
        }

        public new static bool CanDeleteObject(string secure_item, List<ItemLicences> permisos_comprobados)
        {
            bool creado = false;

            for (int i = 0; i < permisos_comprobados.Count; i++)
            {
                if (permisos_comprobados[i].Item == Convert.ToInt64(secure_item))
                {
                    if (permisos_comprobados[i].Remove)
                        return true;
                    else
                        permisos_comprobados[i].Remove = true;

                    creado = true;
                    break;
                }
            }

            if (!creado)
            {
                ItemLicences nuevo = new ItemLicences();
                nuevo.Item = Convert.ToInt64(secure_item);
                nuevo.Remove = true;
                permisos_comprobados.Add(nuevo);
            }

            switch (secure_item)
            {
                //Resources.ElementosSeguros.AUXILIARES:              
                case "001":
                    return (ApplicationContextEx.User.CanRemoveObject(Convert.ToInt64(secure_item))
                        && CanGetObject(Resources.ElementosSeguros.EMPRESA, permisos_comprobados)
                        && moleQule.Library.AutorizationRulesControler.CanGetObject(moleQule.Library.Resources.SecureItems.VARIABLE, permisos_comprobados));
                //Resources.ElementosSeguros.EMPRESA:              
                case "002":
                    return ApplicationContextEx.User.CanRemoveObject(Convert.ToInt64(secure_item));
                //Resources.ElementosSeguros.REGISTRO:              
                case "003":
                    return (ApplicationContextEx.User.CanRemoveObject(Convert.ToInt64(secure_item))
                        && CanGetObject(Resources.ElementosSeguros.EMPRESA, permisos_comprobados)
                        && moleQule.Library.AutorizationRulesControler.CanGetObject(moleQule.Library.Resources.SecureItems.VARIABLE, permisos_comprobados));
            }

            return false;

        }

		#endregion
	}
}
