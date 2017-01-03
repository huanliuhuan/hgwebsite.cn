using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MYHGTOOLS
{
    /// <summary>
    /// 关于本地文件的操作
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// 写入本地文件StreamWriter
        /// </summary>
        /// <param name="message">记录信息</param>
        /// <param name="address">存放地址(需要后缀)</param>
        /// <param name="type">if == 0 address为基路径 if == 1 为全部路径</param>
        /// <param name="exMess">返回的错误信息，ok为成功</param>
        public void saveTicketDataSW(StringBuilder message, string address, int type, out Exception exMess)
        {
            try
            {
                System.Text.StringBuilder sbu = new StringBuilder();
                sbu = message;
                //获取路径
                string LoginPath = "";
                if (type == 0)
                {
                    LoginPath = AppDomain.CurrentDomain.BaseDirectory + address;
                }
                else if (type == 1)
                {
                    LoginPath = address;
                }
                //获取路径
                FileStream fsnew = new FileStream(LoginPath, FileMode.Append);
                StreamWriter bwrite = new StreamWriter(fsnew, Encoding.UTF8);
                bwrite.Write(sbu.ToString());
                exMess = new Exception("ok");
                bwrite.Flush();
                fsnew.Close();
            }
            catch (Exception ex)
            {
                exMess = ex;
            }
        }

        /// <summary>
        /// 读取用户的方法 StreamReader
        /// </summary>
        /// <param name="LoginPath">读取路径</param>
        /// <param name="exMess">返回错误信息，ok为成功</param>
        /// <returns>返回读取到的内容</returns>
        public string readUserSR(string LoginPath, out Exception exMess)
        {
            try
            {
                //打开文件
                FileStream mystream = new FileStream(LoginPath, FileMode.Open);
                //读取文件
                StreamReader bre = new StreamReader(mystream, Encoding.UTF8);
                string inputString = bre.ReadToEnd();

                mystream.Close();
                exMess = new Exception("ok");
                return inputString;
            }
            catch (Exception ex)
            {
                exMess = ex;
                return "";
            }

        }


        /// <summary>
        /// 写入本地文件BinaryWriter
        /// </summary>
        /// <param name="message">记录信息</param>
        /// <param name="address">存放地址(需要后缀)</param>
        /// <param name="type">if == 0 address为基路径 if == 1 为全部路径</param>
        /// <param name="exMess">返回的错误信息，ok为成功</param>
        public void saveTicketDataBW(StringBuilder message, string address, int type, out Exception exMess)
        {
            try
            {
                System.Text.StringBuilder sbu = new StringBuilder();
                sbu = message;
                //获取路径
                string LoginPath = "";
                if (type == 0)
                {
                    LoginPath = AppDomain.CurrentDomain.BaseDirectory + address;
                }
                else if (type == 1)
                {
                    LoginPath = address;
                }
                //获取路径
                FileStream fsnew = new FileStream(LoginPath, FileMode.Append);
                BinaryWriter bwrite = new BinaryWriter(fsnew, Encoding.UTF8);
                bwrite.Write(sbu.ToString());
                exMess = new Exception("ok");
                bwrite.Flush();
                fsnew.Close();
            }
            catch (Exception ex)
            {
                exMess = ex;
            }
        }

        /// <summary>
        /// 读取用户的方法 BinaryReader
        /// </summary>
        /// <param name="LoginPath">读取路径</param>
        /// <param name="exMess">返回错误信息，ok为成功</param>
        /// <returns>返回读取到的内容</returns>
        public string readUserBR(string LoginPath, out Exception exMess)
        {
            try
            {
                //打开文件
                FileStream mystream = new FileStream(LoginPath, FileMode.Open);
                //读取文件
                BinaryReader bre = new BinaryReader(mystream, Encoding.UTF8);
                string inputString = bre.ReadString();

                mystream.Close();
                exMess = new Exception("ok");
                return inputString;
            }
            catch (Exception ex)
            {
                exMess = ex;
                return "";
            }

        }
    }
}
