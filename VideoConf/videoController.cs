using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace VideoConf.Controllers
{
    public class videoController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage videoOff(string numbers)// 提供关闭摄像头，清除本地配置文件的接口
        {
            VideoStatus video_Infos = new VideoStatus();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string json = string.Empty;
            string[] num_List = numbers.Split(',');

            // 传递数据参数合法性判断，是否有非英文","字符
            foreach (string item in num_List)
            {
                if (!int.TryParse(item, out _)) //解析成功是数字则返回true，否则返回false
                {
                    video_Infos.code = "-1";
                    video_Infos.msg = "Error, Invalid parameter";
                    json = serializer.Serialize(video_Infos);
                    return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
                }
            }

            // 参数合法的时候进行删除
            string status = Add_Del_Json.instance().DeleteJson(numbers) ? "0" : "-1";// 双目表达式
            if (status == "0")
            {
                video_Infos.code = "0";
                video_Infos.msg = "Success";
                json = serializer.Serialize(video_Infos);
                return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
            }
            else
            {
                video_Infos.code = "-1";
                video_Infos.msg = "Local configuration file path error";
                json = serializer.Serialize(video_Infos);
                return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
            }

        }//end_videoOff

        [HttpPost]
        public HttpResponseMessage videoOn(string numbers)// 提供关闭摄像头，清除本地配置文件的接口
        {
            VideoStatus video_Infos = new VideoStatus();
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string json = string.Empty;
            string[] num_List = numbers.Split(',');

            // 传递数据参数合法性判断，是否有非英文","字符
            foreach (string item in num_List)
            {
                if (!int.TryParse(item, out _)) //解析成功是数字则返回true，否则返回false
                {
                    video_Infos.code = "-1";
                    video_Infos.msg = "Error, Invalid parameter";
                    json = serializer.Serialize(video_Infos);
                    return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
                }
            }

            // 参数合法的时候进行添加
            string status = Add_Del_Json.instance().AddJson(numbers);
            switch (status)
            {
                case "0":
                    {
                        video_Infos.code = "0";
                        video_Infos.msg = "Success";
                    }
                    break;
                case "1":
                    {
                        video_Infos.code = "1";
                        video_Infos.msg = "More than maxCount, Reject Post request";
                    }
                    break;
                case "-1":
                    {
                        video_Infos.code = "-1";
                        video_Infos.msg = "Local configuration file path error";
                    }
                    break;
                case "-2":
                    {
                        video_Infos.code = "-2";
                        video_Infos.msg = "Remote config file is null or has errors";
                    }
                    break;
                default:
                    break;
            }
            json = serializer.Serialize(video_Infos);
            return new HttpResponseMessage { Content = new StringContent(json, Encoding.GetEncoding("UTF-8"), "application/json") };
        }//end_videoOn

    } //end_class

}
