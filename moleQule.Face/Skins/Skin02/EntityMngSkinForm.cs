using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face.Skin02
{
	/// <summary>
	/// Clase Base para Gestión de un Tipo de Entidad. 
	/// Consulta, Creación, Edición, Borrado, Filtrado y Localización.
	/// Se gestiona mediante una Lista de Elementos 
	/// </summary>
	public partial class EntityMngSkinForm : moleQule.Face.EntityMngForm
    {

		#region Factory Methods

		public EntityMngSkinForm()
        {
			InitializeComponent();
        }

		#endregion

		#region Layout & Source

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
                            col.DefaultCellStyle.BackColor = Color.FromArgb(0, 0, 192);
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
                        //else
                        //    ((SplitContainer)ctl).SplitterDistance = 25;

					} break;
			}
		}

		#endregion

		#region Buttons

        private void Add_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Add); }

        private void View_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.View); }

        private void Edit_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Edit); }

        private void Delete_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Delete); FormMngBase.Instance.RefreshFormsData(); }

        private void Copy_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Copy); }

        private void Print_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

        private void Close_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Close); }

        private void Find_Button_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Datos.Count > 0)
                    OpenLocalizeForm();
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
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
        private void EntityMngSkinForm_Load(object sender, EventArgs e)
        {
            this.MaximizeForm();
        }

		private void Filtros_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ApplyFilter();
			}
			catch (iQImplementationException ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		#endregion



	}
}