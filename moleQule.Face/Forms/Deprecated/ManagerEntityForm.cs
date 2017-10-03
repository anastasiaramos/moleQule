using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face
{
    /// <summary>
    /// Clase Base para Formularios de Consulta, Edición y Borrado de Elementos de una Entidad
    /// </summary>
    public partial class ManagerEntityForm : ManagerForm
    {

        #region Bussiness Methods

        /// <summary>
        /// Oid que hace referencia al objeto que maneja la clase
        /// </summary>
        protected long _oid = -1;

        public long Oid { get { return _oid; } set { _oid = value; } }

		/// <summary>
		/// Timer para depuración
		/// </summary>
		protected moleQule.Library.Timer _timer;

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor para formularios de insercion (AddForms)
        /// No se le especifica Oid asociado al formulario
        /// </summary>
        public ManagerEntityForm() : this(-1, false, null) { }

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ManagerEntityForm(long oid, bool isModal, Form parent)
            : base(isModal, parent)
        {
			// Para depuración de tiempos
			_timer = new moleQule.Library.Timer();

            InitializeComponent();

            _oid = oid;

            if (_oid == -1)
                GetFormData();
            else
                GetFormData(_oid);
        }

        #endregion

        #region Buttons

        #endregion

    }
}

