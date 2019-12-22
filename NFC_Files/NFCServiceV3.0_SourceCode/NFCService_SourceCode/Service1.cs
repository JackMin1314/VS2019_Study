using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.SelfHost;
using System.Windows.Forms;

namespace NFCService
{
    public partial class NFCService : ServiceBase
    {
        // 包含下一个要写入事件日志的事件的标识符
        private int eventId = 1;

        // 设置服务状态
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }
        // 服务控制管理器使用 SERVICE_STATUS 结构的 dwWaitHint 和 dwCheckpoint 成员
        // 来确定等待 Windows 服务启动或关闭所需的时间
        // 如果 OnStart 和 OnStop 方法运行时间较长，
        // 服务可以使用递增的 dwCheckPoint 值 再次调用 SetServiceStatus 来请求更多时间
        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState; // 枚举类型
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };
        // 构造函数
        public NFCService()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource","MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
            // 尝试创建webapi
            RefreshWebApi();

        }

        // 服务开始时
        protected override void OnStart(string[] args)
        {
            //// Update the service state to Start Pending.
            //ServiceStatus serviceStatus = new ServiceStatus();
            //serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            //serviceStatus.dwWaitHint = 100000;
            //SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            ServiceStatus serviceStatus = new ServiceStatus();

           // eventLog1.WriteEntry("In OnStart...");
           // // 设置轮询机制
           // System.Timers.Timer timer = new System.Timers.Timer();
           // timer.Interval = 6000; // 6秒,以毫秒为单位
           //// 启动定时任务（注册委托事件，委托OnTimer()函数去执行）  
           // timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
           // timer.Start();

            // 方法调用完毕，再次设置状态为SERVICE_RUNNING
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

        }

        // 设置定时器内容, OnTimer 方法来处理 Timer.Elapsed 事件(定时事件)
        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System" + DateTime.Now, EventLogEntryType.Information,eventId++);
            eventLog1.WriteEntry("error happened at " + DateTime.Now, EventLogEntryType.Error);//错误
            ErrorLog.WriteMsg("OnTimer Write Msg");
        }

        // 服务结束时
        protected override void OnStop()
        {
            ServiceStatus serviceStatus = new ServiceStatus();
            //eventLog1.WriteEntry("In OnStop...");
            serviceStatus.dwCurrentState = ServiceState.SERVICE_STOPPED;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        // 重写Oncontinue方法
        protected override void OnContinue()
        {
            eventLog1.WriteEntry("In OnContinue...");
        }


        HttpSelfHostConfiguration config = null;
        HttpSelfHostServer server = null;
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

    }
}
