using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class AyudaViewForm : AyudaForm
    {
        #region Attributes & Properties

		public new const string ID = "AyudaViewForm";
		public new static Type Type { get { return typeof(AyudaViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 1; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private AyudaInfo _entity;

        public override AyudaInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public AyudaViewForm(long oid) 
			: this(oid, null) {}

		public AyudaViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = AyudaInfo.Get(oid);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Style

        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

		protected override void SetGridFormat()
		{
			foreach (DataGridViewRow row in Periodos_DGW.Rows)
			{
				if (row.IsNewRow) return;

				AyudaInfo item = (AyudaInfo)row.DataBoundItem;
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			Datos_Periodos.DataSource = _entity.Periodos;
			PgMng.Grow();
						
            base.RefreshMainData();
        }
		
        #endregion

        #region Actions

        protected override void SaveAction() { DialogResult = DialogResult.Cancel; }

        #endregion

	}
}
