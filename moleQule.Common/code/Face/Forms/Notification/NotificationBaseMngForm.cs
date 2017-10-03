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
	public partial class NotificationBaseMngForm : Skin05.EntityMngSkinForm
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
			get { return (Entidades_TV.SelectedNode != null) ? (NotifyEntity)Entidades_TV.SelectedNode.Tag : null; }
		}

		protected ChildForm OpenForm { get; set; }

		#endregion

		#region Factory Methods

		public NotificationBaseMngForm()
			: this(false) { }

		public NotificationBaseMngForm(string schema)
			: this(false, null, null) { }

		public NotificationBaseMngForm(bool isModal)
			: this(isModal, null, null) { }

		public NotificationBaseMngForm(Form parent)
			: this(false, parent, null) { }

		public NotificationBaseMngForm(bool isModal, Form parent, NotifyEntityList list)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Tree);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			//Datos.DataSource = NotifyEntityList.NewList().GetSortedList();
		}

		#endregion

		#region Business Methods

		protected override Type GetColumnType(string column_name)
		{
			return Tabla.Columns[column_name] != null ? Tabla.Columns[column_name].ValueType : null;
		}

		protected override string GetColumnProperty(string column_name)
		{
			return Tabla.Columns[column_name] != null ? Tabla.Columns[column_name].DataPropertyName : null;
		}

		#endregion

		#region Layout

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();

			MaximizeForm(new Size(300, 0));
			Left = 0;

			BuildTree();
		}

		protected virtual void BuildTree() { }

		protected virtual void ResetTree()
		{
			Entidades_TV.Nodes.Clear();
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			HideAction(molAction.Add);
			HideAction(molAction.Delete);
			HideAction(molAction.Edit);
			HideAction(molAction.EmailPDF);
			HideAction(molAction.FilterOff);
			HideAction(molAction.FilterOn);
			HideAction(molAction.Print);
			HideAction(molAction.View);
		}

		#endregion

		#region Source

		protected override void RefreshMainData() {}

		public override void RefreshSecondaryData() {}

		protected override void RefreshSources() {}

		public override void UpdateList() {}

		/// <summary>
		/// Selecciona un elemento de la tabla
		/// </summary>
		/// <param name="oid">Identificar del elemento</param>
		protected override void Select(long oid) {}

		/// <summary>
		/// Filtra la tabla
		/// </summary>
		/// <param name="oid">Identificar del elemento</param>
		protected override void SetFilter(bool on) {}

		#endregion

		#region Actions

		protected virtual void OpenMngFormAction(NotifyEntity item) {}

		protected override bool DoFind(object value)
		{
			return true;
		}

		protected override bool DoFilter(FilterItem fItem)
		{
			return true;
		}

		#endregion

		#region Events

		private void NotificacionBaseMngForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (OpenForm != null) OpenForm.ExecuteAction(molAction.Close);
		}

        private void Entidades_TV_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null) return;
            if ((NotifyEntity)e.Node.Tag == null) return;

            OpenMngFormAction((NotifyEntity)e.Node.Tag);
        }

		#endregion
	}
}
