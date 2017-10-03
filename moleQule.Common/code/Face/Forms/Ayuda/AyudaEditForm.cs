using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class AyudaEditForm : AyudaUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "AyudaEditForm";
		public new static Type Type { get { return typeof(AyudaEditForm); } }

		#endregion
		
        #region Factory Methods

        public AyudaEditForm(long oid)
            : this(oid, null) {}

		public AyudaEditForm(long oid, Form parent)
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
            _entity = Ayuda.Get(oid);
            _entity.BeginEdit();
        }

        #endregion

	}
}
