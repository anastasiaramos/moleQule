using System;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
    public partial class UserSelectForm : UserMngForm
    {

        #region Factory Methods

        public UserSelectForm()
            : this(null) {}

        public UserSelectForm(Form parent)
            : this(parent, UserList.GetList()) {}
		
		public UserSelectForm(Form parent, UserList list)
            : base(true, parent, list)
        {
            InitializeComponent();
			
			SetView(molView.Select);
			
            DialogResult = DialogResult.Cancel;
        }
		
        #endregion

        #region Layout & Source

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
