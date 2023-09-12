using System;
using System.Security.Cryptography;
using System.Text;

namespace LIBUtilities.Core
{
    public class EncryptHelper
    {
        private static string key = "2023!!--**B45ePr0y3Ct";

        public static string EncryptKey(string value, string subKey = "")
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return null;
                byte[] keyArray;
                byte[] array = UTF8Encoding.UTF8.GetBytes(value);
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key + subKey));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(array, 0, array.Length);
                tdes.Clear();
                var temp = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                byte[] arrayTemp = Convert.FromBase64String(temp);
                return temp;
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        public static string DecryptKey(string value, string subKey = "")
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return null;
                byte[] keyArray;
                byte[] array = Convert.FromBase64String(value);
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key + subKey));
                hashmd5.Clear();
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(array, 0, array.Length);
                tdes.Clear();
                return UTF8Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}