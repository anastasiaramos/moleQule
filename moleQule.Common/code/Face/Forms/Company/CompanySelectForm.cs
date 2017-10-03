using System;
using System.Windows.Forms;

using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class CompanySelectForm : CompanyMngForm
    {
		#region Factory Methods

		public new const string ID = "CompanySelectForm";
		public new static Type Type { get { return typeof(CompanySelectForm); } }

		public CompanySelectForm()
			: this(null) {}

		public CompanySelectForm(Form parent, CompanyList list = null)
			: base(true, parent, list)
		{
			InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;

			Text = "Seleccione la empresa activa";
		}

		#endregion

		#region Actions

		protected override void DefaultAction() { ExecuteAction(molAction.Select); }

		#endregion
    }
}
