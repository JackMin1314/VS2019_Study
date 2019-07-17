
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Web.Script.Serialization;          //需要添加到项目中的引用中
using System.Windows;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Test
{
    public class OwithJ
    {
        //不要用static, obj可以是很多种的类型的对象这里用实体类对象
        public  string ObjectToJson(object obj)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            try
            {
                System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
                json.Serialize(obj, sb);
            }
            catch (Exception e)
            {
                MessageBox.Show("失败了: " + e.Message, "提示");
            }
            return sb.ToString();
        }
        public string GetMD5(string parmas)
        {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] data = System.Text.Encoding.UTF8.GetBytes(parmas);
                byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
                md5.Clear();
                string token = string.Empty;
                for (int i = 0; i < md5Data.Length; i++)
                {
                    //返回一个新字符串，该字符串通过在此实例中的字符左侧填充指定的 Unicode 字符来达到指定的总长度，从而使这些字符右对齐。
                    // string num=12; num.PadLeft(4, '0'); 结果为为 '0012' 看字符串长度是否满足4位,不满足则在字符串左边以"0"补足
                    //调用Convert.ToString(整型,进制数) 来转换为想要的进制数
                    token += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
                }
                //使用 PadLeft 和 PadRight 进行轻松地补位
                token = token.PadLeft(32, '0').ToLower();
                return token;
        }
        public   List<Response> JsonToObject(string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Response>));
            jsonString = "[" + jsonString + "]";
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            object ob = serializer.ReadObject(stream);
            List<Response> Ls = (List<Response>)ob;
            return Ls;
        }
        public string ShowResult(List<Response> us)
        {
            string strItem = "";
            foreach (var item in us)
            {
                strItem += "staus:" + item.status + "   ";
                if (item.status == "200")
                    strItem = strItem + "请求成功";
                else if (item.status == "401")
                    strItem = strItem + "授权失败";
                else if (item.status == "500")
                    strItem = strItem + "服务器内部异常";
                else
                    strItem = strItem + "异常返回数据";
                strItem += "/n" + "msg：";
                int i = int.Parse(item.msg);
                switch (i)
                {
                    case 0: strItem = strItem + "成功"; break;
                    case 1: strItem = strItem + "失败，暂无位置信息"; break;
                    case 2: strItem = strItem + "失败，已超时30分钟"; break;
                    case 3: strItem = strItem + "失败，不是移动号码"; break;
                    case 4: strItem = strItem + "失败，录音传输失败"; break;
                    default: strItem = strItem + "失败，异常返回数据"; break;
                }
                strItem += "/n" + "data：";
            }
            return strItem;
        }
    }
}
