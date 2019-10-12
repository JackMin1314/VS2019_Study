using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows.Forms;

namespace VideoConf
{
    // 这里单独建立一个类是为了方便webapi接口使用和代码复用性，来源于测试时Form1的DeleteJson() 和 AddJson()函数
    public class Add_Del_Json:Form1
    {
        /// <summary>
        /// 给定逗号连接的编号，匹配删除,更新配置和全局currentCount成功返回true，失败返回false
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public new bool DeleteJson(string numbers)// 前面加new表示隐藏继承的基类DeleteJson()函数
        {
            string result = get_local_json();
            if (result == "-1") return false;
            JObject jo = JObject.Parse(result);
            string[] numList = numbers.Split(',');
            bool b = true;
            currentCount = count_local_json();// 获取当前配置文件个数,-1表示配置文件路径有误
            if (currentCount == -1) return false;
            foreach (var indexNum in numList)
            {
                // 本地配置文件有内容才可以删除
                if (jo["source"]["src"].HasValues)
                {
                    foreach (var item in jo["source"]["src"])
                    {
                        
                        if (item["strToken"].ToString() == indexNum)
                        {
                            // 移除jo["source"]["src"]中匹配的元素
                            item.Remove();
                            b = true;
                            break;//加break真的是好习惯！！否则再次循环jo["source"]["src"]内部已经修改了会有异常
                        } // end_if
                    }
                }

            }//end_foreach
           
            // 删除后直接修改本地配置文件
            try
            {
                File.WriteAllText(confPath, jo.ToString());
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            currentCount = count_local_json();
            return b;
        }

        /// <summary>
        /// 给定的编号，添加到配置文件里
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns>
        /// '0'--成功添加; '1'--超过最大配置数目;  '-1'--抛出异常
        /// </returns>
        public new string AddJson(string numbers)
        {
            string result = get_local_json();
            if (result == "-1") return "-1";
            JObject jo = JObject.Parse(result);
            string[] numList = numbers.Split(',');
            int numList_len = numList.Length;

            string postresult = PostConf(numbers);
            string statusParse = getjson(postresult); // statusParse 为-1（非json）,1（有data）或0（无data）
            if (statusParse == "-1"|| statusParse == "0")
            {
                return "-2";//为了方便前端展示返回信息，添加返回值-2表示平台返回的配置信息为空或者配置信息有误
            }

            currentCount = count_local_json();// 获取当前配置文件个数,-1表示配置文件路径有误
            if (currentCount == -1) return "-1";
            // 需要判断 maxCount 和 currentCount + numList.length大小,严格大于
            if (currentCount + numList_len > maxCount)
            {
                return "1";//表示当前配置数和返回配置数超过所设定的最大配置数目
            }
            foreach (var indexNum in numList)
            {
                foreach (var dic in result_dic)// result_dic保存全局平台返回的配置信息
                {
                    if (dic.Key.ToString() == indexNum)// 字典里存有请求编号的信息
                    {
                        JObject temp = JObject.Parse(rootJson);
                        temp["strToken"] = dic.Value["id"].ToString();
                        temp["strUrl"] = dic.Value["rtspUrl"].ToString();
                        temp["strUser"] = dic.Value["username"].ToString();
                        temp["strPasswd"] = dic.Value["password"].ToString();

                        if (jo["source"]["src"].HasValues)
                        {
                            int flag = 0;
                            foreach (var guess in jo["source"]["src"])
                            {
                                if (guess["strToken"].ToString()== temp["strToken"].ToString())// 当本地已经有配置信息时，替换主要参数
                                {
                                    guess["strToken"] = dic.Value["id"].ToString();
                                    guess["strUrl"] = dic.Value["rtspUrl"].ToString();
                                    guess["strUser"] = dic.Value["username"].ToString();
                                    guess["strPasswd"] = dic.Value["password"].ToString();
                                    flag = 1;
                                    break;
                                }
                            }
                            // 本地没有返回的配置信息，但是有其他配置信息
                           if(flag==0) jo["source"]["src"][0].AddBeforeSelf(temp);
                        }
                        else// 没有内容时执行
                        {
                            // jo[][]此时虽然配置是列表结构Arrary，但是编辑器认为是查询jo[][]中的元素是JToken，因而要显示转换成JArrary，才可以！！！
                            ((JArray)jo["source"]["src"]).Add(temp);// 解决本地配置文件为空的时候,直接添加
                        }
                    }
                }

            }//end_foreach
            // 删除后直接修改本地配置文件
            try
            {
                File.WriteAllText(confPath, jo.ToString());
            }
            catch (Exception)
            {
                return "-1";
                throw;
            }
            currentCount = count_local_json();
            return "0";
        }

        public static Add_Del_Json instance() { return new Add_Del_Json(); }
    }
}
