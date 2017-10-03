using System;
using System.Windows.Forms;

namespace moleQule.Face
{
    public partial class ImageViewSkinForm : moleQule.Face.ImageViewBaseForm
    {

        #region Factory Methods

        /// <summary>
        /// Constructor para formularios de insercion (AddForms)
        /// No se le especifica Oid asociado al formulario
        /// </summary>
        public ImageViewSkinForm() : this(false, string.Empty) {}

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ImageViewSkinForm(bool isModal, string path) : base(isModal, null) {}
        
        #endregion

    }
}

