using Halsign.DWM.Framework;
using System;
using System.Security.Cryptography;
using System.Text;
namespace Halsign.DWM.Domain
{
	internal class Crypto
	{
		private static string _hashKey;
		private static string GetHashKey()
		{
			if (string.IsNullOrEmpty(Crypto._hashKey))
			{
				Crypto._hashKey = Configuration.GetValueAsString(ConfigItems.CryptoHash);
			}
			return Crypto._hashKey;
		}
		internal static string Encrypt(string text)
		{
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] key = mD5CryptoServiceProvider.ComputeHash(uTF8Encoding.GetBytes(Crypto.GetHashKey()));
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			tripleDESCryptoServiceProvider.Key = key;
			tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
			tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
			byte[] bytes = uTF8Encoding.GetBytes(text);
			byte[] inArray;
			try
			{
				ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
				inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			}
			finally
			{
				tripleDESCryptoServiceProvider.Clear();
				mD5CryptoServiceProvider.Clear();
			}
			return Convert.ToBase64String(inArray);
		}
		internal static string Decrypt(string text)
		{
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] key = mD5CryptoServiceProvider.ComputeHash(uTF8Encoding.GetBytes(Crypto.GetHashKey()));
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			tripleDESCryptoServiceProvider.Key = key;
			tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
			tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
			byte[] array = Convert.FromBase64String(text);
			byte[] bytes;
			try
			{
				ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
				bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			}
			finally
			{
				tripleDESCryptoServiceProvider.Clear();
				mD5CryptoServiceProvider.Clear();
			}
			return uTF8Encoding.GetString(bytes);
		}
	}
}
