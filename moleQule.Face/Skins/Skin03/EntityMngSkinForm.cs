using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace moleQule.Face.Skin03
{
    public partial class EntityMngSkinForm : moleQule.Face.Skin02.EntityLMngSkinForm
    {
		#region Attributes

		protected List<string> _properties_list = new List<string>();

		#endregion

		#region Factory Methods

		public EntityMngSkinForm()
            : this(false) {}

		public EntityMngSkinForm(bool is_modal)
			: this(is_modal, null) {}

		public EntityMngSkinForm(bool is_modal, Form parent)
			: this(is_modal, parent, null) {}

		public EntityMngSkinForm(bool is_modal, Form parent, object list)
			: base(is_modal, parent, list)
		{
			InitializeComponent();
			ViewMode = molView.Normal;
		}

		#endregion

		#region Layout

		protected override void SetView(molView view)
		{
			ViewMode = view;

			switch (ViewMode)
			{
				case molView.Select:

					ShowAction(molAction.Add);
					ShowAction(molAction.View);
					ShowAction(molAction.Edit);
					HideAction(molAction.Delete);
					HideAction(molAction.Unlock);
					HideAction(molAction.Print);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.PrintListQR);
					HideAction(molAction.ExportPDF);
					HideAction(molAction.EmailPDF);
					HideAction(molAction.EmailLink);
					HideAction(molAction.Copy);
					ShowAction(molAction.Select);
					ShowAction(molAction.SelectAll);
					ShowAction(molAction.FilterOn);
					ShowAction(molAction.FilterOff);
					ShowAction(molAction.AdvancedSearch);

					MaximizeForm(new Size(1024, 0));

					break;

				case molView.Normal:

					ShowAction(molAction.Add);
					ShowAction(molAction.View);
					ShowAction(molAction.Edit);
					HideAction(molAction.Copy);
					HideAction(molAction.Unlock);
					HideAction(molAction.PrintDetail);
					HideAction(molAction.PrintListQR);
					HideAction(molAction.EmailPDF);
					HideAction(molAction.EmailLink);
					HideAction(molAction.ExportPDF);
					HideAction(molAction.Select);
					HideAction(molAction.SelectAll);
					HideAction(molAction.AdvancedSearch);

					MaximizeForm();

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshSources()
		{
			DatosSearch.DataSource = Datos.DataSource;

			Fields_CB.DataSource = TablaBase.Columns;
			Fields_CB.DisplayMember = "HeaderText";
			Fields_CB.ValueMember = "DataPropertyName";

			_properties_list = ControlsMng.GetPropertiesList(TablaBase);

			if (_selectedOid > 0) Select(_selectedOid);
		}

		#endregion

	}
}

