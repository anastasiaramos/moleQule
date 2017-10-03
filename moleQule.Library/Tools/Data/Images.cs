using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

namespace moleQule.Library
{
    public static class Images
	{
		#region Business Methods

		public static string GetRootPath()
		{
			return AppControllerBase.Reg32GetServerPath();
		}
		
		#endregion

		#region General Pics Business Methods

		/// <summary>
		/// Función que remuestrea una imagen al tamaño maxSize y 
		/// la almacena en una carpeta 
		/// </summary>
		/// <param name="sourcePath">Ruta del fichero origen</param>
		/// <param name="destinationPath">Ruta del fichero destino</param>
		/// <param name="maxSize">Tamaño del lado mayor</param>
		public static string SaveImage(string sourcePath, string destinationPath, int maxSize)
		{
			System.Drawing.Image imagen = null;

			try
			{
				imagen = System.Drawing.Image.FromFile(sourcePath);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return string.Empty;
			}

			int altura = 0; //height
			int anchura = 0; //width

			if (imagen.Height > imagen.Width)
			{
				if (imagen.Height > maxSize)
				{
					float proporcion = (float)imagen.Height / (float)imagen.Width;
					altura = maxSize;
					anchura = (int)(((float)altura) / proporcion);
				}
				else
				{
					altura = imagen.Height;
					anchura = imagen.Width;
				}
			}
			else
			{
				if (imagen.Width > maxSize)
				{
					float proporcion = (float)imagen.Width / (float)imagen.Height;
					anchura = maxSize;
					altura = (int)(((float)anchura) / proporcion);
				}
				else
				{
					altura = imagen.Height;
					anchura = imagen.Width;
				}
			}

			Bitmap newImage = new Bitmap(imagen, new Size(anchura, altura));
			
			try
			{
				newImage.Save(destinationPath, ImageFormat.Jpeg);
			}
			catch (Exception ex)
			{
				MessageBox.Show(iQExceptionHandler.GetAllMessages(ex));
				return string.Empty;
			}

			newImage.Dispose();
			imagen.Dispose();

			return destinationPath;
		}

        /// <summary>
        /// Función que remuestrea una imagen al tamaño maxSize y 
        /// la almacena en una carpeta 
        /// </summary>
        /// <param name="sourcePath">Ruta del fichero origen</param>
        /// <param name="destinationPath">Ruta del fichero destino</param>
        /// <param name="maxSize">Tamaño del lado mayor</param>
        public static string SaveImage(string sourcePath, string destinationPath, int maxWidth, int maxHeight)
        {
            return SaveImage(sourcePath, destinationPath, maxWidth, maxHeight, false);
        }
        
        /// <summary>
        /// Función que remuestrea una imagen al tamaño maxSize y 
        /// la almacena en una carpeta 
        /// </summary>
        /// <param name="sourcePath">Ruta del fichero origen</param>
        /// <param name="destinationPath">Ruta del fichero destino</param>
        /// <param name="maxSize">Tamaño del lado mayor</param>
        public static string SaveImage(string sourcePath, string destinationPath, int maxWidth, int maxHeight, bool forzado)
        {
            System.Drawing.Image imagen = null;

            try
            {
                imagen = System.Drawing.Image.FromFile(sourcePath,true);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }

            int altura = 0; //height
            int anchura = 0; //width
            float prop_h = (float)imagen.Height / maxHeight;
            float prop_w = (float)imagen.Width / maxWidth;

            if (prop_h > prop_w)//hay que reducir más a lo alto que a lo ancho
            {
                float proporcion = (float)imagen.Height / (float)imagen.Width;
                altura = maxHeight;
                anchura = (int)(((float)altura) / proporcion);
                
            }
            else
            {
                float proporcion = (float)imagen.Width / (float)imagen.Height;
                anchura = maxWidth;
                altura = (int)(((float)anchura) / proporcion);
            }

            Bitmap newImage = new Bitmap(imagen, new Size(anchura, altura));

            try
            {
                //if (newImage.Width < 750 && newImage.Height < 750 && forzado)
                //{
                //    int ancho_relleno = 375 - (int)newImage.Width / 2;
                //    Bitmap aux = new Bitmap(ancho_relleno + newImage.Width, newImage.Height);

                //    for (int i = 0; i < aux.Width; i++)
                //    {
                //        for (int j = 0; j < aux.Height; j++)
                //        {
                //            if (i < ancho_relleno)
                //                aux.SetPixel(i, j, Color.White);
                //            else
                //            {
                //                Color color = Color.FromArgb(newImage.GetPixel(i - ancho_relleno, j).ToArgb());
                //                aux.SetPixel(i, j, color);
                //            }
                //        }
                //    }

                //    //path = Images.GetRootPath() + Resources.Paths.FOTO_PREGUNTAS_EXAMENES.Substring(2) + "temp.jpg";
                //    //aux.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                //    aux.Save(destinationPath, ImageFormat.Jpeg);
                //    aux.Dispose();
                //}
                //else
                    newImage.Save(destinationPath, imagen.RawFormat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(iQExceptionHandler.GetAllMessages(ex));
                return string.Empty;
            }

            newImage.Dispose();
            imagen.Dispose();

            return destinationPath;
        }
        
        /// <summary>
        /// Función que remuestrea una imagen al tamaño maxSize y 
        /// la almacena en una carpeta 
        /// </summary>
        /// <param name="sourcePath">Ruta del fichero origen</param>
        /// <param name="destinationPath">Ruta del fichero destino</param>
        /// <param name="maxSize">Tamaño del lado mayor</param>
        public static string SaveImageFormat(string sourcePath, string destinationPath, int maxWidth, int maxHeight)
        {
            System.Drawing.Image imagen = null;

            try
            {
                imagen = System.Drawing.Image.FromFile(sourcePath, true);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }

            int altura = 0; //height
            int anchura = 0; //width
            float prop_h = (float)imagen.Height / maxHeight;
            float prop_w = (float)imagen.Width / maxWidth;

            if (prop_h > prop_w)//hay que reducir más a lo alto que a lo ancho
            {
                float proporcion = (float)imagen.Height / (float)imagen.Width;
                altura = maxHeight;
                anchura = (int)(((float)altura) / proporcion);

            }
            else
            {
                float proporcion = (float)imagen.Width / (float)imagen.Height;
                anchura = maxWidth;
                altura = (int)(((float)anchura) / proporcion);
            }

            Bitmap newImage = new Bitmap(imagen, new Size(anchura, altura));

            try
            {
                newImage.Save(destinationPath, imagen.RawFormat);
            }
            catch (Exception ex)
            {
                MessageBox.Show(iQExceptionHandler.GetAllMessages(ex));
                return string.Empty;
            }

            newImage.Dispose();
            imagen.Dispose();

            return destinationPath;
        }
        
		/// <summary>
		/// Función que borra una foto de la carpeta correspondiente
		/// </summary>
		/// <param name="path">Ruta del fichero de imagen</param>
		public static void DeleteImage(string path)
		{
			if (File.Exists(path))
				File.Delete(path);
		}

		/// <summary>
		/// Función que muestra la foto del empleado
		/// </summary>
		/// <param name="path">Ruta del fichero de imagen</param>
		/// <param name="picBox">PictureBox en el que mostrar la imagen</param>
		public static void ShowImage(string path, PictureBox picBox)
		{
			picBox.SizeMode = PictureBoxSizeMode.Zoom;

			if (File.Exists(path))
			{
				picBox.ImageLocation = path;
			}
			else
			{
				picBox.ImageLocation = string.Empty;
				picBox.Image = Properties.Resources.nophoto;
			}
		}

		#endregion

		#region Application Pics Business Methods

		/// <summary>
		/// Almacena una imagen en la carpeta de instalación de la aplicación
		/// </summary>
		/// <param name="sourcePath"></param>
		/// <param name="destinationPath"></param>
        public static void Save(string sourcePath, string destinationPath, int maxSize) 
        {
            string ruta = Directory.GetParent(destinationPath).FullName;

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            SaveImage(sourcePath, destinationPath, maxSize);
        }
        public static void Save(string sourcePath, string destinationPath, string fileName) 
        { Save(sourcePath, destinationPath, fileName, 150); }
        public static void Save(string sourcePath, string destinationPath, string fileName, int maxSize)
		{
            Save(sourcePath, destinationPath + fileName, maxSize); 
		}
        public static void Save(string sourcePath, string destinationPath, string fileName, int maxWidth, int maxHeight)
        {
            Save(sourcePath, destinationPath, fileName, maxWidth, maxHeight, false);
        }
        public static void Save(string sourcePath, string destinationPath, string fileName, int maxWidth, int maxHeight, bool forzado)
        {
            string ruta = destinationPath;

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            ruta += fileName;

            Image imagen = Image.FromFile(sourcePath);

            if (imagen.RawFormat.Guid.Equals(ImageFormat.Jpeg.Guid) && !forzado)
                File.Copy(sourcePath, fileName);
            else
                SaveImage(sourcePath, ruta, maxWidth, maxHeight, forzado);
        }
        /// <summary>
        /// Guarda la imagen
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="fileName"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="forzado">Obliga a que se modifique el tamaño de la imagen al especificado</param>
        public static void Save(string sourcePath, string destinationPath, string fileName, int maxWidth, int maxHeight, ImageFormat formato)
        {
            Save(sourcePath, destinationPath, fileName, maxWidth, maxHeight, false, formato);
        }
        /// <summary>
        /// Guarda la imagen
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="fileName"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="forzado">Obliga a que se modifique el tamaño de la imagen al especificado</param>
        public static void Save(string sourcePath, string destinationPath, string fileName, int maxWidth, int maxHeight, bool forzado, ImageFormat formato)
        {
            string ruta = destinationPath;

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            ruta += fileName;

            Image imagen = Image.FromFile(sourcePath, true);

            if (imagen.RawFormat.Guid.Equals(formato.Guid) && !forzado)
                File.Copy(sourcePath, ruta);
            else
            {
                int altura = 0; //height
                int anchura = 0; //width
                float prop_h = (float)imagen.Height / maxHeight;
                float prop_w = (float)imagen.Width / maxWidth;

                if (prop_h > prop_w)//hay que reducir más a lo alto que a lo ancho
                {
                    float proporcion = (float)imagen.Height / (float)imagen.Width;
                    altura = maxHeight;
                    anchura = (int)(((float)altura) / proporcion);

                }
                else
                {
                    float proporcion = (float)imagen.Width / (float)imagen.Height;
                    anchura = maxWidth;
                    altura = (int)(((float)anchura) / proporcion);
                }

                Bitmap newImage = (Bitmap)imagen.GetThumbnailImage(anchura, altura, null, IntPtr.Zero);

                //Bitmap newImage = new Bitmap(imagen, anchura, altura);

                //Bitmap newImage = new Bitmap(anchura, altura, imagen.PixelFormat);
                //Graphics g = Graphics.FromImage((Image)newImage);
                //g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                //g.CompositingQuality = CompositingQuality.GammaCorrected;
                //g.CompositingMode = CompositingMode.SourceCopy;
                //g.SmoothingMode = SmoothingMode.Default;
                //g.PixelOffsetMode = PixelOffsetMode.None;

                //g.DrawImage(imagen, 0, 0, anchura, altura);
                //g.Dispose();

                newImage.Save(ruta, formato);

                newImage.Dispose();
            }
            imagen.Dispose();
        }

        public static void SaveFormat(string sourcePath, string destinationPath, string fileName, int maxWidth, int maxHeight)
        {
            SaveFormat(sourcePath, destinationPath, fileName, maxWidth, maxHeight, false);
        }
        public static void SaveFormat(string sourcePath, string destinationPath, string fileName, int maxWidth, int maxHeight, bool forzado)
        {
            string ruta = destinationPath;

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            ruta += fileName;

            Image imagen = Image.FromFile(sourcePath);

            if (imagen.RawFormat.Guid.Equals(ImageFormat.Jpeg.Guid) && !forzado)
                imagen.Save(ruta + ".jpg");
            else
            {
                if (imagen.RawFormat.Guid.Equals(ImageFormat.Bmp.Guid) && !forzado)
                    imagen.Save(ruta + ".bmp");
                else
                    SaveImageFormat(sourcePath, ruta, maxWidth, maxHeight);
            }
            
        }

		/// <summary>
		/// Función que muestra una foto obtenida a partir de la carpeta de instalación de la aplicación
		/// </summary>
		/// <param name="path">Ruta del fichero de imagen</param>
		public static void Show(string fileName, string sourcePath, PictureBox picBox) { Show(sourcePath + fileName, picBox); }
        public static void Show(string sourcePath, PictureBox picBox)
        {
            ShowImage(sourcePath, picBox);
        }

		/// <summary>
		/// Función que borra una foto obtenida a partir de la carpeta de instalación de la aplicación
		/// </summary>
		/// <param name="path">Ruta del fichero de imagen</param>
		public static void Delete(string fileName, string sourcePath)
		{
			string ruta = GetRootPath() + sourcePath + fileName;
			DeleteImage(ruta);
		}

        /// <summary>
		/// Función que renombra una foto con otro nombre dentro de la misma carpeta
		/// </summary>
		/// <param name="path">Ruta del fichero de imagen</param>
        public static void Rename(string sourceFileName, string destinationFileName, string Path)
        {
			string source = GetRootPath() + Path + sourceFileName;
			string dest = GetRootPath() + Path + destinationFileName;
            File.Copy(source, dest, true);
            //SaveImage(source, dest, 150);
            DeleteImage(source);
        }

        /// <summary>
        /// Guarda la imagen
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="fileName"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="forzado">Obliga a que se modifique el tamaño de la imagen al especificado</param>
        public static void SaveJPG(string sourcePath, string destinationPath, string fileName, int maxWidth, int maxHeight, bool forzado)
        {
            string ruta = destinationPath;
            int resolucion = 0;

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            ruta += fileName;

            Image imagen = Image.FromFile(sourcePath);

            if (imagen.RawFormat.Guid.Equals(ImageFormat.Jpeg.Guid) && !forzado)
                imagen.Save(ruta);
            else
            {
                Bitmap bmPhoto = new Bitmap(imagen.Width, imagen.Height, imagen.PixelFormat);

                // No vamos a permitir dar una resolución mayor de la que tiene
                resolucion = Convert.ToInt32(bmPhoto.HorizontalResolution);
                resolucion = resolucion <= Convert.ToInt32(bmPhoto.VerticalResolution) ? resolucion : Convert.ToInt32(bmPhoto.VerticalResolution);

                bmPhoto.SetResolution(resolucion, resolucion);
                Graphics grPhoto = Graphics.FromImage(bmPhoto);

                grPhoto.CompositingQuality = CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = SmoothingMode.HighQuality;
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
                grPhoto.DrawImage(imagen, new Rectangle(0, 0, imagen.Width, imagen.Height), new Rectangle(0, 0, imagen.Width, imagen.Height), GraphicsUnit.Pixel);

                bmPhoto.Save(ruta, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
        
        /// <summary>
        /// Guarda la imagen
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        /// <param name="fileName"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <param name="forzado">Obliga a que se modifique el tamaño de la imagen al especificado</param>
        public static void SaveJPG(Bitmap newImage, string destinationPath, string fileName, int maxWidth, int maxHeight, bool forzado)
        {
            string ruta = destinationPath;
            int resolucion = 0;

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            ruta += fileName;

            //Image imagen = Image.FromFile(sourcePath);

            if (newImage.RawFormat.Guid.Equals(ImageFormat.Jpeg.Guid) && !forzado)
                newImage.Save(ruta);
            else
            {
                Bitmap bmPhoto = new Bitmap(newImage.Width, newImage.Height, newImage.PixelFormat);

                // No vamos a permitir dar una resolución mayor de la que tiene
                resolucion = Convert.ToInt32(bmPhoto.HorizontalResolution);
                resolucion = resolucion <= Convert.ToInt32(bmPhoto.VerticalResolution) ? resolucion : Convert.ToInt32(bmPhoto.VerticalResolution);

                bmPhoto.SetResolution(resolucion, resolucion);
                Graphics grPhoto = Graphics.FromImage(newImage);

                grPhoto.CompositingQuality = CompositingQuality.HighQuality;
                grPhoto.SmoothingMode = SmoothingMode.HighQuality;
                grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
                grPhoto.DrawImage(newImage, new Rectangle(0, 0, newImage.Width, newImage.Height), new Rectangle(0, 0, newImage.Width, newImage.Height), GraphicsUnit.Pixel);

                bmPhoto.Save(ruta, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

		#endregion

		#region QRCode

		public static Image GenerateQRCodeImage(string encode_data, QRCodeParams qRParams)
		{
			Image image = null;
			
			try
			{
				QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();

				qrCodeEncoder.QRCodeEncodeMode = qRParams.QRCodeEncodeMode;
				qrCodeEncoder.QRCodeScale = qRParams.QRCodeScale;
				qrCodeEncoder.QRCodeVersion = qRParams.QRCodeVersion;
				qrCodeEncoder.QRCodeErrorCorrect = qRParams.QRCodeErrorCorrect;

				if (encode_data.Length >= qRParams.QRCodeMaxLength)
					encode_data = encode_data.Substring(0, qRParams.QRCodeMaxLength);

				image = qrCodeEncoder.Encode(encode_data);
			}
			catch { return null; }

			return image;
		}

		public static byte[] GenerateQRCodeByte(string encode_data, QRCodeParams qRParams)
		{
			Image qrCodeImg = GenerateQRCodeImage(encode_data, qRParams);

			// Pasamos la imagen a bytes
			MemoryStream ms = new MemoryStream();
			qrCodeImg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
			byte[] image = ms.ToArray();
			ms.Close();

			return image;
		}

		#endregion
	}
}
