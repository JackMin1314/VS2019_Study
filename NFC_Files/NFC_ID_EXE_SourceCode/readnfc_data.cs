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
        public string readNFC_data()
        {

            // 读取设备 // 读取设备信息
            OnLotusCardExtendReadWriteCallBackFunc pt = null;
            long nHandle = LotusCardOpenDevice("", 0, 0, 0, 0, pt);
            if (nHandle == -1) { //MessageBox.Show("驱动读取失败！");
                return "驱动读取失败！"; }

            // Request寻卡
            int nRequestType = CLotusCardDriver.RT_NOT_HALT;
            LotusCardParamStruct lotusCardParam = new LotusCardParamStruct();// 结构体
            lotusCardParam.arrCardNo = new byte[8];
            lotusCardParam.arrBuffer = new byte[64];
            lotusCardParam.arrKeys = new byte[64];
            lotusCardParam.arrCosResultBuffer = new byte[256];
            int bResult = CLotusCardDriver.LotusCardRequest(nHandle, nRequestType, ref lotusCardParam);
            if (bResult == 0) { //MessageBox.Show("寻卡失败");
                return "寻卡失败"; }

            // Anticoll读卡
            bResult = Anticoll(nHandle, ref lotusCardParam);
            if (bResult == 0) { //MessageBox.Show("读卡失败");
                return "读卡失败"; }
            // 读卡成功蜂鸣一下
            bResult = LotusCardBeep(nHandle, 10);
            string nfc_id = string.Empty;

            //for (int i = 0; i < lotusCardParam.arrCardNo.Length - 1; i++)
            //{
            //    nfc_id += Convert.ToString(lotusCardParam.arrCardNo[i], 16).PadLeft(2, '0').ToUpper();// 解决左边输出编号为0,丢失0问题
            //}
            // 读取data信息
            int rd = Read(nHandle, 8, ref lotusCardParam);
            uint geterror = LotusCardGetErrorCode(nHandle);
           // MessageBox.Show(Convert.ToString(geterror), "geterror");
            //if (rd == 0)
            //{
            //    MessageBox.Show("ok");
            //    for (int i = 0; i < lotusCardParam.arrBuffer.Length-1; i++)
            //    {
            //        nfc_id += Convert.ToString(lotusCardParam.arrBuffer[i], 16).PadLeft(2, '*').ToUpper();// 解决左边输出编号为0,丢失0问题
            //    }
            //}

            // 关闭设备
            LotusCardCloseDevice(nHandle);
            return nfc_id;
        }//getNFC_id()
        public static Readnfc_data get_instance() { return new Readnfc_data(); }
    }
}
