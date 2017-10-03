using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
	public partial class CompanyAddForm : CompanyUIForm
    {
        #region Factory Methods

        public CompanyAddForm(Form parent) 
			: this(null, parent) {}

		public CompanyAddForm(Company entity, Form parent) 
			: base(-1, new object[1] { entity }, true, parent)
		{
			InitializeComponent();
			SetFormData();
            _mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			if (parameters[0] == null)
			{
				_entity = Company.New();
				_entity.BeginEdit();
			}
			else
			{
				_entity = (Company)parameters[0];
				_entity.BeginEdit();
			}
		}

		#endregion
	}
}

