using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoConf
{
    public class VideoStatus
    {
        private string mycode = "";
        private string mymsg = "";
        /// <summary>
        /// 对于videoOff code = 0/-1；  msg=success / local confpath error or Invalid parameter
        /// 对于videoOn code = 0/-1/1; msg=success / local confpath error or Invalid parameter / over maxCount
        /// </summary>
        public string code { get { return mycode; }set { mycode = value; } }
        public string msg { get { return mymsg; }set { mymsg = value; } }

    }
}
