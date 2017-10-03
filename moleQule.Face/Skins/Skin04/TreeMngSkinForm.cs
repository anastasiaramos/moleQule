using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx;
using moleQule.Library;

namespace moleQule.Face.Skin04
{
	/// <summary>
	/// Clase Base para Gestión de un Tipo de Entidad. 
	/// Consulta, Creación, Edición, Borrado, Filtrado y Localización.
	/// Se gestiona mediante una Lista de Elementos de ese tipo
	/// </summary>
	public partial class TreeMngSkinForm : EntityMngBaseForm
	{
		#region Constants

		protected const string OPEN_IMAGE_KEY = "Open";
		protected const string CLOSE_IMAGE_KEY = "Close";

		#endregion

		#region Attributes

		protected List<string> _properties_list = new List<string>();

		protected bool _show_filter_msg = true;

		protected DateTime _fecha = DateTime.Today;

		#endregion

		#region Factory Methods

		public TreeMngSkinForm()
            : this(false) {}

		public TreeMngSkinForm(bool isModal)
            : this(false, null) { }

		public TreeMngSkinForm(bool isModal, Form parent)
			: this(isModal, parent, null) { }

		public TreeMngSkinForm(bool isModal, Form parent, object list)
			: base(isModal, parent, list)
        {
            InitializeComponent();
			ViewMode = molView.Normal;
        }

		public override void InitForm()
		{
#if TRACE
			PgMng.Record("TreeMngSkinForm::InitForm INI");
#endif
			ApplyAuthorizationRules();
			RefreshSecondaryData();
			RefreshList();
			FormatForm();
#if TRACE
			PgMng.Record("TreeMngSkinForm::InitForm END");
#endif
		}

		#endregion

		#region Layout & Format

		protected override void ActivateAction(molAction action, bool state)
		{
			switch (action)
			{
				case molAction.CustomAction1:

					CustomAction1_TI.Enabled = state;
					CustomAction1_MI.Enabled = state;
					CustomAction1_TI.Visible = state;
					CustomAction1_MI.Visible = state;

					break;

				case molAction.CustomAction2:

					CustomAction2_TI.Enabled = state;
					CustomAction2_MI.Enabled = state;
					CustomAction2_TI.Visible = state;
					CustomAction2_MI.Visible = state;

					break;

				case molAction.CustomAction3:

					CustomAction3_TI.Enabled = state;
					CustomAction3_MI.Enabled = state;
					CustomAction3_TI.Visible = state;
					CustomAction3_MI.Visible = state;

					break;

				case molAction.Print:

					Print_TI.Enabled = state;
					Print_MI.Enabled = state;
					Print_TI.Visible = state;
					Print_MI.Visible = state;

					PrintBlock_TSS.Visible = state;

					break;

				case molAction.Refresh:

					Refresh_TI.Enabled = state;
					Refresh_MI.Enabled = state;
					Refresh_TI.Visible = state;
					Refresh_MI.Visible = state;

					break;
			}
		}

		public override void FormatControls()
		{
			BuildTree();

			base.FormatControls();

			FormatTree();
		}

		protected virtual void FormatTree()
		{
			foreach (TreeNode node in Tree_TV.Nodes)
			{
			}
		}

		protected void SetActionStyle(molAction action, string title, Bitmap image)
		{
			switch (action)
			{
				case molAction.CustomAction1:
					{
						CustomAction1_TI.Image = image;
						CustomAction1_TI.Text = title;

						CustomAction1_MI.Image = image;
						CustomAction1_MI.Text = title;
					}
					break;

				case molAction.CustomAction2:
					{
						CustomAction2_TI.Image = image;
						CustomAction2_TI.Text = title;

						CustomAction2_MI.Image = image;
						CustomAction2_MI.Text = title;
					}
					break;

				case molAction.CustomAction3:
					{
						CustomAction3_TI.Image = image;
						CustomAction3_TI.Text = title;

						CustomAction3_MI.Image = image;
						CustomAction3_MI.Text = title;
					}
					break;

			}
		}

		protected override void SetView(molView view)
		{
			ViewMode = view;

			HideAction(molAction.CustomAction1);
			HideAction(molAction.CustomAction2);
			HideAction(molAction.CustomAction3);
			ShowAction(molAction.DateSelection);
			ShowAction(molAction.Print);
			ShowAction(molAction.Refresh);
		}

		#endregion

		#region Source

		protected virtual void BuildTree() { }

		protected virtual TreeNode NewRootNode(string text)
		{
			TreeNode node = new TreeNode
			{
				Text = text,
				ImageKey = OPEN_IMAGE_KEY,
				SelectedImageKey = OPEN_IMAGE_KEY,
				NodeFont = new Font("Segoe UI", Tree_TV.Font.Size + 1, FontStyle.Bold),
				BackColor = Color.Gainsboro
			};

			Tree_TV.Nodes.Add(node);

			return node;
		}

		protected override void RefreshMainData()
		{
			
		}

		protected virtual void ResetTree()
		{
			Tree_TV.Nodes.Clear();
		}

		protected override void RefreshSources() { }

		public override void UpdateList() { }

		protected override void Select(long oid) { }

		protected override void SetFilter(bool on) { }

        #endregion

        #region Business Methods
		
        #endregion

        #region Actions

		public override void DoExecuteAction(molAction action)
		{
			switch (action)
			{
				case molAction.CustomAction1:

					CustomAction1();

					break;

				case molAction.CustomAction2:

					CustomAction2();
					break;

				case molAction.CustomAction3:

					CustomAction3();

					break;

				case molAction.DateSelection:

					DateSelectAction();

					break;

				case molAction.Print:

					PrintAction();

					break;

				case molAction.Refresh:

					RefreshAction();

					break;

				case molAction.Submit:

					SubmitAction();

					break;

				default:
					base.ExecuteAction(action);
					break;
			}
		}

		protected virtual void DateSelectAction() {}

		public override void RefreshAction()
		{
			try
			{
				ResetTree();

				PgMng.Reset(BarSteps, 1, Resources.Messages.REFRESHING_DATA, _parent);

				RefreshMainData();
				BuildTree();
				FormatTree();

				FormMngBase.Instance.CloseAllForms(this);
			}
			finally
			{
				PgMng.FillUp();
			}
		}

		#endregion

		#region Context Menu

        private void PrintDetail_MI_Click(object sender, EventArgs e)
        {
            ExecuteAction(molAction.PrintDetail);
        }

        #endregion

		#region Buttons

		private void Action1_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction1); }

		private void Action2_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction2); }

		private void Action3_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction3); }

		private void DateSelect_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.DateSelection); }

		private void Print_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

		private void Refresh_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Refresh); }

		#endregion

		#region Events

		private void Tree_TV_AfterSelect(object sender, TreeViewEventArgs e) { ExecuteAction(molAction.Submit); }

		private void Tree_TV_AfterExpand(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Level != 0) return;

			e.Node.ImageKey = CLOSE_IMAGE_KEY;
			e.Node.SelectedImageKey = CLOSE_IMAGE_KEY;

			Refresh();
		}

		private void Tree_TV_AfterCollapse(object sender, TreeViewEventArgs e)
		{
			if (e.Node.Level != 0) return;

			e.Node.ImageKey = OPEN_IMAGE_KEY;
			e.Node.SelectedImageKey = OPEN_IMAGE_KEY;

			Refresh();
		}

		#endregion

	}
}