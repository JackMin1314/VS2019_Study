using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFC_ID
{
    public class Readnfc_data : CLotusCardDriver
    {
        // 十六进制字符串每两位转十进制放到arrCosSendBuffer数组里;//COS指令发送缓冲
        public void mycopyCOSCommand(string cosString, ref LotusCardParamStruct lotusCardParam)
        {
            cosString = cosString.Trim();if (cosString.Length == 0) return;
            //if ((cosString.Length %2) != 0)
            //    cosString += " ";

            //lotusCardParam.arrCosSendBuffer = new byte[cosString.Length / 2];
            int i = 0;
            decimal index;
            for (i=0; i <(cosString.Length)/2; i++)
            {
                // 十六进制字符串转十进制 int32
                index = Convert.ToInt32("0x"+cosString.Substring(i * 2, 2).Trim('\0'),16);// 这里不用.Parse的 原因是Parse解析的是0~9字符；16指明16进制
                lotusCardParam.arrCosSendBuffer[i] = Convert.ToByte(index);
            }
            lotusCardParam.unCosSendBufferLength = i;
            //MessageBox.Show(result_string.ToString(),"存储的指令为:");
        }
        // 重载函数有参
        public string readNFC_data(Boolean sound)
        {
            bool innersound = true;
            innersound =  sound;
            // 读取设备 // 读取设备信息
            OnLotusCardExtendReadWriteCallBackFunc pt = null;
            int nHandle = LotusCardOpenDevice("", 0, 0, 0, 0, pt);
            if (nHandle == -1)
            { //MessageBox.Show("驱动读取失败！");
                return "-1";
            }
            // Request寻卡
            int nRequestType = CLotusCardDriver.RT_NOT_HALT;
            LotusCardParamStruct lotusCardParam = new LotusCardParamStruct();// 结构体
            lotusCardParam.nBufferSize = 0;
            lotusCardParam.arrCardNo = new byte[8];
            lotusCardParam.arrBuffer = new byte[64];
            lotusCardParam.arrKeys = new byte[64];
            //lotusCardParam.arrCosResultBuffer = new byte[256];
            lotusCardParam.arrCosSendBuffer = new byte[256];

            int bResult = CLotusCardDriver.LotusCardRequest(nHandle, nRequestType, ref lotusCardParam);
            if (bResult == 0)
            { //MessageBox.Show("寻卡失败");
                return "-2";
            }

            // Anticoll读卡
            bResult = Anticoll(nHandle, ref lotusCardParam);
            if (bResult == 0)
            {
                return "-3";// 读卡失败
            }
            // 读卡成功蜂鸣一下
            if (innersound) bResult = LotusCardBeep(nHandle, 10);
            
            // 重置卡
            int reset = LotusCardResetCpuCard(nHandle, ref lotusCardParam);
            int sendCOSCommand = 2;
            string cosString = string.Empty;
            if (reset == 1)
            {
                // 有严格的顺序
                // 指令一
                cosString = "00A4040007D276000085010100";
                mycopyCOSCommand(cosString, ref lotusCardParam);
                sendCOSCommand = LotusCardSendCOSCommand(nHandle, ref lotusCardParam);// 成功执行指令返回int 1
                // 指令有误返回“-5”
                if (sendCOSCommand != 1) { return "-5"; }

                // 指令二
                cosString = "00A4000C02E103";
                mycopyCOSCommand(cosString, ref lotusCardParam);
                sendCOSCommand = LotusCardSendCOSCommand(nHandle, ref lotusCardParam);// 成功执行指令返回int 1
                if (sendCOSCommand != 1) { return "-5"; }

                // 指令三
                cosString = "00A4000C020001";
                mycopyCOSCommand(cosString, ref lotusCardParam);
                sendCOSCommand = LotusCardSendCOSCommand(nHandle, ref lotusCardParam);// 成功执行指令返回int 1
                if (sendCOSCommand != 1) { return "-5"; }

                // 指令四
                cosString = " 00B0000008";
                mycopyCOSCommand(cosString, ref lotusCardParam);
                sendCOSCommand = LotusCardSendCOSCommand(nHandle, ref lotusCardParam);// 成功执行指令返回int 1
                if (sendCOSCommand != 1) { return "-5"; }

            }
            else
            { return "-4"; }// 重置卡失败
            // 关闭设备
            LotusCardCloseDevice(nHandle);

            string result = "";
            for (int i = 0; i < lotusCardParam.unCosReultBufferLength; i++)
            {
                result += lotusCardParam.arrCosResultBuffer[i].ToString("X2");
            }

            result = result.Replace("9000", "");
            string new_result = string.Empty;
            for (int i = result.Length - 2; i >= 0; i -= 2)
            {
                new_result += result.Substring(i, 2);
            }
            return new_result;

        }//getNFC_id()
        public static Readnfc_data get_instance() { return new Readnfc_data(); }
    }
}
