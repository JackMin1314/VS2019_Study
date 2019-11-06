# 此软件是高级版本，实现了环境变量的自动添加以及修复安装路径问题
## 【使用步骤】
运行NFCServiceV3.0.exe释放目录，默认安装D:\，更改路径不能包含中文字符或空格
进目录
1.先双击运行AddPath.exe  (目的是首次需要自动配置添加环境变量)
2.再双击运行NFC_ID.exe    (目的是创建桌面快捷方式和启动安装Windows服务程序)
3.此时没有错误提示表示安装成功。以后使用不需要再次运行软件了

## 【异常情况】
如果运行NFC_ID.exe出现弹窗提示“安装NFC服务出错，请确认运行目录中AddPath.exe程序，详见目录日志”
可以再次运行桌面快捷方式或目录NFC_ID.exe即可。
也可以再次重复上述操作。

## 【特殊说明】
由于部分Windows系统问题，导致AddPath.exe可能会添加环境变量失败!

通过查看 右击桌面图标我的电脑(或此电脑)->属性->高级系统设置->环境变量->系统环境变量中的Path
是否成功添加了类似 "C:\Windows\Microsoft.NET\Framework64\v4.x.xxxx"路径

如果没有可以手动添加，按Windows+r键输入 C:\Windows\Microsoft.NET\Framework64\ 然后回车，进入目录v4.x.xxx
将该路径复制加到上述的环境变量里。win7需要先添加英文的分号 ';' 后粘贴路径

## 【卸载说明】
建议卸载之前先卸载服务：powershell管理员方式cd 进入安装目录,然后运行: installutil /u NFCService.exe 即可.
然后运行目录运行unins000.exe进行卸载软件
