[toc]

# NFC_ID读取软件设计

## 一、**引言**

### 1.1 编写目的

> ​        此文档为金坤科创 `NFCUid_Data_SourceCode` 文件 <u>NFC读取设备号id</u>、<u>存储区数据uid编号</u> 程序NFC_ID.exe设计文档，其目的是给出软件内部实现的架构和流程，并规定软件的外部接口，给出相应的注意事项和部分参数设置等。结合代码详细阐述，有助于相关开发人员对NFC软件理解和开发，供相关人员参考。

### 1.2 相关文件介绍

| 文件名              | 功能                                                         |
| ------------------- | ------------------------------------------------------------ |
| Form1.cs            | 软件的界面设计和运行时的功能初始化包括加载时启动设置webapi服务，创建桌面图标等 |
| Properties/...      | 程序关联的信息(AssemblyInfo)和设置允许管理员方式运行程序(app.manifest) |
| CLotusCardDriver.cs | 调用CLotusCardDriver.dll文件里面的接口。CLotusCardDriver.dll为32位平台的，不同平台的不能混用。 |
| ErrorLog.cs         | 日志存储记录                                                 |
| NFC_Info.cs         | 定义nfc查询结果信息，实际返回json格式的字段名                |
| getnfc_uid.cs       | 获取nfc **芯片的id**编号接口，成功返回id，失败返回对应状态信息 |
| readnfc_data.cs     | 利用COS指令，获取人为写入的指定区域数据uid（不要跟上面getnfc_uid.cs混淆） |
| cardController.cs   | 实际接口请求对应的函数；调用读取芯片id和存储区uid，控制蜂鸣，根据结果返回json数据平台 |
| Program.cs          | 主程序入口点，设置了防止程序重复运行导致webapi路由端口占用出错 |



## 二、**软件功能和性能描述**

### 2.1 软件功能需求

* 主要有两大需求功能:

> 1. NFC芯片设备号id读取
>
>    调用 LotusCardDriver.dll 里面的 Anticoll() 返回编号在结构体里面 arrCardNo
>
> 2. NFC存储区uid编号读取(新增)
>
> ​        依据NFC Forum Type 4 Tag protocol 协议，通过COS指令，NFC数据交换格式得到存储区数据

在实现上述功能的同时，还需通过实现WebApi服务(不依赖其他服务器平台)，提供对外的两个web接口，支持控制设备蜂鸣。

### 2.2 软件的性能要求

本地postman测试结果已满足并发的需要

## 三、**软件的总体设计**

### 3.1 软件功能概述

* 总体功能描述：

>    程序可运行在Windows  7/8/10/   X32/64平台下。
>
>    本软件主要实现接受并解析平台前端的请求，然后根据url匹配相应的接口调用函数，访问设备，结果json返回给平台，具体步骤为controller->action->id ==>post. 依据请求的接口例如： http://localhost:9090/card/readNFCCard ；从cardController.cs里面匹配相应的readNFCCard、readNFCData方法，可根据是否有参数控制蜂鸣。然后依据结果返回给平台相应的json格式数据。支持程序运行时自动隐藏最小化，叉掉按钮不退出。
>
>    错误异常处理机制、不依赖服务器平台和浏览器

### 3.2 软件总体架构设计

本程序主要由 `.NET窗体程序` 、`WebApi程序寄宿` 、`错误异常处理机制` 、`NFC读取设备芯片id号` 、`NFC读取存储区数据uid` 五部分构成。

* **.NET窗体程序**

> 程序可运行在Windows 7/8/10/环境下 X32/64平台。需要.net环境支持。是webapi的载体。程序运行实现了自动隐藏最小化，叉掉按钮不退出。控制整个程序的开始和结束。

* **错误异常处理机制**

> 本程序提供程序异常出错日志保存，包括端口占用和容错、程序执行异常日志记录.

* **WebApi程序寄宿**

> 采用应用程序寄宿的方式，运行软件即启动web接口服务，占用端口。关闭软件则关闭服务，释放端口。适用多种浏览器(已自动实现跨域)。不需要额外的服务器例如IIS和Apache等

* **NFC读取设备芯片id号**

> 获取设备信息句柄,打开设备-->寻卡-->读卡-->蜂鸣(可控)-->关闭设备。依次执行操作后，如果成功返回结构体，里面数组保存结果 arrCardNo ；失败则返回错误码(这里的错误码对平台是隐藏的)，交由cardController.cs提供具体的错误信息。

* **NFC读取存储区数据uid**

> 获取设备信息句柄,打开设备-->寻卡-->读卡-->蜂鸣(可控)-->重置卡-->执行COS指令(四步)-->关闭设备。依次执行操作后，如果成功返回结构体到 lotusCardParam.arrCosResultBuffer[i]，里面保存结果；失败则返回错误码(这里的错误码对平台是隐藏的)，交由cardController.cs提供具体的错误信息。

总体详细架构图如下所示:

![NFC_Architecture](https://github.com/JackMin1314/PicturesFiles/blob/master/NFC_id_Software_Architecture.png)

### 3.3 开发环境

* 操作系统：win10 X64系统
* 编程工具和语言：管理员方式运行VS2019、C# .net开发，需要通过NuGet包管理导入额外的第三方库
* 开发环境:  解决方案配置和平台为X86平台。

## 四、**相关配置说明和注意事项**

### 4.1 文件相应的配置说明

文件名和对应的功能详见 `1.2相关文件介绍`

* 如果需要修改程序执行的**权限为管理员**(创建webapi需要)，请修改`Properties/app.manifest`代码第18行,level值改为requireAdministrator

```xml
 <requestedExecutionLevel level="requireAdministrator" uiAccess="false" />
```

* 如果需要修改界面**UI布局以及事件动作**，例如一些点击事件启动隐藏等，请修改 `Form1.cs` 代码以及可能的代码第15 line的Form1()内容

```C#
 public Form1()
        {
            // 为了实现启动时候没有窗口需要先最小化然后隐藏
            // 最后还要在Form1.cs设计 中设置窗体showicon,showin taskbar的属性为false
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.Visible = false;
            InitializeComponent();// 初始化相应控件和事件
        }
```

* 如果需要修改程序启动时候的一些配置例如**修改WebApi端口，路由规则**，**修改桌面程序图标的详情数据** 等，请修改`Form1.cs` 第161 line的 *Form1_Load()*，第166 line *RefreshWebApi()* 和 第206 line *CreateShortcutOnDesktop()* 内容

```c#
 private void Form1_Load(object sender, EventArgs e)
        {
            CreateShortcutOnDesktop();// 桌面图标
            RefreshWebApi();// 创建webapi
        }
```

* 如果需要修改程序的**图标.ico**,请修改`Form1.cs[设计]`,窗体属性ICON出选择*.ico文件即可
* 如果需要添加**新的接口action**，而保持原来路由接口不变，即(http://localhost:9090/card/{action})需要修改文件*cardController.cs* 代码第23 line的*public class cardController : ApiController{}*内容

```C#
public class cardController : ApiController
{
    [HttpPost]
        public HttpResponseMessage readNFCCard(Boolean sound)// 读取的是芯片的卡号
        {...}
    [HttpPost]
        public HttpResponseMessage readNFCData(Boolean sound)// 读取的是芯片的卡号
        {...}
    // add your code here!!
    [HttpPost] // post get delete put
    public HttpResponseMessage YouNewAction(Parama) // 你的新的函数
        {...}
}
```

* 如果需要修改平台请求返回的**json字段**时(默认为code ,msg,data)，先修改*NFC_Info.cs*代码 第17 *line public class NFC_Info{}* 添加字段

```c#
public class NFC_Info
    {

        private string mycode="";
        private string mymsg="";
        private string mydata;
        // first add a private value here

        public string code { get { return mycode; } set { mycode = value; } }
        public string msg { get { return mymsg; } set { mymsg = value; } }
        public string data { get { return mydata; } set { mydata = value; } }
        // last add a get-set struct here

}
```

然后再 *cardController.cs* 代码 *public class cardController : ApiController{}* 相应的action添加返回的字段.这可能会需添加新的返回值状态码的。

* 如果需要修改**设备号id**那块，如果涉及到底层修改请依据具体的官方操作手册(CLotusCardDriver.dll文件为X86平台下，可能需要进行对CLotusCardDriver.cs的代码参数修改配置)。修改 *getnfc_uid.cs*代码第 4 line *public class getnfc_uid: CLotusCardDriver{}* 内容

```c#
public class getnfc_uid : CLotusCardDriver
{
      getnfc_uid() { }
      ~getnfc_uid() { }
    public string getNFC_id(Boolean sound)
        {}
    public string getNFC_id()
        {}
    // add you code here 
    
    public static getnfc_uid get_instance() { return new getnfc_uid(); }
}
```

* 如果需要**修改读取存储区数据的读取长度**,请依据具体的 NFC Forum Type 4 Tag protocol 协议，通过COS指令一步步得到结果. 本次读取一共使用四次指令，修改最后一次的指令可以调节读取长度.修改*readnfc_data.cs*代码第10 line 内容 *public class Readnfc_data : CLotusCardDriver{}* 里面的第 95 line 

```C#
cosString="00B0000008" 
```

* 如果需要修改错误日志保存格式，请修改*ErrorLog.cs*代码第2 line msg内容

```c#
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
```

### 4.2 注意事项

* 程序需要创建webapi，因为以管理员权限IDE打开程序,且生成的程序运行时候也要是管理员权限.（具体做法见上）
* 因为利用底层的.dll文件是*X86平台*的(开发环境),不能使用其他的*X64*环境的.dll文件，如果搞混淆了，请进入官网找X86，或者本人个人[Github](https://github.com/JackMin1314/VS2019_Study/tree/master/NFC_Files/NFCUid_Data_SourceCode) 里面的 LotusCardDriver.dll、 LotusCardDriver.lib文件。例外需要将LotusCardDriver.dll添加到生成的程序里面!!
* 代码移植时候可能部分第三方库没有，需要通过VS自带的NuGet下载相应的库。如果打不开，可以复制代码重新建项目。

