using System;
using System.Windows.Forms;

namespace moleQule.Face
{
	/// <summary>
	/// Clase Base para Formularios de Edición de Hijos de una Entidad
	/// </summary>
    public partial class ChildListMngBaseForm : moleQule.Face.ItemMngBaseForm
    {

		#region Factory Methods

		/// <summary>
		/// Constructor por defecto necesario para que el entorno cargue
		/// el formulario en modo desarrollo
		/// </summary>
		public ChildListMngBaseForm() : this(-1) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms)
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
        public ChildListMngBaseForm(long oid)
			: this(oid, true, null) { }

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms)
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ChildListMngBaseForm(long oid, bool isModal, Form parent)
            : base(oid, isModal, parent)
        {
            InitializeComponent();

            _oid = oid;
        }

		#endregion

		#region Layout & Source

		#endregion

	}
}