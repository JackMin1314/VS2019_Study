namespace NFCService
{
    public class NFC_Info
    {

        private string mycode = "";
        private string mymsg = "";

        // 定义嵌套json里面的data
        private string mydata;

        public string code { get { return mycode; } set { mycode = value; } }
        public string msg { get { return mymsg; } set { mymsg = value; } }
        public string data { get { return mydata; } set { mydata = value; } }

        //public static NFC_Info getinstance() { return new NFC_Info(); }
    }

}
