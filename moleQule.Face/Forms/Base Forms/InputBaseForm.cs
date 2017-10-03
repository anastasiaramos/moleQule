using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace moleQule.Face
{
    public partial class InputBaseForm : moleQule.Face.ActionBaseForm
    {
        #region Factory Methods

        public InputBaseForm() 
            : this(true) {}

		public InputBaseForm(bool isModal)
			: this(isModal, null) { }

		public InputBaseForm(bool isModal, Form parent)
			: base(isModal, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Source

        public void SetDataSource(object list)
        {
            Datos.DataSource = list;
        }

        #endregion
    }
}

