using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face;
using moleQule.Library.Common;

namespace moleQule.Face.Common
{
    public partial class MunicipioForm : Skin01.ItemMngSkinForm
    {
        #region Attributes & Properties
		
        public const string ID = "MunicipioForm";
		public static Type Type { get { return typeof(MunicipioForm); } }

        protected override int BarSteps { get { return base.BarSteps + 0; } }
		
        public virtual Municipio Entity { get { return null; } set { } }
        public virtual MunicipioInfo EntityInfo { get { return null; } }

		
        #endregion

        #region Factory Methods

        public MunicipioForm() 
			: this(-1, null) { }

		public MunicipioForm(long oid, Form parent)
			: this(oid, null, true, parent) { }

		public MunicipioForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(oid, parameters, isModal, parent)
		{
			InitializeComponent();
		}
		
        #endregion

        #region Style & Source

        /// <summary>Da formato a los controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos principal
		/// y las listas hijas a los origenes de datos correspondientes
        /// </summary>
        protected override void RefreshMainData()
        {
			
        }

        /// <summary>
        /// Asigna los datos a los origenes de datos secundarios
        /// </summary>
        public override void RefreshSecondaryData()
		{
			
        }
		
		#endregion

        #region Validation & Format

        #endregion

        #region Print

        //public override void PrintObject()
        //{
        //    MunicipioReportMng reportMng = new MunicipioReportMng(AppContext.ActiveSchema);
        //    ReportViewer.SetReport(reportMng.GetReport(EntityInfo);
        //    ReportViewer.ShowDialog();
        //}

        #endregion

        #region Actions

        #endregion

        #region Events

        private void Datos_DataSourceChanged(object sender, EventArgs e)
        {
            //SetDependentControlSource(ID_GB.Name);
        }

		
        #endregion
    }
}
