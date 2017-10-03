using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class CargoUIForm : Skin01.ItemMngSkinForm
    {
        #region Business Methods

        public const string ID = "CargoUIForm";
        public static Type Type { get { return typeof(CargoUIForm); } }

        /// <summary>
        /// Se trata de la empresa actual y que se va a editar.
        /// </summary>
        private Cargos _cargos;

        public Cargos Cargos
        {
            get { return _cargos; }
            set { _cargos = value; }
        }

        #endregion

        #region Factory Methods

        protected CargoUIForm()
            : this(null) {}

        public CargoUIForm(Form parent)
            : base(true)
        {
            InitializeComponent();
            SetFormData();
            this.Text = Resources.Labels.CARGO_TITLE;
        }

        protected override void GetFormSourceData()
        {
            _cargos = Cargos.GetList();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false; ;

                // do the save
                try
                {
                    _cargos.Save();
                    return true;
                }
                catch (iQValidationException ex)
                {
                    MessageBox.Show(ex.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,
                                    Application.ProductName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;
                }
                finally
                {
                    RefreshMainData();
                    this.Datos.RaiseListChangedEvents = true;
                }
            }
        }

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();

            List<string> visibles = new List<string>();

			visibles.Add(Valor.Name);

            ControlsMng.ShowDataGridColumns(Datos_Grid, visibles);

            VScrollBar vs = new VScrollBar();

            int rowWidth = (int)(Datos_Grid.Width - vs.Width
                                                - Datos_Grid.RowHeadersWidth);

			Datos_Grid.Columns[Valor.Name].Width = (int)(rowWidth * 0.995);
            Datos_Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = Cargos.SortList(_cargos, "Valor", ListSortDirection.Ascending);
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        #endregion

        #region Events

        private void CargoUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cargos.CloseSession();
        }

        #endregion

    }
}

