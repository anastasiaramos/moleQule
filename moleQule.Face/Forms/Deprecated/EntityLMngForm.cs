using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face
{
	/// <summary>
	/// DEPRECATED. Mantenida por compatibilidad.
	/// Ver EntityMngBaseForm
	/// </summary>
	public partial class EntityLMngForm : moleQule.Face.EntityMngBaseForm
	{
		#region Factory Methods

        public EntityLMngForm() : this(false, null) {}

        public EntityLMngForm(bool isModal) : this(isModal, null) {}

        public EntityLMngForm(bool isModal, Form parent) : base(isModal, parent) 
        {
            InitializeComponent();
        }

        public EntityLMngForm(bool isModal, Form parent, object list)
            : this(isModal, parent, EntityMngFormTypeData.ByParameter, list) {}

        public EntityLMngForm(bool isModal, Form parent, EntityMngFormTypeData data_type, object list)
            : base(isModal, parent, data_type, list)
        {
            InitializeComponent();
        }

		#endregion          
    }
}