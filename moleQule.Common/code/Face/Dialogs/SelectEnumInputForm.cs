using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using Csla;
using moleQule.Face;
using moleQule.Library;

namespace moleQule.Face.Common
{
	public partial class SelectEnumInputForm : Skin01.InputSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps; } }

		public const string ID = "SelectEnumInputForm";
		public static Type Type { get { return typeof(SelectEnumInputForm); } }

        private object _selected;

		public object Selected { get { return _selected; } set { _selected = value; } }

        #endregion

        #region Factory Methods

		public SelectEnumInputForm(bool IsModal)
            : base(IsModal)
        {
            InitializeComponent();

            SetFormData();
        }

        #endregion

        #region Style

        public override void FormatControls()
        {
            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Texto.Tag = 1;

			cols.Add(Texto);

            ControlsMng.MaximizeColumns(Tabla, cols);
            Tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #endregion

        #region Source

        protected override void RefreshMainData()
        {
        }

		#endregion

        #region Buttons

        protected override void SubmitAction()
        {
            if (Datos.Current != null)
            {
				_selected = Datos.Current;
            }

            _action_result = DialogResult.OK;
        }

        #endregion

        #region Events

        private void Tabla_DoubleClick(object sender, EventArgs e) { ExecuteAction(molAction.Submit); }

        #endregion
    }
}

