using System;
using System.Windows.Forms;

using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class PesajeSelectForm : PesajeMngForm
    {
        #region Factory Methods

        public PesajeSelectForm()
            : this(null) {}

        public PesajeSelectForm(Form parent)
            : this(parent, PesajeList.GetList(false)) {}
		
		public PesajeSelectForm(Form parent, PesajeList list)
            : base(true, parent, list)
        {
            InitializeComponent();
			
			SetView(molView.Select);
			
            DialogResult = DialogResult.Cancel;
        }
		
        #endregion

        #region Style & Source

        /// <summary>Formatea los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            SetSelectView();
            base.FormatControls();
        }

        #endregion

        #region Actions

        /// <summary>
        /// Accion por defecto. Se usa para el Double_Click del Grid
        /// <returns>void</returns>
        /// </summary>
        protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}
