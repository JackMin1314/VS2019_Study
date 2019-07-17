using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class Positioninfo
        {
            public string logintime { get; set; }  // 测试参数logintime
            public string number { get; set; }  // 测试参数number
            public string token { get; set; }  // 测试参数token
        }
        public string jsonString="";//保存json字符串
        public string resultJson = "";//保存json解析后的字符串
  
         //生成md5
        private void Button1_Click(object sender, EventArgs e)
        {
            OwithJ Oj = new OwithJ();
            string parmas = "2396370149CFDdefefsfff555fffefefecxxjzyj2eg54g54g5e897564563214";
            string str = "";
            str = Oj.GetMD5(parmas).ToLower();
            MessageBox.Show("生成MD5成功!");
            label1.Text = "生成的MD5为:";
            textBox1.Text = "";
            textBox1.Text = str;

        }
        //生成对象转json格式
        private void Button2_Click(object sender, EventArgs e)
        {
            //为了给内部数据logintime,number等赋值,这里创建Positioninfo对象test
            Positioninfo test = new Positioninfo();
            test.logintime = "2019-07-16";
            test.number = "177**";
            test.token = "abcd123";

            /// <summary>
            /// OwithJ类是"对象和json转换"类, 里面有两个函数ObjectToJson()和JsonToObject()
            /// 要想使用这两个函数必须先创建;类的实例化(创建类类型的对象,并new..)如 : OwithJ otj = new OwithJ();
            /// </summary>
            OwithJ otj = new OwithJ();
            //otj.ObjectToJson(test),函数参数可以有很多种,这里用实体类对象
            jsonString = otj.ObjectToJson(test);//这里jsonString类的公有变量
            Console.WriteLine(jsonString);
            //确实转换成了json,内部形式是json的按照string形式输出
            MessageBox.Show("字符串转为Json成功!");
            textBox1.Text = "";
            label1.Text = "发送的json为:";
            textBox1.Text =  jsonString;
        }
        //生成json转对象格式
        private void Button3_Click(object sender, EventArgs e)
        {
            //样例数据含有换行符,需要去掉
            string jsonText = "{\"status\":\"200\",\"msg\":\"成功\",\"data\":{\"lng\":\"119.29925\",\"lat\":\"32.16172\"}}";
            List<Response> result = new List<Response>();
            OwithJ jto = new OwithJ();
            result = jto.JsonToObject(jsonText);
            //下面完全可以用格式化字符串输出
            //  foreach (var item in us)
            //      strItem += "staus:" + item.status + "   ";
            MessageBox.Show("Json转字符串成功!");
            label1.Text = "Json转字符串为:";
            resultJson = result[0].status.ToString();
            textBox1.Text = "";
            textBox1.Text += resultJson+"\r\n";
            resultJson = result[0].msg.ToString();
            textBox1.Text += resultJson + "\r\n";
            resultJson = result[0].data.lng.ToString();
            textBox1.Text += resultJson + "\r\n";
            resultJson = result[0].data.lat.ToString();
            textBox1.Text += resultJson + "\r\n";

        }
    }

}
