[TOC]

# NFC读取uid的Windows服务版本使用说明

## 一、安装前准备

**为了顺利安装并使用NFCService服务，请认真阅读该部分内容然后按照操作执行。**<font color ="red">对版本V3也适用，添加了AddPath功能，自动添加系统环境变量</font>font>

### 1. 解压后不要立即运行exe程序（以下检查可忽略）

* 检查Windows系统是否为64位
* 检查系统是否有.net环境(一般都会有)，如果后面运行程序提示缺少.net框架需要去官网 [下载][https://dotnet.microsoft.com/download/dotnet-framework/net472]    (点击下载即可安装.net4.7.2 runtime)

### 2. 添加Windows安装服务程序到环境变量里

* *64位* 的安装程序添加到环境变量中，路径为` C:\Windows\Microsoft.NET\Framework64` 进入v4.0.*文件夹，找到  **`InstallUtil.exe`** 添加到环境变量里。

* 添加后，在Windows菜单搜索power shell(管理员模式)，win10系统右击菜单选择power shell(管理员).

  输入`installutil` 回车，看是否添加成功。

## 二、开始安装程序

**请确保前面操作无误，继续。**

### 1. 解压运行安装程序

* 运行 `NFCUidService.exe` 后，在安装目录有三个.exe程序，分别是 **`NFC_ID.exe`** 、**`NFCService.exe` **、 **`unins000.exe`**  
* 同时桌面创建快捷方式且自动运行程序，自动安装，自动注册Windows服务，同时默认设置为开机自启动，可以在服务中看到NFCUid；
* 退出右下角程序图标，不影响服务的使用，程序采用webapi服务提供 **post接口**。

### 2. 使用程序

* 如果启动程序过程杀毒软件拦截，请允许操作。
* 不需要重复运行桌面快捷方式，首次解压安装后即可一直使用。**不要双击运行服务程序** **`NFCService.exe` **

## 三、卸载软件和停止服务

### 1. 卸载NFC_ID.exe

先退出程序, 然后进入安装目录文件夹运行卸载程序 **`unins000.exe`** ;

### 2. 停止服务NFCUid

管理员方式打开 `powershell`，运行 **`net stop NFCService`** 停止服务，如果启动则运行**`net start NFCService`** 

### 3. 卸载服务软件

管理员方式打开 `powershell`，一定要cd 到安装目录(含有**`NFCService.exe` **)下，然后运行 **`installutil /u NFCService.exe`** 等待卸载完成即可

## 四、运行出错日志

> 安装失败报错，或者运行报错时检查日志。
>
> 运行程序的目录下有出错日志文件夹ErrorLogFile，保存在ErrorLog.txt,若没有文件夹则说明运行没有错误。

