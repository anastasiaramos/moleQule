using System;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class MunicipioAddForm : MunicipioUIForm
    {
        #region Attributes & Properties
		
        public new const string ID = "MunicipioAddForm";
		public new static Type Type { get { return typeof(MunicipioAddForm); } }

		#endregion
		
        #region Factory Methods

		public MunicipioAddForm(Form parent)
			: this((Municipio)null, parent) {}

		public MunicipioAddForm(Municipio entity, Form parent) 
			: this(new object[1] { entity }, parent) {}

		public MunicipioAddForm(object[] parameters, Form parent)
			: base(-1, parameters, true, parent)
		{
			InitializeComponent();
			SetFormData();
			_mf_type = ManagerFormType.MFAdd;
		}

		protected override void GetFormSourceData(object[] parameters)
		{
			if (parameters[0] == null)
			{
				_entity = Municipio.New();
				_entity.BeginEdit();
			}
			else
			{
				_entity = (Municipio)parameters[0];
				_entity.BeginEdit();
			}
		}

        #endregion

        #region Buttons

        #endregion
    }
}
