using System;
using System.Collections.Generic;

using moleQule.Library.Hipatia;

namespace moleQule.Library.Common
{
    public partial class AgenteSelector : AgenteSelectorBase
    {
        #region Business Methods

        #endregion

        #region Style & Source

        public new static IAgenteHipatiaList GetAgentes(EntidadInfo entidad)
        {
            IAgenteHipatiaList lista = new IAgenteHipatiaList(new List<IAgenteHipatia>());

            if (entidad.Tipo == typeof(CuentaBancaria).Name)
            {
				CuentaBancariaList list = CuentaBancariaList.GetList(false);

				foreach (CuentaBancariaInfo obj in list)
                {
                    if (entidad.Agentes.GetItemByProperty("Oid", obj.Oid) == null)
                        lista.Add(obj);
                }
            }
            else
                throw new iQException("No se ha encontrado el tipo de entidad " + entidad.Tipo);

            return lista;
        }

        #endregion
    }
}