using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face.Common;
using moleQule.Face.Hipatia;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Common
{
    public partial class CompanyForm : Skin01.ItemMngSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 3; } }

		public virtual Company Entity { get { return null; } set { } }
		public virtual CompanyInfo EntityInfo { get { return null; } }

        public virtual long ContactosActiveOID()
        {
            if (Datos_Contactos.Current != null)
                return ((ContactoEmpresa)(Datos_Contactos.Current)).Oid;
            else
                return -1;
        }


        #endregion

        #region Factory Methods

        public CompanyForm() 
			: this(-1, null, true, null) {}

		public CompanyForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            MaximizeForm(new Size(900, 550));

            ShowAction(molAction.ShowDocuments);

            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Nombre.Tag = 1;

            cols.Add(Nombre);

            ControlsMng.MaximizeColumns(Contactos_Grid, cols);
        }

		public override void RefreshSecondaryData()
		{
			Datos_TipoID.DataSource = Library.Common.EnumText<ETipoID>.GetList();
			PgMng.Grow();

			Datos_Cargos.DataSource = CargoList.GetList(false);
            PgMng.Grow();

            Datos_Municipios_Contactos.DataSource = MunicipioList.GetList(false);
            PgMng.Grow();
		}

		/// <summary>
		/// Asigna el objeto principal al origen de datos 
		/// <returns>void</returns>
		/// </summary>
		protected override void RefreshMainData()
		{
			Images.Show(EntityInfo.Logo, Library.Common.ModuleController.LOGOS_EMPRESAS_PATH, Logo_PictureBox);
            PgMng.Grow();
		}

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        protected override void DocumentsAction()
        {
            try
            {
				AgenteEditForm form = new AgenteEditForm(typeof(Company), EntityInfo as IAgenteHipatia);
                form.ShowDialog(this);
            }
            catch (HipatiaException ex)
            {
                if (ex.Code == HipatiaCode.NO_AGENTE)
                {
					AgenteAddForm form = new AgenteAddForm(typeof(Company), EntityInfo as IAgenteHipatia);
                    form.ShowDialog(this);
                }
            }
        }

		protected virtual void SelectTipoIDAction() { }

        #endregion

        #region Buttons

        #endregion

        #region Events

        private void Contactos_Grid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (this is CompanyViewForm) return;

            if (e.Button == MouseButtons.Left)
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        {
                            CargoUIForm form = new CargoUIForm(this);
                            if (form != null && !form.IsDisposed) form.ShowDialog(this);
                            break;
                        }
                    case 4:
                        {
                            if (Contactos_Grid.Rows[e.RowIndex].DataBoundItem == null) return;

                            MunicipioSelectForm form = new MunicipioSelectForm(this);
                            if (form != null && !form.IsDisposed) form.ShowDialog(this);
                            if (form.Selected != null)
                            {
                                ((ContactoEmpresa)Contactos_Grid.Rows[e.RowIndex].DataBoundItem).CodPostal =
                                    ((MunicipioInfo)form.Selected).CodPostal;
                                ((ContactoEmpresa)Contactos_Grid.Rows[e.RowIndex].DataBoundItem).Provincia =
                                    ((MunicipioInfo)form.Selected).Provincia;
                                ((ContactoEmpresa)Contactos_Grid.Rows[e.RowIndex].DataBoundItem).Municipio =
                                    ((MunicipioInfo)form.Selected).Nombre;
                            }
                            break;
                        }
                }
            }
        }

        private void Contactos_Grid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this is CompanyViewForm) return;
            if (Contactos_Grid.CurrentRow != null)
            {
                if ((Contactos_Grid.CurrentCell.Value != null) && (Contactos_Grid.CurrentCell.ColumnIndex == 4))
                {
                    Contactos_Grid.CurrentRow.Cells["CodPostal"].Value = ((MunicipioInfo)((DataGridViewComboBoxCell)Contactos_Grid.CurrentRow.Cells["Municipio"]).Items[Datos_Municipios_Contactos.Position]).CodPostal;
                    Contactos_Grid.CurrentRow.Cells["Provincia"].Value = ((MunicipioInfo)((DataGridViewComboBoxCell)Contactos_Grid.CurrentRow.Cells["Municipio"]).Items[Datos_Municipios_Contactos.Position]).Provincia;
                }
            }
        }
		
		void TipoID_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectTipoIDAction();
		}
        
        private void Contactos_Grid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #endregion

    }
}

