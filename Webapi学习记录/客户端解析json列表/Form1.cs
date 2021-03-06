// 客户端
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace VideoConf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // 解析本地json文件
        private void Button1_Click(object sender, EventArgs e)
        {
            string localpath = System.IO.Directory.GetCurrentDirectory();
            string FilePath = localpath + "\\h5ss.conf";
            string jsonString = File.ReadAllText(FilePath,Encoding.UTF8);
            get_local_json(jsonString);

        }

        // 请求获取配置信息
        private void Button2_Click(object sender, EventArgs e)
        {
            string PostParama ="1,2,3";
            string result = Post(PostParama);
            textBox1.Text = "";
            getjson(result);
        }

        public static string Post(string str)
        {
            string result = "";
            string RemoteIP = "";// 读取配置文件里面ip地址
            RemoteIP = ConfigurationManager.AppSettings["RemoteIP"];
         
            // 读取配置,添加请求的地址 http://{ip:port}/video/findVideoConf?ids=1,2
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://"+ RemoteIP+ ":9090/video/findVideoConf?ids="+str.ToString());
            req.Method = "Post";
            req.ContentType = "application/json";

            byte[] ids = Encoding.UTF8.GetBytes(str);// 把字符串转换为字节

            req.ContentLength = ids.Length; // 请求长度

            using (Stream reqStream = req.GetRequestStream()) // 获取
            {
                reqStream.Write(ids, 0, ids.Length);// 向当前流中写入字节
                reqStream.Close(); // 关闭当前流
            }

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse(); //响应结果
            Stream stream = resp.GetResponseStream();
            // 获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;

        }
        
        public void getjson(string result)
        {
            // 用JArray是因为请求返回的配置文件可能是一个数组，多个配置
            JObject jo = JObject.Parse(result);
            
            textBox1.Text = "查询结果为:"+"\r\n";
            textBox1.Text +=jo["code"] + "\r\n";// 相当于每次取ja数组中的元素，转换成JObject单独再单独提取里面code(注意括号)
            textBox1.Text +=jo["msg"] + "\r\n";
            // 查询code为113表示data有内容
            if (jo["code"].ToString()=="113")
            {
                foreach (var item in (JArray)jo["data"])
                {
                    // 这里试了很多次，hasvalues表明item是否有子token；Gets a value indicating whether this token has child tokens.
                    if (item.HasValues)
                    {
                        textBox1.Text += item["id"] + "\r\n";
                        textBox1.Text += item["isOpen"] + "\r\n";
                        textBox1.Text += item["username"] + "\r\n";
                        textBox1.Text += item["password"] + "\r\n";
                        textBox1.Text += item["rtspUrl"] + "\r\n";
                        textBox1.Text += "\r\n";
                    }//end_if                  
                }//end_foreach
            }//end_IF
            else
            {
                MessageBox.Show("No result!");
            }//IF_else
     
        }
        public void get_local_json(string result)
        {
            // 非数组用JObject加载,本地只有一个大json内容，数组用JArray
            JObject jo = JObject.Parse(result);
            //string msg = jo["source"]["src"][0]["strUrl"].ToString();
            textBox1.Text = "";         
            foreach (var item in jo["source"]["src"])
            {
                textBox1.Text +=  item["strUrl"].ToString()+"\r\n";
            }
           
        }
    }
}
