using System;
using System.Collections.Generic;
using System.Linq;
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
        private Video_Info_Data mydata;
        public Video_Info() { data = new Video_Info_Data(); }
        // 定义嵌套json里面的data
        public string code { get { return mycode; } set { mycode = value; } }
        public string msg { get { return mymsg; } set { mymsg = value; } }
        public Video_Info_Data data { get { return mydata; } set { mydata = value; } }
        
    }
}
