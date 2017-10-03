using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace moleQule.Face.Skin02
{
    public partial class SelectSkinForm : moleQule.Face.SelectBaseForm
    {
        #region Bussiness Methods

        /// <summary>
        /// Devuelve el OID del objeto activo seleccionado de la tabla
        /// </summary>
        /// <returns></returns>
        public virtual long ActiveOID
        {
            get
            {
                return 0;
            }
        }

        #endregion

        #region Factory Methods

        public SelectSkinForm()
        {
            InitializeComponent();
        }

        public SelectSkinForm(bool isModal) : base(isModal)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout & Source

        public virtual void OpenAddForm() { }
        public virtual void OpenEditForm() { }
        public virtual void OpenViewForm() { }
        public virtual void DeleteObject(long oid) { }

        #endregion

        #region Buttons

        private void Cerrar_Button_Click(object sender, EventArgs e)
        {
            Cerrar();
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            OpenAddForm();
        }
        
        private void Edit_Button_Click(object sender, EventArgs e)
        {
            OpenEditForm();
        }

        private void View_Button_Click(object sender, EventArgs e)
        {
            OpenViewForm();
        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            DeleteObject(ActiveOID);
        }

        #endregion

    }
}

