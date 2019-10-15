using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFC_ID
{
    // 如果后期需要添加nfc其他的返回信息，在下面添加字段
    //public class Data
    //{
    //    private string myuid ="";
    //    public string uid { get { return myuid; } set { myuid = value; } }

    //}
    // 定义nfc查询结果信息，用于返回json格式
    public class NFC_Info
    {

        private string mycode="";
        private string mymsg="";
        
        // 定义嵌套json里面的data
        private string mydata;

        public string code { get { return mycode; } set { mycode = value; } }
        public string msg { get { return mymsg; } set { mymsg = value; } }
        public string data { get { return mydata; } set { mydata = value; } }

        //public static NFC_Info getinstance() { return new NFC_Info(); }
    }

   
}
