using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face.Skin01
{
	/// <summary>
	/// Clase Base para Selección de Datos para un Informe
	/// dentro de una entidad (ManagerForm)
	/// </summary>
	public partial class PrintSelectSkinForm : moleQule.Face.PrintSelectBaseForm
	{

		#region Business Methods

		public override PrintType Type
		{
			get { return _type; }
			set
			{
				_type = value;
				Detalle_RB.Checked = (_type == PrintType.Detail);
				Lista_RB.Checked = (_type == PrintType.List);
			}
		}
		public override PrintSource Source
		{
			get { return _source; }
			set
			{
				_source = value;
				Seleccion_RB.Checked = (_source == PrintSource.Selection);
				Todos_RB.Checked = (_source == PrintSource.All);
			}
		}

		public void EnableSelection(bool enable)
		{
			Seleccion_RB.Enabled = enable;
			if (!enable) Source = PrintSource.All;
		}
		public void EnableAll(bool enable)
		{
			Todos_RB.Enabled = enable;
			if (!enable) Source = PrintSource.Selection;
		}
		public void EnableDetail(bool enable)
		{
			Detalle_RB.Enabled = enable;
			if (!enable) Type = PrintType.List;
		}
		public void EnableList(bool enable)
		{
			Lista_RB.Enabled = enable;
			if (!enable) Type = PrintType.Detail;
		}

		#endregion

		#region Factory Methods

		public PrintSelectSkinForm() : this(true) {}

		public PrintSelectSkinForm(bool isModal)
			: base(isModal)
		{
			InitializeComponent();
		}

		#endregion

		#region Buttons

		private void Aceptar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Submit); }

		private void Cancelar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

		#endregion

		#region Events

		private void Source_GB_Validated(object sender, EventArgs e)
		{
			_source = Seleccion_RB.Checked ? PrintSource.Selection : PrintSource.All;
		}

		private void Type_GB_Validated(object sender, EventArgs e)
		{
			_type = Detalle_RB.Checked ? PrintType.Detail : PrintType.List;
		}

		#endregion

	}
}

