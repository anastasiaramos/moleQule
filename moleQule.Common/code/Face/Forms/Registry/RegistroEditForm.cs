using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class RegistroEditForm : RegistroUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "RegistroEditForm";
		public new static Type Type { get { return typeof(RegistroEditForm); } }

		#endregion
		
        #region Factory Methods

        public RegistroEditForm(long oid)
            : this(oid, null) {}

        public RegistroEditForm(long oid, Form parent)
            : base(oid, parent)
        {
            InitializeComponent();
            if (_entity != null) { SetFormData(); }
            _mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();

			base.DisposeForm();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = Registro.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

        #region Actions

        #endregion
    }
}
