using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using zlib;

namespace MYHGTOOLS
{
    /// <summary>
    /// 创建人：刘欢
    /// 创建时间：2014年12月31日 22:40:36
    /// 说明：对字符串进行压缩
    /// </summary>
    public class DataCompressHelper
    {
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="SourceString"></param>
        /// <returns></returns>
        public string CompressString(string SourceString)
        {
            //把string转换为byte
            byte[] buffer1 = Encoding.UTF8.GetBytes(SourceString);
            //获取压缩后的数据
            byte[] buffer2 = this.CompressBytes(buffer1);
            if (buffer2 != null)
            {
                //Base64转换
                return Convert.ToBase64String(buffer2);
            }
            return null;
        }

        /// <summary>
        /// 压缩字节
        /// </summary>
        /// <param name="SourceByte"></param>
        /// <returns></returns>
        public byte[] CompressBytes(byte[] SourceByte)
        {
            byte[] buffer2;
            try
            {
                //创建一个内存流byte
                MemoryStream stream1 = new MemoryStream(SourceByte);
                //压缩stream1
                Stream stream2 = this.CompressStream(stream1);
                //定义一个byte
                byte[] buffer1 = new byte[stream2.Length];
                //设置当前流的位置
                stream2.Position = 0;
                //读取字节流
                stream2.Read(buffer1, 0, buffer1.Length);
                buffer2 = buffer1;
            }
            catch
            {
                buffer2 = null;
            }
            return buffer2;
        }

        /// <summary>
        /// 给SourceStream进行压缩
        /// </summary>
        /// <param name="SourceStream"></param>
        /// <returns></returns>
        public Stream CompressStream(Stream SourceStream)
        {
            Stream stream3;
            try
            {
                //创建新内存流
                MemoryStream stream1 = new MemoryStream();
                //进行压缩
                ZOutputStream stream2 = new ZOutputStream(stream1, -1);
                //复制当前字节流
                this.CopyStream(SourceStream, stream2);
                //释放stream2
                stream2.finish();
                //把stream1赋值stream3
                stream3 = stream1;
            }
            catch
            {
                stream3 = null;
            }
            return stream3;
        }

        /// <summary>
        /// 复制一个字节流
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        private void CopyStream(Stream input, Stream output)
        {
            int num1;
            //定义一个规则byte数组
            byte[] buffer1 = new byte[0x7d000];
            //读取input字节流，从0开始到最后一位
            while ((num1 = input.Read(buffer1, 0, buffer1.Length)) > 0)
            {
                //将读取到的值写入output字节流
                output.Write(buffer1, 0, num1);
            }
            //释放资源
            output.Flush();
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="SourceString"></param>
        /// <returns></returns>
        public string DecompressString(string SourceString)
        {
            //把string进行Base64转换为byte
            byte[] buffer1 = Convert.FromBase64String(SourceString);
            //将buffer1转为为string
            string text1 = Encoding.Default.GetString(buffer1);
            byte[] buffer2 = this.DecompressBytes(buffer1);
            if (buffer2 != null)
            {
                return Encoding.UTF8.GetString(buffer2);
            }
            return null;
        }

        /// <summary>
        /// 解压字节
        /// </summary>
        /// <param name="SourceByte"></param>
        /// <returns></returns>
        public byte[] DecompressBytes(byte[] SourceByte)
        {
            byte[] buffer2;
            try
            {
                //创建内存流
                MemoryStream stream1 = new MemoryStream(SourceByte);
                //获取解压流
                Stream stream2 = this.DecompressStream(stream1);
                byte[] buffer1 = new byte[stream2.Length];
                stream2.Position = 0;
                stream2.Read(buffer1, 0, buffer1.Length);
                buffer2 = buffer1;
            }
            catch
            {
                buffer2 = null;
            }
            return buffer2;
        }

        /// <summary>
        /// 解压流
        /// </summary>
        /// <param name="SourceStream"></param>
        /// <returns></returns>
        public Stream DecompressStream(Stream SourceStream)
        {
            Stream stream3;
            try
            {
                //定义内存流
                MemoryStream stream1 = new MemoryStream();
                //进行解压
                ZOutputStream stream2 = new ZOutputStream(stream1);
                //复制当前字节流
                this.CopyStream(SourceStream, stream2);
                //释放资源
                stream2.finish();
                stream3 = stream1;
            }
            catch
            {
                stream3 = null;
            }
            return stream3;
        }

    }
}
