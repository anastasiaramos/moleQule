using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;

namespace moleQule.Library
{
	public class BCEngine<T> 
		where T : IBlockCipher
	{
		private readonly Encoding _encoding;
		private readonly IBlockCipher _blockCipher;
		private PaddedBufferedBlockCipher _cipher;
		private IBlockCipherPadding _padding;

		public BCEngine(T blockCipher)
			: this(blockCipher, Encoding.UTF8) {}

		public BCEngine(T blockCipher, Encoding encoding)
		{
			_blockCipher = blockCipher;
			_encoding = encoding;
		}

		public void SetPadding(IBlockCipherPadding padding)
		{
			if (padding != null)
				_padding = padding;
		}

		public string Encrypt(string plain, string key)
		{
			byte[] result = BouncyCastleCrypto(true, _encoding.GetBytes(plain), key);
			return Convert.ToBase64String(result);
		}

		public string Decrypt(string cipher, string key)
		{
			byte[] result = BouncyCastleCrypto(false, Convert.FromBase64String(cipher), key);
			return _encoding.GetString(result);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="forEncrypt"></param>
		/// <param name="input"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <exception cref="CryptoException"></exception>
		private byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, string key)
		{
			try
			{
				_cipher = _padding == null ? new PaddedBufferedBlockCipher(_blockCipher) : new PaddedBufferedBlockCipher(_blockCipher, _padding);
				byte[] keyByte = _encoding.GetBytes(key);
				_cipher.Init(forEncrypt, new KeyParameter(keyByte));
				return _cipher.DoFinal(input);
			}
			catch (Org.BouncyCastle.Crypto.CryptoException ex)
			{
				throw new CryptoException(ex.Message, ex);
			}
		}
	}

	public class AESEncryptor : BCEngine<AesEngine>
	{
		public AESEncryptor()
			: base(new AesEngine()) {}

		public static string Encryption(string plain, string key) { return Encryption(plain, key, null); }
		public static string Encryption(string plain, string key, IBlockCipherPadding padding)
		{
			AESEncryptor encriptor = new AESEncryptor();
			encriptor.SetPadding(padding);
			return encriptor.Encrypt(plain, key);
		}

		public static string Decryption(string plain, string key) { return Encryption(plain, key, null); }
		public static string Decryption(string cipher, string key, IBlockCipherPadding padding)
		{
			AESEncryptor encriptor = new AESEncryptor();
			encriptor.SetPadding(padding);
			return encriptor.Decrypt(cipher, key);
		}		
	}
	
	public class BlowFishEncryptor : BCEngine<BlowfishEngine>
	{
		public BlowFishEncryptor()
			: base(new BlowfishEngine()) { }

		public static string Encryption(string plain, string key) { return Encryption(plain, key, null); }
		public static string Encryption(string plain, string key, IBlockCipherPadding padding)
		{
			BlowFishEncryptor encriptor = new BlowFishEncryptor();
			encriptor.SetPadding(padding);
			return encriptor.Encrypt(plain, key);
		}

		public static string Decryption(string plain, string key) { return Encryption(plain, key, null); }
		public static string Decryption(string cipher, string key, IBlockCipherPadding padding)
		{
			BlowFishEncryptor encriptor = new BlowFishEncryptor();
			encriptor.SetPadding(padding);
			return encriptor.Decrypt(cipher, key);
		}
	}
}
