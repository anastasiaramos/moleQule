using System;
using System.Windows.Forms;

using moleQule.Face.Resources;
using moleQule.Library;

namespace moleQule.Face.Skin01
{
	/// <summary>
	/// Clase Base para Formularios de Edición de Hijos de una Entidad
	/// </summary>
	public partial class ChildListMngSkinForm : ChildListMngBaseForm
    {

		#region Factory Methods

		/// <summary>
		/// Constructor por defecto necesario para que el entorno cargue
		/// el formulario en modo desarrollo
		/// </summary>
		public ChildListMngSkinForm() : this(-1) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms)
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
        public ChildListMngSkinForm(long oid) : this(oid, null) {}

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms)
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ChildListMngSkinForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
        }

		#endregion

		#region Layout & Source

		protected void ShowStatusBar(string message)
		{
			this.Height = this.Height + BarraEstado_ST.Height;
			Paneles2.Panel2Collapsed = false;
			Paneles2.Panel2MinSize = BarraEstado_ST.Height;
			Info_SL.Text = message;
		}

		#endregion

		#region Buttons

        private void Guardar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Save); }

        private void Cancelar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

        private void Imprimir_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

		#endregion

	}
}