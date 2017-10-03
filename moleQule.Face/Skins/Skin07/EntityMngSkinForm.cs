using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx;
using moleQule.Library;

namespace moleQule.Face.Skin07
{
	public partial class EntityMngSkinForm<T, C> : Skin05.EntityMngSkinForm
		where T : ReadOnlyListBaseEx<T, C>
		where C : ReadOnlyBaseEx<C>
	{
		#region Attributes & Properties

		protected new SortedBindingList<C> _filter_results = null;
		protected new SortedBindingList<C> _sorted_list = null;
		protected new SortedBindingList<C> _search_results = null;
		protected new T _last_results = null;

		protected new T List
		{
			get { return _item_list as T; }
			set { _item_list = value; _sorted_list = (_item_list as T).GetSortedList(); _filter_results = _sorted_list; _last_results = value; }
		}

		protected SortedBindingList<C> FilterResults
		{
			get { return _filter_results; }
			set { _filter_results = value; base._filter_results = _filter_results; }
		}
		protected SortedBindingList<C> SearchResults
		{
			get { return _search_results; }
			set { _search_results = value; base._search_results = _search_results; }
		}
		protected T LastResults
		{
			get { return _last_results; }
			set { _last_results = value; base._last_results = _last_results; }
		}

		protected new T FilteredList { get { return (_filter_results == null) ? null : (T)InvokeStaticMethod("GetList", new object[1] { _filter_results }); } }
		protected SortedBindingList<C> CurrentList { get { return (Datos == null) ? null : (Datos.List as SortedBindingList<C>); } }
		protected new SortedBindingList<C> SortedList { get { return _sorted_list; } }

		public override long ActiveOID { get { return (ActiveItem != null) ? ActiveItem.Oid : -1; } }
		public C ActiveItem { get { return (Datos == null) ? null : ((Datos.Current != null) ? (C)Datos.Current : null); } }
		public override long ActiveFoundOID { get { return (DatosSearch == null) ? -1 : ((DatosSearch.Current != null) ? ((C)(DatosSearch.Current)).Oid : -1); } }

		#endregion

		#region Factory Methods

		public EntityMngSkinForm()
			: this(false) { }

		public EntityMngSkinForm(bool isModal)
			: this(isModal, null) { }

		public EntityMngSkinForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public EntityMngSkinForm(bool isModal, Form parent, object list)
			: base(isModal, parent, list) 
		{
			InitializeComponent();
		}

		protected override void SetMainDataGridView(DataGridView grid)
		{
			TablaBase = grid;

			this.TablaBase.DoubleClick += new System.EventHandler(this.TablaBase_DoubleClick);
			this.TablaBase.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TablaBase_CellClick);
			this.TablaBase.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.TablaBase_ColumnHeaderMouseClick);
			this.TablaBase.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.TablaBase_RowPrePaint);
			this.TablaBase.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TablaBase_KeyPress);
		}

		internal object InvokeStaticMethod(string methodName, object[] args)
		{
			return typeof(T).InvokeMember(methodName, BindingFlags.Public | BindingFlags.Static | BindingFlags.InvokeMethod, null, null, args);
		}

		#endregion

		#region Source

		protected override void RefreshSources()
		{
			switch (FilterType)
			{
				case IFilterType.None:
					SetMainList(_sorted_list, SortProperty, SortDirection, true);
					PgMng.Grow(string.Empty, "Ordenar Lista");
					break;

				case IFilterType.Filter:
					SetMainList(_filter_results, SortProperty, SortDirection, true);
					PgMng.Grow(string.Empty, "Ordenar Lista");
					break;
			}

			base.RefreshSources();
		}

		/// <summary>
		/// Selecciona un elemento de la tabla
		/// </summary>
		/// <param name="oid">Identificar del elemento</param>
		protected override void Select(long oid)
		{
			int foundIndex = Datos.IndexOf(List.GetItem(oid));
			Datos.Position = foundIndex;
		}

		#endregion

		#region Filter

		protected override bool DoFilter(FilterItem fItem)
		{
			FilterResults = Localize(fItem);
			return FilterResults != null;
		}

		protected new SortedBindingList<C> Localize(FilterItem item)
		{
			SortedBindingList<C> list = null;
			T sourceList = null;

			switch (FilterType)
			{
				case IFilterType.None:
					if (List == null) return null;
					sourceList = (T)_item_list;
					break;

				case IFilterType.Filter:
					if (FilteredList == null) return null;
					sourceList = (((IList)FilteredList).Count > 0) ? (T)FilteredList : (T)_last_results;
					break;

				case IFilterType.FilterBack:
					sourceList = (T)_item_list;
					break;
			}

			if (item.FilterProperty == IFilterProperty.All)
			{
                FCriteria criteria = GetCriteria(string.Empty, item.Value, item.SecondValue, item.Operation);
				list = sourceList.GetSortedSubList(criteria, _properties_list);
			}
			else
			{
                FCriteria criteria = GetCriteria(item.Column, item.Value, item.SecondValue, item.Operation);
				list = sourceList.GetSortedSubList(criteria, _properties_list);
			}

			LastResults = sourceList;

			DatosSearch.DataSource = list;
			DatosSearch.MoveFirst();

			AddFilterItem(item);

			return list;
		}

		protected override void SetFilter(bool on)
		{
			//Esto es sustituible por la llamada a la funcion del EntityMngBaseForm
			//no se hace por compatibilidad con los Skin04.EntityMngSkinForm
			try
			{
				SetMainList(on ? _filter_results : _sorted_list, SortProperty, SortDirection, true);
			}
			catch (Exception)
			{
				SetMainList(_sorted_list, SortProperty, SortDirection, true);
			}

			base.SetFilter(on);
		}

		#endregion

		#region Events

		private void TablaBase_DoubleClick(object sender, EventArgs e)
		{
			ExecuteAction(molAction.Default);
		}

		private void TablaBase_KeyPress(object sender, KeyPressEventArgs e)
		{
			FilterByKey(e.KeyChar.ToString());
		}

		private void TablaBase_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			SetColumnActive(TablaBase.Columns[e.ColumnIndex]);
		}

		private void TablaBase_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			SetColumnActive();
		}

		private void TablaBase_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			if (e.RowIndex < 0) return;
			if (!_show_colors) return;

			SetRowFormat(TablaBase.Rows[e.RowIndex]);
		}

		#endregion
	}
}
