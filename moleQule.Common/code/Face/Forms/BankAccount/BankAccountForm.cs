using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class BankAccountForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties

        public const string ID = "BankAccountForm";
		public static Type Type { get { return typeof(BankAccountForm); } }

        protected override int BarSteps { get { return base.BarSteps + 0; } }

        public virtual BankAccount Entity { get { return null; } set { } }
        public virtual BankAccountInfo EntityInfo { get { return null; } }
		
        #endregion

        #region Factory Methods

        public BankAccountForm() 
			: this(-1) {}

        public BankAccountForm(long oid) 
			: this(oid, true, null) {}

		public BankAccountForm(Form parent) 
		: this(-1, true, parent) {}

        public BankAccountForm(long oid, bool is_modal, Form parent)
            : base(oid, is_modal, parent)
        {
            InitializeComponent();
        }
		
        #endregion

        #region Layout

        public override void FormatControls()
        {
            base.FormatControls();
        }

		#endregion
		
		#region Source

        protected override void RefreshMainData()
        {
			
        }

        public override void RefreshSecondaryData()
		{
			
        }
		
		#endregion

        #region Validation & Format

        #endregion

        #region Print

        //public override void PrintObject()
        //{
        //    CuentaBancariaReportMng reportMng = new CuentaBancariaReportMng(AppContext.ActiveSchema);
        //    ReportViewer.SetReport(reportMng.GetReport(EntityInfo);
        //    ReportViewer.ShowDialog();
        //}

        #endregion

        #region Actions

		public virtual void AddCuentaAsociadaAction() { }
		public virtual void DeleteCuentaAsociadaAction() { }
		public virtual void SetStateCuentaAsociadaAction() { }
        public virtual void RealizarCancelacionAction() { }
        public virtual void AddFondoInversionAction() { }
        public virtual void DeleteFondoInversionAction() { }
        public virtual void SetStateFondoInversionAction() { }

        #endregion

		#region Buttons

		private void AddCuentaAsociada_TI_Click(object sender, EventArgs e)
		{
			AddCuentaAsociadaAction();
		}

		private void ChangeStateCuentaAsociada_TI_Click(object sender, EventArgs e)
		{
			SetStateCuentaAsociadaAction();
		}

		private void DeleteCuentaAsociada_TI_Click(object sender, EventArgs e)
		{
			DeleteCuentaAsociadaAction();
		}

        private void Cancelacion_BT_Click(object sender, EventArgs e)
        {
            RealizarCancelacionAction();
        }

        private void AddFondoInvesion_TI_Click(object sender, EventArgs e)
        {
            AddFondoInversionAction();
        }

        private void DeleteFondoInversion_TI_Click(object sender, EventArgs e)
        {
            DeleteFondoInversionAction();
        }

        private void SetStateFondoInversion_TI_Click(object sender, EventArgs e)
        {
            SetStateFondoInversionAction();
        }

		#endregion

		#region Events

		private void Datos_DataSourceChanged(object sender, EventArgs e)
        {
            //SetDependentControlSource(ID_GB.Name);
        }

		
        #endregion

    }
}