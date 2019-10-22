using System;
using System.Net.Http;
using System.Web.Http;
using NFC_ID;
using System.Windows.Forms;
using System.Web.Http.Cors;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;

// 注意命名空间
namespace NFC_info.Controllers
{
    // 定义枚举类型，消息
    //public enum msg_return : Int32
    //{
    //    驱动读取失败 = -1,
    //    寻卡失败 = -2,
    //    读卡失败 = -3,
    //    NFC编号读取失败 = -4,
    //}

    // url路由接口，webapi会根据Controller匹配NFCController，在找到对应得action。
    // 例如http://localhost:port/api/NFC/readNfcNo
   //[System.Web.Http.Cors.EnableCors(origins: "127.0.0.1:9090", headers: "*", methods: "*")]
    public class cardController : ApiController
    {
        // 有参数判断是否响声音
        [HttpPost]
        public HttpResponseMessage readNFCCard(Boolean sound)// 读取的是芯片的卡号
        {
            Boolean innersound = sound;
            // code
            string result_code = getnfc_uid.get_instance().getNFC_id(innersound);
            if (result_code == null) { result_code = Convert.ToString(-4); } //"NFC编号读取失败"; 
            // msg
            string msg = string.Empty;
            string data = "\"\"";
            // 根据返回值0， -1，-2，-3，-4，匹配枚举类型对应消息
            int i = 0;
            for (i=0;i<4;i++) {
                if (result_code == "-1") { msg = "驱动读取失败"; data = result_code;break; }
                if (result_code == "-2") { msg = "寻卡失败"; data = result_code; break; }
                if (result_code == "-3") { msg = "读卡失败"; data = result_code; break; }
                if (result_code == "-4") { msg = "NFC编号读取失败"; data = result_code; break; }

            }
            // 如果-1Equals到-4没匹配到，则默认为成功,0
            if (i == 4)
            {
                msg = "成功"; data = result_code; result_code = "0";
            }
            string json = string.Empty;
            //json += ("Uid:" + result_code + "\t时间:" + DateTime.Now + "\n");
            NFC_Info nfc_details = new NFC_Info();
            if (result_code != "0")
            {
                nfc_details.code = result_code;
                nfc_details.msg = msg;
               }
            else
            {
                nfc_details.code = result_code;
                nfc_details.msg = msg;
                nfc_details.data = data;
               }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            json = serializer.Serialize(nfc_details);
            return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
        }

        [HttpPost]
        // 这里设置无参默认有声音
        public HttpResponseMessage readNFCCard()
        {
            Boolean innersound = true;
            // code
            string result_code = getnfc_uid.get_instance().getNFC_id(innersound);

            if (result_code == null) { result_code = Convert.ToString(-4); } //"NFC编号读取失败"; 
            // msg
            string msg = string.Empty;
            string data = "\"\"";
            // 根据返回值0， -1，-2，-3，-4，匹配枚举类型对应消息
            int i = 0;
            for (i = 0; i < 4; i++)
            {
                if (result_code == "-1") { msg = "驱动读取失败"; data = result_code; break; }
                if (result_code == "-2") { msg = "寻卡失败"; data = result_code; break; }
                if (result_code == "-3") { msg = "读卡失败"; data = result_code; break; }
                if (result_code == "-4") { msg = "NFC编号读取失败"; data = result_code; break; }

            }
            // 如果-1Equals到-4没匹配到，则默认为成功,0
            if (i == 4)
            {
                msg = "成功"; data = result_code; result_code = "0";
            }
            string json = string.Empty;
            //json += ("Uid:" + result_code + "\t时间:" + DateTime.Now + "\n");
            NFC_Info nfc_details = new NFC_Info();
            if (result_code != "0")
            {

                nfc_details.code = result_code;
                nfc_details.msg = msg;
             }
            else
            {
                nfc_details.code = result_code;
                nfc_details.msg = msg;
                nfc_details.data = data;
            }
           
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            json = serializer.Serialize(nfc_details);
            return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
        }

        [HttpPost]
        public HttpResponseMessage readNFCData(Boolean sound) // 读取的是写入的数据
        {
            Boolean innersound = sound;
            // code
            string result_code = Readnfc_data.get_instance().readNFC_data(sound);
            string msg = string.Empty;
            string data = "\"\"";
            // 根据返回值0， -1，-2，-3，-4，匹配枚举类型对应消息
            int i = 0;
            for (i = 0; i < 5; i++)
            {
                if (result_code == "-1") { msg = "驱动读取失败"; data = result_code; break; }
                if (result_code == "-2") { msg = "寻卡失败"; data = result_code; break; }
                if (result_code == "-3") { msg = "读卡失败"; data = result_code; break; }
                if (result_code == "-4") { msg = "重置卡失败"; data = result_code; break; }
                if (result_code == "-5") { msg = "指令执行失败"; data = result_code; break; }
            }
            // 如果-1Equals到-4没匹配到，则默认为成功,0
            if (i == 5)
            {
                msg = "成功"; data = result_code; result_code = "0";
            }
            string json = string.Empty;
          
            NFC_Info nfc_details = new NFC_Info();
            if (result_code != "0")
            {
                nfc_details.code = result_code;
                nfc_details.msg = msg;
            }
            else
            {
                nfc_details.code = result_code;
                nfc_details.msg = msg;
                nfc_details.data = data;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            json = serializer.Serialize(nfc_details);
            return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
        }// End_readNFCData(bool sound)

        [HttpPost]
        public HttpResponseMessage readNFCData() // 读取的是写入的数据
        {
            Boolean innersound = true;

            // code
            string result_code = Readnfc_data.get_instance().readNFC_data(innersound);

            //if (result_code == null) { result_code = Convert.ToString(-4); } ; 
            // msg
            string msg = string.Empty;
            string data = "\"\"";
            // 根据返回值0， -1，-2，-3，-4，匹配枚举类型对应消息
            int i = 0;
            for (i = 0; i < 5; i++)
            {
                if (result_code == "-1") { msg = "驱动读取失败"; data = result_code; break; }
                if (result_code == "-2") { msg = "寻卡失败"; data = result_code; break; }
                if (result_code == "-3") { msg = "读卡失败"; data = result_code; break; }
                if (result_code == "-4") { msg = "重置卡失败"; data = result_code; break; }
                if (result_code == "-5") { msg = "指令执行失败"; data = result_code; break; }
            }
            // 如果-1Equals到-4没匹配到，则默认为成功,0
            if (i == 5)
            {
                msg = "成功"; data = result_code; result_code = "0";
            }
            string json = string.Empty;

            NFC_Info nfc_details = new NFC_Info();
            if (result_code != "0")
            {
                nfc_details.code = result_code;
                nfc_details.msg = msg;
            }
            else
            {
                nfc_details.code = result_code;
                nfc_details.msg = msg;
                nfc_details.data = data;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            json = serializer.Serialize(nfc_details);
            return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
        }// End_readNFCData()
    }//End_Class
}
