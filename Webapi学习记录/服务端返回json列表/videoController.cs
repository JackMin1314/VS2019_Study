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
        [HttpPost]
        public HttpResponseMessage findVideoConf(string ids)
        {
            string json = string.Empty;
            // 根据ids，产生多个json对象
            List<Video_Info> video_details = new List<Video_Info>();
            
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string[] ids_split = ids.Split(',');
            foreach (string ids_index in ids_split)
            {
                Video_Info video_Info = new Video_Info();
                switch (ids_index)
                {
                    case "1":{
                            video_Info.code = "113";
                            video_Info.msg = "查询成功!";
                            video_Info.data.id = "1";
                            video_Info.data.username = "admin";
                            video_Info.data.password = "adm5201020";
                            video_Info.data.rtspUrl = "rtsp://admin:adm12345@192.168.5.4:554/Streaming/Channels/101";
                            video_Info.data.isOpen = "1";break;
                        }
                    case "2": {
                            video_Info.code = "113";
                            video_Info.msg = "查询成功!";
                            video_Info.data.id = "2";
                            video_Info.data.username = "admin";
                            video_Info.data.password = "adm5201020";
                            video_Info.data.rtspUrl = "rtsp://admin:adm12345@192.168.5.4:554/Streaming/Channels/102";
                            video_Info.data.isOpen = "1";break;
                        }
                    default:{
                            video_Info.code = "114";
                            video_Info.msg = "无结果";
                            video_Info.data = null;
                            break;
                        }
                } // switch
                video_details.Add(video_Info);
            }
            // 序列化一个list可以用于返回 多个json
            json = serializer.Serialize(video_details);
            return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
        }
    }
}
