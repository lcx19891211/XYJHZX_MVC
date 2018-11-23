using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Globalization;

namespace XYJHZX_MVC.Lib
{
    public class Crypt
    {
        const string Str_key = "d!n@z#x$";

        #region MD5加密
        /// <summary>
        /// 获取MD5小写字符串
        /// </summary>
        /// <param name="str_convert"></param>
        /// <returns></returns>
        public static string GetMd5Str(string str_convert)
        {
            MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
            string str_result = BitConverter.ToString(_md5.ComputeHash(UTF8Encoding.Default.GetBytes(str_convert)), 4, 8);
            str_result = str_result.Replace("-", "");

            str_result = str_result.ToLower();

            return str_result;
        }
        /// <summary>
        /// 获取MD5大写字符串
        /// </summary>
        /// <param name="str_convert"></param>
        /// <returns></returns>
        public static string GetMd5STR(string str_convert)
        {
            MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
            string str_result = BitConverter.ToString(_md5.ComputeHash(UTF8Encoding.Default.GetBytes(str_convert)), 4, 8);
            str_result = str_result.Replace("-", "");
            return str_result;
        }
        /// <summary>
        /// 获取MD5哈希码
        /// </summary>
        /// <param name="str_convert"></param>
        /// <returns></returns>
        public static string StringToMD5Hash(string str_convert)
        {
            MD5CryptoServiceProvider _MD5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = _MD5.ComputeHash(Encoding.ASCII.GetBytes(str_convert));
            StringBuilder _strResult = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                _strResult.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return _strResult.ToString();
        }
        #endregion

        #region Des加密

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="pubKey">公匙</param>
        /// <param name="input">加密文</param>
        /// <returns></returns>
        public static string DesEncrypt(string pubKey, string input)
        {
            var des = new DESCryptoServiceProvider();
            var bytes = Encoding.UTF8.GetBytes(input);
            des.Key = Encoding.ASCII.GetBytes(pubKey);
            des.IV = Encoding.ASCII.GetBytes(pubKey);
            using (var ms = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.FlushFinalBlock();
                }
                var data = ms.ToArray();
                return BitConverter.ToString(data).Replace("-", "");
            }
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="pubKey">公匙</param>
        /// <param name="input">解密文</param>
        /// <returns></returns>
        public static string DesDecrypt(string pubKey, string input)
        {
            var des = new DESCryptoServiceProvider();
            var bytes = Hex2Bytes(input);
            des.Key = Encoding.ASCII.GetBytes(pubKey);
            des.IV = Encoding.ASCII.GetBytes(pubKey);
            using (var ms = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                    cryptoStream.FlushFinalBlock();
                }
                var data = ms.ToArray();
                return Encoding.UTF8.GetString(data);
            }
        }


        #region 使用 缺省密钥字符串 加密/解密string

        /// <summary>
        /// 使用缺省密钥字符串加密string
        /// </summary>
        /// <param name="original">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string original)
        {
            return Encrypt(original, Str_key);
        }
        /// <summary>
        /// 使用缺省密钥字符串解密string
        /// </summary>
        /// <param name="original">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original)
        {
            return Decrypt(original, Str_key, System.Text.Encoding.Default);
        }

        #endregion

        #region 使用 给定密钥字符串 加密/解密string
        /// <summary>
        /// 使用给定密钥字符串加密string
        /// </summary>
        /// <param name="original">原始文字</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>密文</returns>
        public static string Encrypt(string original, string key)
        {
            byte[] buff = System.Text.Encoding.Default.GetBytes(original);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return Convert.ToBase64String(Encrypt(buff, kb));
        }
        /// <summary>
        /// 使用给定密钥字符串解密string
        /// </summary>
        /// <param name="original">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, System.Text.Encoding.Default);
        }

        /// <summary>
        /// 使用给定密钥字符串解密string,返回指定编码方式明文
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buff = Convert.FromBase64String(encrypted);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return encoding.GetString(Decrypt(buff, kb));
        }
        #endregion

        #region 使用 缺省密钥字符串 加密/解密/byte[]
        /// <summary>
        /// 使用缺省密钥字符串解密byte[]
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes(Str_key);
            return Decrypt(encrypted, key);
        }
        /// <summary>
        /// 使用缺省密钥字符串加密
        /// </summary>
        /// <param name="original">原始数据</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] original)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes(Str_key);
            return Encrypt(original, key);
        }
        #endregion

        #region  使用 给定密钥 加密/解密/byte[]

        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="original">数据源</param>
        /// <returns>摘要</returns>
        public static byte[] MakeMD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            hashmd5 = null;
            return keyhash;
        }


        /// <summary>
        /// 使用给定密钥加密
        /// </summary>
        /// <param name="original">明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }

        /// <summary>
        /// 使用给定密钥解密数据
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        #endregion
        #endregion


        #region 数值转化
        /// <summary>
        /// 将16进制数组转换成字节
        /// </summary>
        /// <param name="input">转换字节</param>
        /// <returns>16进制数组</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static byte[] Hex2Bytes(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;
            var offset = input.Length % 2;
            if (offset == 1) input = "0" + input;
            int i;
            var list = new List<byte>();
            for (i = 0; i < input.Length; i += 2)
            {
                var temp = input.Substring(i, 2);
                byte bv;
                var success = byte.TryParse(temp, NumberStyles.HexNumber, null, out bv);
                if (!success) throw new ArgumentOutOfRangeException();
                list.Add(bv);
            }
            return list.ToArray();
        }
        #endregion

    }
}