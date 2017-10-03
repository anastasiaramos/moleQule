using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
	/// <summary>
	/// Clase Base para Selección de Datos para un Informe
	/// </summary>
	public partial class PrintSelectBaseForm : moleQule.Face.ReportSelectBaseForm
	{
		#region Attributes & Properties

		protected PrintType _type = PrintType.Detail;

		public virtual PrintType Type { get { return _type; } set { _type = value; } }

		#endregion

		#region Factory Methods

		public PrintSelectBaseForm() : this(true) {}

		public PrintSelectBaseForm(bool isModal)
			: base(isModal)
		{
			InitializeComponent();
		}

		#endregion

		#region Layout & Source

		#endregion

		#region Actions

        protected override void SubmitAction() { AcceptAction(); }

        protected override void AcceptAction() { throw new iQImplementationException("No se ha definido SubmitAction()"); }

		#endregion
	}
}

