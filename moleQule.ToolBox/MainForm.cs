using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace moleQule.ToolBox
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void hbm_TI_Click(object sender, EventArgs e)
		{
			hbmForm form = new hbmForm();
			
			form.Show();
		}
	}
}