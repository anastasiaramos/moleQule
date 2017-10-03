﻿using System;
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
    public partial class IRPFUIForm : Skin02.ListMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "IRPFUIForm";
        public static Type Type { get { return typeof(IRPFUIForm); } }

        private IRPFs _list;

        #endregion

        #region Factory Methods

        protected IRPFUIForm()
            : this(null) { }

        public IRPFUIForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            SetFormData();

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Datos_DG.DataSource = DatosLocal_BS;
        }

        protected override void GetFormSourceData()
        {
            _list = IRPFs.GetList();
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

            ControlsMng.MaximizeColumns(Datos_DG, cols);
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
            Datos.DataSource = _list;
        }

        #endregion

        #region Business Methods

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
            PgMng.ShowInfoException(Resources.Messages.BORRAR_IMPUESTOS_NO_PERMITIDO);
        }

        protected override void CancelAction()
        {
            _list.CancelEdit();
            _action_result = DialogResult.Cancel;
        }

        #endregion

        #region Events

        private void IRPFUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseSession();
        }

        private void Datos_DG_DoubleClick(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Default);
        }

        #endregion

    }
}
