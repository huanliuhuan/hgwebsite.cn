using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace MYHGTOOLS
{
    /// <summary>
    /// 创建人：刘欢
    /// 创建日期：2014-10-13
    /// 说明：安全类
    /// </summary>
    public class SafetyOperations
    {
        private string key;
        public SafetyOperations()
        {
            this.key = "Pconcool";
        }

        public SafetyOperations(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="toDecrypt">解密字符</param>
        /// <returns></returns>
        public StringBuilder Decrypt(string toDecrypt)
        {
            return Decrypt(toDecrypt, this.key);
        }

        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="toEncrypt">加密字符</param>
        /// <returns></returns>
        public StringBuilder Encrypt(string toEncrypt)
        {
            return Encrypt(toEncrypt, this.key);
        }

        #region  解密
        private StringBuilder Decrypt(string toDecrypt, string skey)
        {
            try
            {
                StringBuilder sbu = new StringBuilder();
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                byte[] buffer = new byte[toDecrypt.Length / 2];
                for (int i = 0; i < (toDecrypt.Length / 2); i++)
                {
                    int num = Convert.ToInt32(toDecrypt.Substring(i * 2, 2), 0x10);
                    buffer[i] = (byte)num;
                }
                provider.Key = Encoding.ASCII.GetBytes(skey);
                provider.IV = Encoding.ASCII.GetBytes(skey);
                MemoryStream staream = new MemoryStream();
                CryptoStream crypto = new CryptoStream(staream, provider.CreateDecryptor(), CryptoStreamMode.Write);
                crypto.Write(buffer, 0, buffer.Length);
                crypto.FlushFinalBlock();
                sbu.Append(Encoding.Default.GetString(staream.ToArray()));
                return sbu;
            
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex);
            }
        }
        #endregion

        #region 加密
        private StringBuilder Encrypt(string toEncrypt, string skey)
        {
            try
            {
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                byte[] bytes = Encoding.Default.GetBytes(toEncrypt);
                provider.Key = Encoding.ASCII.GetBytes(skey);
                provider.IV = Encoding.ASCII.GetBytes(skey);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream,provider.CreateEncryptor(),CryptoStreamMode.Write);
                stream2.Write(bytes, 0, bytes.Length);
                stream2.FlushFinalBlock();
                StringBuilder sbu = new StringBuilder();
                foreach (byte num in stream.ToArray())
                {
                    sbu.AppendFormat("{0:X2}", num);
                }
                return sbu;
            }
            catch (Exception ex)
            {
                return Common.ReturnException(ex);
            }
        }
        #endregion

    }
}
