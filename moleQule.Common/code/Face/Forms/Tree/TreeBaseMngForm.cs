using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using Csla;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Face.Common
{
	public partial class TreeBaseMngForm : Skin04.TreeMngSkinForm
	{
		#region Attributes & Properties

		public const string ID = "NotificationBaseMngForm";
		public static Type Type { get { return typeof(NotificationBaseMngForm); } }
		public override Type EntityType { get { return null; } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

		/// <summary>
		///  Lista de objetos de sólo lectura
		/// </summary>
		protected new NotifyEntityList List
		{
			get { return _item_list as NotifyEntityList; }
			set { _item_list = value; }
		}

		protected NotifyEntity CurrentNotification
		{
			get { return (Tree_TV.SelectedNode != null) ? (NotifyEntity)Tree_TV.SelectedNode.Tag : null; }
		}

		protected ChildForm OpenForm { get; set; }

		#endregion

		#region Factory Methods

		public TreeBaseMngForm()
			: this(false) { }

		public TreeBaseMngForm(string schema)
			: this(false, null, null) { }

		public TreeBaseMngForm(bool isModal)
			: this(isModal, null, null) { }

		public TreeBaseMngForm(Form parent)
			: this(false, parent, null) { }

		public TreeBaseMngForm(bool isModal, Form parent, NotifyEntityList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Tree);
		}

		#endregion

		#region Layout

		public override void FormatControls()
		{
			base.FormatControls();

			MaximizeForm(new Size(300, 0));
			Left = 0;
		}

		#endregion

		#region Source

        protected override TreeNode NewRootNode(string text) { return NewRootNode(text, false); }
		protected TreeNode NewRootNode(string text, bool setTotal = false)
		{
			TreeNode node = base.NewRootNode(text);

			node.Tag = new NotifyEntity()
            {
                ETipoNotificacion = ETipoNotificacion.Node, 
                Title = node.Text,
                SetTotal = setTotal,
                Level = 0
            };    
        
			return node;
		}

		#endregion

		#region Actions

		protected override void DateSelectAction()
		{
			InputDateForm form = new InputDateForm();

			form.ShowDialog(this);

			if (form.DialogResult == DialogResult.OK)
			{
				_fecha = form.Value;
				Date_TI.Text = _fecha.ToShortDateString();

				RefreshAction();
			}
		}

		protected virtual void OpenMngFormAction(NotifyEntity item) {}

		protected override void SubmitAction()
		{
			if (Tree_TV.SelectedNode == null) return;
			if ((NotifyEntity)Tree_TV.SelectedNode.Tag == null) return;

			OpenMngFormAction((NotifyEntity)Tree_TV.SelectedNode.Tag);
		}

		#endregion

		#region Events

		private void NotificacionBaseMngForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (OpenForm != null) OpenForm.ExecuteAction(molAction.Close);
		}

        private void Tree_TV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null) return;
            if ((NotifyEntity)e.Node.Tag == null) return;

            OpenMngFormAction((NotifyEntity)e.Node.Tag);
        }

		#endregion
	}
}