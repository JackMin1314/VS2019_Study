using System;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace NFC_ID
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            // 为了实现启动时候没有窗口需要先最小化然后隐藏
            // 最后还要在Form1.cs设计 中设置窗体showicon,showin taskbar的属性为false
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.Visible = false;
            InitializeComponent();
        }

        #region
        /// <summary>
        /// 点击按钮事件，获取nfc的7字节uid和当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion
        private void GetNFC_btn_Click(object sender, EventArgs e)
        {
            string result_uid = getnfc_uid.get_instance().getNFC_id();
            textBox1.Text += ("Uid:" + result_uid + "\t时间:" + DateTime.Now + "\r\n");
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #region
        /// <summary>
        /// 清除text内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion
        private void ClearNFC_btn_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("确认全部清除吗？", "清除提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.OK)
            {
                textBox1.Text = "";
            }
            else
            {
                return;
            }
        }

        #region
        /// <summary>
        /// 读取写入的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion
        private void ReadData_btn_Click(object sender, EventArgs e)
        {
            string read_data = Readnfc_data.get_instance().readNFC_data();
            textBox1.Text += ("Data:" + read_data + "\t时间:" + DateTime.Now + "\r\n");
        }

        #region
        /// <summary>
        /// 实现程序最小化到任务栏小图标，判断单机右击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #endregion
        private void NotifyIcon1_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//判断鼠标的按键为单机事件
            {
                // 最小化并隐藏
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();
                }
                // 再次点击最小化保持隐藏
                else if (this.WindowState == FormWindowState.Minimized)
                {
                    //this.Show();// 正常窗体并显示
                    //this.WindowState = FormWindowState.Normal;
                    //this.Activate();
                    this.WindowState = FormWindowState.Minimized;
                    this.Hide();
                }
            }

            else if (e.Button == MouseButtons.Right)
            {
                // 右键输出菜单
                if (e.Button == MouseButtons.Right)
                {
                    myMenu.Show();
                }
            }
        }

        // 假关闭，关窗隐藏到任务栏
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //窗体关闭原因为单击"关闭"按钮或Alt+F4

            //为了解决异常的时候程序可以退出，关闭则运行关闭e.Cancel = true;
            //e.Cancel = true;
            this.Visible = false;
        }

        // menu退出按钮时候弹窗提示
        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("关闭程序则会导致NFC读取失败，确定吗？", "提示:", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)//出错提示
            {
                //关闭窗口
                DialogResult = DialogResult.No;
                Dispose();
                Close();
            }
        }

        // menu显示窗体
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        // menu隐藏窗体
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
        }

        HttpSelfHostConfiguration config = null;
        HttpSelfHostServer server = null;

        // 程序启动时候加载webapi路由规则配置
        private void Form1_Load(object sender, EventArgs e)
        {
            // 注意如果取消RefreshWebApi()的注释,则单独这个程序就可以实现webapi接口寄宿应用程序
            // 为了避免端口占用问题注释RefreshWebApi(),同时启用RunScript()调用Windows powershell，命令行自动注册并启动Windows服务
            // RefreshWebApi();
            try
            {

                RunScript("", "installutil NFCService.exe");
            }
            catch(Exception ex)
            {
                
                MessageBox.Show("安装NFCService服务出错或已安装，详见目录日志", "提示:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                ErrorLog.ErrorLogTxt(ex);
               Close();

            } 
        }
        // webapi接口寄宿应用程序
        private void RefreshWebApi()
        {
            try
            {
                config = new HttpSelfHostConfiguration("http://127.0.0.1:9090");// "填写自己的ip地址"
                //var allowedMethods = ConfigurationManager.AppSettings["cors:allowedMethods"];
                //var allowedOrigin = ConfigurationManager.AppSettings["cors:allowedOrigin"];
                //var allowedHeaders = ConfigurationManager.AppSettings["cors:allowedHeaders"];
                //MessageBox.Show(allowedOrigin+"\n"+allowedHeaders+"\n"+allowedMethods);


                var geduCors = new EnableCorsAttribute("*", "*", "*");
                // 先跨域在路由
                config.EnableCors(geduCors);
                GlobalConfiguration.Configuration.EnableCors(new EnableCorsAttribute("*", "*", "*"));
                

               // config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
                //config.Routes.MapHttpRoute("API Default", "{Controller}/{id}", new { id = RouteParameter.Optional });
                config.MapHttpAttributeRoutes();
                
                // 注册自定义路由
                config.Routes.MapHttpRoute(
                    name: "MyAPI",
                    routeTemplate: "{Controller}/{id}", // Controller不可少,id为可选参数
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

        // path路径为空""时候即为当前程序运行目录，否则cd path；scriptText为需要执行的脚本
        // 为了能够安装Windows服务，这里写好了命令
          private static string RunScript(string path, string scriptText)
        {

            // create Powershell runspace

            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it

            runspace.Open();

            // create a pipeline and feed it the script text

            Pipeline pipeline = runspace.CreatePipeline();
            if (path != "") { pipeline.Commands.AddScript("cd " + path); }// 先进入文件目录

            // add your command here
            // 安装服务
            pipeline.Commands.AddScript(scriptText);
            
            //pipeline.Commands.AddScript("installutil NFCService.exe");
            
            // 启动服务
            pipeline.Commands.AddScript("net start NFCService");

            // pipeline.Commands.AddScript("date");

            pipeline.Commands.Add("Out-String");
            // execute the script,invoke your pipline
            Collection<PSObject> results = pipeline.Invoke();
            // close the runspace
            runspace.Close();
            // convert the script result into a single string

            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            foreach (PSObject obj in results){ stringBuilder.AppendLine(obj.ToString());  }
            return stringBuilder.ToString();
        }
        
    }
}
