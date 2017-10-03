using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face.Skin02
{
    /// <summary>
    /// Clase Base para Gestión de un Tipo de Entidad. 
    /// Consulta, Creación, Edición, Borrado, Filtrado y Localización.
    /// Se gestiona mediante una Lista de Elementos de ese tipo
    /// </summary>
    public partial class LibrarySkinForm : moleQule.Face.EntityMngForm
    {

        #region Bussiness Methods

        public virtual long ActiveEntidadOID { get { return -1; } }
        public virtual long ActiveAgenteOID { get { return -1; } }
        public virtual long ActiveDocumentoOID { get { return -1; } }

        #endregion

        #region Factory Methods

        public LibrarySkinForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Layout & Source

        protected virtual void ApplyAgentFilter() { throw new iQImplementationException("ApplyAgentFilter"); }

        protected virtual void ApplyDocFilter() { throw new iQImplementationException("ApplyDocFilter"); }

        protected virtual void AgentSearch() { throw new iQImplementationException("AgentSearch"); }

        protected virtual void DocSearch() { throw new iQImplementationException("DocSearch"); }

        #endregion

        #region Buttons

        public virtual void DeleteEntidadObject(long oid) { throw new iQImplementationException("DeleteEntidadObject"); }
        public virtual void OpenEntidadAddForm() { throw new iQImplementationException("OpenEntidadAddForm"); }
        public virtual void OpenAgenteAddForm() { throw new iQImplementationException("OpenAgenteAddForm"); }
        public virtual void OpenAgenteEditForm() { throw new iQImplementationException("OpenAgenteEditForm"); }
        public virtual void DeleteAgenteObject(long oid) { throw new iQImplementationException("DeleteAgenteObject"); }
        public virtual void OpenDocumentoAddForm() { throw new iQImplementationException("OpenDocumentoAddForm"); }
        public virtual void OpenDocumentoViewForm() { throw new iQImplementationException("OpenDocumentoViewForm"); }
        public virtual void OpenDocumentoEditForm() { throw new iQImplementationException("OpenDocumentoEditForm"); }
        public virtual void DeleteDocumentoObject(long oid) { throw new iQImplementationException("DeleteDocumentoObject"); }

        private void Entidad_Del_BT_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Datos.Count > 0)
                    DeleteEntidadObject(ActiveEntidadOID);

                FormMngBase.Instance.RefreshFormsData();
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Entidad_Add_BT_Click(object sender, EventArgs e)
        {
            try
            {
#if TRACE
                Globals.Instance.Timer.Start();
#endif
                OpenEntidadAddForm();

#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Agente_Add_BT_Click(object sender, EventArgs e)
        {
            try
            {
#if TRACE                
                Globals.Instance.Timer.Start();
#endif
                OpenAgenteAddForm();
#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Agente_Edit_BT_Click(object sender, EventArgs e)
        {
            try
            {
#if TRACE
                Globals.Instance.Timer.Start();
#endif
                if (this.Datos.Count > 0)
                    OpenAgenteEditForm();
#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Agente_Del_BT_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Datos.Count > 0)
                    DeleteAgenteObject(ActiveAgenteOID);

                FormMngBase.Instance.RefreshFormsData();
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Documento_Add_BT_Click(object sender, EventArgs e)
        {
            try
            {
#if TRACE
                Globals.Instance.Timer.Start();
#endif
                OpenDocumentoAddForm();
#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Documento_View_BT_Click(object sender, EventArgs e)
        {
            try
            {
#if TRACE
                Globals.Instance.Timer.Start();
#endif
                if (this.Datos_Documentos.Count > 0)
                    OpenDocumentoViewForm();
#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Documento_Edit_BT_Click(object sender, EventArgs e)
        {
            try
            {
#if TRACE
                Globals.Instance.Timer.Start();
#endif
                if (this.Datos_Documentos.Count > 0)
                    OpenDocumentoEditForm();
#if TRACE
                MessageBox.Show(Globals.Instance.ProgressInfoMng.GetRecords());
#endif
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Documento_Del_BT_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Datos_Documentos.Count > 0)
                    DeleteDocumentoObject(ActiveDocumentoOID);

                FormMngBase.Instance.RefreshFormsData();
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Cierra el formulario y los formularios dependientes de la lista de
        /// formularios activos.
        /// <returns>void</returns>
        /// </summary>
        private void Close_Button_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        #endregion

        #region Context Menu

        private void Salir_MI_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private void Nueva_Entidad_MI_Click(object sender, EventArgs e)
        {
            OpenEntidadAddForm();
        }

        private void Eliminar_Entidad_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                DeleteEntidadObject(ActiveEntidadOID);
        }

        private void Nuevo_Agente_MI_Click(object sender, EventArgs e)
        {
            OpenAgenteAddForm();
        }

        private void Editar_Agente_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                OpenAgenteEditForm();
        }

        private void Eliminar_Agente_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos.Count > 0)
                DeleteAgenteObject(ActiveAgenteOID);
        }

        private void Agregar_Documento_MI_Click(object sender, EventArgs e)
        {
            OpenDocumentoAddForm();
        }

        private void Ver_Documento_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos_Documentos.Count > 0)
                OpenDocumentoViewForm();
        }

        private void Editar_Documento_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos_Documentos.Count > 0)
                OpenDocumentoEditForm();
        }

        private void Eliminar_Documento_MI_Click(object sender, EventArgs e)
        {
            if (this.Datos_Documentos.Count > 0)
                DeleteDocumentoObject(ActiveDocumentoOID);
        }

        #endregion

        #region Events

        /// <summary>
        /// Maximiza la ventana porque si utilizamos el Maximize lo aplica
        /// a todos los formularios abiertos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntityMngSkinForm_Load(object sender, EventArgs e)
        {
            this.MaximizeForm();
        }

        private void Filtros_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ApplyFilter();
            }
            catch (iQImplementationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TipoEntidad_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyAgentFilter();
        }

        private void TipoDocumento_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyDocFilter();
        }

        #endregion

    }
}

