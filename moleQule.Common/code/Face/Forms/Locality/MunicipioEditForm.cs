using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class MunicipioEditForm : MunicipioUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "MunicipioEditForm";
		public new static Type Type { get { return typeof(MunicipioEditForm); } }

		#endregion
		
        #region Factory Methods

		public MunicipioEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (_entity != null) SetFormData();            
			_mf_type = ManagerFormType.MFEdit;
        }

		public override void DisposeForm()
		{
			if (_entity != null) _entity.CloseSession();
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = Municipio.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

        #region Actions

        #endregion

		#region Events

		private void MunicipioEditForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_entity != null) _entity.CloseSession();
		}

		#endregion

    }
}
