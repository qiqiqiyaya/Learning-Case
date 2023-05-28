// See https://aka.ms/new-console-template for more information

using System.Collections;
using ReadFile;

Console.WriteLine("Hello, World!");



string ConfigFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "config.dat");
//写入
Hashtable para = new Hashtable();
para.Add("ZH", "fdasfsa");
para.Add("MM", "pbxMM.Password");
EncryptUtilSeal.EncryptObject(para, ConfigFilePath);

//读取
Hashtable para2 = new Hashtable();
object obj = EncryptUtilSeal.DecryptObject(ConfigFilePath);
para2 = obj as Hashtable;
string ZH = para2["ZH"].ToString();
string MM = para2["MM"].ToString();