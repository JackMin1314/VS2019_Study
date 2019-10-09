using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;
using System.Web.Routing;
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
            e.Cancel = true;
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
            CreateShortcutOnDesktop();
            RefreshWebApi();
        }
        private void RefreshWebApi()
        {
            try
            {
                config = new HttpSelfHostConfiguration("http://127.0.0.1:9090");// "填写自己的ip地址"
                
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

        private void CreateShortcutOnDesktop()
        {
            //添加引用 (com->Windows Script Host Object Model)，using IWshRuntimeLibrary;
            String shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "快捷方式名称.lnk");
            if (!System.IO.File.Exists(shortcutPath))
            {
                // 获取当前应用程序目录地址
                String exePath = Process.GetCurrentProcess().MainModule.FileName;
                IWshShell shell = new WshShell();
                // 确定是否已经创建的快捷键被改名了
                foreach (var item in Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "*.lnk"))
                {
                    WshShortcut tempShortcut = (WshShortcut)shell.CreateShortcut(item);
                    if (tempShortcut.TargetPath == exePath)
                    {
                        return;
                    }
                }
                WshShortcut shortcut = (WshShortcut)shell.CreateShortcut(shortcutPath);
                shortcut.TargetPath = exePath;
                shortcut.Arguments = "";// 参数  
                shortcut.Description = "金坤科创NFC设备7字节uid读取";
                shortcut.WorkingDirectory = Environment.CurrentDirectory;//程序所在文件夹，在快捷方式图标点击右键可以看到此属性  
                shortcut.IconLocation = exePath;//图标，该图标是应用程序的资源文件  
                shortcut.WindowStyle = 1;
                shortcut.Save();
            }
        }

    }
}
