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
	public partial class EntityMngSkinForm : EntityMngBaseForm
	{
		#region Attributes

		protected override int BarSteps { get { return base.BarSteps + 1; } }

		protected List<string> _properties_list = new List<string>();

		#endregion

		#region Factory Methods

		public EntityMngSkinForm()
            : this(false) {}

		public EntityMngSkinForm(bool isModal)
			: this(isModal, null) { }

		public EntityMngSkinForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public EntityMngSkinForm(bool isModal, Form parent, object list)
			: base(isModal, parent, list)
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

					allow = (bool)EntityType.InvokeMember("CanAddObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

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

					allow = (bool)EntityType.InvokeMember("CanEditObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					Edit_Button.Enabled = (allow) ? state : false;
					Edit_Button.Visible = (allow) ? state : false;
					Modificar_MI.Enabled = (allow) ? state : false;

					break;

				case molAction.Delete:

					allow = (bool)EntityType.InvokeMember("CanDeleteObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					Delete_Button.Enabled = (allow) ? state : false;
					Delete_Button.Visible = (allow) ? state : false;
					Borrar_MI.Enabled = (allow) ? state : false;
                    
					break;

				case molAction.Copy:

					allow = (bool)EntityType.InvokeMember("CanAddObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					Copy_MI.Visible = (allow) ? state : false;
					Copy_TI.Visible = (allow) ? state : false;

					break;

				case molAction.ChangeStateAnulado:

					allow = (bool)EntityType.InvokeMember("CanEditObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					ChangeState_TI.Visible = (ChangeState_TI.Visible) ? true : state;
					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Anulado_TI.Visible = (allow) ? state : false;
					Anulado_MI.Visible = (allow) ? state : false;

					break;

				case molAction.ChangeStateContabilizado:

					ChangeState_TI.Visible = (ChangeState_TI.Visible) ? true : state;
					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Contabilizado_TI.Visible = state;
					Contabilizado_MI.Visible = state;

					break;

				case molAction.ChangeStateEmitido:

					ChangeState_TI.Visible = (ChangeState_TI.Visible) ? true : state;
					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Emitido_TI.Visible = state;
					Emitido_MI.Visible = state;

					if ((AppContext.User != null) && (state))
					{
						Emitido_TI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
						Emitido_MI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
					}

					break;

				case molAction.CustomAction1:
					CustomAction1_TI.Enabled = state;
					CustomAction1_MI.Enabled = state;
					CustomAction1_TI.Visible = state;
					CustomAction1_MI.Visible = state;
					break;

				case molAction.CustomAction2:
					CustomAction2_TI.Enabled = state;
					CustomAction2_MI.Enabled = state;
					CustomAction2_TI.Visible = state;
					CustomAction2_MI.Visible = state;
                    break;

                case molAction.CustomAction3:
                    CustomAction3_TI.Enabled = state;
                    CustomAction3_TI.Visible = state;
                    CustomAction3_MI.Visible = state;
                    CustomAction3_MI.Enabled = state;
                    break;

                case molAction.CustomAction4:
                    CustomAction4_TI.Enabled = state;
                    CustomAction4_TI.Visible = state;
                    CustomAction4_MI.Visible = state;
                    CustomAction4_MI.Enabled = state;
                    break;

				case molAction.ExportPDF:
					ExportPDF_MI.Visible = state;
					ExportPDF_TI.Visible = state;
					break;

				case molAction.EmailPDF:
					EmailPDF_MI.Visible = state;
					EmailPDF_TI.Visible = state;
					break;

				case molAction.EmailLink:
					EmailLink_MI.Visible = state;
					EmailLink_TI.Visible = state;
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

					Filter_TSS.Enabled = state;
					Filter_TSS.Visible = state;

					break;

				case molAction.PrintDetail:
					PrintDetail_MI.Visible = state;
					PrintDetail_MI.Enabled = state;

					PrintDetail_TI.Visible = state;
					PrintDetail_TI.Enabled = state;

					break;

				case molAction.PrintListQR:
					PrintQR_MI.Visible = state;
					PrintQR_TI.Visible = state;
					break;

				case molAction.Refresh:

					Refresh_TI.Visible = state;
					Refresh_MI.Visible = state;
					Close_TSS.Visible = state;

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

				case molAction.SelectAll:
					SelectAll_TI.Visible = state;
					break;

				case molAction.Close:
					Close_TI.Enabled = state;
					break;

			}
		}

		protected override void EnableAction(molAction action, bool state)
		{
			switch (action)
			{
				case molAction.Add:

					Add_Button.Enabled = state;
					Nuevo_MI.Enabled = state;

					break;

				case molAction.View:

					View_Button.Enabled = state;
					Detalle_MI.Enabled = state;

					break;

				case molAction.Edit:

					Edit_Button.Enabled = state;
					Modificar_MI.Enabled = state;

					break;

				case molAction.Delete:

					Delete_Button.Enabled = state;
					Borrar_MI.Enabled = state;

					break;

				case molAction.Copy:

					Copy_MI.Enabled = state;
					Copy_TI.Enabled = state;

					break;

				case molAction.ChangeStateAnulado:

					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Anulado_TI.Enabled = state;
					Anulado_MI.Enabled = state;

					break;

				case molAction.ChangeStateContabilizado:

					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;
					Contabilizado_TI.Enabled = state;
					Contabilizado_MI.Enabled = state;

					break;

				case molAction.ChangeStateEmitido:

					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					if ((AppContext.User != null) && (state))
					{
						Emitido_TI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
						Emitido_MI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
					}

					break;

				case molAction.CustomAction1:
					
					CustomAction1_TI.Enabled = state;
					CustomAction1_MI.Enabled = state;
					break;

				case molAction.CustomAction2:
					
					CustomAction2_TI.Enabled = state;
					CustomAction2_MI.Enabled = state;
					break;

				case molAction.CustomAction3:
					
					CustomAction3_TI.Enabled = state;
					CustomAction3_MI.Enabled = state;
					break;

				case molAction.CustomAction4:
					
					CustomAction4_TI.Enabled = state;
					CustomAction4_MI.Enabled = state;
					break;

				case molAction.ExportPDF:
					
					ExportPDF_MI.Enabled = state;
					ExportPDF_TI.Enabled = state;
					break;

				case molAction.EmailPDF:

					EmailPDF_MI.Enabled = state;
					EmailPDF_TI.Enabled = state;
					break;

				case molAction.EmailLink:

					EmailLink_MI.Visible = state;
					EmailLink_TI.Visible = state;
					break;

				case molAction.FilterOn:
				case molAction.FilterOff:

					Filtrar_MI.Enabled = state;
					FilterValue_TI.Enabled = state;
					FilterOff_MI.Enabled = state;
					FilterOff_TI.Enabled = state;
					break;

				case molAction.Print:

					Print_Button.Enabled = state;
					Imprimir_MI.Enabled = state;

					break;

				case molAction.PrintDetail:

					PrintDetail_MI.Enabled = state;
					PrintDetail_TI.Enabled = state;

					break;

				case molAction.PrintListQR:

					PrintQR_MI.Enabled = state;
					PrintQR_TI.Enabled = state;
					break;

				case molAction.Refresh:

					Refresh_TI.Enabled = state;
					Refresh_MI.Enabled = state;

					break;

				case molAction.Unlock:

					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Abierto_TI.Enabled = state;
					Abierto_MI.Enabled = state;

					if ((AppContext.User != null) && (state))
					{
						Abierto_TI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
						Abierto_MI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
					}

					break;

				case molAction.Select:
					Select_TI.Enabled = state;
					break;

				case molAction.SelectAll:
					SelectAll_TI.Enabled = state;
					break;

				case molAction.Close:
					Close_TI.Enabled = state;
					break;

			}
		}

		#endregion

		#region Layout

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
			Fields_CB.Text = col.HeaderText;
		}

		protected override void SetView(molView view)
		{
			ViewMode = view;

			switch (ViewMode)
			{
				case molView.Select:

					ShowAction(molAction.Refresh);
					ShowAction(molAction.Add);
					ShowAction(molAction.View);
					ShowAction(molAction.Edit);
					HideAction(molAction.Delete);
					HideAction(molAction.Unlock);
					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);
					HideAction(molAction.ShowDocuments);
					HideAction(molAction.Print);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.PrintListQR);
					HideAction(molAction.ExportPDF);
					HideAction(molAction.EmailPDF);
					HideAction(molAction.EmailLink);
					HideAction(molAction.Copy);
					ShowAction(molAction.Select);
					ShowAction(molAction.SelectAll);
					ShowAction(molAction.FilterOn);
					ShowAction(molAction.FilterOff);
					ShowAction(molAction.AdvancedSearch);

					MaximizeForm(new Size(1024, 0));

					break;

				case molView.Normal:
				case molView.Enbebbed:

					ShowAction(molAction.Refresh);
					ShowAction(molAction.Add);
					ShowAction(molAction.View);
					ShowAction(molAction.Edit);
					ShowAction(molAction.Delete);
					HideAction(molAction.Copy);
					HideAction(molAction.Unlock);
					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);
					HideAction(molAction.ShowDocuments);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.PrintListQR);
					HideAction(molAction.EmailPDF);
					HideAction(molAction.EmailLink);
					HideAction(molAction.ExportPDF);
					HideAction(molAction.Select);
					HideAction(molAction.SelectAll);
					HideAction(molAction.AdvancedSearch);

					if (ViewMode == molView.Normal)
						MaximizeForm();

					break;
			}
		}

		protected void SetActionStyle(molAction action, string title, Bitmap image)
		{
			switch (action)
			{
				case molAction.CustomAction1:
					{
						CustomAction1_TI.Image = image;
						CustomAction1_TI.Text = title;

						CustomAction1_MI.Image = image;
						CustomAction1_MI.Text = title;
					}
					break;

				case molAction.CustomAction2:
					{
						CustomAction2_TI.Image = image;
						CustomAction2_TI.Text = title;

						CustomAction2_MI.Image = image;
						CustomAction2_MI.Text = title;
					}
					break;

				case molAction.CustomAction3:
					{
						CustomAction3_TI.Image = image;
						CustomAction3_TI.Text = title;

						CustomAction3_MI.Image = image;
						CustomAction3_MI.Text = title;
					}
					break;

				case molAction.CustomAction4:
					{
						CustomAction4_TI.Image = image;
						CustomAction4_TI.Text = title;

						CustomAction4_MI.Image = image;
						CustomAction4_MI.Text = title;
					}
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

		#region Source

		protected override void RefreshSources()
        {
			//Design time compatibility
			if (TablaBase == null) return;

			PgMng.Grow(string.Empty, "EMngSkinForm04::RefreshSources INI");

            DatosSearch.DataSource = Datos.DataSource;

			DataGridViewColumn activeCol = _active_column;

            Fields_CB.DataSource = TablaBase.Columns;
            Fields_CB.DisplayMember = "HeaderText";
            Fields_CB.ValueMember = "DataPropertyName";

			_properties_list = ControlsMng.GetPropertiesList(TablaBase);

			if (activeCol != null) SetColumnActive(activeCol);

            if (_selectedOid > 0) Select(_selectedOid);

			PgMng.Grow(string.Empty, "EMngSkinForm04::RefreshSources END");
        }

        #endregion

        #region Business Methods

        private void SetOperator(Type columnType = null)
        {
            if (Operador_CB.SelectedItem == null) _operation = Operation.Equal;

            string op = Operador_CB.SelectedItem.ToString();

            if (columnType != null && !columnType.Equals(typeof(System.DateTime)))
                Valor_TB.Visible = true;

			if (op == Library.CslaEx.EnumText.GetString(Operation.Contains)) { _operation = Operation.Contains; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.Distinct)) { _operation = Operation.Distinct; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.StartsWith)) { _operation = Operation.StartsWith; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.Less)) { _operation = Operation.Less; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.LessOrEqual)) { _operation = Operation.LessOrEqual; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.Greater)) { _operation = Operation.Greater; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.GreaterOrEqual)) { _operation = Operation.GreaterOrEqual; return; }
			if (op == Library.CslaEx.EnumText.GetString(Operation.Equal)) { _operation = Operation.Equal; return; }
            if (op == Library.CslaEx.EnumText.GetString(Operation.Between)) 
            {   
                _operation = Operation.Between;
                if (columnType != null)
                {
                    if (columnType.Equals(typeof(System.DateTime)))
                    {
                        FechaFin_DTP.Visible = true;
                        _second_search_value = FechaFin_DTP.Value;
                    }
                    else if ((columnType.Equals(typeof(System.Int32))) ||
                            (columnType.Equals(typeof(System.Int64))) ||
                            (columnType.Equals(typeof(System.Decimal))) ||
                            (columnType.Equals(typeof(System.Double))))
                    {
                        ValorIni_TB.Visible = true;
                        ValorFin_TB.Visible = true;
                        Valor_TB.Visible = false;
                        _search_value = ValorIni_TB.Text;
                        _second_search_value = ValorFin_TB.Text;
                    }
                }
                return; 
            }
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

        private void CustomAction3_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction3); }

        private void CustomAction4_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction4); }

		private void Delete_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Delete); }

		private void Docs_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ShowDocuments); }

		private void Edit_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Edit); }		

		private void EmailPDF_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.EmailPDF); }

		private void EmailLink_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.EmailLink); }

		private void Emitido_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ChangeStateEmitido); }

		private void ExportPDF_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ExportPDF); }

		private void Filtrar_TI_Click(object sender, EventArgs e)
		{
            _search_value = TablaBase[_active_column.Index, TablaBase.CurrentCell.RowIndex].Value;
			ExecuteAction(molAction.FilterOn);
		}

		private void FilterOff_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.FilterOff); }

		private void FilterOn_BT_Click(object sender, EventArgs e)
		{
			SetColumnActive(ControlsMng.GetColumn(TablaBase, ((DataGridViewColumn)Fields_CB.SelectedItem).DataPropertyName));
			ExecuteAction(molAction.FilterOn);
		}

		private void FilterOff_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.FilterOff); }

		private void Letra_Click(object sender, EventArgs e)
		{
			FilterOffAction();
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

		private void StateContabilizado_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ChangeStateContabilizado); }

		private void StateUnlock_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Unlock); }

		private void View_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.View); }

		#endregion

        #region Actions

        public override void FilterOffAction()
        {
            base.FilterOffAction();
            SetOperator();

			Filters_LB.Items.Clear();
        }

		public override void RefreshAction() 
		{
			RefreshMainData();			
			FilterAllAction();
            RefreshSources();
            Select(ActiveOID);
		}

		#endregion

		#region Find & Filter

		protected override void FilterByKey(string key)
		{
#if TRACE
			PgMng.Record("EntityMngSkinForm04::FilterByKey - INI");
#endif
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

			EnableEvents(false);
			Search_TB.Text = _filter_keys;
			EnableEvents(true);

			if (_filter_keys != string.Empty)
				ExecuteAction(molAction.FilterGlobal);
			else
				ExecuteAction(molAction.FilterOff);
#if TRACE
			PgMng.Record("EntityMngSkinForm04::FilterByKey - INI");
#endif
		}

		// DEPRECATED
		protected void AddFilterLabel(FilterItem fItem) { AddFilterItem(fItem); }

		protected override void AddFilterItem(FilterItem fItem)
		{
			base.AddFilterItem(fItem);

			Filters_LB.Items.Clear();
			 foreach (FilterItem item in FilterList)
			{
				EnableEvents(false);
				Filters_LB.Items.Add(item.Text, item.Active);
				int width = (int)Filters_LB.CreateGraphics().MeasureString(item.Text, Filters_LB.Font).Width + 5;
				Filters_LB.ColumnWidth = (width > Filters_LB.ColumnWidth) ? width : Filters_LB.ColumnWidth;
				EnableEvents(true);
			}
		}

		protected FilterItem BuildFilterItem(object value, object secondValue = null)
		{
			Type type = TablaBase.Columns[((DataGridViewColumn)(Fields_CB.SelectedItem)).Name].ValueType;

			return BuildFilterItem(value, secondValue, ((DataGridViewColumn)(Fields_CB.SelectedItem)).Name, type);
		}

		protected override bool DoFilter(object value, object secondValue = null)
		{
			return DoFilterByProperty(value, secondValue, _active_column.Name);
		}

		protected override bool DoFilterByProperty(object value, object secondValue, string column_name)
		{
			Type type = TablaBase.Columns[column_name].ValueType;
            //string dataPropertyName = TablaBase.Columns[column_name].DataPropertyName;

			FilterItem fItem = BuildFilterItem(value, secondValue, column_name, type);
			_filter_property = IFilterProperty.ByParamenter;

			return DoFilter(fItem);
		}

		protected override bool DoFind(object value)
		{
			FilterItem fItem = new FilterItem();
			fItem.Column = ((DataGridViewColumn)(Fields_CB.SelectedItem)).Name;
			fItem.Value = value;
			fItem.FilterProperty = FilterProperty;
			fItem.Operation = _operation;
			_search_results = Localize(fItem);
			return _search_results != null;
		}

		protected override void SetFilter(bool on)
		{
			if (!on && EventsEnabled)
			{
				EnableEvents(false);
				Search_TB.Text = string.Empty;
				EnableEvents(true);
			}
		}

		#endregion

		#region Context Menu

		public void Borrar_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Delete); }
		public void Detalle_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.View); }
		public void Duplicar_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Copy); }
		public void Imprimir_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }
		public void Localizar_MI_Click(object sender, EventArgs e)
		{
			if (this.Datos.Count > 0)
				OpenLocalizeForm();
		}
		private void Lock_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Lock); }
		public void Modificar_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Edit); }
		public void Nuevo_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Add); }
        private void PrintDetail_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.PrintDetail);}

        #endregion

		#region Events

		protected override void EnableEvents(bool enable) 
		{
			if (enable)
			{
				Search_TB.TextChanged += new EventHandler(Search_TB_TextChanged);
			}
			else
			{
				Search_TB.TextChanged -= new EventHandler(Search_TB_TextChanged);
			}

			base.EnableEvents(enable);
		}

		private void Search_TB_TextChanged(object sender, EventArgs e)
		{
#if TRACE
			PgMng.Record("EntityMngSkinForm04::Search_TB_TextChanged - INI");
#endif
			if (_filter_keys.Length > Search_TB.Text.Length)
			{
				string filter = Search_TB.Text;
				EnableEvents(false);
				_filter_type = IFilterType.FilterBack;
				_filter_keys = (_filter_keys.Length > 1) ? _filter_keys.Substring(0, _filter_keys.Length - 1) : string.Empty;
				EnableEvents(true);
			}
			else
			{
				_filter_keys = Search_TB.Text;
			}

			ExecuteAction(molAction.FilterGlobal);
			Search_TB.Focus();
#if TRACE
			PgMng.Record("EntityMngSkinForm04::Search_TB_TextChanged - END");
#endif
		}

        private void Fields_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Fields_CB.SelectedItem == null) return;
            
			DataGridViewColumn col = Fields_CB.SelectedItem as DataGridViewColumn;
            Type col_type = null;

            if (GetColumnType(col.Name) == null)
                col_type = typeof(System.String);
            else
                col_type = GetColumnType(col.Name);

            if (col_type.Equals(typeof(System.DateTime)))
             {
                FechaIni_DTP.Visible = true;
                FechaFin_DTP.Visible = false;
                Valor_TB.Visible = false;
                Valor_CkB.Visible = false;
                ValorIni_TB.Visible = false;
                ValorFin_TB.Visible = false;

                this.Operador_CB.Items.Clear();
			    this.Operador_CB.Items.AddRange(new object[] {  Library.CslaEx.EnumText.GetString(Operation.Equal),
															    Library.CslaEx.EnumText.GetString(Operation.Distinct),
                                                                Library.CslaEx.EnumText.GetString(Operation.Less),
                                                                Library.CslaEx.EnumText.GetString(Operation.LessOrEqual),
                                                                Library.CslaEx.EnumText.GetString(Operation.Greater),
                                                                Library.CslaEx.EnumText.GetString(Operation.GreaterOrEqual),
                                                                Library.CslaEx.EnumText.GetString(Operation.Between)});

			    Operador_CB.Text = Library.CslaEx.EnumText.GetString(Operation.Equal);

			    _search_value = FechaIni_DTP.Value;
            }
            else if ((col_type.Equals(typeof(System.Int32))) ||
                    (col_type.Equals(typeof(System.Int64))) ||
                    (col_type.Equals(typeof(System.Decimal))) ||
                    (col_type.Equals(typeof(System.Double))))
            {
                FechaIni_DTP.Visible = false;
                FechaFin_DTP.Visible = false;
                Valor_TB.Visible = true;
                Valor_CkB.Visible = false;
                ValorIni_TB.Visible = false;
                ValorFin_TB.Visible = false;

                this.Operador_CB.Items.Clear();
				this.Operador_CB.Items.AddRange(new object[] {  Library.CslaEx.EnumText.GetString(Operation.Equal),
																Library.CslaEx.EnumText.GetString(Operation.Distinct),
                                                                Library.CslaEx.EnumText.GetString(Operation.Less),
                                                                Library.CslaEx.EnumText.GetString(Operation.LessOrEqual),
                                                                Library.CslaEx.EnumText.GetString(Operation.Greater),
                                                                Library.CslaEx.EnumText.GetString(Operation.GreaterOrEqual),
                                                                Library.CslaEx.EnumText.GetString(Operation.Between)});

				Operador_CB.Text = Library.CslaEx.EnumText.GetString(Operation.Equal);
            }
            else if (col_type.Equals(typeof(System.Boolean)))
            {
                FechaIni_DTP.Visible = false;
                FechaFin_DTP.Visible = false;
                Valor_TB.Visible = false;
                Valor_CkB.Visible = true;
                ValorIni_TB.Visible = false;
                ValorFin_TB.Visible = false;

                this.Operador_CB.Items.Clear();
				this.Operador_CB.Items.AddRange(new object[] { Library.CslaEx.EnumText.GetString(Operation.Equal) });

				Operador_CB.Text = Library.CslaEx.EnumText.GetString(Operation.Equal);

				_search_value = (Valor_CkB.CheckState == CheckState.Checked);
            }
            else 
            {
                FechaIni_DTP.Visible = false;
                FechaFin_DTP.Visible = false;
                Valor_TB.Visible = true;
                Valor_CkB.Visible = false;
                ValorIni_TB.Visible = false;
                ValorFin_TB.Visible = false;

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
            
            if (Fields_CB.SelectedItem == null) return;

            DataGridViewColumn col = Fields_CB.SelectedItem as DataGridViewColumn;
            Type col_type = null;

            if (GetColumnType(col.Name) == null)
                col_type = typeof(System.String);
            else
                col_type = GetColumnType(col.Name);

            FechaFin_DTP.Visible = false;
            ValorIni_TB.Visible = false;
            ValorIni_TB.Text = string.Empty;
            ValorFin_TB.Visible = false;
            ValorFin_TB.Text = string.Empty;
            SetOperator(col_type); 
        }

        private void Valor_TB_TextChanged(object sender, EventArgs e)
        {
            _search_value = Valor_TB.Text;
        }

        private void ValorIni_TB_TextChanged(object sender, EventArgs e)
        {
            _search_value = ValorIni_TB.Text;
        }

        private void ValorFin_TB_TextChanged(object sender, EventArgs e)
        {
            _second_search_value = ValorFin_TB.Text;
        }

        private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
        {
            _search_value = FechaIni_DTP.Value;
        }

        private void FechaFin_DTP_ValueChanged(object sender, EventArgs e)
        {
            _second_search_value = FechaFin_DTP.Value;
        }

        private void Valor_CkB_CheckedChanged(object sender, EventArgs e)
        {
            _search_value = (Valor_CkB.CheckState == CheckState.Checked);
        }
		
		private void Filters_LB_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (!EventsEnabled) return;

			FilterList[e.Index].Active = (e.NewValue == CheckState.Checked);
			ExecuteAction(molAction.FilterAll);
		}

		#endregion
    }
}