using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Face.Skin04
{
	/// <summary>
	/// Clase Base para Gestión de un Tipo de Entidad. 
	/// Consulta, Creación, Edición, Borrado, Filtrado y Localización.
	/// Se gestiona mediante una Lista de Elementos de ese tipo
	/// </summary>
	public partial class SchemaMngSkinForm : SchemaMngBaseForm
	{
		#region Attributes

		private bool _suspend_event = false;
		protected List<string> _properties_list = new List<string>();

		#endregion

		#region Factory Methods

		/// <summary>
        /// Definido solo por compatibilidad con el IDE
        /// </summary>
		public SchemaMngSkinForm()
			: this(false, null, null, null) {}

		public SchemaMngSkinForm(bool isModal, Form parent, Type type, object list)
            : base(isModal, parent, type, list)
        {
            InitializeComponent();
			ViewMode = molView.Normal;
        }

		#endregion

		#region Authorization

		protected override void ActivateAction(molAction action, bool state)
		{
			if (EntityType == null) return;

			bool allow;

			switch (action)
			{
				case molAction.Add:

					allow = allow = AppContext.User.IsAdmin;

					Add_Button.Enabled = (allow) ? state : false;
					Add_Button.Visible = (allow) ? state : false;
					Nuevo_MI.Enabled = (allow) ? state : false;

					break;

				case molAction.AdvancedSearch:
					ShowSearchPanel(state);
					break;

				case molAction.View:

					allow = (bool)EntityType.InvokeMember("CanGetObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					View_Button.Enabled = (allow) ? state : false;
					View_Button.Visible = (allow) ? state : false;
					Detalle_MI.Enabled = (allow) ? state : false;

					break;

				case molAction.Edit:

					allow = AppContext.User.IsAdmin;

					Edit_Button.Enabled = (allow) ? state : false;
					Edit_Button.Visible = (allow) ? state : false;
					Modificar_MI.Enabled = (allow) ? state : false;

					break;

				case molAction.Delete:

					allow = AppContext.User.IsAdmin;

					Delete_Button.Enabled = (allow) ? state : false;
					Delete_Button.Visible = (allow) ? state : false;
					Borrar_MI.Enabled = (allow) ? state : false;

					break;

				case molAction.Copy:

					allow = AppContext.User.IsAdmin;

					Copy_MI.Visible = (allow) ? state : false;
					Copy_TI.Visible = (allow) ? state : false;

					break;

				case molAction.ChangeStateAnulado:

					allow = AppContext.User.IsAdmin;

					ChangeState_TI.Visible = (ChangeState_TI.Visible) ? true : state;
					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Anulado_TI.Visible = (allow) ? state : false;
					Anulado_MI.Visible = (allow) ? state : false;

					break;

				case molAction.FilterOn:
				case molAction.FilterOff:
					Filtrar_MI.Visible = state;
					FilterValue_TI.Visible = state;
					FilterOff_MI.Visible = state;
					FilterOff_TI.Visible = state;
					Separator3_TI.Visible = state;
					break;

				case molAction.Print:
					Print_Button.Enabled = state;
					Print_Button.Visible = state;

					Imprimir_MI.Enabled = state;
					Imprimir_MI.Visible = state;

					Separator2_TI.Enabled = state;
					Separator2_TI.Visible = state;

					break;

				case molAction.PrintDetail:
					PrintDetail_MI.Visible = state;
					PrintDetail_MI.Enabled = state;

					PrintDetail_TI.Visible = state;
					PrintDetail_TI.Enabled = state;

					break;

				case molAction.Refresh:

					Refresh_TI.Visible = state;
					Refresh_MI.Visible = state;
					Separator5_TI.Visible = state;

					break;

				case molAction.ShowDocuments:

					Docs_TI.Visible = state;
					Docs_TSS.Visible = state;

					Docs_MI.Visible = state;
					Docs_MSS.Visible = state;

					break;

				case molAction.Unlock:

					ChangeState_TI.Visible = (ChangeState_TI.Visible) ? true : state;
					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Abierto_TI.Enabled = state;
					Abierto_MI.Enabled = state;
					Abierto_TI.Visible = state;
					Abierto_MI.Visible = state;

					if ((AppContext.User != null) && (state))
					{
						Abierto_TI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
						Abierto_MI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
					}

					break;

				case molAction.Select:
					Select_TI.Visible = state;
					break;

				case molAction.Close:
					Close_TI.Enabled = state;
					break;
			}
		}

		#endregion
		
		#region Source

		protected override void RefreshSources()
        {
			//Design time compatibility
			if (TablaBase == null) return;

            DatosSearch.DataSource = Datos.DataSource;

            Fields_CB.DataSource = TablaBase.Columns;
            Fields_CB.DisplayMember = "HeaderText";
            Fields_CB.ValueMember = "DataPropertyName";

			_properties_list = ControlsMng.GetPropertiesList(TablaBase);

            if (_selectedOid > 0) Select(_selectedOid);
        }

        #endregion

        #region Layout & Format

		public override void FormatControls()
		{
			base.FormatControls();

			//IDE Compatibility
			if (TablaBase == null) return;

			if (SortProperty == string.Empty)
				SortProperty = (TablaBase.Columns.Count > 0) ? TablaBase.Columns[0].DataPropertyName : string.Empty;

			Content_Panel.IsSplitterFixed = false;
			Content_Panel.SplitterDistance = Content_Panel.Height - Content_Panel.SplitterWidth
																	- Navegador.Height
																	- 4 /*Margen*/;
			Content_Panel.IsSplitterFixed = true;

			bool collapse = Main_Panel.Panel1Collapsed;
			Main_Panel.Panel1Collapsed = false;
			Letras_Panel.Left = (Main_Panel.Panel1.Width - Letras_Panel.Width) / 2;
			Letras_Panel.Left = Letras_Panel.Left < 0 ? 0 : Letras_Panel.Left;
			Main_Panel.Panel1Collapsed = collapse;

			collapse = Content_Panel.Panel2Collapsed;
			Content_Panel.Panel2Collapsed = false;
			Campos_Panel.Left = (Content_Panel.Panel2.Width - Campos_Panel.Width) / 2;
			Campos_Panel.Left = Campos_Panel.Left < 0 ? 0 : Campos_Panel.Left;
			Content_Panel.Panel2Collapsed = collapse;
		}

		protected override void FormatControl(Control ctl)
		{
			if (ctl is DataGridView)
			{
				((DataGridView)ctl).ColumnHeadersDefaultCellStyle = ControlTools.Instance.HeaderStyle;
				((DataGridView)ctl).DefaultCellStyle = ControlTools.Instance.BasicStyle;
				((DataGridView)ctl).RowHeadersVisible = true;
				((DataGridView)ctl).ColumnHeadersHeight = 34;
				((DataGridView)ctl).RowHeadersWidth = 25;
				((DataGridView)ctl).AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
				((DataGridView)ctl).SelectionMode = DataGridViewSelectionMode.FullRowSelect;
				((DataGridView)ctl).MultiSelect = false;

				foreach (DataGridViewColumn col in ((DataGridView)ctl).Columns)
				{
					if ((col.Tag != null) && (col.Tag.ToString().ToUpper() == Resources.Consts.NO_FORMAT)) continue;

					if ((col.ValueType != null) && ((col.DefaultCellStyle.Format == string.Empty)))
					{
						if (col.ValueType.Equals(typeof(long)))
							col.DefaultCellStyle = ControlTools.Instance.LongStyle;

						if (col.ValueType.Equals(typeof(double)))
							col.DefaultCellStyle = ControlTools.Instance.DecimalStyle;

						if (col.ValueType.Equals(typeof(decimal)))
							col.DefaultCellStyle = ControlTools.Instance.DecimalStyle;

						if (col.ValueType.Equals(typeof(DateTime)))
							col.DefaultCellStyle = ControlTools.Instance.DateStyle;
					}
				}
			}
			else
				base.FormatControl(ctl);
		}

		protected void SetColumnActive() { SetColumnActive(ControlsMng.GetCurrentColumn(TablaBase)); }
		protected override void SetColumnActive(DataGridViewColumn col)
		{
			base.SetColumnActive(col);
			Fields_CB.Text = ControlsMng.GetCurrentColumn(TablaBase).HeaderText;
		}

		protected override void SetView(molView view)
		{
			switch (view)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.View);
					HideAction(molAction.Edit);
					HideAction(molAction.Delete);
					HideAction(molAction.Print);
					HideAction(molAction.Copy);
					ShowAction(molAction.Select);
					HideAction(molAction.Close);
					HideAction(molAction.Unlock);
					HideAction(molAction.Print);
					HideAction(molAction.PrintDetail);

					MaximizeForm(new Size(1024, 600));
					ControlBox = false;

					break;

				case molView.Normal:

					ShowAction(molAction.Add);
					ShowAction(molAction.View);
					ShowAction(molAction.Edit);
					ShowAction(molAction.Delete);
					HideAction(molAction.Print);
					HideAction(molAction.Copy);
					ShowAction(molAction.Select);
					ShowAction(molAction.Close);
					HideAction(molAction.Unlock);
					HideAction(molAction.Print);
					HideAction(molAction.PrintDetail);

					MaximizeForm();

					break;
			}
		}

		protected virtual void ShowSearchPanel(bool show)
		{
			Search_Panel.Panel2Collapsed = !show;
			Filters_LB.Top = 0;
			Filters_LB.Height = show ? 20 : 1;
			Main_Panel.SplitterDistance = show ? 115 : Main_Panel.Panel1MinSize;
			Main_Panel.SplitterWidth = show ? 4 : 1;
			AdvSearch_BT.Tag = (show) ? "Hide" : "Show";
			AdvSearch_BT.Image = (show) ? Properties.Resources.advSearchOff_16 : Properties.Resources.advSearchOn_16;
			AdvSearch_TI.Image = (show) ? Properties.Resources.avdSearchOff : Properties.Resources.advSearchOn;
		}

		#endregion

        #region Business Methods

        private void SetOperator()
        {
            if (Operador_CB.SelectedItem == null) _operation = Operation.Equal;

            string op = Operador_CB.SelectedItem.ToString();

			if (op == Library.CslaEx.EnumText.GetString(Operation.Contains)) { _operation = Operation.Contains; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.StartsWith)) { _operation = Operation.StartsWith; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.Less)) { _operation = Operation.Less; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.LessOrEqual)) { _operation = Operation.LessOrEqual; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.Greater)) { _operation = Operation.Greater; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.GreaterOrEqual)) { _operation = Operation.GreaterOrEqual; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.Equal)) { _operation = Operation.Equal; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.Distinct)) { _operation = Operation.Distinct; return; }
        }

        #endregion

		#region Buttons

		private void Add_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Add); }

		private void AvdSearch_TI_Click(object sender, EventArgs e)
		{
			if (AdvSearch_BT.Tag.ToString() == "Show")
				ShowAction(molAction.AdvancedSearch);
			else
				HideAction(molAction.AdvancedSearch);
		}

		private void Buscar_Button_Click(object sender, EventArgs e) { FindItems(_search_value); }

		private void Close_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Close); }

		private void Copy_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Copy); }

		private void CustomAction1_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction1); }

		private void CustomAction2_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction2); }

		private void Default_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Default); }

		private void Delete_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Delete); }

		private void Edit_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Edit); }		

		private void EmailPDF_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.EmailPDF); }

		private void EmailLink_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.EmailLink); }

		private void Emitido_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ChangeStateEmitido); }

		private void ExportPDF_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ExportPDF); }

		private void Filtrar_TI_Click(object sender, EventArgs e)
		{
			_search_value = TablaBase.CurrentCell.Value;
			ExecuteAction(molAction.FilterOn);
		}

		private void FilterOff_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.FilterOff); }

		private void FilterOn_BT_Click(object sender, EventArgs e)
		{
			SetColumnActive(ControlsMng.GetColumn(TablaBase, Fields_CB.SelectedValue.ToString()));
			ExecuteAction(molAction.FilterOn);
		}

		private void FilterOff_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.FilterOff); }

		private void Letra_Click(object sender, EventArgs e)
		{
			_filter_property = IFilterProperty.ByParamenter;
			FilterByFirst(((Button)(sender)).Text);
		}

		private void Print_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

		private void PrintDetail_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.PrintDetail); }
		
		private void PrintQR_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.PrintListQR); }

		private void Refresh_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Refresh); }

		private void Select_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Select); }

		private void SelectAll_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.SelectAll); }

		private void StateAnulado_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ChangeStateAnulado); }

		private void StateUnlock_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Unlock); }

		private void View_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.View); }

		#endregion

        #region Actions

		protected override void DefaultAction() { SetDefault(); }

		public override void DeleteAction()
		{
			try
			{
				if (this.Datos.Count > 0)
					DeleteObject(ActiveOID);

				FormMngBase.Instance.RefreshFormsData();
			}
			catch (iQImplementationException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

        public override void FilterOffAction()
        {
            base.FilterOffAction();
            SetOperator();

			Filters_LB.Items.Clear();
        }

		public override void RefreshAction() 
		{
			PgMng.Reset(4, 1, Resources.Messages.LOADING_DATA, this);

			RefreshMainData();
			
			FilterAllAction();

			PgMng.FillUp();
		}

		/*public override void SelectObject()
		{
			try
			{
				if (this.Datos.Count > 0)
					LoadSchema();
			}
			catch (iQImplementationException ex)
			{
				PgMng.ShowErrorException(ex);
			}
		}*/

		#endregion

		#region Find & Filter

		/*protected override void FilterByKey(string key)
		{
			DataGridViewColumn column = ControlsMng.GetCurrentColumn(TablaBase);

			string keys;

			switch (key)
			{
				case "\b":
					if (_filter_keys.Length > 1)
						keys = (_active_column != column) ? string.Empty : _filter_keys.Substring(0, _filter_keys.Length - 1);
					else
						keys = string.Empty;
					break;

				default:
					keys = (_active_column != column) ? string.Empty : _filter_keys + key;
					break;
			}

			_suspend_event = true;
			Search_TB.Text = keys;
			_suspend_event = false;

			base.FilterByKey(key);
		}*/

		protected override void FilterByKey(string key)
		{
			switch (key)
			{
				case "\b":

					_filter_type = IFilterType.FilterBack;

					_filter_keys = (_filter_keys.Length > 1) ? _filter_keys.Substring(0, _filter_keys.Length - 1) : string.Empty;

					break;

				default:

					_filter_type = IFilterType.Filter;

					_filter_keys = _filter_keys + key;

					break;
			}

			_suspend_event = true;
			Search_TB.Text = _filter_keys;
			_suspend_event = false;

			if (_filter_keys != string.Empty)
				ExecuteAction(molAction.FilterGlobal);
			else
				ExecuteAction(molAction.FilterOff);
		}

		// DEPRECATED
		protected void AddFilterLabel(FilterItem fItem) { AddFilterItem(fItem); }

		protected override void AddFilterItem(FilterItem fItem)
		{
			base.AddFilterItem(fItem);

			Filters_LB.Items.Clear();
			foreach (FilterItem item in FilterList)
			{
				_suspend_event = true;
				Filters_LB.Items.Add(item.Text, item.Active);
				int width = (int)Filters_LB.CreateGraphics().MeasureString(item.Text, Filters_LB.Font).Width + 5;
				Filters_LB.ColumnWidth = (width > Filters_LB.ColumnWidth) ? width : Filters_LB.ColumnWidth;
				_suspend_event = false;
			}
		}

		protected FilterItem BuildFilterItem(object value, object secondValue = null)
		{
			Type type = TablaBase.Columns[((DataGridViewColumn)(Fields_CB.SelectedItem)).Name].ValueType;

			return BuildFilterItem(value, secondValue, ((DataGridViewColumn)(Fields_CB.SelectedItem)).Name, type);
		}

		protected override bool DoFilter(object value, object secondValue = null)
		{
			return DoFilterByProperty(value, secondValue, ((DataGridViewColumn)(Fields_CB.SelectedItem)).Name);
		}

		protected override bool DoFilterByProperty(object value, object secondValue, string column_name)
		{
			Type type = TablaBase.Columns[((DataGridViewColumn)(Fields_CB.SelectedItem)).Name].ValueType;

			FilterItem fItem = BuildFilterItem(value, secondValue, column_name, type);
			_filter_property = IFilterProperty.ByParamenter;

			return DoFilter(fItem);
		}

		protected override void SetFilter(bool on)
		{
			if (!on && !_suspend_event)
			{
				_suspend_event = true;
				Search_TB.Text = string.Empty;
				_suspend_event = false;
			}
		}

		#endregion

		#region Context Menu

		public void Nuevo_MI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Add);
        }

        public void Detalle_MI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.View);
        }

        public void Modificar_MI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Edit);
        }

        public void Borrar_MI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Delete);
        }

        public void Duplicar_MI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Copy);
        }

        public void Localizar_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                OpenLocalizeForm();
        }

        public void Imprimir_MI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Print);
        }

        private void PrintDetail_MI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.PrintDetail);
        }

        private void Lock_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Lock); }

        #endregion

		#region Events

		private void Search_TB_TextChanged(object sender, EventArgs e)
		{
			if (_suspend_event) return;

			if (_filter_keys.Length > Search_TB.Text.Length)
			{
				string filter = Search_TB.Text;
				_suspend_event = true;
				FilterOffAction();
				_suspend_event = false;
			}

			_filter_keys = Search_TB.Text;
			ExecuteAction(molAction.FilterGlobal);
			Search_TB.Focus();
		}

        private void Fields_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Fields_CB.SelectedItem == null) return;

            Type col_type = null;

            if (GetColumnType((Fields_CB.SelectedItem as DataGridViewColumn).Name) == null)
                col_type = typeof(System.String);
            else
                col_type = GetColumnType((Fields_CB.SelectedItem as DataGridViewColumn).Name);

            if (col_type.Equals(typeof(System.DateTime)))
             {
                Fecha_DTP.Visible = true;
                Valor_TB.Visible = false;
                Valor_CkB.Visible = false;

                this.Operador_CB.Items.Clear();
				this.Operador_CB.Items.AddRange(new object[] {  Library.CslaEx.EnumText.GetString(Operation.Equal),
																Library.CslaEx.EnumText.GetString(Operation.Distinct),
                                                                Library.CslaEx.EnumText.GetString(Operation.Less),
                                                                Library.CslaEx.EnumText.GetString(Operation.LessOrEqual),
                                                                Library.CslaEx.EnumText.GetString(Operation.Greater),
                                                                Library.CslaEx.EnumText.GetString(Operation.GreaterOrEqual)});

				Operador_CB.Text = Library.CslaEx.EnumText.GetString(Operation.Equal);
            }
            else if ((col_type.Equals(typeof(System.Int32))) ||
                    (col_type.Equals(typeof(System.Int64))) ||
                    (col_type.Equals(typeof(System.Decimal))) ||
                    (col_type.Equals(typeof(System.Double))))
            {
                Fecha_DTP.Visible = false;
                Valor_TB.Visible = true;
                Valor_CkB.Visible = false;

                this.Operador_CB.Items.Clear();
				this.Operador_CB.Items.AddRange(new object[] {  Library.CslaEx.EnumText.GetString(Operation.Equal),
																Library.CslaEx.EnumText.GetString(Operation.Distinct),
                                                                Library.CslaEx.EnumText.GetString(Operation.Less),
                                                                Library.CslaEx.EnumText.GetString(Operation.LessOrEqual),
                                                                Library.CslaEx.EnumText.GetString(Operation.Greater),
																Library.CslaEx.EnumText.GetString(Operation.GreaterOrEqual)});

				Operador_CB.Text = Library.CslaEx.EnumText.GetString(Operation.Equal);
            }
            else if (col_type.Equals(typeof(System.Boolean)))
            {
                Fecha_DTP.Visible = false;
                Valor_TB.Visible = false;
                Valor_CkB.Visible = true;

                this.Operador_CB.Items.Clear();
				this.Operador_CB.Items.AddRange(new object[] { Library.CslaEx.EnumText.GetString(Operation.Equal) });

				Operador_CB.Text = Library.CslaEx.EnumText.GetString(Operation.Equal);
            }
            else 
            {
                Fecha_DTP.Visible = false;
                Valor_TB.Visible = true;
                Valor_CkB.Visible = false;

                this.Operador_CB.Items.Clear();
				this.Operador_CB.Items.AddRange(new object[] {  Library.CslaEx.EnumText.GetString(Operation.Equal),
																Library.CslaEx.EnumText.GetString(Operation.Distinct),
                                                                Library.CslaEx.EnumText.GetString(Operation.Less),
                                                                Library.CslaEx.EnumText.GetString(Operation.LessOrEqual),
                                                                Library.CslaEx.EnumText.GetString(Operation.Greater),
                                                                Library.CslaEx.EnumText.GetString(Operation.GreaterOrEqual),                                                                   
																Library.CslaEx.EnumText.GetString(Operation.Contains),
                                                                Library.CslaEx.EnumText.GetString(Operation.StartsWith)});

				Operador_CB.Text = Library.CslaEx.EnumText.GetString(Operation.Contains);
            }
        }

        private void Operador_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetOperator();
        }

        private void Valor_TB_TextChanged(object sender, EventArgs e)
        {
            _search_value = Valor_TB.Text;
        }

        private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
        {
            _search_value = Fecha_DTP.Value;
        }

        private void Valor_CkB_CheckedChanged(object sender, EventArgs e)
        {
            _search_value = (Valor_CkB.CheckState == CheckState.Checked);
        }
		
		private void Filters_LB_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (_suspend_event) return;

			FilterList[e.Index].Active = (e.NewValue == CheckState.Checked);
			ExecuteAction(molAction.FilterAll);
		}

		#endregion
    }
}