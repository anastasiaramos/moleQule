using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class RegistroAddForm : RegistroUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "RegistroAddForm";
		public new static Type Type { get { return typeof(RegistroAddForm); } }

		#endregion
		
        #region Factory Methods

        public RegistroAddForm() 
			: this((Form)null) { }

        public RegistroAddForm(Form parent)
            : base(parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFAdd;
        }

        public RegistroAddForm(Registro source)
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
            _entity = Registro.New();
            _entity.BeginEdit();
        }

        #endregion

        #region Buttons

        #endregion
    }
}
