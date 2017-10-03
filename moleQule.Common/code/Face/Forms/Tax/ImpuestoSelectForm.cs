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
    public partial class ImpuestoSelectForm : ImpuestoUIForm
    {
        #region Attributes & Properties

        public new const string ID = "ImpuestoSelectForm";
        public new static Type Type { get { return typeof(ImpuestoSelectForm); } }

        private ImpuestoList _list;

        #endregion

        #region Factory Methods

        public ImpuestoSelectForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }

        protected override void GetFormSourceData()
        {
            _list = ImpuestoList.GetList();
        }

        protected override void CloseSession() {}

        #endregion

        #region Style

        /// <summary>Formatea los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            PanelesV.Panel2Collapsed = true;
            
            base.FormatControls();
            Datos_DG.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

        #region Actions

        protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}
