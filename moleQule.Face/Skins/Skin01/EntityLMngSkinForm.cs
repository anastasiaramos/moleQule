using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library.CslaEx;
using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face.Skin01
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
            : this(false) { }

        public EntityLMngSkinForm(bool isModal)
            : this(isModal, null) { }

        public EntityLMngSkinForm(bool isModal, Form parent)
            : this(isModal, parent, null) { }

        public EntityLMngSkinForm(bool isModal, Form parent, object list)
            : base(isModal, parent, list)
        {
            InitializeComponent();
        }
      
		#endregion

		#region Layout & Source

		public override void FormatControls()
		{
			base.FormatControls();

			int margen = (PanelesH.Panel1.Width - Delete_Button.Width) / 2;

			foreach (Control ctl in PanelesH.Panel1.Controls)
			{
				if (ctl is Button)
					ctl.SetBounds(margen, ctl.Top, ctl.Width, ctl.Height);
			}

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
                        ((DataGridView)ctl).MultiSelect = false;
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
                    FormBorderStyle = FormBorderStyle.FixedToolWindow;

                    foreach (Control ctl in Controls)
                    {
                        Type ctlType = ctl.GetType();
                        switch (ctl.GetType().Name)
                        {
                            case "Button":
                                {
                                    if (ctl.Name != Close_Button.Name)
                                        ctl.Enabled = false;

                                } break;
                        }
                    }

                    Width = 900;
                    Height = 800;

                    PanelesH.Panel1Collapsed = true;

                    this.Top = 100;
                    this.StartPosition = FormStartPosition.CenterParent;

                    break;

                case molView.Normal:
                    HideAction(molAction.Select);
                    HideAction(molAction.Copy);
                    HideAction(molAction.SelectAll);
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
			try
			{
				if (this.Datos.Count > 0)
					DeleteObject(ActiveOID);
			}
			catch (iQImplementationException ex)
			{
				MessageBox.Show(ex.Message);
			}
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

		/// <summary>
		/// Cierra el formulario y los formularios dependientes de la lista de
		/// formularios activos.
		/// <returns>void</returns>
		/// </summary>
		private void Close_Button_Click(object sender, EventArgs e)
		{
			Cerrar();
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

        /// <summary>
        /// Abre el formulario para buscar item
        /// <returns>void</returns>
        /// </summary>
        public override void OpenLocalizeForm() 
        {
            PanelesSearch.Panel2Collapsed = !PanelesSearch.Panel2Collapsed; 
        }

        protected virtual void SelectFilter()
        {
            if (Fields_CB.SelectedItem == null) return;

            if (GetColumnType(((DataGridViewColumn)(Fields_CB.SelectedItem)).Name).Equals(typeof(System.DateTime)))
            {
                Fecha_DTP.Visible = true;
                Valor_TB.Visible = false;
            }
            else
            {
                Fecha_DTP.Visible = false;
                Valor_TB.Visible = true;
            }

            if (GetColumnType(((DataGridViewColumn)(Fields_CB.SelectedItem)).Name).Equals(typeof(System.String)))
            {
                this.Operador_CB.Items.Clear();
                this.Operador_CB.Items.AddRange(new object[] {  Labels.OP_EQUAL,
                                                                Labels.OP_LESS,
                                                                Labels.OP_LESS_OR_EQUAL,
                                                                Labels.OP_GREATER,
                                                                Labels.OP_GREATER_OR_EQUAL,
                                                                Labels.OP_CONTAINS,
                                                                Labels.OP_STARTS_BY,
                                                            });

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
		
        /// <summary>
        /// Maximiza la ventana porque si utilizamos el Maximize lo aplica
        /// a todos los formularios abiertos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntityLMngSkinForm_Load(object sender, EventArgs e)
        {
            if (!_is_modal) this.MaximizeForm();
        }

        private void Fields_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectFilter();
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