using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face
{
	/// <summary>
	/// Clase Base para Formularios de Consulta, Edición y Borrado de una lista raíz de Entidades
	/// </summary>
	public partial class ManagerListForm : moleQule.Face.ManagerForm
	{

		#region Bussiness Methods

        #endregion
        
        #region Factory Methods
		
		/// <summary>
		/// Constructor para formularios de insercion (AddForms)
		/// No se le especifica Oid asociado al formulario
		/// </summary>
		public ManagerListForm() : this(false, null) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
        public ManagerListForm(bool isModal, Form parent)
			: base(isModal, parent)
		{
			InitializeComponent();

			GetFormData();
		}

		#endregion

		#region Style & Source

        #endregion

		#region Validation & Format

		#endregion

		#region Print

	    #endregion

		#region Buttons

        #endregion

        #region Events

		#endregion

	}
}