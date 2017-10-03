using System;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
    /// <summary>
    /// Clase Base asociada al Manejo de un Tipo de Entidad
	/// EntityMngForm maneja una lista de elementos de esta clase
    /// </summary>
	public partial class EntityDriverForm : moleQule.Face.ChildForm
	{
		#region Attributes & Properties

		#endregion

		#region Factory Methods

		/// <summary>
		/// Constructor por defecto.
		/// Necesario para los LocalizeForm
		/// </summary>
		protected EntityDriverForm() : this(false, null) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms)
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
        protected EntityDriverForm(bool isModal, Form parent)
            : base(isModal, parent)
		{
			InitializeComponent();
		}

        #endregion

        #region CloseForm

        /// <summary>
		/// Evento que se genera al cerrar el formulario
		/// </summary>
		public event EventHandler CloseForm;

        /// <summary>
        /// Función para que lance un evento al cerrar el formulario 
        /// o lo cierre directamente
        /// </summary>
        public void Cerrar()
        {
#if TRACE
			PgMng.Record("EntityDriverForm::Cerrar() INI");
#endif
            if (CloseForm != null)
                CloseForm(this, EventArgs.Empty);
#if TRACE
			PgMng.Record("EntityDriverForm::Cerrar() END");
#endif
        }

        private void EntityDriverForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cerrar();
        }

		#endregion
    }
}