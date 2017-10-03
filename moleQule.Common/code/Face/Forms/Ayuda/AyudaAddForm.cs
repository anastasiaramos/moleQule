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
    public partial class AyudaAddForm : AyudaUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "AyudaAddForm";
		public new static Type Type { get { return typeof(AyudaAddForm); } }

		#endregion
		
        #region Factory Methods

        public AyudaAddForm() 
			: this((Form)null) { }

        public AyudaAddForm(Form parent)
            : base(parent)
        {
            InitializeComponent();

            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        protected override void GetFormSourceData()
        {
			_entity = Ayuda.New();
            _entity.BeginEdit();
        }

        #endregion
	}
}
