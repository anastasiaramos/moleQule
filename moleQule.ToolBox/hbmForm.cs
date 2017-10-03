using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace moleQule.ToolBox
{
	public partial class hbmForm : Form
	{
		public hbmForm()
		{
			InitializeComponent();
		}

		private void Examinar_Button_Click(object sender, EventArgs e)
		{
			FolderBrowser.SelectedPath = Application.StartupPath;

			if (FolderBrowser.ShowDialog() == DialogResult.OK)
			{
				if (!File.Exists(FolderBrowser.SelectedPath + "\\hibernate_nh0001.cfg.xml"))
				{
					Model_TB.Text = string.Empty;
					MessageBox.Show("La carpeta seleccionada no contiene un modelo de fichero de configuración válido.",
									"Aviso",
									MessageBoxButtons.OK);
				}
				else
				{
					Model_TB.Text = FolderBrowser.SelectedPath;
					if (!Directory.Exists(Copy_TB.Text))
						Copy_TB.Text = Model_TB.Text;
				}
			}
		}

		private void Folder_Button_Click(object sender, EventArgs e)
		{
			FolderBrowser.SelectedPath = Application.StartupPath;

			if (FolderBrowser.ShowDialog() == DialogResult.OK)
			{
				Copy_TB.Text = FolderBrowser.SelectedPath;
			}
		}

		private void Aceptar_Button_Click(object sender, EventArgs e)
		{
			try
			{
				string line = null;
				string newName = null;
				string[] lines = File.ReadAllLines(Model_TB.Text + "\\hibernate_nh0001.cfg.xml");
				int pos = 0;

				DirectoryInfo newDir = null;
				StreamWriter newFile = null;

				for (int numFile = 2; numFile <= 10; numFile++)
				{
					// Fichero de configuración general

					newName = Copy_TB.Text + "\\hibernate_nh" + numFile.ToString("0000") + ".cfg.xml";
					newFile = File.CreateText(newName);
					pos = 0;

					while (pos < lines.Length)
					{
						line = lines[pos++];
						newFile.WriteLine(line.Replace("nh0001", "nh" + numFile.ToString("0000")));
					}

					if (newFile != null) newFile.Close();

					// Carpetas de ficheros de configuración de objetos

					string[] fileLines = null;
					string[] fileEntries = Directory.GetFiles(Copy_TB.Text + "\\..\\nh0001\\");

					if (fileEntries.Length == 0) continue;
					
					newName = Copy_TB.Text + "\\..\\nh" + numFile.ToString("0000");
					if (!Directory.Exists(newName))
						newDir = Directory.CreateDirectory(newName);

					// Recorremos todos los ficheros
					foreach (string fileName in fileEntries)
					{
						// Lineas del fichero
						fileLines = File.ReadAllLines(fileName);
						newFile = File.CreateText(fileName.Replace("nh0001", "nh" + numFile.ToString("0000")));
						pos = 0;

						while (pos < fileLines.Length)
						{
							line = fileLines[pos++];
							newFile.WriteLine(line.Replace("0001", numFile.ToString("0000")));
						}

						if (newFile != null) newFile.Close();
					}
				}

				MessageBox.Show("Ficheros generados con éxito",
								Application.ProductName,
								MessageBoxButtons.OK);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Cancelar_Button_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}