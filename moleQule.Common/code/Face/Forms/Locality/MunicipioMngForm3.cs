using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using CslaEx;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class MunicipioMngForm : Skin02.EntityLMngSkinForm
    {

        #region Attributes & Properties
		
        public const string ID = "MunicipioMngForm";
		public static Type Type { get { return typeof(MunicipioMngForm); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }		
		
		public Municipio _entity;
		
		private new SortedBindingList<MunicipioInfo> _filter_results = null;
        private new SortedBindingList<MunicipioInfo> _sorted_list = null;
        private new SortedBindingList<MunicipioInfo> _search_results = null;
		
        private new MunicipioList List 
        { 
            get { return _item_list as MunicipioList; }
            set { _item_list = value; _sorted_list = (value as MunicipioList).GetSortedList(); } 
        }
        private new SortedBindingList<MunicipioInfo> SortedList { get { return _sorted_list; } }
        private new MunicipioList  FilteredList { get { return  MunicipioList.GetList(_filter_results); } }
		public SortedBindingList<MunicipioInfo> CurrentList { get { return (Datos.List as SortedBindingList<MunicipioInfo>); } }
		
		/// <summary>
		/// Devuelve el OID del objeto activo seleccionado de la tabla
		/// </summary>
		/// <returns></returns>
		public override long ActiveOID { get { return ActiveItem != null ? ActiveItem.Oid : -1; } }

        /// <summary>
        /// Devuelve el objeto activo seleccionado de la tabla
        /// </summary>
        /// <returns></returns>
        public MunicipioInfo ActiveItem { get { return (Datos.Current != null) ? Datos.Current as MunicipioInfo : null; } }
		
        public override long ActiveFoundOID { get { return (DatosSearch.Current != null) ? ((MunicipioInfo)(DatosSearch.Current)).Oid : -1; } }

        public override string SortProperty { get { return this.GetGridSortProperty(Tabla); } }
        public override ListSortDirection SortDirection { get { return this.GetGridSortDirection(Tabla); } }
		
		#endregion
		
		#region Factory Methods

		public MunicipioMngForm()
            : this(false) {}

		public MunicipioMngForm(bool isModal)
			: this(isModal, null) {}
		
		public MunicipioMngForm(bool isModal, Form parent)
			: this(isModal, parent, null) {}
		
		public MunicipioMngForm(bool isModal, Form parent, MunicipioList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
			
			TablaBase = Tabla;
			
			base.SortProperty = Localidad.DataPropertyName;
        }
		
		#endregion

        #region Business Methods

        protected override Type GetColumnType(string column_name)
        {
            return Tabla.Columns[column_name] != null ? Tabla.Columns[column_name].ValueType : null;
        }
        
        protected override string GetColumnProperty(string column_name)
        {
            return Tabla.Columns[column_name] != null ? Tabla.Columns[column_name].DataPropertyName : null;
        }

        #endregion
		
		#region Autorizacion

		/// <summary>Aplica las reglas de validación de usuarios al formulario.
		/// <returns>void</returns>
		/// </summary>
		protected override void ApplyAuthorizationRules() 
		{
		}

		#endregion

		#region Style & Format

		public override void FormatControls()
		{
            if (Tabla == null) return;
			
			base.FormatControls();

			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Localidad.Tag = 0.5;
			Municipio.Tag = 0.5;

            cols.Add(Localidad);
			cols.Add(Municipio);

            ControlsMng.MaximizeColumns(Tabla, cols);

            ControlsMng.OrderByColumn(Tabla, Municipio, ListSortDirection.Ascending);
            ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla));			
			
			Fields_CB.Text = ControlsMng.GetCurrentColumn(Tabla).HeaderText;
			
			SetGridFormat();
		}
		
		protected override void SetRowFormat(DataGridViewRow row)
        {
            /*if (row.IsNewRow) return;
            MunicipioInfo item = (MunicipioInfo)row.DataBoundItem;*/
        }
		
		#endregion

		#region Source
		
        protected void SetMainList(SortedBindingList<MunicipioInfo> list, bool order)
        {
            base.SortProperty = SortProperty;
            base.SortDirection = SortDirection;

            Datos.DataSource = list;
			Datos.ResetBindings(true);

            if (order)
            {
                ControlsMng.OrderByColumn(Tabla, Tabla.Columns[base.SortProperty], base.SortDirection);
                ControlsMng.SetCurrentCell(Tabla);
            }

            SetGridFormat();
        }

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Municipio");
			
			long oid = ActiveOID;
			
			
			switch (DataType)
            { 
                case EntityMngFormTypeData.Default:
                    List = MunicipioList.GetList(false);
                    break;
					
                case EntityMngFormTypeData.ByParameter:
                    _sorted_list = List.GetSortedList();
                    break;					
            } 
			PgMng.Grow(string.Empty, "Lista de Municipios");
		}
		
        protected override void RefreshSources()
        {
            switch (FilterType)
            {
                case IFilterType.None:
                    SetMainList(_sorted_list, true);
                    PgMng.Grow(string.Empty, "Ordenar Lista");
                    break;

                case IFilterType.Filter:
                    SetMainList(_filter_results, true);
                    PgMng.Grow(string.Empty, "Ordenar Lista");
                    break;
            }
            base.RefreshSources();
        }

        public override void UpdateList()
        {
            switch (_current_action)
            {
                case molAction.Add:
                    if (_entity == null) return;
                    List.AddItem(_entity.GetInfo());
                    if (FilterType == IFilterType.Filter)
                    {
                        MunicipioList listA = MunicipioList.GetList(_filter_results);
                        listA.AddItem(_entity.GetInfo());
                        _filter_results = listA.GetSortedList();
                    }
                    break;

                case molAction.Edit:
                case molAction.Lock:
                case molAction.Unlock:
                    if (_entity == null) return;
                    ActiveItem.CopyFrom(_entity);
                    break;

                case molAction.Delete:
                    if (ActiveItem == null) return;
                    List.RemoveItem(ActiveOID);
                    if (FilterType == IFilterType.Filter)
                    {
                        MunicipioList listD = MunicipioList.GetList(_filter_results);
                        listD.RemoveItem(ActiveOID);
                        _filter_results = listD.GetSortedList();
                    }
                    break;
            }

            _entity = null;
            RefreshSources();
        }	
						
		protected override void Select(long oid)
		{
			int foundIndex = Datos.IndexOf(List.GetItem(oid));
			Datos.Position = foundIndex;
		}

		protected override void SetFilter(bool on)
		{
			try
			{
                SetMainList(on ? _filter_results : _sorted_list, true);
			}
			catch (Exception)
			{
                SetMainList(_sorted_list, true);
			}
			
			base.SetFilter(on);
		} 

		#endregion

        #region Actions

        public override void OpenAddForm()
        {
            try
            {
                MunicipioAddForm form = new MunicipioAddForm(this);
                AddForm(form);
                _entity = form.Entity;
            }
            catch (Csla.DataPortalException ex)
            {
                MessageBox.Show(ex.BusinessException.ToString(),
                                moleQule.Face.Resources.Labels.ERROR_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),
                                moleQule.Face.Resources.Labels.ERROR_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        public override void OpenViewForm()
        {
            try
            {
                AddForm(new MunicipioViewForm(ActiveOID, this));
            }
            catch (Csla.DataPortalException ex)
            {
                MessageBox.Show(ex.BusinessException.ToString(),
                                moleQule.Face.Resources.Labels.ERROR_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),
                                moleQule.Face.Resources.Labels.ERROR_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        public override void OpenEditForm()
        {
            try
            {
                MunicipioEditForm form = new MunicipioEditForm(ActiveOID, this);
                if (form.Entity != null)
                {
                    AddForm(form);
                    _entity = form.Entity;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(),
                                moleQule.Face.Resources.Labels.ERROR_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

        public override void DeleteObject(long oid)
        {

            if (MessageBox.Show(moleQule.Face.Resources.Messages.DELETE_CONFIRM,
                                moleQule.Face.Resources.Labels.ADVISE_TITLE,
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Library.Common.Municipio.Delete(oid);
                    _action_result = DialogResult.OK;

                    //Se eliminan todos los formularios de ese objeto
                    foreach (EntityDriverForm form in _list_active_form)
                    {
                        if (form is ItemMngBaseForm)
                        {
                            if (((ItemMngBaseForm)form).Oid == oid)
                            {
                                form.Dispose();
                                break;
                            }
                        }
                    }
                }
                catch (DataPortalException ex)
                {
                    MessageBox.Show(iQExceptionHandler.GetiQException(ex).Message);
                }
            }
        }

        protected override bool DoFind(object value) 
        {
            _search_results = Localize(value, ((DataGridViewColumn)(Fields_CB.SelectedItem)).Name);
            return _search_results != null; 
        }

        protected override bool DoFilter(object value) 
        {
            _filter_results = Localize(value, ((DataGridViewColumn)(Fields_CB.SelectedItem)).Name);
            return _filter_results != null; 
        }
        
        protected override bool DoFilterByFirst(string value, string column_name) 
        { 	
			if (column_name == null)
				column_name = ControlsMng.GetCurrentColumn(Tabla).Name;
			
            _filter_results = Localize(value, column_name); 
            return _filter_results != null; 
        }

        protected new SortedBindingList<MunicipioInfo> Localize(object value, string column_name)
        {
            SortedBindingList<MunicipioInfo> list = null;
				
            MunicipioList sourceList = null;

            switch (FilterType)
            {
                case IFilterType.None:
                    if (List == null)
                    {
                        MessageBox.Show(moleQule.Face.Resources.Messages.NO_RESULTS);
                        return null;
                    }
                    sourceList = List;
                    break;

                case IFilterType.Filter:
                    if (FilteredList == null)
                    {
                        MessageBox.Show(moleQule.Face.Resources.Messages.NO_RESULTS);
                        return null;
                    }
                    sourceList = FilteredList;
                    break;
            }

            FCriteria criteria = null;

            string related = "none";

            switch (column_name)
            {		
						
                default:		
					criteria = GetCriteria(column_name, value, _operation);
                    break;
            }

            switch (related)
            {	
				case "none":
                    list = sourceList.GetSortedSubList(criteria);
				    break;
            }

            if (list.Count == 0)
            {
                MessageBox.Show(moleQule.Face.Resources.Messages.NO_RESULTS);
                return sourceList.GetSortedList();
            }

            DatosSearch.DataSource = list;
            DatosSearch.MoveFirst();

			AddFilterLabel(column_name, value);

            return list;
        }

		#endregion

		#region Events
		
        private void Tabla_KeyPress(object sender, KeyPressEventArgs e)
        {
            FilterByKey(e.KeyChar.ToString());
        }
		
		private void Tabla_DoubleClick(object sender, EventArgs e)
		{
			ExecuteAction(molAction.Default);
		}

        private void Tabla_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ControlsMng.SetCurrentCell(Tabla);
            ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla));
			Fields_CB.Text = ControlsMng.GetCurrentColumn(Tabla).HeaderText;
			SetGridFormat();
        }
        
        private void Tabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla));
			Fields_CB.Text = ControlsMng.GetCurrentColumn(Tabla).HeaderText;
        }
		
		#endregion

    }
}
