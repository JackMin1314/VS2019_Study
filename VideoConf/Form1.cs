using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace VideoConf
{


    public partial class Form1 : Form
    {
        // 构造json字符串的模板
        public string rootJson = "{\"strNameComment\":\"name for this stream\",\"strName\":\"Stream 203\",\"strTokenComment\":\"token for this stream. must unique. if same. only first will be available\",\"strToken\":\"token5\",\"nTypeComment\":\"source type H5_FILE/H5_STREAM/H5_ONVIF\",\"nType\":\"H5_STREAM\",\"strUrlComment\":\"url(RTSP/RTMP...) or file path\",\"strUrl\":\"rtsp://admin:adm12345@192.168.10.203:554/Streaming/Channels/101\",\"strUserComment\":\"username\",\"strUser\":\"admin\",\"strPasswdComment\":\"password\",\"strPasswd\":\"adm12345\",\"bPasswdEncryptComment\":\"Password Encrypted\",\"bPasswdEncrypt\":false,\"bEnableAudioComment\":\"Enable Audio\",\"bEnableAudio\":false,\"nConnectTypeComment\":\"H5_ONDEMAND/H5_ALWAYS/H5_AUTO\",\"nConnectType\":\"H5_AUTO\",\"nRTSPTypeComment\":\"RTSP Connect protocol H5_RTSP_TCP/H5_RTSP_UDP/H5_RTSP_HTTP/H5_RTSP_HTTPS/H5_RTSP_AUTO\",\"nRTSPType\":\"H5_RTSP_AUTO\",\"strSrcIpAddressComment\":\"Ip Address for the device\",\"strSrcIpAddress\":\"192.168.0.1\",\"strSrcPortComment\":\"Port for the device\",\"strSrcPort\":\"80\",\"nChannelNumberComment\":\"Channel number (1-512)\",\"nChannelNumber\":1,\"bOnvifProfileAutoComment\":\"ONVIF Auto select the video profile\",\"bOnvifProfileAuto\":true,\"strOnvifAddrComment\":\"ONVIF address (/onvif/device_service)\",\"strOnvifAddr\":\"/onvif/device_service\",\"strOnvifProfileMainComment\":\"ONVIF Main stream profile name\",\"strOnvifProfileMain\":\"Profile_1\",\"strOnvifProfileSubComment\":\"ONVIF Sub stream profile name\",\"strOnvifProfileSub\":\"Profile_2\",\"bRTSPPlaybackComment\":\"RTSP playback source\",\"bRTSPPlayback\":false,\"nRTSPPlaybackSpeedComment\":\"RTSP playback speed\",\"nRTSPPlaybackSpeed\":1}";
        // 定义一个全局字典存储请求配置的json结果,再次保存之前需要清除
        public Dictionary<string, JToken> result_dic = new Dictionary<string, JToken>();
        /// <summary>
        /// 全局本地摄像头配置文件个数
        /// 注意每次配置文件修改(add,delete)都需要重新获取currentCount
        /// </summary>
        public int currentCount = 0;

        /// <summary>
        /// 读取程序配置文件中摄像头配置h5ss.conf的绝对路径，非常重要，绝不能错！!
        /// </summary>
        public string confPath = ConfigurationManager.AppSettings["confPath"].Trim(' ');

        /// <summary>
        /// 读取配置文件获取运行摄像头配置数目的最大值maxCount
        /// </summary>
        public int maxCount = Convert.ToInt32(ConfigurationManager.AppSettings["maxCount"].Trim(' '));

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            Refresh_Video_WebApi();
        }
        HttpSelfHostConfiguration config = null;
        HttpSelfHostServer server = null;
       
        public void Refresh_Video_WebApi()
        {
            try
            {
                config = new HttpSelfHostConfiguration("http://127.0.0.1:9099");// "填写自己的ip地址"

                var geduCors = new EnableCorsAttribute("*", "*", "*");
                // 先跨域，再路由
                config.EnableCors(geduCors);
                GlobalConfiguration.Configuration.EnableCors(new EnableCorsAttribute("*", "*", "*"));


                // config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
                //config.Routes.MapHttpRoute("API Default", "{Controller}/{id}", new { id = RouteParameter.Optional });
                config.MapHttpAttributeRoutes();

                // 注册自定义路由
                config.Routes.MapHttpRoute(
                    name: "MyAPI",
                    routeTemplate: "{Controller}/{action}/{id}", // Controller不可少,id为可选参数
                    defaults: new { id = RouteParameter.Optional }
                );
                server = new HttpSelfHostServer(config);
                server.OpenAsync().Wait();
            }
            catch (System.Exception e)
            {
                MessageBox.Show("程序重复运行或9090端口被占用", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // 错误日志保存: 文件路径\NFC_ID\bin\x64\Debug\LogFile\ErrorLog.txt
                ErrorLog.ErrorLogTxt(e);
                throw e;
            }

        }

        /// <summary>
        /// 返回从App.config的confPath读取的json配置文件字符串
        /// </summary>
        /// <returns>the string of json format</returns>
        public string get_local_json()
        {
            // 尝试读取配置文件
            try
            {
                string jsonString = File.ReadAllText(confPath, Encoding.UTF8);
                return jsonString;
            }
            catch (Exception)
            {
                return "-1";// 没找到文件 读取失败
                throw;
            }

        }

        // 解析本地json文件
        private void Button1_Click(object sender, EventArgs e)
        {
            bool flag = DeleteJson("1,2");
            if (flag == true)
            {
                currentCount = count_local_json();// 每次配置文件的修改都需要重新获取下
            }
        }

        // 请求获取配置信息
        private void Button2_Click(object sender, EventArgs e)
        {
            string PostParama = "1,2,3,4";
            string result = PostConf(PostParama);
            textBox1.Text = "";
            string statusParse = getjson(result); // statusParse 为-1（非json）,1（有data）或0（无data）
            if (statusParse == "-1")
            {
                MessageBox.Show("解析失败，数据不是json格式");
            }
            if (statusParse == "0")
            {
                MessageBox.Show("解析成功，data无内容");
            }
            if (statusParse == "1")
            {
                MessageBox.Show("解析成功，data有内容");
            }
            string teststr = AddJson("1,2,3,4");
        }

        /// <summary>
        /// Post请求云平台摄像头配置
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string PostConf(string str)
        {
            string result = "";
            string RemoteIP = ConfigurationManager.AppSettings["RemoteIP"];
            try
            {
                // 读取配置,添加请求的地址 http://{ip:port}/video/findVideoConf?ids=1,2
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://" + RemoteIP + ":9090/video/findVideoConf?ids=" + str.ToString());
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
            catch (Exception)
            {
                MessageBox.Show("请求平台摄像头配置失败!!!");
                throw;
            }
        }

        /// <summary>
        /// 解析请求平台返回的配置信息
        /// </summary>
        /// <param name="result"></param>
        /// <returns>
        /// '0'--是json格式, data无数据; '1'--是json格式, data有数据;  '-1'--参数不是json格式
        /// </returns>
        public string getjson(string result)
        {
            string OpenVideo = ConfigurationManager.AppSettings["OpenVideo"];
            try
            {

                result_dic.Clear();// 先清空存储信息
                JObject jo = JObject.Parse(result);
                //textBox1.Text = "查询结果为:" + "\r\n";
                //textBox1.Text += jo["code"] + "\r\n";
                //textBox1.Text += jo["msg"] + "\r\n";

                // 查询code为113表示data有内容
                if (jo["code"].ToString() == "113")
                {
                    foreach (var item in (JArray)jo["data"])
                    {
                        // 这里试了很多次，hasvalues表明item是否有子token；
                        // Gets a value indicating whether this token has child tokens.
                        if (item.HasValues&&(!result_dic.ContainsKey(item["id"].ToString())))
                        {
                            result_dic.Add(item["id"].ToString(), item);

                        }//end_if                  
                    }//end_foreach

                    //foreach (var dic in result_dic)
                    //{
                    //    //textBox1.Text += dic.Value["id"].ToString() + "\r\n";
                    //}
                    return "1";//是json格式，data有数据
                }//end_IF
                else
                {
                    return "0";//是json格式，data无数据
                }//end_IF_else
            }
            catch (Exception)
            {
                return "-1"; // 参数不是json格式
                throw;
            }
        }

        /// <summary>
        /// 获取本地配置的个数
        /// </summary>
        /// <returns></returns>
        public int count_local_json()
        {
            string jsonString = get_local_json();
            // 非数组用JObject加载,本地只有一个大json内容，数组用JArray
            if (jsonString == "-1") return -1;// 读取本都json文件失败
            JObject jo = JObject.Parse(jsonString);
            int count = 0;
            foreach (var item in jo["source"]["src"]) { count++; }
            return count;
        }

        /// <summary>
        /// 给定逗号连接的编号，匹配删除,更新配置和全局currentCount成功返回true，失败返回false
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public bool DeleteJson(string numbers)
        {
            string result = get_local_json();
            if (result == "-1") return false;
            JObject jo = JObject.Parse(result);
            string[] numList = numbers.Split(',');
            bool b = false;
            currentCount = count_local_json();// 获取当前配置文件个数,-1表示配置文件路径有误
            if (currentCount == -1) return false;
            foreach (var indexNum in numList)
            {
                // 本地配置文件有内容才可以删除
                if (jo["source"]["src"].HasValues)
                {
                    foreach (var item in jo["source"]["src"])
                    {
                        if (item["strToken"].ToString() == indexNum)
                        {
                            // 移除jo["source"]["src"]中匹配的元素
                            item.Remove();
                            b = true;
                            break;//加break真的是好习惯！！否则再次循环jo["source"]["src"]内部已经修改了会有异常
                        } // end_if
                    }
                }

            }//end_foreach
            textBox1.Text = jo["source"]["src"].ToString();
            // 删除后直接修改本地配置文件
            try
            {
                File.WriteAllText(confPath, jo.ToString());
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            currentCount = count_local_json();
            return b;
        }

        /// <summary>
        /// 给定的编号，添加到配置文件里
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>
        /// '0'--成功添加; '1'--超过最大配置数目;  '-1'--抛出异常
        /// </returns>
        public string AddJson(string numbers)
        {
            string result = get_local_json();
            if (result == "-1") return "-1";
            JObject jo = JObject.Parse(result);
            //JObject srcJson = jo["source"]["src"] as JObject;
            string[] numList = numbers.Split(',');
            int numList_len = numList.Length;
            currentCount = count_local_json();// 获取当前配置文件个数,-1表示配置文件路径有误
            if (currentCount == -1) return "-1";
            // 需要判断 maxCount 和 currentCount + numList.length大小,严格大于
            if (currentCount + numList_len > maxCount)
            {
                MessageBox.Show("over maxCount, reject writing config");
                return "1";//表示当前配置数和返回配置数超过所设定的最大配置数目
            }
            foreach (var indexNum in numList)
            {
                foreach (var dic in result_dic)
                {
                    if (dic.Key.ToString() == indexNum)// 字典里存有请求编号的信息
                    {
                        JObject temp = JObject.Parse(rootJson);
                        temp["strToken"] = dic.Value["id"].ToString();
                        temp["strUrl"] = dic.Value["rtspUrl"].ToString();
                        temp["strUser"] = dic.Value["username"].ToString();
                        temp["strPasswd"] = dic.Value["password"].ToString();

                        if (jo["source"]["src"].HasValues)
                        {
                            jo["source"]["src"][0].AddAfterSelf(temp);
                        }
                        else// 没有内容时执行
                        {
                            // jo[][]此时虽然配置是列表结构Arrary，但是编辑器认为是查询jo[][]中的元素是JToken，因而要显示转换成JArrary，才可以！！！
                            ((JArray)jo["source"]["src"]).Add(temp);// 解决本地配置文件为空的时候,直接添加
                        }
                    }
                }

            }//end_foreach
            textBox1.Text = jo["source"]["src"].ToString();
            // 删除后直接修改本地配置文件
            try
            {
                File.WriteAllText(confPath, jo.ToString());
            }
            catch (Exception)
            {
                return "-1";
                throw;
            }
            currentCount = count_local_json();
            return "0";
        }

    }
}