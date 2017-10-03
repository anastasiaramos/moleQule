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
	public partial class SchemaLMngSkinForm : moleQule.Face.SchemaMngBaseForm
    {
		#region Factory Methods

        /// <summary>
        /// Definido solo por compatibilidad con el IDE
        /// </summary>
		public SchemaLMngSkinForm()
			: this(false, null, null) {}

        public SchemaLMngSkinForm(bool isModal, Form parent, Type type)
            : base(isModal, parent, type, null)
        {
            InitializeComponent();
            SetView(molView.Normal);
        }

		public override void InitForm()
		{			
			base.InitForm();
		}

		#endregion

		#region Layout & Source

		protected override void ActivateAction(molAction action, bool state)
		{
			if (EntityType == null) return;

			bool allow;

			switch (action)
			{
				case molAction.Add:

					allow = (bool)EntityType.InvokeMember("CanAddObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					Add_Button.Enabled = (allow) ? state : false;
					Nuevo_MI.Enabled = (allow) ? state : false;
					Add_Button.Visible = (allow) ? state : false;
					Nuevo_MI.Visible = (allow) ? state : false;

					break;

				case molAction.View:

					allow = (bool)EntityType.InvokeMember("CanGetObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					View_Button.Enabled = (allow) ? state : false;
					Detalle_MI.Enabled = (allow) ? state : false;
					View_Button.Visible = (allow) ? state : false;
					Detalle_MI.Visible = (allow) ? state : false;

					break;

				case molAction.Edit:

					allow = (bool)EntityType.InvokeMember("CanEditObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					Edit_Button.Enabled = (allow) ? state : false;
					Modificar_MI.Enabled = (allow) ? state : false;
					Edit_Button.Visible = (allow) ? state : false;
					Modificar_MI.Visible = (allow) ? state : false;

					break;

				case molAction.Delete:

					allow = (bool)EntityType.InvokeMember("CanDeleteObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					Delete_Button.Enabled = (allow) ? state : false;
					Borrar_MI.Enabled = (allow) ? state : false;
					Delete_Button.Visible = (allow) ? state : false;
					Borrar_MI.Visible = (allow) ? state : false;

					break;

				case molAction.Print:
					Imprimir_MI.Enabled = state;
					Imprimir_MI.Visible = state;
					break;

				case molAction.Copy:
					Duplicar_MI.Enabled = state;
					Duplicar_MI.Visible = state;
					break;

				case molAction.Select:
					Select_TI.Enabled = state;
					Select_TI.Visible = state;
					break;

				case molAction.Close:
					Close_TI.Enabled = state;
					Close_TI.Visible = state;
					break;
			}
		}		

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
		}

		protected override void FormatControl(Control ctl)
		{
			if ((ctl.Tag != null) && (ctl.Tag.ToString().ToUpper() == Resources.Consts.NO_FORMAT)) return;

            base.FormatControl(ctl);

			Type ctlType = ctl.GetType();
			switch (ctl.GetType().Name)
			{
				case "Button":
					{
						if (ctl.Name == "Close_Button")
							ctl.Top = ctl.Parent.Height - (ctl.Height * 2);

					} break;

				case "DataGridView":
					{
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
							((SplitContainer)ctl).SplitterDistance = 25;

					} break;
			}

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
                    HideAction(molAction.Select);
					ShowAction(molAction.Close);

					MaximizeForm(new Size(1024, 600));

                    break;
            }
        }

        #endregion
        
		#region Buttons

		private void Add_Button_Click(object sender, EventArgs e)
		{
			try
			{
#if TRACE				
                Globals.Instance.Timer.Start();
#endif
				OpenAddForm();
                FormMngBase.Instance.RefreshFormsData();
#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
			}
			catch (iQImplementationException ex)
			{
				MessageBox.Show(ex.Message);
			}
        }

		private void View_Button_Click(object sender, EventArgs e)
		{
			try
			{
#if TRACE
                Globals.Instance.Timer.Start();
#endif
				if (this.Datos.Count > 0)
					OpenViewForm();
#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
			}
			catch (iQImplementationException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Edit_Button_Click(object sender, EventArgs e)
		{
			try
			{
#if TRACE
                Globals.Instance.Timer.Start();
#endif
                if (this.Datos.Count > 0)
                    OpenEditForm();
#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
			}
			catch (iQImplementationException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Delete_Button_Click(object sender, EventArgs e)
		{
		}

		private void Copy_Button_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.Datos.Count > 0)
					DuplicateObject(ActiveOID);
			}
			catch (iQImplementationException ex)
			{
				MessageBox.Show(ex.Message);
			}			
		}

		private void Print_Button_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.Datos.Count > 0)
					PrintList();
			}
			catch (iQImplementationException ex)
			{
				MessageBox.Show(ex.Message);
			}			
		}

        private void Select_TI_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Datos.Count > 0)
                    LoadSchema();
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

		/// <summary>
		/// Cierra el formulario y los formularios dependientes de la lista de
		/// formularios activos.
		/// <returns>void</returns>
		/// </summary>
		private void Close_Button_Click(object sender, EventArgs e)
		{
            DialogResult = DialogResult.Cancel;            
			Cerrar();
		}

        private void ByDefault_Button_Click(object sender, EventArgs e)
        {
            SetDefault();
        }

        /// <summary>
        /// LLama a la action FindItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Buscar_Button_Click(object sender, EventArgs e)
        {
            FindItems(_search_value);
        }

        /// <summary>
        /// LLama a la action FilterItems
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Filtrar_Button_Click(object sender, EventArgs e)
        {
            FilterItems(_search_value);
        }

        private void FilterOff_BT_Click(object sender, EventArgs e)
        {
            SetFilter(false);
        }

        private void Letra_Click(object sender, EventArgs e)
        {
            FilterByFirst(((Button)(sender)).Text);
        }

		#endregion

        #region Actions

        #endregion

        #region Context Menu

        public void Nuevo_MI_Click(object sender, EventArgs e)
        {
            OpenAddForm();
        }

        public void Detalle_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                OpenViewForm();
        }

        public void Modificar_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                OpenEditForm();
        }

        public void Borrar_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                DeleteObject(ActiveOID);
            FormMngBase.Instance.RefreshFormsData();
        }

        public void Duplicar_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                DuplicateObject(ActiveOID);
        }

        public void Localizar_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                OpenLocalizeForm();
        }

        public void Imprimir_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                PrintList();
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
            }
            else
            {
                Fecha_DTP.Visible = false;
                Valor_TB.Visible = true;
            }

            if (col_type.Equals(typeof(System.String)))
            {
                this.Operador_CB.Items.Clear();
                this.Operador_CB.Items.AddRange(new object[] {  Labels.OP_CONTAINS,
                                                                Labels.OP_STARTS_BY,
                                                                Labels.OP_EQUAL});

                Operador_CB.Text = Labels.OP_CONTAINS;

            }
            else
            {
                this.Operador_CB.Items.Clear();
                this.Operador_CB.Items.AddRange(new object[] {  Labels.OP_EQUAL,
                                                                Labels.OP_LESS,
                                                                Labels.OP_LESS_OR_EQUAL,
                                                                Labels.OP_GREATER,
                                                                Labels.OP_GREATER_OR_EQUAL});

                Operador_CB.Text = Labels.OP_EQUAL;
            }

        }

        private void Operador_CB_SelectedIndexChanged(object sender, EventArgs e)
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

        private void Valor_TB_TextChanged(object sender, EventArgs e)
        {
            _search_value = Valor_TB.Text;
        }

        private void Fecha_DTP_ValueChanged(object sender, EventArgs e)
        {
            _search_value = Fecha_DTP.Value;
        }

		#endregion

  }
}