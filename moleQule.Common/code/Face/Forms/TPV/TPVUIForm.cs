using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class TPVUIForm : Skin02.ListMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "TPVUIForm";
        public static Type Type { get { return typeof(TPVUIForm); } }

        private TPVs _list;

        #endregion

        #region Factory Methods

        protected TPVUIForm()
            : this(null) {}

        public TPVUIForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Datos_DGW.DataSource = DatosLocal_BS;
        }

        protected override void GetFormSourceData()
        {
            _list = TPVs.GetList();
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
                    _list.Save();
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

        protected virtual void CloseSession()
        {
            if (_list != null) _list.CloseSession();
        }
        
        #endregion

        #region Style

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Observaciones.Tag = 1;

            cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(Datos_DGW, cols);
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _list;
        }

        #endregion

        #region Business Methods

        protected virtual void SetCuenta()
        {
            TPV item = (TPV)Datos.Current;

			BankAccountSelectForm form = new BankAccountSelectForm(this);

            if (form.ShowDialog(this) == DialogResult.OK)
            {
				BankAccountInfo cuenta = form.Selected as BankAccountInfo;

				item.OidCuentaBancaria = cuenta.Oid;
				item.CuentaBancaria = cuenta.Valor;
				Datos_DGW.CurrentCell.Value = cuenta.Valor;
            }
        }
        
        #endregion

        #region Buttons

        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void AddAction()
        {
            _list.NewItem();
            Datos.ResetBindings(false);
        }

        protected override void DeleteAction()
        {
            throw new Exception("Comprobar que no hay cobros asociados");
        }

        protected override void CancelAction()
        {
            _list.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion

        #region Events

        private void TPVUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           CloseSession();
        }

        private void TPVUIForm_Shown(object sender, EventArgs e)
        {
            SetUnlinkedGridValues(Datos_DGW.Name);
        }

        private void Datos_DG_DoubleClick(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Default);
        }

        private void Datos_DG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Datos_DGW.CurrentRow == null) return;
			if (e.ColumnIndex == -1) return;

            if (Datos_DGW.Columns[e.ColumnIndex].DataPropertyName == CuentaBancaria.DataPropertyName)
            {
                SetCuenta();
            }
        }

        #endregion

    }
}