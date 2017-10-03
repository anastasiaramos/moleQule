using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
	public partial class CompanyEditForm : CompanyUIForm
    {
        #region Factory Methods

        public CompanyEditForm(long oid, Form parent)
            : base(oid, null, true, parent)
		{
			InitializeComponent();
            if (_entity != null) SetFormData();            
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();

			base.DisposeForm();
		}

		protected override void GetFormSourceData(long oid)
		{
			_entity = Company.Get(oid);
			_entity.BeginEdit();
		}

        #endregion
	}
}

