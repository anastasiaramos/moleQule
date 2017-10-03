using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face.Skin02
{
	/// <summary>
	/// Clase Base para Selecci�n de Datos para un Informe
	/// dentro de una entidad (ManagerForm)
	/// </summary>
	public partial class PrintSelectSkinForm : moleQule.Face.PrintSelectBaseForm
	{

		#region Business Methods

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

		#endregion

	}
}

