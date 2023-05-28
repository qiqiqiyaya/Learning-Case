using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExtractionData
{
    public class Encrypt
    {
        /// <summary>
        /// ECB加密
        /// </summary>
        /// <param name="paymentCode">加密字符串</param>
        /// <param name="key">秘钥</param>
        /// <param name="iv">ECB模式不需要IV</param>
        /// <returns></returns>
        public static string DESEncrypt(string paymentCode, string key, string iv = "12345678")
        {
            SymmetricAlgorithm symmetric;
            ICryptoTransform iCrypto;
            MemoryStream memory;
            CryptoStream crypto;
            byte[] byt;
            using (symmetric = new TripleDESCryptoServiceProvider())
            {
                //symmetric.Key = Encoding.UTF8.GetBytes(key);
                symmetric.Key = Convert.FromBase64String(key);
                //symmetric.IV = Encoding.UTF8.GetBytes(iv);
                symmetric.Mode = CipherMode.ECB;
                symmetric.Padding = PaddingMode.PKCS7;


                iCrypto = symmetric.CreateEncryptor();
                byt = Encoding.UTF8.GetBytes(paymentCode);
                memory = new MemoryStream();
                using (crypto = new CryptoStream(memory, iCrypto, CryptoStreamMode.Write))
                {
                    crypto.Write(byt, 0, byt.Length);
                    crypto.FlushFinalBlock();
                    crypto.Close();
                    //return Convert.ToBase64String(memory.ToArray());
                    //return Encoding.UTF8.GetString(memory.ToArray());
                    return Convert.ToBase64String(memory.ToArray());
                }
            }
        }

        /// <summary>
        /// 生成字符串的MD5码
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public static string GetMd5FromString(string sInput)
        {
            var lstData = Encoding.UTF8.GetBytes(sInput);
            var lstHash = new MD5CryptoServiceProvider().ComputeHash(lstData);
            var result = new StringBuilder(32);
            for (int i = 0; i < lstHash.Length; i++)
            {
                result.Append(lstHash[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 

            }
            return result.ToString();
        }
    }
}
