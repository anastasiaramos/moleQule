using System;
using System.Windows.Forms;

namespace moleQule.Face
{
	/// <summary>
	/// Clase Base para Formularios de Edición de Hijos de una Entidad
	/// </summary>
    public partial class ManagerChildListForm : moleQule.Face.ManagerEntityForm
    {

		#region Factory Methods

		/// <summary>
		/// Constructor por defecto necesario para que el entorno cargue
		/// el formulario en modo desarrollo
		/// </summary>
		public ManagerChildListForm() : this(-1, false, null) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms)
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
        public ManagerChildListForm(long oid, bool isModal, Form parent)
			: base(oid, isModal, parent)
		{
			InitializeComponent();

			_oid = oid;
		}

		#endregion

		#region Style & Source

		#endregion

	}
}