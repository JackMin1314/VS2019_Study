using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace NFC_ID

{
    //回调函数
    public delegate int OnLotusCardExtendReadWriteCallBackFunc(int bRead, [In, Out] byte[] pReadWriteBuffer, int nBufferLength);
    public delegate void OnLotusCardWrite2FlashCallBackFunc(UInt32 unCurrentPos, UInt32 unFileLength);
    //参数结构体
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct LotusCardParamStruct
    {
        [MarshalAs(UnmanagedType.I4)]
        public int nCardType;//卡片类型
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] arrCardNo;// 4字节卡号
        [MarshalAs(UnmanagedType.I4)]
        public int nCardSize;// 卡片容量大小
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] arrBuffer;//读写缓冲
        [MarshalAs(UnmanagedType.I4)]
        public int nBufferSize;//缓冲长度
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] arrKeys;//密钥缓冲
        [MarshalAs(UnmanagedType.I4)]
        public int nKeysSize;//密钥缓冲长度

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] arrCosResultBuffer; // COS执行结果缓冲
        [MarshalAs(UnmanagedType.I4)]
        public int unCosReultBufferLength;//COS执行结果缓冲长度 

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] arrCosSendBuffer;//COS指令发送缓冲
        [MarshalAs(UnmanagedType.I4)]
        public int unCosSendBufferLength;//COS指令发送缓冲长度 
    }

    //UHF参数结构体
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct UhfInventoryStruct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public byte[] arrEPCBuffer;//EPC缓冲 
        [MarshalAs(UnmanagedType.U1)]
        byte ucEPCRealLength;//EPC真实长度
        [MarshalAs(UnmanagedType.U1)]
        byte ucRssi;//型号强度
        [MarshalAs(UnmanagedType.U2)]
        UInt16 usPC;//协议控制码 这个信息里面也有EPC长度 考虑方便 和 字节对齐 上面多定义了一个
    }

    //WIFI 结构体参数
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct BssInfoStruct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] arrBssid; //BSSID
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] arrSsid;  //SSID
        [MarshalAs(UnmanagedType.U1)]
        public byte ucChannel;  //通道
        [MarshalAs(UnmanagedType.I1)]
        public char scRssi;         //信号强度
        [MarshalAs(UnmanagedType.U1)]
        public byte ucAuthMode; //认证模式
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct StaInfoStruct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] arrSsid;              //SSID
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] arrPassword;          //Password 密码
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] arrStaIp;             //Sta IP 地址
                                            //unsigned char arrStaNetMaskIp[4];		//Sta 掩码 地址
                                            //unsigned char arrStaDnsIp[4];			//Sta DNS 地址

        //unsigned char arrApIp[4];				//Ap IP 地址
        //unsigned char arrApNetMaksIp[4];		//Ap 掩码 地址

        //unsigned char ucStaUseDhcp;			//Sta 使用DHCP获取 1=使用DHCP 0=不使用DHCP
    }


    //二代证信息结构
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct TwoIdInfoStruct
    {


        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public byte[] arrTwoIdName;                 //姓名 UNICODE
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public byte[] arrTwoIdSex;                  //性别 UNICODE
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] arrTwoIdNation;                   //民族 UNICODE
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] arrTwoIdBirthday;             //出生日期 UNICODE YYYYMMDD
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 70)]
        public byte[] arrTwoIdAddress;              //住址 UNICODE
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
        public byte[] arrTwoIdNo;                   //身份证号码 UNICODE
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
        public byte[] arrTwoIdSignedDepartment;     //签发机关 UNICODE
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] arrTwoIdValidityPeriodBegin;  //有效期起始日期 UNICODE YYYYMMDD
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] arrTwoIdValidityPeriodEnd;        //有效期截止日期 UNICODE YYYYMMDD 有效期为长期时存储“长期”
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 72)]//此处有点问题
        public byte[] arrTwoIdNewAddress;           //最新住址 UNICODE
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public byte[] arrTwoIdPhoto;        //照片信息
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public byte[] arrTwoIdFingerprint;//指纹信息
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4096)]
        public byte[] arrTwoIdPhotoJpeg;    //照片信息 JPEG 格式
        [MarshalAs(UnmanagedType.U4)]
        public UInt32 unTwoIdPhotoJpegLength;   //照片信息长度 JPEG格式
    }

    //社保卡结构 里面存放公开信息
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SocialSecurityCardStruct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] szCardNo;//实际只有9个字符
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szName;//一般是4-8个字符 考虑到特殊人名 搞多点
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szSSCNo;//一般是身份证号18个字符
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] szSex;//性别
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szNation;//民族 有的民族名称有点长
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szBankNo;//对应的银行卡号
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szSignedDate;//签发日期
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szValidityEndDate;//到期日期
    }

    //错误代码
    public enum LotusCardErrorCode
    {
        LCEC_
            = 0,                //正常执行
        LCEC_UNKNOWN,               //未知的 
        LCEC_SEND_FALSE,            //发送失败
        LCEC_RECV_TIME_OUT,         //接收超时
        LCEC_RECV_ZERO_LEN,         //接收长度为0
        LCEC_RECV_CRC_FALSE,        //接收校验失败
        LCEC_REQUEST,               //寻卡
        LCEC_ANTICOLL,              //防冲突
        LCEC_SELECT,                //选卡
        LCEC_AUTHENTICATION,        //三次验证
        LCEC_HALT,                  //中止
        LCEC_READ,                  //读
        LCEC_WRITE,                 //写
        LCEC_INCREMENT,             //加值
        LCEC_DECREMENT,             //减值
        LCEC_LOADKEY,               //装载密码
        LCEC_BEEP,                  //蜂鸣
        LCEC_RESTORE,               //卡数据块传入卡的内部寄存器
        LCEC_TRANSFER,              //内部寄存器传入卡的卡数据块
        LCEC_SEND_COMMAND,          //发送14443指令
        LCEC_WIFI_SCANAP_BEGIN,     //发送扫描动作开始信号
        LCEC_WIFI_SCANAP_COUNT,     //获取扫描AP计数
        LCEC_WIFI_SCANAP_RESULT,    //获取扫描结果
        LCEC_WIFI_GET_STAINFO,  //获取STA信息
        LCEC_WIFI_SET_STAINFO,  //设置STA信息
        LCEC_WIFI_GET_MODE,     //获取模式
        LCEC_WIFI_SET_MODE,     //设置模式
        LCEC_WIFI_RESET,            //复位WIFI模块
        LCEC_FELICA_POLLING,        //FELICA寻卡动作
        LCEC_REQUESTB,              //typeb寻卡错误
        LCEC_SELECTB,               //typeb选卡
        LCEC_HALTB,                 //typeb中止
        LCEC_M100_INVENTORY_FAIL,   //轮询操作失败。没有标签返回或者返回数据CRC 校验错误。
    };
    //二代证错误编码
    public enum TwoIdErrorCode
    {
        TIEC_NO_ERROR = 0,                  //正常执行 没有错误
        TIEC_IPADDRESS,                     //错误IP地址 格式错误
        TIEC_REQUESTB,                      //寻卡错误
        TIEC_SELECTB,                       //选卡错误
        TIEC_GET_NO,                        //获取卡号错误 执行卡ID COS指令失败
        TIEC_GET_NO_RESULT,                 //获取卡号错误结果 没有返回9000
        TIEC_GET_NO_OTHER,                  //获取卡号其他错误
        TIEC_GET_RANDOM,                    //取随机数错误
        TIEC_GET_RANDOM_RESULT,             //取随机数错误结果 没有返回9000
        TIEC_SELECT_FIRST_FILE,             //选第一个文件错误
        TIEC_SELECT_FIRST_FILE_RESULT,      //选第一个文件错误结果 没有返回9000
        TIEC_READ_FIRST_FILE,               //读第一个文件错误
        TIEC_READ_FIRST_FILE_RESULT,        //选第一个文件错误结果 没有返回9000
        TIEC_RECEIVE_INTERNAL_AUTHENTICATE, //接收内部认证 TCP 动作
        TIEC_EXEC_INTERNAL_AUTHENTICATE,    //执行内部认证
        TIEC_SEND_INTERNAL_AUTHENTICATE,    //发送内部认证结果 TCP
        TIEC_EXEC_GET_RANDOM,               //获取随机数
        TIEC_SEND_RANDOM,                   //发送随机数 TCP
        TIEC_RECEIVE_EXTERNAL_AUTHENTICATE, //接收外部认证 TCP 动作
        TIEC_EXEC_EXTERNAL_AUTHENTICATE,    //执行外部认证
        TIEC_READ_SEND_SECOND_FILE,         //读取并发送第二个文件
        TIEC_READ_SEND_THIRD_FILE,          //读取并发送第三个文件
        TIEC_READ_SEND_FOURTH_FILE,         //读取并发送第四个文件
        TIEC_RECEIVE_LAST_DATA,             //接收最后的数据
        TIEC_CONNECT_SERVER,                //连接服务器失败
        TIEC_SAMV_BUSY,                     //服务器端SAMV 繁忙
        TIEC_READ_SEND_FIFTH_FILE,          //读取并发送第五个文件

    };
    public class CLotusCardDriver
    {

        public const int RT_NOT_HALT = 0x26;
        public const int RT_ALL = 0x52;
        public const int AM_A = 0x60;
        public const int AM_B = 0x61;
        /**
         * 打开设备
         *
         * @param strDeviceName
         *            串口设备名称
         * @param nVID
         *            USB设备VID
         * @param nPID
         *            USB设备PID
		 * @param nUsbDeviceIndex
		 *            USB设备索引
		 * @param unRecvTimeOut
		 *            接收超时
		 * @param pLotusCardExtendReadWriteCallBack 外部读写通道回调函数 只要针对ANDROID 
		 * 			如果没有设备写权限时，可以使用外部USB或串口进行通讯，
		 * 			需要改造callBackProcess中相关代码完成读写工作 目前范例提供USB操作
         * @return 句柄
         */

        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardOpenDevice", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardOpenDevice(string pszDeviceName, int nVID, int nPID, int nUsbDeviceIndex, uint unRecvTimeOut, OnLotusCardExtendReadWriteCallBackFunc CallBackFunc);

        /**
         * 关闭设备
         *
         * @param nDeviceHandle
         *            设备句柄
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardCloseDevice", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void LotusCardCloseDevice(int nDeviceHandle);

        /**
         * 蜂鸣
         * @param nDeviceHandle
         *            设备句柄
         * @param nDeviceHandle 设备句柄
         * @param nBeepLen 蜂鸣长度 毫秒为单位
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardBeep", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardBeep(int nDeviceHandle, int nBeepLen);


        /**
         * 寻卡
         *
         * @param nDeviceHandle
         *            设备句柄   
         * @param nRequestType
         *            请求类型
         * @param tLotusCardParam
         *            结果值 用里面的卡片类型
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardRequest", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardRequest( int nDeviceHandle,int nRequestType, ref LotusCardParamStruct sttLotusCardParam);

        /**
         * 防冲突
         *
         * @param nDeviceHandle
         *            设备句柄
         * @param tLotusCardParam
         *            结果值 用里面的卡号
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardAnticoll", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardAnticoll(int nDeviceHandle, ref LotusCardParamStruct sttLotusCardParam);

        /**
         * 选卡
         *
         * @param nDeviceHandle
         *            设备句柄
         * @param tLotusCardParam
         *            参数(使用里面的卡号)与结果值(使用里面的卡容量大小)
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSelect", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSelect(long nDeviceHandle, ref LotusCardParamStruct sttLotusCardParam);

        /**
         * 密钥验证
         *
         * @param nDeviceHandle
         *            设备句柄
         * @param nAuthMode
         *            验证模式
         * @param nSectionIndex
         *            扇区索引
         * @param tLotusCardParam
         *            参数(使用里面的卡号)
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardAuthentication", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardAuthentication(long nDeviceHandle, int nAuthMode, int nSectionIndex, ref LotusCardParamStruct sttLotusCardParam);

        /**
         * 卡片中止响应
         *
         * @param nDeviceHandle
         *            设备句柄
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardHalt", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardHalt(long nDeviceHandle);

        /**
         * 读指定地址数据
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress 块地址
         * @param tLotusCardParam 结果值（读写缓冲）
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardRead", SetLastError = true,
         CharSet = CharSet.Ansi, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardRead(long nDeviceHandle, int nAddress, ref LotusCardParamStruct sttLotusCardParam);

        /**
         * 写指定地址数据
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress 块地址
         * @param tLotusCardParam 参数（读写缓冲）
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardWrite", SetLastError = true,
         CharSet = CharSet.Ansi, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardWrite(long nDeviceHandle, int nAddress, ref LotusCardParamStruct sttLotusCardParam);

        /**
         * 加值
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress 块地址
         * @param nValue 值
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardIncrement", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardIncrement(long nDeviceHandle, int nAddress, int nValue);

        /**
         * 减值
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress 块地址
         * @param nValue 值
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardDecrement", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardDecrement(long nDeviceHandle, int nAddress, int nValue);

        /**
         * 卡数据块传入卡的内部寄存器
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress 块地址
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardRestore", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardRestore(long nDeviceHandle, int nAddress);
        /**
         * 内部寄存器传入卡的卡数据块
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress 块地址
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardTransfer", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardTransfer(long nDeviceHandle, int nAddress);
        /**
         * 装载密钥
         * @param nDeviceHandle
         *            设备句柄
         * @param nAuthMode 验证模式
         * @param nSectionIndex
         *            扇区索引   
         * @param tLotusCardParam 参数（密钥）
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardLoadKey", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardLoadKey(long nDeviceHandle, int nAuthMode, int nSectionIndex, ref LotusCardParamStruct sttLotusCardParam);
        /**
         * 发送指令 用于CPU卡
         * @param nDeviceHandle
         *            设备句柄
         * @param nTimeOut 超时参数
         * @param tLotusCardParam 参数（指令缓冲,返回结果）
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSendCpuCommand", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSendCpuCommand(long nDeviceHandle, int nTimeOut, ref LotusCardParamStruct sttLotusCardParam);
        /********************************以下函数调用上述函数，为了简化第三方调用操作***************************/
        /**
		* 读指定地址文本
		* @param nSectionIndex 扇区索引
		* @param pTextInfo 结果值（读写缓冲）
		* @param unTextInfoLength 文本缓冲长度
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardReadText", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardReadText(long nDeviceHandle, int nSectionIndex, [In, Out] byte[] pTextInfo, uint unTextInfoLength);
        /**
		* 写指定地址文本
		* @param nSectionIndex 扇区索引
		* @param pTextInfo 结果值（读写缓冲）
		* @param unTextInfoLength 文本缓冲长度
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardWriteText", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardWriteText(long nDeviceHandle, int nSectionIndex, Byte[] pTextInfo, uint unTextInfoLength);

        /**
         * 获取卡号
         * @param nDeviceHandle
         *            设备句柄
         * @param nRequestType
         *            请求类型
         * @param tLotusCardParam
         *            结果值 
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetCardNo", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetCardNo(long nDeviceHandle, int nRequestType,
                ref LotusCardParamStruct sttLotusCardParam);

        /**
         * 初始值
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress 块地址
         * @param nValue 值
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardInitValue", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardInitValue(long nDeviceHandle, int nAddress, int nValue);

        /**
         * 修改密码AB
         * @param nDeviceHandle
         *            设备句柄
         * @param pPasswordA 密码A
         * @param pPasswordB 密码B
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardChangePassword", SetLastError = true,
         CharSet = CharSet.Ansi, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardChangePassword(long nDeviceHandle, int nSectionIndex, string pPasswordA, string pPasswordB);

        /**
         * 复位CPU卡
         * @param nDeviceHandle
         *            设备句柄
         * @param tLotusCardParam
         *            结果值 
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardResetCpuCard", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardResetCpuCard(int nDeviceHandle,
        ref LotusCardParamStruct sttLotusCardParam);

        /**
         * 发送指令 用于CPU卡 封装LotusCardSendCpuCommand
         * @param nDeviceHandle
         *            设备句柄
         * @param tLotusCardParam 参数（指令缓冲,返回结果）
         * @return true = 成功
         */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSendCOSCommand", SetLastError = true,
         CharSet = CharSet.Auto, ExactSpelling = false,
         CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSendCOSCommand(int nDeviceHandle,
        ref LotusCardParamStruct sttLotusCardParam);


        /**
		* 获取银行卡卡号
		* @param nDeviceHandle
		* @param pBankCardNo 银行卡号 同印刷号码
		* @param unBankCardNoLength 银行卡号长度
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetBankCardNo", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetBankCardNo(long nDeviceHandle, [In, Out] byte[] pBankCardNo, uint unBankCardNoLength);

        /**
		 * 7816通道获取银行卡
		 * @param nDeviceHandle
		 * @param pBankCardNo 银行卡号 同印刷号码
		 * @param unBankCardNoLength 银行卡号长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetBankCardNoBy7816", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetBankCardNoBy7816(long nDeviceHandle, [In, Out] byte[] pBankCardNo, uint unBankCardNoLength);

        /**
		 * 7816通道获取社保卡号
		 * @param nDeviceHandle
		 * @param psttSocialSecurityCard 社保卡信息结构体指针
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetSocialSecurityInfoBy7816", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetSocialSecurityInfoBy7816(long nDeviceHandle, ref SocialSecurityCardStruct psttSocialSecurityCard);

        /**
		 * 进入Esp isp模式
		 * @param nDeviceHandle
		 *            设备句柄
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSendEspEnterIspCommand", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSendEspEnterIspCommand(long nDeviceHandle);
        /**
		 * 发送Esp isp开始动作
		 * @param nDeviceHandle
		 *            设备句柄
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSendEspIspBeginCommand", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSendEspIspBeginCommand(long nDeviceHandle);

        /**
		 * 写文件到ESP FLASH
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszFilePath
		 *            文件路径
		 * @param unOffset
		 *            FLASH 偏移量
		 * @param pOnLotusCardWrite2FlashCallBack
		 *            回调函数 用于处理进度显示
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardWriteEspFile2Flash", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardWriteEspFile2Flash(long nDeviceHandle, ref char pszFilePath, UInt32 unOffset, OnLotusCardWrite2FlashCallBackFunc pOnLotusCardWrite2FlashCallBack);

        /**
		 * 发送Esp isp结束动作
		 * @param nDeviceHandle
		 *            设备句柄
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSendEspIspEndCommand", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSendEspIspEndCommand(long nDeviceHandle);

        /**
		* 读指定地址数据 一个指令就完成所有动作
		* @param nDeviceHandle
		*            设备句柄
		* @param nRequestType
		*            请求类型
		* @param nAddress 块地址
		* @param ucUsePassWord 使用密码 1=使用参数密码 0 =使用设备内部密码
		* @param ucBeepLen 蜂鸣长度 最长255毫秒
		* @param ucUseHalt 使用中止
		* @param tLotusCardParam 结果值（读写缓冲）
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardReadData", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardReadData(long nDeviceHandle, int nRequestType, int nAddress, byte ucUseParameterPassWord,
            byte ucBeepLen, byte ucUseHalt, ref LotusCardParamStruct pLotusCardParam);

        /**
		 * 写指定地址数据
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param nAddress 块地址
		 * @param ucBeepLen 蜂鸣长度 最长255毫秒
		 * @param ucUseHalt 使用中止
		 * @param tLotusCardParam 参数（读写缓冲）
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardWriteData", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardWriteData(long nDeviceHandle, int nAddress, byte ucBeepLen, byte ucUseHalt, ref LotusCardParamStruct pLotusCardParam);
        /******************************** 以下函数为WIFI操作函数 ***************************/
        /**
		 * 扫描AP
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pBssInfoArray 装载返回AP信息数组
		 * @param unBssInfoArraySize 装载返回AP数组大小
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardScanAp", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardScanAp(long nDeviceHandle, ref BssInfoStruct pBssInfoArray, UInt32 unBssInfoArraySize);

        /**
		* 获取WIFI 工作模式
		 * @param nDeviceHandle
		 *            设备句柄
		* @param pucWifiMode
		*            模式返回值
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetWifiMode", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetWifiMode(long nDeviceHandle, [In, Out] byte[] pucWifiMode);

        /**
		* 设置WIFI 工作模式
		 * @param nDeviceHandle
		 *            设备句柄
		* @param ucWifiMode
		*            模式值
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSetWifiMode", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSetWifiMode(long nDeviceHandle, byte ucWifiMode);

        /**
		* 获取TA信息
		 * @param nDeviceHandle
		 *            设备句柄
		* @param pStaInfoStruct
		*            STA相关参数
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetStaInfo", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetStaInfo(long nDeviceHandle, ref StaInfoStruct pStaInfoStruct);
        /**
		* 设置STA信息
		 * @param nDeviceHandle
		 *            设备句柄
		* @param pStaInfoStruct
		*            STA相关参数
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSetStaInfo", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSetStaInfo(long nDeviceHandle, ref StaInfoStruct pStaInfoStruct);


        /**
		* 连接测试
		 * @param pServerIp
		 *            服务器IP地址
		* @param nConnectTimeOut
		*            超时us为单位
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardConnectTest", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardConnectTest(ref char pServerIp, int nConnectTimeOut);

        /**
		* 获取错误编码 
		 * @param nDeviceHandle
		 *            设备句柄
		* @return 错误编码 详见枚举值定义
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetErrorCode", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 LotusCardGetErrorCode(long nDeviceHandle);

        /**
		* 获取二代证操作错误编码 
		 * @param nDeviceHandle
		 *            设备句柄
		* @return 错误编码 详见枚举值定义
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdErrorCode", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 LotusCardGetTwoIdErrorCode(long nDeviceHandle);


        /**
		* 获取错误信息
		 * @param nDeviceHandle
		 *            设备句柄
		* @param errCode 错误编码
		* @param pszErrorInfo 错误信息地址
		* @param unErrorInfoLength 错误信息长度
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetErrorInfo", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void LotusCardGetErrorInfo(long nDeviceHandle, LotusCardErrorCode errCode, ref char pszErrorInfo, UInt32 unErrorInfoLength);


        /**
		* 获取二代证错误信息
		 * @param nDeviceHandle
		 *            设备句柄
		* @param errCode 错误编码
		* @param pszErrorInfo 错误信息地址
		* @param unErrorInfoLength 错误信息长度
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdErrorInfo", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void LotusCardGetTwoIdErrorInfo(long nDeviceHandle, TwoIdErrorCode errCode, ref char pszErrorInfo, UInt32 unErrorInfoLength);


        /**
		 * 设置卡片类型
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param cCardType 卡片类型 A='A'/'a' B='B'/'b' F='F'/'f' C='C'/'c'
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSetCardType", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSetCardType(long nDeviceHandle, char cCardType);

        /**
		 * Felica寻卡 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param unTimerSlot timer slot
		 * @param tLotusCardParam 参数（读写缓冲）
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardFelicaPolling", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardFelicaPolling(long nDeviceHandle, byte unTimerSlot, ref LotusCardParamStruct pLotusCardParam);

        /**
		 * 发送SAMV命令
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param tLotusCardParam 参数（读写缓冲）
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSendSamvCommand", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSendSamvCommand(long nDeviceHandle, ref LotusCardParamStruct pLotusCardParam);

        /**
		 * 发送COS指令结果给SAMV
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param tLotusCardParam 参数（读写缓冲）
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSendCosResult2Samv", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSendCosResult2Samv(long nDeviceHandle, ref LotusCardParamStruct pLotusCardParam);

        /**
		 * 获取安全模块串口数据
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param tLotusCardParam 参数（读写缓冲）
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetSamvUartData", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetSamvUartData(long nDeviceHandle, ref LotusCardParamStruct pLotusCardParam);

        /**
		 * 获取二代证信息
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pTwoIdInfo 参数 二代证信息结构体地址
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdInfo", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetTwoIdInfo(long nDeviceHandle, ref TwoIdInfoStruct pTwoIdInfo);

        /**
		 * 通过网络获取二代证信息
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszServerIp 参数 安全模块所在服务器IP
		 * @param pTwoIdInfo 参数 二代证信息结构体地址
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdInfoByServer", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetTwoIdInfoByServer(long nDeviceHandle, string pszServerIp, ref TwoIdInfoStruct pTwoIdInfo);

        /**
		 * 通过网络获取二代证信息
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszServerIp 参数 安全模块所在服务器IP
		 * @param unServerPort 参数 服务器端口
		 * @param pTwoIdInfo 参数 二代证信息结构体地址
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdInfoByServerEx", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetTwoIdInfoByServerEx(long nDeviceHandle, string pszServerIp, int unServerPort, ref TwoIdInfoStruct pTwoIdInfo);

        /**
		 * 通过单片机服务器网络获取二代证信息
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszServerIp 参数 调度服务器IP
		 * @param nUserAccount 参数 用户账号 整形 便于服务器检索
		 * @param pszPassWord 参数 密码
		 * @param pTwoIdInfo 参数 二代证信息结构体地址
		 * @param nPostcode 参数 邮政编码
		 * @param nLineType 参数 线路类型 0=未知 1=电信 2=联通 3=移动 4=其他
		 * @param unRecvTimeOut 参数 接收超时 默认10秒
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdInfoByMcuServer", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetTwoIdInfoByMcuServer(long nDeviceHandle, string pszDispatchServerIp, int nUserAccount, string pszPassWord, ref TwoIdInfoStruct pTwoIdInfo, int nPostcode, int nLineType, int unRecvTimeOut);

        /**
		 * 通过单片机服务器网络获取二代证信息 带指纹
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszServerIp 参数 调度服务器IP
		 * @param nUserAccount 参数 用户账号 整形 便于服务器检索
		 * @param pszPassWord 参数 密码
		 * @param pTwoIdInfo 参数 二代证信息结构体地址
		 * @param nPostcode 参数 邮政编码
		 * @param nLineType 参数 线路类型 0=未知 1=电信 2=联通 3=移动 4=其他
		 * @param unRecvTimeOut 参数 接收超时 默认10秒
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdInfoByMcuServer", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetTwoIdInfoByMcuServerEx(long nDeviceHandle, string pszDispatchServerIp, int nUserAccount, string pszPassWord, ref TwoIdInfoStruct pTwoIdInfo, int nPostcode, int nLineType, int unRecvTimeOut);


        /**
		 * 通过网络获取二代证信息 这个API用于网络环境比较糟糕的地方 内部有重试动作
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszServerIp 参数 安全模块所在服务器IP
		 * @param unServerPort 参数 服务器端口
		 * @param pTwoIdInfo 参数 二代证信息结构体地址
		 * @param unRecvTimeOut 参数 接收超时 默认10秒
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdInfoByWireless", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetTwoIdInfoByWireless(long nDeviceHandle, string pszServerIp, int unServerPort, ref TwoIdInfoStruct pTwoIdInfo, int unRecvTimeOut);


        /**
		 * 通过网络获取二代证信息 这个API用于网络环境比较糟糕的地方 内部有重试动作 带指纹
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszServerIp 参数 安全模块所在服务器IP
		 * @param unServerPort 参数 服务器端口
		 * @param pTwoIdInfo 参数 二代证信息结构体地址
		 * @param unRecvTimeOut 参数 接收超时 默认10秒
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdInfoByWireless", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetTwoIdInfoByWirelessEx(long nDeviceHandle, string pszServerIp, int unServerPort, ref TwoIdInfoStruct pTwoIdInfo, int unRecvTimeOut);


        /**
		 * 通过网络获取二代证照片信息 WL格式输入
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszServerIp 参数 安全模块所在服务器IP
		 * @param pTwoIdInfo 参数 二代证信息结构体地址
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardWlDecodeByServer", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardWlDecodeByServer(long nDeviceHandle, string pszDecodeServerIp, ref TwoIdInfoStruct pTwoIdInfo);

        /**
		* 读取NFC缓冲
		 * @param nDeviceHandle
		 *            设备句柄
		* @param pszNfcBuffer
		*            缓冲地址
		* @param unNfcBufferLength
		*            缓冲长度
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardReadNfcBuffer", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardReadNfcBuffer(long nDeviceHandle, [In, Out] byte[] pszNfcBuffer, UInt32 unNfcBufferLength);

        /**
		* 写入NFC缓冲
		 * @param nDeviceHandle
		 *            设备句柄
		* @param pszNfcBuffer
		*            缓冲地址
		* @param unNfcBufferLength
		*            缓冲长度
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardWriteNfcBuffer", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardWriteNfcBuffer(long nDeviceHandle, [In, Out] byte[] pszNfcBuffer, UInt32 unNfcBufferLength);

        /******************************** 以下函数为type b操作函数 ***************************/
        /**
		 * 寻卡
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param nRequestType
		 *            请求类型
		 * @param tLotusCardParam
		 *            结果值 用里面的卡片类型
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardRequestB", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardRequestB(long nDeviceHandle, int nRequestType,
                ref LotusCardParamStruct pLotusCardParam);



        /**
		 * 选卡
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param tLotusCardParam
		 *            参数(使用里面的卡号)与结果值(使用里面的卡容量大小)
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSelectB", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSelectB(long nDeviceHandle,
            ref LotusCardParamStruct pLotusCardParam);


        /**
		 * 卡片中止响应
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardHaltB", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardHaltB(long nDeviceHandle);

        /**
		 * 获取超高频固件信息
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucVersionType 
		 *			  参数 版本类型 硬件版本  0x00 软件版本 0x01  制造商信息  0x02
		 * @param pUhfFwVersionInfo 
		 *			  参数 超高频固件版本信息字串地址
		 * @param unVersionInfoLength 
		 *			  参数 字串分配内存长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetUhfFwVersion", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetUhfFwVersion(long nDeviceHandle, byte ucVersionType, ref char pUhfFwVersionInfo, UInt32 unVersionInfoLength);


        /**
		* 发送M100系列UHF 单次轮询指令
		* 
		* @param nDeviceHandle
		*            设备句柄
		* @param unInventoryCount 
		*			  参数 盘存返回的标签总数
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfInventory", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfInventory(long nDeviceHandle, ref UInt32 unInventoryCount);

        /**
		 * 发送M100系列UHF 获取盘存返回信息 在单次轮询/多次轮询后调用 以后如果需要也可以搞成回调的方式 搞个线程处理 不过有点麻烦 暂时就这样 wdy 2015-05-04
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucInventoryIndex 
		 *			  参数 盘存返回信息的索引
		 * @param pUhfInventory 
		 *			  参数 盘存返回信息结构体指针
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfGetInventoryStruct", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfGetInventoryStruct(long nDeviceHandle, byte ucInventoryIndex, ref UhfInventoryStruct pUhfInventory);

        /**
		 * 发送M100系列UHF 多次轮询指令
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucRssi 
		 *			  参数 获取RSSI
		 * @param ucPC 
		 *			  参数 获取PC
		 * @param pEPCBuffer 
		 *			  参数 获取EPC 存放地址
		 * @param unEPCBufferLength 
		 *			  参数 EPC存放缓冲长度
		 * @param unEPCLength 
		 *			  参数 获取EPC实际长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfReadMulti", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfReadMulti(long nDeviceHandle, [In, Out] byte[] ucRssi, ref UInt16 usPC, [In, Out] byte[] pEPCBuffer,
                                                                                                                                     UInt32 unEPCBufferLength, ref UInt32 unEPCLength);

        /**
		 * 发送M100系列UHF 停止多次轮询指令
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfStopMulti", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfStopMulti(long nDeviceHandle);

        /**
		 * 发送M100系列UHF 获取发射功率
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param usPower 
		 *			  参数 0x07D0(当前功率为十进制2000，即20dBm)
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfGetPower", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfGetPower(long nDeviceHandle, ref UInt16 usPower);


        /**
		 * 发送M100系列UHF 设置发射功率
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param usPower 
		 *			  参数 0x07D0(当前功率为十进制2000，即20dBm)
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfSetPower", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfSetPower(long nDeviceHandle, UInt16 usPower);

        /**
		 * 发送M100系列UHF 设置工作地区
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucRegion 
		 *			  参数 中国900MHz 01 中国800MHz 04 美国02 欧洲03 韩国06 
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfSetRegion", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfSetRegion(long nDeviceHandle, byte ucRegion);

        /**
		 * 发送M100系列UHF 获取工作信道
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucRfChannel 
		 *			  参数 0x00(Channel_Index 为0x00)
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfGetRfChannel", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfGetRfChannel(long nDeviceHandle, [In, Out] byte[] ucRfChannel);

        /**
		 * 发送M100系列UHF 设置工作信道
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucRfChannel 
		 *			  参数 0x00(Channel_Index 为0x00)
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfSetRfChannel", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfSetRfChannel(long nDeviceHandle, byte ucRfChannel);

        /**
		 * 发送M100系列UHF 设置自动跳频
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucFHSS 
		 *			  参数 0xFF(0xFF 为设置自动跳频，0x00 为取消自动跳频)
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfSetFHSS", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfSetFHSS(long nDeviceHandle, byte ucFHSS);

        /**
		 * 发送M100系列UHF 获取Query 参数
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param usQueryParameter 
		 *			  参数 设置Query 命令中的相关参数。参数为2 字节，有下面的具体参数按位拼接而成：
		 *DR(1 bit): DR=8(1’b0), DR=64/3(1’b1). 只支持DR=8 的模式
		 *M(2 bit): M=1(2’b00), M=2(2’b01), M=4(2’b10), M=8(2’b11). 只支持M=1 的模式
		 *TRext(1 bit): No pilot tone(1’b0), Use pilot tone(1’b1). 只支持Use pilot tone(1’b1)模式
		 *Sel(2 bit): ALL(2’b00/2’b01), ~SL(2’b10), SL(2’b11)
		 *Session(2 bit): S0(2’b00), S1(2’b01), S2(2’b10), S3(2’b11)
		 *Target(1 bit): A(1’b0), B(1’b1)
		 *Q(4 bit): 4’b0000-4’b1111
		 *如果DR=8, M=1, TRext=Use pilot tone, Sel=00, Session=00, Target=A, Q=4，则指令如下：
		 *BB 00 0E 00 02 10 20 40 7E
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfGetQueryParamter", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfGetQueryParamter(long nDeviceHandle, ref UInt16 usQueryParameter);

        /**
		 * 发送M100系列UHF 设置Query 参数
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param usQueryParameter 
		 *			  参数 设置Query 命令中的相关参数。参数为2 字节，有下面的具体参数按位拼接而成：
		 *DR(1 bit): DR=8(1’b0), DR=64/3(1’b1). 只支持DR=8 的模式
		 *M(2 bit): M=1(2’b00), M=2(2’b01), M=4(2’b10), M=8(2’b11). 只支持M=1 的模式
		 *TRext(1 bit): No pilot tone(1’b0), Use pilot tone(1’b1). 只支持Use pilot tone(1’b1)模式
		 *Sel(2 bit): ALL(2’b00/2’b01), ~SL(2’b10), SL(2’b11)
		 *Session(2 bit): S0(2’b00), S1(2’b01), S2(2’b10), S3(2’b11)
		 *Target(1 bit): A(1’b0), B(1’b1)
		 *Q(4 bit): 4’b0000-4’b1111
		 *如果DR=8, M=1, TRext=Use pilot tone, Sel=00, Session=00, Target=A, Q=4，则指令如下：
		 *BB 00 0E 00 02 10 20 40 7E
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfSetQueryParamter", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfSetQueryParamter(long nDeviceHandle, UInt16 usQueryParameter);

        /**
		 * 发送M100系列UHF 设置发射连续载波
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucCW 
		 *			  参数 0xFF 为打开连续波，0x00 为关闭连续波
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfSetCW", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfSetCW(long nDeviceHandle, byte ucCW);

        /**
		 * 发送M100系列UHF 测试射频输入端阻塞信号
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucChannelLow 
		 *			  参数 信道最低数值
		 * @param ucChannelHigh 
		 *			  参数 信道最高数值
		 * @param parrJammerBuffer 
		 *			  参数 Jammer数组缓冲地址
		 * @param unJammerBufferLength 
		 *			  参数 Jammer数组缓冲长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfScanJammer", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfScanJammer(long nDeviceHandle, [In, Out] byte[] ucChannelLow, [In, Out] byte[] ucChannelHigh, [In, Out] byte[] parrJammerBuffer, ref UInt32 unJammerBufferLength);

        /**
		 * 发送M100系列UHF 测试信道RSSI
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucChannelLow 
		 *			  参数 信道最低数值
		 * @param ucChannelHigh 
		 *			  参数 信道最高数值
		 * @param parrRssiBuffer 
		 *			  参数 RSSI数组缓冲地址
		 * @param unRssiBufferLength 
		 *			  参数 RSSI数组缓冲长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfScanRssi", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfScanRssi(long nDeviceHandle, [In, Out] byte[] ucChannelLow, [In, Out] byte[] ucChannelHigh, [In, Out] byte[] parrRssiBuffer, ref UInt32 unRssiBufferLength);

        /**
		 * 发送M100系列UHF 获取接收解调器参数
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucMixerGain 
		 *			  参数 混频器增益
		 * @param ucIFGain 
		 *			  参数 中频放大器增益
		 * @param usThrd 
		 *			  参数 信号解调阈值
		 * @param unJammerBufferLength 
		 *			  参数 Jammer数组缓冲长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfReadModemParameter", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfReadModemParameter(long nDeviceHandle, [In, Out] byte[] ucMixerGain, [In, Out] byte[] ucIFGain, ref UInt16 usThrd);

        /**
		 * 发送M100系列UHF 设置接收解调器参数
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucMixerGain 
		 *			  参数 混频器增益
		 * @param ucIFGain 
		 *			  参数 中频放大器增益
		 * @param usThrd 
		 *			  参数 信号解调阈值
		 * @param unJammerBufferLength 
		 *			  参数 Jammer数组缓冲长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfSetModemParameter", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfSetModemParameter(long nDeviceHandle, byte ucMixerGain, byte ucIFGain, UInt16 usThrd);

        /**
		 * 发送M100系列UHF 设置Inventory模式
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucInventoryMode 
		 *			  参数 Inventory模式
		 *			  Select 模式Mode 含义：
		 *			  0x00: 在对标签的所有操作之前都预先发送Select 指令选取特定的标签。
		 *			  0x01: 在对标签操作之前不发送Select 指令。
		 *			  0x02: 仅对除轮询Inventory 之外的标签操作之前发送Select 指令，如在
		 *			  Read，Write，Lock，Kill 之前先通过Select 选取特定的标签。
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfSetInventoryMode", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfSetInventoryMode(long nDeviceHandle, byte ucInventoryMode);

        /**
		 * 发送M100系列UHF 设置选择参数
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucSelParam 
		 *			  参数 0x01 (Target: 3’b000, Action: 3’b000, MemBank: 2’b01)
		 * @param unPtr 
		 *			  参数 0x00000020(以bit 为单位，非word) 从EPC 存储位开始
		 * @param ucMaskLen 
		 *			  参数 0x60(6 个word，96bits)
		 * @param ucTruncate 
		 *			  参数 0x00(0x00 是Disable truncation，0x80 是Enable truncation)
		 * @param parrMask 
		 *			  参数 Mask缓冲地址
		 * @param unMaskLength 
		 *			  参数 Mask缓冲长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfSetSelectParameter", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfSetSelectParameter(long nDeviceHandle, byte ucSelParam, UInt32 unPtr, byte ucMaskLen, byte ucTruncate,
                                                                                                                                                                       [In, Out] byte[] parrMask, UInt32 unMaskLength);


        /**
		 * 发送M100系列UHF 读取数据
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param unAccessPassword 
		 *			  参数 访问密码
		 * @param ucMemBank 
		 *			  参数 标签数据存储区
		 * @param usSAdreess 
		 *			  参数 读标签数据区地址偏移
		 * @param usDataLength 
		 *			  参数 读标签数据区地址长度 WORD为单位
		 * @param parrDataBuffer 
		 *			  参数 数据缓冲地址
		 * @param unDataBufferLength 
		 *			  参数 数据缓冲长度 送入buffer长度 返回实际长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfReadData", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfReadData(long nDeviceHandle, UInt32 unAccessPassword, byte ucMemBank, UInt16 usSAdreess, UInt16 usDataLength,
            [In, Out] byte[] parrDataBuffer, ref UInt32 unDataBufferLength);

        /**
		 * 发送M100系列UHF 写标签数据存储区
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param unAccessPassword 
		 *			  参数 访问密码
		 * @param ucMemBank 
		 *			  参数 标签数据存储区
		 * @param usSAdreess 
		 *			  参数 读标签数据区地址偏移
		 * @param usDataLength 
		 *			  参数 读标签数据区地址长度 WORD为单位
		 * @param parrDataBuffer 
		 *			  参数 数据缓冲地址
		 * @param unDataBufferLength 
		 *			  参数 数据缓冲长度 送入buffer长度 返回实际长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfWriteData", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfWriteData(long nDeviceHandle, UInt32 unAccessPassword, byte ucMemBank, UInt16 usSAdreess, UInt16 usDataLength,
            [In, Out] byte[] parrDataBuffer, ref UInt32 unDataBufferLength);

        /**
		 * 发送M100系列UHF 锁定Lock 标签数据存储区
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param unAccessPassword 
		 *			  参数 访问密码
		 * @param parrLockLd 
		 *			  参数 LockLd数组缓冲地址
		 * @param unLockLdLength 
		 *			  参数 LockLd数组缓冲长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfLockUnLock", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfLockUnLock(long nDeviceHandle, UInt32 unAccessPassword, [In, Out] byte[] parrLockLd, UInt32 unLockLdLength);

        /**
		 * 发送M100系列UHF 灭活Kill 标签
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param unKillPassword 
		 *			  参数 KILL密码
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardM100UhfKill", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardM100UhfKill(long nDeviceHandle, UInt32 unKillPassword);

        /**
		 * 获取设备号
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param parrDeviceNo 
		 *			  参数 设备号缓冲地址
		 * @param unDeviceNoLength 
		 *			  参数 设备号缓冲长度 目前不小于8字节 以后可能有调整
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetDeviceNo", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetDeviceNo(long nDeviceHandle, [In, Out] byte[] pstrDeviceNo, UInt32 unDeviceNoLength);

        /**
		 * 获取二代身份证卡片ID
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pstrTwoGenerationID 
		 *			  参数 二代身份证ID缓冲地址
		 * @param unTwoGenerationIDLength 
		 *			  参数 二代身份证ID缓冲长度 
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoGenerationIDCardNo", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetTwoGenerationIDCardNo(long nDeviceHandle, [In, Out] byte[] pstrTwoGenerationID, UInt32 unTwoGenerationIDLength);


        /**
		 * 获取服务器IP PORT
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param punIpAddress 
		 *			  参数 IP地址
		 * @param pusPort 
		 *			  参数 端口
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetServerIpPort", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetServerIpPort(long nDeviceHandle, ref UInt32 punIpAddress, ref UInt16 pusPort);

        /**
		 * 根据编码获取性别字符串
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszSexCode 
		 *			  参数 性别编码 0 未知 2 女 1 男 9 未说明
		 * @param pszSexBuffer 
		 *			  结果 存放返回字符串
		 * @param nSexBufferLength 
		 *			  参数 buffer长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdSexByCode", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void LotusCardGetTwoIdSexByCode(long nDeviceHandle, ref char pszSexCode, ref char pszSexBuffer, int nSexBufferLength);
        /**
		 * 根据编码获取民族字符串
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszNationCode 
		 *			  参数 民族编码
		 * @param pszNationBuffer 
		 *			  结果 存放返回字符串
		 * @param nNationBufferLength 
		 *			  参数 buffer长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetTwoIdNationByCode", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void LotusCardGetTwoIdNationByCode(long nDeviceHandle, ref char pszNationCode, ref char pszNationBuffer, int nNationBufferLength);

        /**
		 * b64编码
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pSrcDataBuffer 
		 *			  参数 源数据缓冲
		 * @param nSrcDataLen 
		 *			  参数 源数据缓冲长度
		 * @param pDestDataBuffer 
		 *			  参数 目标Buffer
		 * @param nDestDataLen 
		 *			  参数 目标Buffer长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardBase64Encode", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void LotusCardBase64Encode(long nDeviceHandle, [In, Out] byte[] pSrcDataBuffer, int nSrcDataLen, [In, Out] byte[] pDestDataBuffer, int nDestDataLen);

        /**
		 * 获取b64编码后长度
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pSrcDataBuffer 
		 *			  参数 源数据缓冲
		 * @param nSrcDataLen 
		 *			  参数 源数据缓冲长度
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetEncodeNewLen", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetEncodeNewLen(long nDeviceHandle, ref char pSrcDataBuffer, int nSrcDataLen);

        /**
		 * b64解码
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pSrcDataBuffer 
		 *			  参数 源数据缓冲
		 * @param nSrcDataLen 
		 *			  参数 源数据缓冲长度
		 * @param pDestDataBuffer 
		 *			  参数 目标Buffer
		 * @param nDestDataLen 
		 *			  参数 目标Buffer长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardBase64Decode", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void LotusCardBase64Decode(long nDeviceHandle, [In, Out] byte[] pSrcDataBuffer, int nSrcDataLen, [In, Out] byte[] pDestDataBuffer, int nDestDataLen);

        /**
		 * 获取b64解码后长度
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pSrcDataBuffer 
		 *			  参数 源数据缓冲
		 * @param nSrcDataLen 
		 *			  参数 源数据缓冲长度
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetDecodeNewLen", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetDecodeNewLen(long nDeviceHandle, ref char pSrcDataBuffer, int nSrcDataLen);

        /**
		 * 获取二代证用随机数
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pszRandom 
		 *			  参数 随机数缓冲
		 * @param unRandomLength 
		 *			  参数 随机数缓冲长度
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetIdRandom", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void LotusCardGetIdRandom([In, Out] byte[] pszRandom, UInt32 unRandomLength);

        /**
		 * 保存日志
		 * 
		 * @param pszLogFile 
		 *			  参数 日志文件
		 * @param pszLog 
		 *			  参数 日志内容
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSaveLog", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern void LotusCardSaveLog(ref char pszLogFile, ref char pszLog);

        /**
		 * 获取USB设备数量
		 * 
		 * @param nVID 
		 *			  参数 设备VID
		 * @param nPID 
		 *			  参数 设备PID
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetUsbDeviceCount", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetUsbDeviceCount(int nVID, int nPID);


        /**
		 * 获取MCU配置
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pMcuConfig 
		 *			  参数 MCU配置数据地址  0=禁止 非0=允许
		 * @param unMcuConfigLegnth 
		 *			  参数 MCU配置数据长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetMcuConfig", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetMcuConfig(long nDeviceHandle, [In, Out] byte[] pMcuConfig, UInt32 unMcuConfigLegnth);

        /**
		 * 获取ISP选项
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pIspOption 
		 *			  参数 选项数据地址  0=禁止 非0=允许
		 * @param unIspOptionLegnth 
		 *			  参数 选型数据长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardGetIspOption", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardGetIspOption(long nDeviceHandle, [In, Out] byte[] pIspOption, UInt32 unIspOptionLegnth);

        /**
		 * 设置ISP选项
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pIspOption 
		 *			  参数 选项数据地址 0=禁止 非0=允许
		 * @param unIspOptionLegnth 
		 *			  参数 选型数据长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSetIspOption", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSetIspOption(long nDeviceHandle, [In, Out] byte[] pIspOption, UInt32 unIspOptionLegnth);



        /**
		* 设置射频开关
		* @param nDeviceHandle
		*            设备句柄
		* @param ucRfOnOff
		*            1=打开射频信号 0= 关闭射频信号
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSetRfOnOff", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSetRfOnOff(long nDeviceHandle, byte ucRfOnOff);

        /**
		 * 设置LED状态
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucLedStatus 
		 *			  参数 0=关闭 1=绿灯亮 2=红灯亮 3=两个灯都亮
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSetLedStatus", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSetLedStatus(long nDeviceHandle, byte ucLedStatus);

        /**
		 * 复位射频IC
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardResetRfIc", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardResetRfIc(long nDeviceHandle);



        /**
		 * 获取ntag版本 21x支持
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pNtagVersionBuffer 
		 *			  参数 版本信息缓冲地址
		 * @param unNtagVersionBuffeLegnth 
		 *			  参数 版本信息缓冲长度
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardNtagGetVersion", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardNtagGetVersion(long nDeviceHandle, [In, Out] byte[] pNtagVersionBuffer, uint unNtagVersionBuffeLegnth);

        /**
		 * ntag密码验证 21x支持
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param pNtagPasswordBuffer 
		 *			  参数 密码缓冲地址
		 * @param unNtagPasswordBuffeLegnth 
		 *			  参数 密码缓冲长度 默认4字节
		 * @return true = 成功
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardNtagPwdAuth", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardNtagPwdAuth(long nDeviceHandle, [In, Out] byte[] pNtagPasswordBuffer, uint unNtagPasswordBuffeLegnth);

        /**
		 * 设置7816插槽索引
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucIndex 
		 *			  参数 插槽索引0=大卡 1,2,3=小卡
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSetSamSlotIndex", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSetSamSlotIndex(long nDeviceHandle, byte ucIndex);
        /**
		 * 设置7816上下电 仅对大卡座有效
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param ucPowerOnOff 
		 *			  参数 0=下电操作 1=上电操作
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSetSamPowerOnOff", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSetSamPowerOnOff(long nDeviceHandle, byte ucPowerOnOff);

        /**
		 * 复位7816
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param tLotusCardParam
		 *            结果值 返回复位ATR信息
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardResetSam", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardResetSam(long nDeviceHandle, ref LotusCardParamStruct sttLotusCardParam);

        /**
		 * 发送SAM APDU
		 * 
		 * @param nDeviceHandle
		 *            设备句柄
		 * @param tLotusCardParam
		 *            结果值 返回APDU执行结果
		 */
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardSendSamAPDU", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardSendSamAPDU(long nDeviceHandle, ref LotusCardParamStruct sttLotusCardParam);

        /**
		* CPU卡根据名称选择文件或目录
		* @param nDeviceHandle
		*            设备句柄
		* @param pszDirOrFileName
		*           文件或目录名
		* @param pszResultBuffer
		*			返回结果BUFFER指针
		* @param unResultBufferLength
		*			返回结果BUFFER长度 256字节长
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardCpuCardSelectByName", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardCpuCardSelectByName(long nDeviceHandle, String szDirOrFileName, ref String szResultBuffer, ref int unResultBufferLength);


        /**
		* CPU卡读取二进制文件
		* @param nSfi
		*           文件标识
		* @param pszResultBuffer
		*			返回结果BUFFER指针
		* @param unResultBufferLength
		*			返回结果BUFFER长度 256字节长
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardCpuCardReadBinary", SetLastError = true,
             CharSet = CharSet.Auto, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardCpuCardReadBinary(long nDeviceHandle, int nSfi, String szResultBuffer, ref int unResultBufferLength);

        /**
		* 单DES
		* @param pszInData
		*           输入字符串
		* @param pszKey
		*			密钥
		* @param pszOutData
		*			输出字符串
		* @param unOutDataLength
		*			输出字符串长度
		* @param bDecrypt
		*			TRUE = 解密 FALSE =加密
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardDes", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardDes(String szInData, String szKey, [In, Out] byte[] pszOutData, uint unOutDataLength, int bDecrypt);

        /**
		* 3DES
		* @param pszInData
		*           输入字符串
		* @param pszKey
		*			密钥
		* @param pszOutData
		*			输出字符串
		* @param unOutDataLength
		*			输出字符串长度
		* @param bDecrypt
		*			TRUE = 解密 FALSE =加密
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCard3Des", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCard3Des(String szInData, String szKey, [In, Out] byte[] pszOutData, uint unOutDataLength, int bDecrypt);

        /**
		* MAC计算
		* @param pszInitString
		*			初始字符串 PBOC是随机数 普通MAC是8个0字符串
		* @param pszInData
		*           输入字符串
		* @param pszKey
		*			密钥
		* @param pszOutData
		*			输出字符串
		* @param unOutDataLength
		*			输出字符串长度
		* @return true = 成功
		*/
        [DllImport("LotusCardDriver.dll", EntryPoint = "LotusCardMac", SetLastError = true,
             CharSet = CharSet.Ansi, ExactSpelling = false,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int LotusCardMac(String szInitString, String szInData, String szKey, [In, Out] byte[] pszOutData, uint unOutDataLength);



        /************************************************以下代码是把动态库接口封装成一个库********************************************************************/
        private int m_nHandle;//句柄
        //private LotusCardParamStruct m_sttLotusCardParam;//参数
        public CLotusCardDriver()
        {
            //参数结构体初始化
            m_nHandle = -1;
            //m_sttLotusCardParam = new LotusCardParamStruct();
            //m_sttLotusCardParam.arrCardNo = new byte[4];
            //m_sttLotusCardParam.arrBuffer = new byte[64];
            //m_sttLotusCardParam.arrKeys = new byte[64];
        }
        ~CLotusCardDriver()
        {
            //关闭设备
            CloseDevice(m_nHandle);
        }
        /**
         * 打开设备
         * 
         * @param strDeviceName
         *            串口设备名称
         * @param nVID
         *            USB设备VID
         * @param nPID
         *            USB设备PID
         * @param bUseExendReadWrite
         *            是否使用外部读写通道 如果没有设备写权限时，可以使用外部USB或串口进行通讯，
         *            需要改造callBackProcess中相关代码完成读写工作 目前范例提供USB操作
         * @return 设备句柄
         */
        public long OpenDevice(string pszDeviceName, int nVID, int nPID, int nUsbDeviceIndex, OnLotusCardExtendReadWriteCallBackFunc CallBackFunc)
        {
            m_nHandle = LotusCardOpenDevice(pszDeviceName, nVID, nPID, nUsbDeviceIndex, 0, CallBackFunc);
            return m_nHandle;
        }


        /**
         * 关闭设备
         * 
         * @param nDeviceHandle
         *            设备句柄
         */
        public void CloseDevice(int nDeviceHandle)
        {
            if (-1 != nDeviceHandle)
            {
                LotusCardCloseDevice(nDeviceHandle);
            }
        }

        /**
         * 寻卡
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nRequestType
         *            请求类型
         * @param tLotusCardParam
         *            结果值 用里面的卡片类型
         * @return true = 成功
         */
        public static int Request( int  nDeviceHandle,  int nRequestType,
               ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardRequest(nDeviceHandle, nRequestType, ref sttLotusCardParam);
        }

        /**
         * 防冲突
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param tLotusCardParam
         *            结果值 用里面的卡号
         * @return true = 成功
         */
        public int Anticoll(int nDeviceHandle,
                ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardAnticoll(nDeviceHandle, ref sttLotusCardParam);
        }

        /**
         * 选卡
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param tLotusCardParam
         *            参数(使用里面的卡号)与结果值(使用里面的卡容量大小)
         * @return true = 成功
         */
        public int Select(long nDeviceHandle,
                ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardSelect(nDeviceHandle, ref sttLotusCardParam);
        }

        /**
         * 密钥验证
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nAuthMode
         *            验证模式
         * @param nSectionIndex
         *            扇区索引
         * @param tLotusCardParam
         *            参数(使用里面的卡号)
         * @return true = 成功
         */
        public int Authentication(long nDeviceHandle, int nAuthMode,
                int nSectionIndex, ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardAuthentication(nDeviceHandle, nAuthMode, nSectionIndex, ref sttLotusCardParam);
        }

        /**
         * 卡片中止响应
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @return true = 成功
         */
        public int Halt(long nDeviceHandle)
        {
            return LotusCardHalt(nDeviceHandle);
        }

        /**
         * 读指定地址数据
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress
         *            块地址
         * @param tLotusCardParam
         *            结果值（读写缓冲）
         * @return true = 成功
         */
        public int Read(long nDeviceHandle, int nAddress,
                ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardRead(nDeviceHandle, nAddress, ref sttLotusCardParam);
        }

        /**
         * 写指定地址数据
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress
         *            块地址
         * @param tLotusCardParam
         *            参数（读写缓冲）
         * @return true = 成功
         */
        public int Write(long nDeviceHandle, int nAddress,
                ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardWrite(nDeviceHandle, nAddress, ref sttLotusCardParam);
        }

        /**
         * 加值
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress
         *            块地址
         * @param nValue
         *            值
         * @return true = 成功
         */
        public int Increment(long nDeviceHandle, int nAddress, int nValue)
        {
            return LotusCardIncrement(nDeviceHandle, nAddress, nValue);
        }

        /**
         * 减值
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress
         *            块地址
         * @param nValue
         *            值
         * @return true = 成功
         */
        public int Decrement(long nDeviceHandle, int nAddress, int nValue)
        {
            return LotusCardDecrement(nDeviceHandle, nAddress, nValue);
        }

        /**
         * 装载密钥
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nAuthMode
         *            验证模式
         * @param nSectionIndex
         *            扇区索引
         * @param tLotusCardParam
         *            参数（密钥）
         * @return true = 成功
         */
        public int LoadKey(long nDeviceHandle, int nAuthMode,
                int nSectionIndex, ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardLoadKey(nDeviceHandle, nAuthMode, nSectionIndex, ref sttLotusCardParam);
        }

        /**
         * 蜂鸣
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nBeepLen
         *            蜂鸣长度 毫秒为单位
         * @return true = 成功
         */
        public int Beep(int nDeviceHandle, int nBeepLen)
        {
            return LotusCardBeep(nDeviceHandle, nBeepLen);
        }

        /**
         * 发送指令 用于CPU卡
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nTimeOut
         *            超时参数
         * @param tLotusCardParam
         *            参数（指令缓冲,返回结果）
         * @return true = 成功
         */
        public int SendCpuCommand(long nDeviceHandle, int nTimeOut,
                ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardSendCpuCommand(nDeviceHandle, nTimeOut, ref sttLotusCardParam);
        }

        /******************************** 以下函数调用上述函数，为了简化第三方调用操作 ***************************/
        /**
         * 获取卡号
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nRequestType
         *            请求类型
         * @param tLotusCardParam
         *            结果值
         * @return true = 成功
         */
        public int GetCardNo(long nDeviceHandle, int nRequestType,
                ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardGetCardNo(nDeviceHandle, nRequestType, ref sttLotusCardParam);
        }

        /**
         * 初始值
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param nAddress
         *            块地址
         * @param nValue
         *            值
         * @return true = 成功
         */
        public int InitValue(long nDeviceHandle, int nAddress, int nValue)
        {
            return LotusCardInitValue(nDeviceHandle, nAddress, nValue);
        }

        /**
         * 修改密码AB
         * 
         * @param nDeviceHandle
         *            设备句柄
         * @param pPasswordA
         *            密码A
         * @param pPasswordB
         *            密码B
         * @return true = 成功
         */
        public int ChangePassword(long nDeviceHandle, int nSectionIndex,
                String strPasswordA, String strPasswordB)
        {
            return LotusCardChangePassword(nDeviceHandle, nSectionIndex, strPasswordA, strPasswordB);
        }


        /**
         * 复位CPU卡
         * @param nDeviceHandle
         *            设备句柄
         * @param tLotusCardParam
         *            结果值 
         * @return true = 成功
         */
        public int ResetCpuCard(int nDeviceHandle, ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardResetCpuCard(nDeviceHandle, ref sttLotusCardParam);
        }

        /**
         * 发送指令 用于CPU卡 封装LotusCardSendCpuCommand
         * @param nDeviceHandle
         *            设备句柄
         * @param tLotusCardParam 参数（指令缓冲,返回结果）
         * @return true = 成功
         */
        public int SendCOSCommand(int nDeviceHandle, ref LotusCardParamStruct sttLotusCardParam)
        {
            return LotusCardSendCOSCommand(nDeviceHandle, ref sttLotusCardParam);
        }


    }
}