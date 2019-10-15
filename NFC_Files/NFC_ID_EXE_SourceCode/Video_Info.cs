using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NFC_ID
{

    public class Video_Info_Data
    {
        private string myid = "";
        private string myusername = "";
        private string mypassword = "";
        private string myrtspUrl = "";
        private string myisOpen = "";

        public string id { get { return myid; } set { myid = value; } }
        public string username { get { return myusername; } set { myusername = value; } }
        public string password { get { return mypassword; } set { mypassword = value; } }
        public string rtspUrl { get { return myrtspUrl; } set { myrtspUrl = value; } }
        public string isOpen { get { return myisOpen; } set { myisOpen = value; } }
    }
    public class Video_Info
    {
        private string mycode = "";
        private string mymsg = "";
        private List<Video_Info_Data> mydata;
        // 构造函数为嵌套类创建空间

       
        public Video_Info() { data = new List<Video_Info_Data>(); }
        // 定义嵌套json里面的data
        public string code { get { return mycode; } set { mycode = value; } }
        public string msg { get { return mymsg; } set { mymsg = value; } }
        // 新修改，产生一个json，里面data为列表json
       
        public List<Video_Info_Data> data { get { return mydata; } set { mydata = value; } }
        
    }
}
