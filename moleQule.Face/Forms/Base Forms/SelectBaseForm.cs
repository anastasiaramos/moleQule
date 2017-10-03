using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
	/// <summary>
	/// Clase Base para Formularios de Selección de Elementos de una Lista
	/// </summary>
	public partial class SelectBaseForm : ChildForm
    {
        #region Attributes & Properties

        protected long _oid = -1;
        protected object _selected;
		
		public long Oid { get { return _oid; } }
        public object Selected { get { return _selected; } }

        #endregion

		#region Factory Methods

		public SelectBaseForm() : this(true) {}

		public SelectBaseForm(bool isModal)
			: base(isModal, null)
		{
			InitializeComponent();
		}

		#endregion

		#region Layout & Source

		/// <summary>
		/// Asigna una lista como Origen de Datos del formulario
		/// </summary>
		/// <param name="list"></param>
		public virtual void SetDataSource(object list)
		{
			Datos.DataSource = list;
		}

		#endregion

        #region Buttons

        protected override void SubmitAction()
        {
            _selected = Datos.Current;
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
            if (CloseForm != null)
                CloseForm(this, EventArgs.Empty);
        }

        #endregion
	}
}