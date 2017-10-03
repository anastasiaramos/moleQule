using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
    public partial class ImageViewBaseForm : moleQule.Face.EntityDriverForm
    {

        #region Attributes & Properties

        protected string _path;

        public string Path { get { return _path; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor para formularios de insercion (AddForms)
        /// No se le especifica Oid asociado al formulario
        /// </summary>
        public ImageViewBaseForm() : this(false, string.Empty) { }

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ImageViewBaseForm(bool isModal, string path)
            : base(isModal, null)
        {
            InitializeComponent();
            _path = path;
            Images.ShowImage(Path, Image_PB);
        }
        
        #endregion

        #region Layout & Source

        #endregion

        #region Validation & Format

        #endregion

        #region Buttons

        #endregion

        #region Events


        private void ImageViewForm_Load(object sender, EventArgs e)
        {
            MaximizeForm();
        }

        #endregion
    }
}

