using System;
using System.Windows.Forms;

using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class MunicipioSelectForm : MunicipioMngForm
    {
        #region Factory Methods

        public MunicipioSelectForm(Form parent)
            : this(parent, null) {}
		
		public MunicipioSelectForm(Form parent, MunicipioList list)
            : base(true, parent, list)
        {
            InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
        }
		
        #endregion

        #region Style & Source

        public override void UpdateList()
        {
            if (_entity != null) _selected = _entity.GetInfo();

            base.UpdateList();
        }

        #endregion

        #region Actions

        protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        #endregion
    }
}
