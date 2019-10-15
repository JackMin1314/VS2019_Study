using System;
namespace NFC_ID
{
    public class getnfc_uid : CLotusCardDriver
    {
        //CLotusCardDriver CLotusCardDriver = new CLotusCardDriver();        
        /// <summary>
        /// getNFC_id() 尝试识别nfc成功返回string::uid，失败返回对应状态信息
        /// </summary>
        getnfc_uid() { }
        ~getnfc_uid() { }
        public string getNFC_id(Boolean sound)
        {

            // 读取设备 // 读取设备信息
            OnLotusCardExtendReadWriteCallBackFunc pt = null;
            long nHandle = LotusCardOpenDevice("", 0, 0, 0, 0, pt);
            if (nHandle == -1) { //MessageBox.Show("驱动读取失败！");
                return Convert.ToString(-1);// "驱动读取失败！";
            }

            // Request寻卡
            int nRequestType = CLotusCardDriver.RT_NOT_HALT;
            LotusCardParamStruct lotusCardParam = new LotusCardParamStruct();// 结构体
            lotusCardParam.arrCardNo = new byte[8];
            lotusCardParam.arrBuffer = new byte[64];
            lotusCardParam.arrKeys = new byte[64];
            lotusCardParam.arrCosResultBuffer = new byte[256];
            int bResult;
            bResult = CLotusCardDriver.LotusCardRequest(nHandle, nRequestType, ref lotusCardParam);
            if (bResult == 0) { //MessageBox.Show("寻卡失败");
                return Convert.ToString(-2);//"寻卡失败";
            }

            // Anticoll读卡
            bResult = Anticoll(nHandle, ref lotusCardParam);
            if (bResult == 0) { //MessageBox.Show("读卡失败");
                return Convert.ToString(-3); //"读卡失败";
            }
            // 读卡成功蜂鸣一下
            if (sound) { bResult = LotusCardBeep(nHandle, 10); }
            string nfc_id = string.Empty;
            //foreach (var i in lotusCardParam.arrCardNo)
            //{
            //    nfc_id +=Convert.ToString(i,16);// 可能会多取
            //}
            for (int i = 0; i < lotusCardParam.arrCardNo.Length - 1; i++)
            {
                nfc_id += Convert.ToString(lotusCardParam.arrCardNo[i], 16).PadLeft(2, '0').ToUpper();// 解决左边输出编号为0,丢失0问题
            }
            // 关闭设备
            LotusCardCloseDevice(nHandle);
            return nfc_id;
        }//getNFC_id()

        // 函数重载无参getNFC_id
        public string getNFC_id()
        {

            // 读取设备 // 读取设备信息
            OnLotusCardExtendReadWriteCallBackFunc pt = null;
            long nHandle = LotusCardOpenDevice("", 0, 0, 0, 0, pt);
            if (nHandle == -1)
            { //MessageBox.Show("驱动读取失败！");
                return Convert.ToString(-1);//"驱动读取失败！";
            }

            // Request寻卡
            int nRequestType = CLotusCardDriver.RT_NOT_HALT;
            LotusCardParamStruct lotusCardParam = new LotusCardParamStruct();// 结构体
            lotusCardParam.arrCardNo = new byte[8];
            lotusCardParam.arrBuffer = new byte[64];
            lotusCardParam.arrKeys = new byte[64];
            lotusCardParam.arrCosResultBuffer = new byte[256];
            int bResult;
            bResult = CLotusCardDriver.LotusCardRequest(nHandle, nRequestType, ref lotusCardParam);
            if (bResult == 0)
            { //MessageBox.Show("寻卡失败");
                return Convert.ToString(-2); //"寻卡失败";
            }

            // Anticoll读卡
            bResult = Anticoll(nHandle, ref lotusCardParam);
            if (bResult == 0)
            { //MessageBox.Show("读卡失败");
                return Convert.ToString(-3); //"读卡失败";
            }
            // 读卡成功蜂鸣一下
            bResult = LotusCardBeep(nHandle, 10); 
            string nfc_id = string.Empty;
            //foreach (var i in lotusCardParam.arrCardNo)
            //{
            //    nfc_id +=Convert.ToString(i,16);// 可能会多取
            //}
            
            // 获取 nfc_id卡号信息
            for (int i = 0; i < lotusCardParam.arrCardNo.Length - 1; i++)
            {
                nfc_id += Convert.ToString(lotusCardParam.arrCardNo[i], 16).PadLeft(2, '0').ToUpper();// 解决左边输出编号为0,丢失0问题
            }
            // 关闭设备
            LotusCardCloseDevice(nHandle);
            return nfc_id;
        }//getNFC_id()
        public static getnfc_uid get_instance() { return new getnfc_uid(); }
    }

}
