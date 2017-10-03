using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using moleQule.Library;
using moleQule.Face.Skin02;

namespace moleQule.Face
{
    public partial class SchemaMngForm : SchemaLMngSkinForm
    {

        #region Business Methods

        public const string ID = "SchemaMngForm";
        public static Type Type { get { return typeof(SchemaMngForm); } }

        private new SortedBindingList<SchemaInfo> _sorted_list = null;

        internal new SchemaList List
        {
            get { return _item_list as SchemaList; }
            set { _item_list = value; _sorted_list = (value as SchemaList).GetSortedList(); }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public SchemaMngForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Tipo del formulario que maneja la entidad principal</param>
        /// <remarks>
        /// El constructor del SchemaMngForm debe inicializar el tipo de schema que va 
        /// a utilizarse como clase principal de la aplicacion. 
        /// </remarks>
        public SchemaMngForm(bool isModal, Form parent, Type type)
            : base(isModal, parent, type)
        {
            InitializeComponent();
        }

        #endregion

        #region Actions

        /// <summary>
        /// Marca en la tabla el schema por defecto
        /// </summary>
        public override void MarkDefaultSchema()
        {
            List.SetPrincipal(SettingsMng.Instance.GetDefaultSchema());
            Datos.ResetBindings(false);
        }

        #endregion
    }
}

