using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class BankAccountEditForm : BankAccountUIForm
    {
        #region Attributes & Properties

        public new const string ID = "BankAccountEditForm";
		public new static Type Type { get { return typeof(BankAccountEditForm); } }

		#endregion
		
        #region Factory Methods

        public BankAccountEditForm(long oid)
            : this(oid, null) {}

        public BankAccountEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null)
			{
				_entity.CloseSession();
			}
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = BankAccount.Get(oid);
            _entity.BeginEdit();
        }

        #endregion
    }
}