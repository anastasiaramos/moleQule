using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
    /// <summary>
    /// Clase Base para Búsqueda y Filtrado de Elementos de un Tipo de Entidad
    /// </summary>
    public partial class LocalizeForm : moleQule.Face.EntityDriverForm
    {

		#region Business Methods

		protected object _filtered_list = null;
		public object FilteredList { get { return _filtered_list; } }

		private string _sortProperty = string.Empty;
		private ListSortDirection _sortDirection = ListSortDirection.Ascending;

        public EntityMngForm MngParent 
        { 
            get { throw new iQImplementationException("Parent"); } 
            set { throw new iQImplementationException("Parent"); } 
        }

		public string SortProperty
		{
			get { return _sortProperty; }
			set { _sortProperty = value;  }
		}
		public ListSortDirection SortDirection			
		{
			get { return _sortDirection; }
			set { _sortDirection = value; }
		}

		public virtual long ActiveOID() { return -1; }

		#endregion

        #region Factory Methods

        public LocalizeForm()
        {
            InitializeComponent();
        }

        #endregion

		#region Layout & Source

		/// <summary>
		/// Asigna la entidad y sus hijos al origen de datos
		/// <returns>void</returns>
		/// </summary>
		protected override void RefreshMainData() { }

		#endregion

		#region Find & Filter

		/// <summary>
        /// Evento que se genera al realizar una búsqueda
        /// </summary>
        public event EventHandler FindElement;

		/// <summary>
		/// Evento que se genera al realizar un filtrado
		/// </summary>
		public event EventHandler FilterElement;

		/// <summary>
		/// Lanza un evento al realizar una búsqueda
		/// </summary>
		protected void Find(string value)
		{
			if (value != String.Empty)
			{
				if (DoSearch())
				{
					if (FindElement != null)
						FindElement(this, EventArgs.Empty);
				}
			}
			else
			{
				MessageBox.Show(Resources.Messages.NO_SELECTED,
					"Campo de búsqueda vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// Lanza un evento al realizar un filtrado
		/// </summary>
		protected void Filter(string value)
		{
			if (value != String.Empty)
			{
				if (DoFilter())
				{
					if (FilterElement != null)
						FilterElement(this, EventArgs.Empty);
				}
			}
			else
			{
				MessageBox.Show(Resources.Messages.NO_SELECTED,
					"Campo de búsqueda vacío", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

        /// <summary>
        /// Lanza un evento al realizar una búsqueda
        /// </summary>
        public void Find()
        {
            if (DoSearch())
            {
                if (FindElement != null)
                    FindElement(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Lanza un evento al realizar un filtrado
        /// </summary>
        public void Filter()
        {
            if (DoFilter())
            {
                if (FilterElement != null)
                    FilterElement(this, EventArgs.Empty);
            }
        }

		protected virtual bool DoSearch() { return false; }

		protected virtual bool DoFilter() { return false; }

        #endregion

		#region Events

		private void LocalizeForm_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			Cerrar();
		}

		private void Datos_CurrentChanged(object sender, EventArgs e)
		{
			if (FindElement != null)
				FindElement(this, EventArgs.Empty);
		}

		#endregion

	}
}