using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class BankAccountAddForm : BankAccountUIForm
    {
        #region Attributes & Properties

        public new const string ID = "BankAccountAddForm";
		public new static Type Type { get { return typeof(BankAccountAddForm); } }

		#endregion
		
        #region Factory Methods

        public BankAccountAddForm() 
			: this((Form)null) { }

        public BankAccountAddForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public BankAccountAddForm(BankAccount source)
            : base()
        {
            InitializeComponent();
            _entity = source.Clone();
            _entity.BeginEdit();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
            _entity = BankAccount.New();
            _entity.BeginEdit();
        }

        #endregion

        #region Actions

        #endregion
    }
}