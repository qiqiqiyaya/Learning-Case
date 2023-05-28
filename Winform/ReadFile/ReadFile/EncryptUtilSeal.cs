using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace ReadFile
{
    /// <summary>
    /// 加密、解密
    /// </summary>
    public class EncryptUtilSeal
    {
        private static byte[] key = new byte[] { 78, 56, 61, 94, 12, 88, 56, 63, 66, 111, 102, 77, 1, 186, 97, 45 };
        private static byte[] iv = new byte[] { 36, 34, 42, 122, 242, 87, 2, 90, 59, 117, 123, 63, 72, 171, 130, 61 };

        private static IFormatter S_Formatter = null;

        static EncryptUtilSeal()
        {
            S_Formatter = new BinaryFormatter();//创建一个序列化的对象
        }
        /// <summary>
        /// 采用Rijndael128位加密二进制可序列化对象至文件
        /// </summary>
        /// <param name="para">二进制对象</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool EncryptObject(object para, string filePath)
        {
            //创建.bat文件 如果之前错在.bat文件则覆盖，无则创建
            using (Stream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream csEncrypt = new CryptoStream(fs, RMCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write);
                S_Formatter.Serialize(csEncrypt, para);//将数据序列化后给csEncrypt
                csEncrypt.Close();
                fs.Close();
                return true;
            }
        }

        /// <summary>
        /// 从采用Rijndael128位加密的文件读取二进制对象
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>二进制对象</returns>
        public static object DecryptObject(string filePath)
        {
            //打开.bat文件
            using (Stream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                object para;
                RijndaelManaged RMCrypto = new RijndaelManaged();
                CryptoStream csEncrypt = new CryptoStream(fs, RMCrypto.CreateDecryptor(key, iv), CryptoStreamMode.Read);
                para = S_Formatter.Deserialize(csEncrypt); //将csEncrypt反序列化回原来的数据格式；
                csEncrypt.Close();
                fs.Close();
                return para;
            }
        }
    }
}
