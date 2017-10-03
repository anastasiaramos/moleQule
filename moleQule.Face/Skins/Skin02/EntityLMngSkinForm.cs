using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using moleQule.Library.CslaEx;
using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face.Skin02
{
	/// <summary>
	/// Clase Base para Gestión de un Tipo de Entidad. 
	/// Consulta, Creación, Edición, Borrado, Filtrado y Localización.
	/// Se gestiona mediante una Lista de Elementos de ese tipo
	/// </summary>
	public partial class EntityLMngSkinForm : moleQule.Face.EntityMngBaseForm
    {
		#region Factory Methods

		public EntityLMngSkinForm()
            : this(false) {}

        public EntityLMngSkinForm(bool isModal)
            : this(false, null) { }

        public EntityLMngSkinForm(bool isModal, Form parent)
            : this(isModal, parent, null) {}

        public EntityLMngSkinForm(bool isModal, Form parent, object list)
            : base(isModal, parent, list)
        {
            InitializeComponent();
            ViewMode = molView.Normal;
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

            if (_selectedOid > 0) Select(_selectedOid);
        }

        protected override void SetFilter(bool on)
        {
            Filter_TB.Text = (on) ? FilterValues : string.Empty;
        }

        #endregion

        #region Layout & Format
                
		public override void FormatControls()
		{
			base.FormatControls();

			PanelesSearch.IsSplitterFixed = false;
			PanelesSearch.SplitterDistance = PanelesSearch.Height - PanelesSearch.SplitterWidth
																	- Campos_Panel.Height
																	- Navegador.Height
																	- 4 /*Margen*/;
			PanelesSearch.IsSplitterFixed = true;

			bool collapse = PanelesV.Panel1Collapsed;
			PanelesV.Panel1Collapsed = false;
			Letras_Panel.Left = (PanelesV.Panel1.Width - Letras_Panel.Width) / 2;
			Letras_Panel.Left = Letras_Panel.Left < 0 ? 0 : Letras_Panel.Left;
			PanelesV.Panel1Collapsed = collapse;

			collapse = PanelesSearch.Panel2Collapsed;
			PanelesSearch.Panel2Collapsed = false;
			Campos_Panel.Left = (PanelesSearch.Panel2.Width - Campos_Panel.Width) / 2;
			Campos_Panel.Left = Campos_Panel.Left < 0 ? 0 : Campos_Panel.Left;
			PanelesSearch.Panel2Collapsed = collapse;

			//SetGridFormat(Controls.Owner);
		}

		protected override void FormatControl(Control ctl)
		{
			base.FormatControl(ctl);

			Type ctlType = ctl.GetType();

			switch (ctl.GetType().Name)
			{
				case "DataGridView":
					{
						((DataGridView)ctl).DefaultCellStyle.Font = new Font("Tahoma", (float)8.25, FontStyle.Regular);
						((DataGridView)ctl).RowHeadersWidth = 25;
						((DataGridView)ctl).BackgroundColor = System.Drawing.SystemColors.ControlLight;
						foreach (DataGridViewColumn col in ((DataGridView)ctl).Columns)
						{
							col.DefaultCellStyle.BackColor = Color.White;
                            col.DefaultCellStyle.ForeColor = Color.FromArgb(0,0,192);
						}
						((DataGridView)ctl).SelectionMode = DataGridViewSelectionMode.FullRowSelect;
					} break;

				case "TabControl":
					{
						ctl.Left = (ctl.Parent.Width - ctl.Width) / 2;

					} break;

				case "SplitContainer":
					{
                        if (((SplitContainer)ctl).Orientation == Orientation.Vertical)
                            ((SplitContainer)ctl).SplitterDistance = 114;
                        else
                        {
                            if (((SplitContainer)ctl).Parent.Height > 25)
                                ((SplitContainer)ctl).SplitterDistance = 25;
                        }

					} break;
			}
		}

		protected override void SetSelectView()
		{
			PanelesSearch.Panel2Collapsed = false;

			base.SetSelectView();
		}

        protected override void SetView(molView view)
        {
            switch (view)
            {
                case molView.Select:
                    ShowAction(molAction.Add);
                    ShowAction(molAction.View);
                    HideAction(molAction.Edit);
                    HideAction(molAction.Delete);
                    HideAction(molAction.Print);
                    HideAction(molAction.PrintDetail);
					HideAction(molAction.ExportPDF);
					HideAction(molAction.EmailPDF);
					HideAction(molAction.EmailLink);
                    HideAction(molAction.Copy);
                    ShowAction(molAction.Select);
                    ShowAction(molAction.SelectAll);
                    ShowAction(molAction.FilterOn);
                    ShowAction(molAction.FilterOff);

					MaximizeForm(new Size(1024, 0));

                    break;

                case molView.Normal:
					HideAction(molAction.Copy);
					HideAction(molAction.Lock);
					HideAction(molAction.Unlock);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.EmailPDF);
					HideAction(molAction.EmailLink);
					HideAction(molAction.ExportPDF);
                    HideAction(molAction.Select);
					HideAction(molAction.SelectAll);

                    SuspendLayout();
					MaximizeForm();
                    ResumeLayout();

                    break;
            }
        }

        protected override void ActivateAction(molAction action, bool state)
        {
            bool allow;

            if (EntityType == null)
                return;

            switch (action)
            {
                case molAction.Add:

                    allow = (bool)EntityType.InvokeMember("CanAddObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

                    Add_Button.Visible = state;
                    Nuevo_MI.Visible = state;
                    break;

                case molAction.View:

                    allow = (bool)EntityType.InvokeMember("CanGetObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

                    View_Button.Visible = state;
                    Detalle_MI.Visible = state;
                    break;

                case molAction.Edit:

                    allow = (bool)EntityType.InvokeMember("CanEditObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

                    Edit_Button.Visible = state;
                    Modificar_MI.Visible = state;
                    break;

                case molAction.Delete:

                    allow = (bool)EntityType.InvokeMember("CanDeleteObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

                    Delete_Button.Visible = state;
                    Borrar_MI.Visible = state;
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
                    Print_Button.Visible = state;
                    Imprimir_MI.Visible = state;
                    Separator2_TI.Visible = state;
                    break;

                case molAction.PrintDetail:
                    PrintDetail_MI.Visible = state;
                    PrintDetail_TI.Visible = state;
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

                case molAction.Copy:

                    allow = (bool)EntityType.InvokeMember("CanAddObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

                    Copy_MI.Visible = state;
                    Copy_TI.Visible = state;
                    break;

                case molAction.Select:
                    Select_TI.Visible = state;
                    break;

                case molAction.SelectAll:
                    SelectAll_TI.Visible = state;
                    break;

                case molAction.Close:
                    Close_TI.Visible = state;
                    break;

                case molAction.Lock:
                case molAction.Unlock:
                    Unlock_TI.Visible = state;
                    Unlock_MI.Visible = state;
                    Lock_TI.Visible = state;
                    Lock_MI.Visible = state;
                    Separator4_TI.Visible = state;
                    Separator3_MI.Visible = state;

					if (AppContext.User != null)
					{
						Unlock_TI.Enabled = AppContext.User.IsAdmin;
						Unlock_MI.Enabled = AppContext.User.IsAdmin;
						Lock_TI.Enabled = AppContext.User.IsAdmin;
						Lock_MI.Enabled = AppContext.User.IsAdmin;
					}

                    break;
            }
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

        private void Add_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Add); }

		private void View_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.View); }

		private void Edit_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Edit); }

		private void Delete_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Delete); }

        private void Copy_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Copy); }

		private void Print_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

        private void PrintDetail_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.PrintDetail); }

		private void ExportPDF_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ExportPDF); }

		private void EmailPDF_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.EmailPDF); }

		private void EmailLink_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.EmailLink); }

        private void Filtrar_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.FilterOn); }

        private void FilterOff_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.FilterOff); }

        private void Select_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Select); }

        private void SelectAll_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.SelectAll); }

		private void Close_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Close); }

        /// <summary>
        /// LLama a la action FindItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buscar_Button_Click(object sender, EventArgs e) { FindItems(_search_value); }

        /// <summary>
        /// LLama a la action FilterItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Filtrar_Button_Click(object sender, EventArgs e) { FilterItems(_search_value); }

        private void FilterOff_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.FilterOff); }

        private void Letra_Click(object sender, EventArgs e) { FilterByFirst(((Button)(sender)).Text); }

		#endregion

        #region Actions

        /// <summary>
        /// Abre el formulario para buscar item
        /// <returns>void</returns>
        /// </summary>
        public override void OpenLocalizeForm() 
        {
            PanelesSearch.Panel2Collapsed = !PanelesSearch.Panel2Collapsed; 
        }

        public override void FilterOffAction()
        {
            base.FilterOffAction();
            SetOperator();
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

        private void Unlock_TI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Unlock);
        }

        private void Lock_TI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Lock);
        }

        #endregion

		#region Events

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

		#endregion

    }
}