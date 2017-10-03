using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;

namespace moleQule.Library
{
    public class EnumTextBase<T> 
    {
        public static ComboBoxList<T> GetList(ResourceManager r_manager) 
        { 
            return GetList(r_manager, true); 
        }

        public static ComboBoxList<T> GetList(ResourceManager r_manager, bool empty_value)
        {
            return GetList(r_manager, empty_value, false);
        }

        public static ComboBoxList<T> GetList(ResourceManager r_manager, bool empty_value, bool special_values)
        {
            return new ComboBoxList<T>(r_manager, empty_value, special_values, true, null);
        }

		public static ComboBoxList<T> GetList(ResourceManager r_manager, bool empty_value, bool special_values, bool all_value)
		{
			return new ComboBoxList<T>(r_manager, empty_value, special_values, all_value, null);
		}

		public static ComboBoxList<T> GetList(ResourceManager r_manager, T[] list)
		{
			return new ComboBoxList<T>(r_manager, list);
		}

		public static ComboBoxList<T> GetList(ResourceManager r_manager, T[] list, bool empty_value)
		{
			return new ComboBoxList<T>(r_manager, list, empty_value);
		}

        public static string GetLabel(ResourceManager r_manager, object value)
        {
            string type_name = (typeof(T)).Name;
            string label = string.Empty;

            if ((value is long) || (value is string))
            {
                throw new iQException("Utilice el tipo enumeración y no su valor entero");
                //label = type_name + "_" + ((T)value).ToString();
            }
            else
                if (value is List<T>)
                {
                    List<T> lista = value as List<T>;
                    for (int i = 0; i < lista.Count; i++)
                        label += r_manager.GetString(type_name + "_" + lista[i].ToString()) + ", ";

                    return label.Substring(0, label.Length - 2);
                }
                else
                    label = type_name + "_" + value.ToString();
            
            return r_manager.GetString(label);
        }

		public static string GetPrintLabel(ResourceManager r_manager, object value)
		{
			string type_name = (typeof(T)).Name;
			string label = string.Empty;

			if ((value is long) || (value is string))
			{
				throw new iQException("Utilice el tipo enumeración y no su valor entero");
				//label = type_name + "_" + ((T)value).ToString();
			}
			else
				label = type_name + "_PRINT_" + value.ToString();

			return r_manager.GetString(label);
		}

		public static T GetValue(string valueName)
		{
			return (T)Enum.Parse(typeof(T), valueName);
		}
    }

    public class ComboBoxList<T> : ComboBoxSourceList
    {
        public ComboBoxList(ResourceManager r_manager)
            : this(r_manager, true) {}

        public ComboBoxList(ResourceManager r_manager, bool empty_value)
            : this(r_manager, empty_value, false, true, null) {}

		public ComboBoxList(ResourceManager r_manager, T[] list)
			: this(r_manager, false, false, false, list) {}

		public ComboBoxList(ResourceManager r_manager, T[] list, bool empty_value)
			: this(r_manager, empty_value, false, false, list) { }

        public ComboBoxList(ResourceManager r_manager, bool empty_value, bool special_values, bool all_value, T[] list)
        {
            ComboBoxSource combo = null;

            string[] values = Enum.GetNames(typeof(T));

            if (empty_value)
            {
                combo = new ComboBoxSource();
                combo.Oid = -1;
                combo.Texto = r_manager.GetString("EMPTY_VALUE");
                this.Add(combo);
            }

            if (values != null)
            {
				if (list == null)
				{
					foreach (string label in values)
					{
						combo = new ComboBoxSource();

						combo.Oid = Convert.ToInt64(Enum.Parse(typeof(T), label));

						if ((!special_values) && (combo.Oid <= -2)) continue;
						if ((!all_value) && (combo.Oid == 0)) continue;

						if (combo.Oid != -1)
						{
							combo.Texto = r_manager.GetString(typeof(T).Name + "_" + label);
							if (combo.Texto == null)
								throw new iQImplementationException(typeof(T).ToString() + "." + label + " no tiene etiqueta de recurso asociada", string.Empty);

							this.Add(combo);
						}
					}
				}
				else
				{
					foreach (string label in values)
					{
						foreach (T item in list)
						{
							if (label != Enum.GetName(typeof(T), item)) continue;

							combo = new ComboBoxSource();
							combo.Oid = Convert.ToInt64(Enum.Parse(typeof(T), label));

							combo.Texto = r_manager.GetString(typeof(T).Name + "_" + label);
							if (combo.Texto == null)
								throw new iQImplementationException(typeof(T).ToString() + "." + label + " no tiene etiqueta de recurso asociada", string.Empty);

							this.Add(combo);
						}
					}
				}
            }
        }

    }
}
