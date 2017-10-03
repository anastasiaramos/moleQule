using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using moleQule.Library;

namespace moleQule.Face
{
    
    public class DataSourceList 
    {
        private ComboBoxSourceList _cb_list = null;

		/// <summary>
		/// Lista de sublistas de hijos filtradas por OID
		/// </summary>
		protected List<BindingSource> _childs_by_oid = new List<BindingSource>();

		public ComboBoxSourceList CBList { get { return _cb_list; } }
		
		public long CombosListCount
		{
			get { return _childs_by_oid.Count; }
		}

		public DataSourceList(ComboBoxSourceList cb_list) 
        { 
            _cb_list = cb_list; 
        }


		/// <summary>
		/// Añade una nueva lista de valores de un combo
		/// </summary>
		/// <param name="oid"></param>
		public void AddCombosList(long oid)
		{
			BindingSource source = new BindingSource();
			source.DataSource = _cb_list.GetFilteredChilds(oid); 
			_childs_by_oid.Add(source);
		}

		/// <summary>
		/// Actualiza la lista de valores de un combo
		/// </summary>
		/// <param name="index"></param>
		/// <param name="oid"></param>
		public void UpdateCombosList(int index, long oid)
		{
			_childs_by_oid[index].DataSource = _cb_list.GetFilteredChilds(oid);
		}

		public BindingSource GetCombosList(int index)
		{
            if (index >= _childs_by_oid.Count) return null;
            else return _childs_by_oid[index];
		}

		public void DeleteCombosList(int index)
		{
            _childs_by_oid.RemoveAt(index);
		}

		public ComboBoxSource GetCurrentChild(int index)
		{
            if (index >= _childs_by_oid.Count) return null;
			return (ComboBoxSource)_childs_by_oid[index].Current;
		}
       
    }
}
