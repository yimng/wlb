using Halsign.DWM.Framework;
using System;
using System.Security.Cryptography;
using System.Text;
namespace Halsign.DWM.WlbConfig
{
	internal class Crypto
	{
		private static string _hashKey;
		private static string GetHashKey()
		{
			if (string.IsNullOrEmpty(Crypto._hashKey))
			{
				Crypto._hashKey = Configuration.GetValueAsString(ConfigItems.CryptoHash, string.Empty);
				if (string.IsNullOrEmpty(Crypto._hashKey))
				{
					Crypto._hashKey = Crypto.GenHashKey();
					Configuration.SetValue(ConfigItems.CryptoHash, Crypto._hashKey);
				}
			}
			return Crypto._hashKey;
		}
		private static string GenHashKey()
		{
			int value = 0;
			StringBuilder stringBuilder = new StringBuilder(20);
			Random random = new Random();
			for (int i = 0; i < 16; i++)
			{
				switch (random.Next(1, 4))
				{
				case 1:
					value = random.Next(48, 58);
					break;
				case 2:
					value = random.Next(65, 91);
					break;
				case 3:
					value = random.Next(97, 123);
					break;
				}
				stringBuilder.Append(Convert.ToChar(value));
			}
			return stringBuilder.ToString();
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
		internal static string HashPassword(string text, string salt)
		{
			SHA384Managed sHA384Managed = new SHA384Managed();
			byte[] bytes = Encoding.UTF8.GetBytes(text + salt);
			byte[] inArray = sHA384Managed.ComputeHash(bytes);
			sHA384Managed.Clear();
			return Convert.ToBase64String(inArray);
		}
	}
}
