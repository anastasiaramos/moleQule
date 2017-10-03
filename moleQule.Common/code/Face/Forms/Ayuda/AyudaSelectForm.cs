using System;
using System.Windows.Forms;

using moleQule.Library.Common;

namespace moleQule.Face.Common
{
	public partial class AyudaSelectForm : AyudaMngForm
	{
		#region Factory Methods

		public AyudaSelectForm()
			: this(null) { }

		public AyudaSelectForm(Form parent)
			: this(parent, null) { }

		public AyudaSelectForm(Form parent, AyudaList list)
			: base(true, parent, list)
		{
			InitializeComponent();
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;
		}

		#endregion

		#region Actions

		protected override void DefaultAction() { ExecuteAction(molAction.Select); }

		#endregion
	}
}
