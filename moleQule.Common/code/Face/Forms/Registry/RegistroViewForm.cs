using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Face;

namespace moleQule.Face.Common
{
    public partial class RegistroViewForm : RegistroForm
    {
        #region Attributes & Properties
		
        public new const string ID = "RegistroViewForm";
		public new static Type Type { get { return typeof(RegistroViewForm); } }

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        /// Se trata del objeto actual.
        /// </summary>
        private RegistroInfo _entity;

        public override RegistroInfo EntityInfo { get { return _entity; } }

		#endregion
		
        #region Factory Methods

        public RegistroViewForm(long oid) 
			: this(oid, null) {}

        public RegistroViewForm(long oid, Form parent)
            : base(oid, true, parent)
        {
            InitializeComponent();
            SetFormData();
            _mf_type = ManagerFormType.MFView;
        }

        protected override void GetFormSourceData(long oid)
        {
            _entity = RegistroInfo.Get(oid, true);
            _mf_type = ManagerFormType.MFView;
        }

        #endregion

        #region Style

        public override void FormatControls()
        {
            base.FormatControls();
            SetReadOnlyControls(this.Controls);
        }

		protected override void SetGridFormat()
		{
			foreach (DataGridViewRow row in LineaRegistros_DGW.Rows)
			{
				if (row.IsNewRow) return;

				LineaRegistroInfo item = (LineaRegistroInfo)row.DataBoundItem;
				Face.Common.ControlTools.Instance.SetRowColor(row, item.EEstado);
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            PgMng.Grow();

			Datos_LineaRegistros.DataSource = _entity.LineaRegistros;
			PgMng.Grow();
						
            base.RefreshMainData();
        }
		
        protected override void SetUnlinkedGridValues(string gridName)
        {
            /*switch (gridName)
            {
                
				case "_Grid":
                    {
                        LineaRegistroList lineainforme = LineaRegistroList.GetList(false);
                        foreach (DataGridViewRow row in LineaRegistro_Grid.Rows)
                        {
                            if (row.IsNewRow) continue;
                            Alumno_ExamenInfo info = (Alumno_ExamenInfo)row.DataBoundItem;
                            if (info != null)
                            {
                                LineaRegistroInfo lineainforme = lineainformes.GetItem(info.OidLineaRegistro);
                                if (examen != null)
                                    row.Cells[LineaRegistro.Name].Value = lineainforme.Titulo;
                            }
                        }

                    } break;
				
            }*/
        }
		
        #endregion

        #region Validation & Format

        /// <summary>
        /// Asigna formato deseado a los controles del objeto cuando éste es modificado
        /// </summary>
        protected override void FormatData()
        {
        }

        #endregion

        #region Print

        #endregion

        #region Actions

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
			DialogResult = DialogResult.Cancel;
        }

        #endregion

        #region Events

        #endregion
    }
}
