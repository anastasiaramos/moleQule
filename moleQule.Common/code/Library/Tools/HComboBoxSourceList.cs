using System;
using System.Collections.Generic;
using System.Text;

using moleQule.Library;

namespace moleQule.Library.Common
{
    public class HComboBoxSourceList : ComboBoxSourceList
    {
        public HComboBoxSourceList() { }

        public HComboBoxSourceList(MunicipioList lista)
        {
            foreach (MunicipioInfo item in lista)
            {
                ComboBoxSource combo = new ComboBoxSource();
                combo.Texto = item.Nombre;
                combo.Oid = item.Oid;
                this.Add(combo);
            }

        }
    }
}
