using System;
using System.Globalization;
using System.Text;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

namespace moleQule.Library
{
	public class QRCodePrintBase
	{
		public byte[] QRCode { get; set; }

		public void LoadQRCode(string encode_data, QRCodeVersion version)
		{
			QRCode = Images.GenerateQRCodeByte(RemoveAccentsWithNormalization(encode_data), new QRCodeParams(version));
		}

		public static string RemoveAccentsWithNormalization(string inputString)
		{
			string normalizedString = inputString.Normalize(NormalizationForm.FormD);
			
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < normalizedString.Length; i++)
			{
				UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
				if (uc != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(normalizedString[i]);
				}
			}

			return NormalizeForQREncode((sb.ToString().Normalize(NormalizationForm.FormC)));
		}

		public static string NormalizeForQREncode(string text)
		{
			string nString = string.Empty;
			
			nString = text.Replace("º", "");
			nString = text.Replace("ª", "");

			return nString;
		}
	}

	public class QRCodeParams
	{
		#region Attributes & Properties

		public QRCodeEncoder.ENCODE_MODE QRCodeEncodeMode {get; set; }
		public int QRCodeScale { get; set; }
		public int QRCodeVersion { get; set; }
		public QRCodeEncoder.ERROR_CORRECTION QRCodeErrorCorrect { get; set; }
		public int QRCodeMaxLength { get; set; }

		#endregion

		#region Factory Methods

		public QRCodeParams(QRCodeVersion version)
		{
			Init(version);
		}

		private void Init(QRCodeVersion version)
		{
			switch (version)
			{
				case Library.QRCodeVersion.v5:

					QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
					QRCodeScale = 1;
					QRCodeVersion = (int)version;
					QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
					QRCodeMaxLength = 106;

					break;

				case Library.QRCodeVersion.v8:

					QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
					QRCodeScale = 1;
					QRCodeVersion = (int)version;
					QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
					QRCodeMaxLength = 192;

					break;

				case Library.QRCodeVersion.v10:

					QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
					QRCodeScale = 1;
					QRCodeVersion = (int)version;
					QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
					QRCodeMaxLength = 271;

					break;

				case Library.QRCodeVersion.v15:

					QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
					QRCodeScale = 1;
					QRCodeVersion = (int)version;
					QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
					QRCodeMaxLength = 520;

					break;
			}
		}

		#endregion
	}

	public enum QRCodeVersion { v5 = 5, v8 = 8, v10 = 10, v15 = 15 }

	public enum QREncodeVersion { v1 = 1 }
}
