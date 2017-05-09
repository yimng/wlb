using System;
using System.Security.Cryptography;
using System.Text;
namespace Halsign.DWM.Framework
{
	public class Crypto
	{
		private static Random randGenerator = new Random();
		private static string GetHashKey()
		{
			string text = Configuration.GetValueAsString(ConfigItems.CryptoHash, string.Empty);
			if (string.IsNullOrEmpty(text))
			{
				text = Crypto.GenHashKey();
				Configuration.SetValue(ConfigItems.CryptoHash, text);
			}
			return text;
		}
		private static string GenHashKey()
		{
			int value = 0;
			StringBuilder stringBuilder = new StringBuilder(20);
			for (int i = 0; i < 16; i++)
			{
				switch (Crypto.randGenerator.Next(1, 4))
				{
				case 1:
					value = Crypto.randGenerator.Next(48, 58);
					break;
				case 2:
					value = Crypto.randGenerator.Next(65, 91);
					break;
				case 3:
					value = Crypto.randGenerator.Next(97, 123);
					break;
				}
				stringBuilder.Append(Convert.ToChar(value));
			}
			return stringBuilder.ToString();
		}
		public static string Encrypt(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException();
			}
			byte[] inArray = new byte[0];
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] key = mD5CryptoServiceProvider.ComputeHash(uTF8Encoding.GetBytes(Crypto.GetHashKey()));
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			tripleDESCryptoServiceProvider.Key = key;
			tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
			tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
			byte[] bytes = uTF8Encoding.GetBytes(text);
			try
			{
				ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateEncryptor();
				inArray = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
			}
			catch
			{
			}
			finally
			{
				tripleDESCryptoServiceProvider.Clear();
				mD5CryptoServiceProvider.Clear();
			}
			return Convert.ToBase64String(inArray);
		}
		public static string Decrypt(string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException();
			}
			byte[] bytes = new byte[0];
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] key = mD5CryptoServiceProvider.ComputeHash(uTF8Encoding.GetBytes(Crypto.GetHashKey()));
			TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
			tripleDESCryptoServiceProvider.Key = key;
			tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
			tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
			byte[] array = Convert.FromBase64String(text);
			try
			{
				ICryptoTransform cryptoTransform = tripleDESCryptoServiceProvider.CreateDecryptor();
				bytes = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			}
			catch
			{
			}
			finally
			{
				tripleDESCryptoServiceProvider.Clear();
				mD5CryptoServiceProvider.Clear();
			}
			return uTF8Encoding.GetString(bytes);
		}
		public static string HashPassword(string text, string salt)
		{
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(salt))
			{
				throw new ArgumentException();
			}
			SHA384Managed sHA384Managed = new SHA384Managed();
			byte[] bytes = Encoding.UTF8.GetBytes(text + salt);
			byte[] inArray = sHA384Managed.ComputeHash(bytes);
			sHA384Managed.Clear();
			return Convert.ToBase64String(inArray);
		}
	}
}
