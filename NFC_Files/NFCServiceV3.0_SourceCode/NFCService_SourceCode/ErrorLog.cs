using System;
using System.IO;
using System.Text;

namespace NFCService
{
    public class ErrorLog
    {
        public static void ErrorLogTxt(Exception ex)
        {
            // 判断路径是否存在，不存在则创建
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogFile\\ErrorLog.txt") == false)
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogFile");
            }
            // 获取文件路径（相对于程序的基目录路径）
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogFile\\ErrorLog.txt";
            // 创建字符串缓冲区
            StringBuilder msg = new StringBuilder();
            msg.Append("*************************************** \r\n");
            msg.AppendFormat(" 异常发生时间： {0} \r\n", DateTime.Now);
            msg.AppendFormat(" 异常类型： {0} \r\n", ex.HResult);
            msg.AppendFormat(" 导致当前异常的 Exception 实例： {0} \r\n", ex.InnerException);
            msg.AppendFormat(" 导致异常的应用程序或对象的名称： {0} \r\n", ex.Source);
            msg.AppendFormat(" 引发异常的方法： {0} \r\n", ex.TargetSite);
            msg.AppendFormat(" 异常堆栈信息： {0} \r\n", ex.StackTrace);
            msg.AppendFormat(" 异常消息： {0} \r\n", ex.Message);
            msg.Append("***************************************");
            try
            {
                if (File.Exists(FilePath))//如果文件存在
                {
                    //写异常信息写入文件
                    using (StreamWriter tw = File.AppendText(FilePath))
                    {
                        tw.WriteLine(msg.ToString());
                    }
                }
                else
                {
                    // 如果文件不存在则创建后将异常信息写入
                    TextWriter tw = new StreamWriter(FilePath); tw.WriteLine(msg.ToString());
                    tw.Flush();// 将缓冲区的数据强制输出，清空缓冲区
                    tw.Close();// 关闭数据流
                    tw = null;
                }
            }
            catch (Exception exx)
            {
                // 日志写入失败异常
                throw exx;
            }

        }
        // 用来测试是否能够定时写入数据到WriteMsg
        public static void WriteMsg(string str)
        {
            // 判断路径和文件是否存在，不存在则创建
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogFile\\WriteMsg.txt") == false)
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogFile");
            }
            // 获取文件路径（相对于程序的基目录路径）
            string FilePath = AppDomain.CurrentDomain.BaseDirectory + "\\ServiceLogFile\\WriteMsg.txt";
            // 创建可变字符串(本质是字符串缓冲区)
            // 字符串一旦创建就不可修改大小，所以对字符串添加或删除操作比较频繁的话。那就不要用String而用StringBuilder。
            // String一旦赋值或实例化后就不可更改，如果赋予新值将会重新开辟内存地址进行存储。
            StringBuilder msg = new StringBuilder();
            msg.Append("*************************************** \r\n");
            msg.AppendFormat(" 文本读写时间： {0} \r\n", DateTime.Now);
            msg.Append(str+ "\r\n");
            msg.Append("*************************************** \r\n");
            try
            {
                if (File.Exists(FilePath))//如果文件路径且文件存在
                {
                    //写信息写入文件
                    using (StreamWriter tw = File.AppendText(FilePath))
                    {
                        tw.WriteLine(msg.ToString()); // msg是一个对象
                    }
                }
                else
                {
                    //如果文件不存在则创建后,将信息写入
                    TextWriter tw = new StreamWriter(FilePath);
                    tw.WriteLine(msg.ToString());
                    tw.Flush();//将缓冲区的数据强制输出，清空缓冲区
                    tw.Close();//关闭数据流
                    tw = null;
                }
            }
            catch (Exception exx)
            {
                // 写入失败异常
                throw exx;
            }
        }
    }
}
