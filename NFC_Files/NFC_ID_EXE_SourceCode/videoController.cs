using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;



namespace NFC_ID.Controllers
{
    /// <summary>
    /// 添加视频配置读取接口
    /// </summary>
   
    public class videoController : ApiController
    {
        int flag = 0;
        [HttpPost]
        public HttpResponseMessage findVideoConf(string ids)
        {
            string json = string.Empty;

            // 根据ids，产生一个json，里面data为列表json
            Video_Info video_Infos = new Video_Info();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string[] ids_split = ids.Split(',');
           
           
            foreach (string ids_index in ids_split)
            {
                Video_Info_Data video_Info_data = new Video_Info_Data();
                switch (ids_index)
                {
                    case "1":{
                            video_Infos.code = "113";
                            video_Infos.msg = "查询成功!";flag = 1;
                            video_Info_data.id = "1";
                            video_Info_data.username = "admin";
                            video_Info_data.password = "adm5201020";
                            video_Info_data.rtspUrl = "rtsp://admin:adm12345@192.168.5.4:554/Streaming/Channels/101";
                            video_Info_data.isOpen = "1";break;
                        }
                    case "2": {
                            video_Infos.code = "113";
                            video_Infos.msg = "查询成功!";flag = 1;
                            video_Info_data.id = "2";
                            video_Info_data.username = "admin";
                            video_Info_data.password = "adm5201222";
                            video_Info_data.rtspUrl = "rtsp://admin:adm12345@192.168.5.4:554/Streaming/Channels/102";
                            video_Info_data.isOpen = "1"; break;
                        }
                    default:{
                            
                            video_Info_data = null;
                            break;
                        }
                } // end_switch
                if (flag == 1)
                {
                    video_Infos.data.Add(video_Info_data);
                }
                if (flag==0)
                {
                    video_Infos.code = "114";
                    video_Infos.msg = "无结果";
                    video_Info_data = null;
                    video_Infos.data.Add(video_Info_data);
                }
          
            }
            // 序列化一个list可以用于返回 多个json
           
            json = serializer.Serialize(video_Infos);
            return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
        }
    }
}
