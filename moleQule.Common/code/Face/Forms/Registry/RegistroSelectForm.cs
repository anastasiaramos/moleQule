using System;
using System.Windows.Forms;

using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class RegistroSelectForm : RegistroMngForm
    {

        #region Factory Methods

        public RegistroSelectForm()
            : this(null) {}

        public RegistroSelectForm(Form parent)
            : this(parent, null) {}
		
		public RegistroSelectForm(Form parent, RegistroList list)
            : base(true, parent, list)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
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
