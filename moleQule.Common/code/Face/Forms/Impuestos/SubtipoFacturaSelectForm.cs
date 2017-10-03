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
    public partial class SubtipoFacturaSelectForm : SubtipoFacturaUIForm
    {
        #region Attributes & Properties

        public new const string ID = "SubtipoFacturaSelectForm";
        public new static Type Type { get { return typeof(SubtipoFacturaSelectForm); } }

        private SubtipoFacturaList _list;

        #endregion

        #region Factory Methods

        public SubtipoFacturaSelectForm(Form parent)
            : this(ESubtipoFactura.Todas, parent)
        { }
        
        public SubtipoFacturaSelectForm(ESubtipoFactura tipo, Form parent)
            : this(new object[1]{tipo}, parent) { }

        public SubtipoFacturaSelectForm(object [] parameters, Form parent)
            :base (parameters, parent)
        {
            InitializeComponent();
            _view_mode = molView.Select;

            _action_result = DialogResult.Cancel;
        }

        protected override void GetFormSourceData(object []parameters)
        {
            _list = SubtipoFacturaList.GetList((ESubtipoFactura)parameters[0]);
        }

        protected override void CloseSession() { }

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
