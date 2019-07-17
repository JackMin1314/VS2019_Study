using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    /// <summary>
    /// 定义Data类,为了后面定义data嵌套json格式
    /// 放在前面为了避免后面定义找不到
    /// </summary>
    public class Data
    {
        private string mylng;
        private string mylat;
        public string lng
        {
            get { return mylng; }
            set { mylng = value; }
        }
        public string lat
        {
            get { return mylat; }
            set { mylat = value; }
        }

    }
    /// <summary>
    /// Json字符串生成C#实体类
    /// </summary>
    public class Response
    {
        private string mystatus;
        private string mymsg;
        private Data mydata;
        public string status
        {
            get { return mystatus; }
            set { mystatus = value; }
        }
        public string msg
        {
            get { return mymsg; }
            set { mymsg = value; }
        }
        /// <summary>
        /// 这里定义在Response类里面,类可以嵌套
        /// 嵌套的实体类.见http://www.jsons.cn/jsontomodel/在线转换
        /// </summary>
        public Data data
        {
            get { return mydata; }
            set { mydata = value; }
        }

    }
}
