using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Face;

namespace moleQule.Face.Common
{
	public partial class InputDecimalForm : Skin01.InputSkinForm
	{
		#region Attributes & Properties

		decimal _value = 0;

		public decimal Value { get { return _value; } set { _value = value; } }
		public string Message { get { return Source_GB.Text; } set { Source_GB.Text = value; } }

		#endregion

		#region Factory Methods

		public InputDecimalForm()
		{
			InitializeComponent();
		}

		#endregion

		#region Actions

		protected override void SubmitAction()
		{
			try
			{
				_value = Value_NTB.DecimalValue;
			}
			catch { _value = 0; }

			_action_result = DialogResult.OK;
		}

		#endregion
	}
}
