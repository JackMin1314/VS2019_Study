using IWshRuntimeLibrary;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Threading;
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
            int isclose = 0;
            if (System.Diagnostics.Process.GetProcessesByName("AddPath").ToList().Count > 0)
            {
                MessageBox.Show("请先关闭AddPath.exe程序后再运行", "警告:",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                isclose = 1;
            }
            if (isclose==1)
            {
                Close();
                
            }
            // 为了避免端口占用问题注释RefreshWebApi(),同时启用RunScript()调用Windows powershell，命令行自动注册并启动Windows服务
            // RefreshWebApi();
            try
            {
                //string add_environment_exe = Environment.CurrentDirectory;
                //RunExeByProcess(add_environment_exe.ToString()+ "\\Add_Environment.exe");
                Check_installutil_Path();
                CreateShortcutOnDesktop();
                // 检测是否将安装服务的程序添加到环境变量
                // Check_installutil_Path();

                RunScript(@"installutil NFCService.exe");
            }
            catch(Exception ex)
            {
                
                MessageBox.Show("安装NFC服务出错，请确认运行目录中AddPath.exe程序，详见目录日志", "警告:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                ErrorLog.ErrorLogTxt(ex);
                Close();

            } 
        }
        private void CreateShortcutOnDesktop()
        {
            //添加引用 (com->Windows Script Host Object Model)，using IWshRuntimeLibrary;
            String shortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "NFC_ID.lnk");
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
                shortcut.Description = "NFC设备uid读取";
                shortcut.WorkingDirectory = Environment.CurrentDirectory;//程序所在文件夹，在快捷方式图标点击右键可以看到此属性  
                shortcut.IconLocation = exePath;//图标，该图标是应用程序的资源文件  
                shortcut.WindowStyle = 1;
                shortcut.Save();
            }
        }

        // path路径为空""时候即为当前程序运行目录，否则cd path；scriptText为需要执行的脚本
        // 为了能够安装Windows服务，这里写好了命令
        private  void RunScript(string scriptText)
        {
            var strlist = Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.Machine);
            string[] pathList = strlist.Split(';');
            int flag = 0;
            foreach (var item in pathList)
            {
                // 系统变量里面有这个值得时候,flag =1
                if (item.Contains("C:\\Windows\\Microsoft.NET\\Framework64\\v4"))
                {
                    flag = 1;
                    break;
                }
            }
            if (flag==0)
            {
                return ;
            }
            // create Powershell runspace

            Runspace runspace = RunspaceFactory.CreateRunspace();

            // open it

            runspace.Open();

            // create a pipeline and feed it the script text

            Pipeline pipeline = runspace.CreatePipeline();
            string path = Application.StartupPath;
           pipeline.Commands.AddScript("cd " + path); // 先进入文件目录

            // add your command here
            // 安装服务
            pipeline.Commands.AddScript(scriptText.ToString());
            
            //pipeline.Commands.AddScript("installutil NFCService.exe");
            
            // 启动服务
            pipeline.Commands.AddScript("net start NFCService");

            // pipeline.Commands.AddScript("date");

            //pipeline.Commands.Add("Out-String");
            // execute the script,invoke your pipline
           Collection<PSObject> results = pipeline.Invoke();
            // close the runspace
            runspace.Close();
            // convert the script result into a single string

            ////System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            ////foreach (PSObject obj in results){ stringBuilder.AppendLine(obj.ToString());  }
            ////return stringBuilder.ToString();
           
        }
        public void Check_installutil_Path()
        {
            // 获取系统环境变量,如果不加EnvironmentVariableTarget.Machine时候，表示当前进程获取结果。
            // 一旦另一个进程修改了系统变量，程序不结束，再次获取时候就不会重新从系统变量获取，而是沿用这个变量.
            // 还可以加User，详见EnvironmentVariableTarget
            var strlist = Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.Machine);
            string[] pathList = strlist.Split(';');
             int flag = 0;
            foreach (var item in pathList)
            {
                // 系统变量里面有这个值得时候,flag =1
                if (item.Contains("C:\\Windows\\Microsoft.NET\\Framework64\\v4"))
                {
                    flag = 1; 
                    break;
                }
            }
            // 判断本地是否有installutil.exe的路径
            if (flag == 0)
            {
                string path = @"C:\\Windows\\Microsoft.NET\\Framework64\\";
                DirectoryInfo root = new DirectoryInfo(path);
                DirectoryInfo[] fileList = root.GetDirectories();
                // FileInfo[] files = root.GetFiles();
                foreach (var item in fileList)
                {
                    if (item.FullName.Contains("C:\\Windows\\Microsoft.NET\\Framework64\\v4"))//本地有该程序的路径
                    {
                        // 添加进系统环境变量
                        Environment.SetEnvironmentVariable("path", Environment.GetEnvironmentVariable("path") + ";" + item.FullName, EnvironmentVariableTarget.Machine);
                        break;
                    }
                }
            }//end_if
        }//end_Check_installutil_path()
        public  void RunExeByProcess(string exePath)
        {
            //开启新线程
            Process p = new Process();
            try
            {
                //设置要启动的应用程序
                p.StartInfo.FileName = exePath;

                //p.StartInfo.Arguments = arguments;
                //是否使用操作系统shell启动
                p.StartInfo.UseShellExecute = false;    
                // 接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardInput = false;//?????
                //输出信息
                p.StartInfo.RedirectStandardOutput = false;
                // 输出错误
                p.StartInfo.RedirectStandardError = true;
                //不显示程序窗口
                p.StartInfo.CreateNoWindow = false;////???
                //启动程序
                p.Start();
              
            }
            finally
            {
                p.WaitForExit();
                MessageBox.Show(Environment.GetEnvironmentVariable("path", EnvironmentVariableTarget.Machine).ToString(), "");
                //p.Close();
            }
        }
    }
}
