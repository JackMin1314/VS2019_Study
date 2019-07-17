using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Test
{
    public class PositionInfo
    {
        private string myNumber;
        private string myLogintime;
        private string myToken;
        private FileStream mySound;
        private string mykey;
        private string m_TaiHao;
        public string number
        {
            get { return myNumber; }
            set { myNumber = value; }
        }
        public string logintime
        {
            get { return myLogintime; }
            set { myLogintime = value; }
        }
        public string Token
        {
            get { return myToken; }
            set { myToken = value; }
        }
        public FileStream Sound
        {
            get { return mySound; }
            set { mySound = value; }
        }
        public string securitykey
        {
            get { return mykey; }
            set { mykey = value; }
        }
        //
        public string TaiHao
        {
            get { return m_TaiHao; }
            set { this.m_TaiHao = value; }
        }

    }
}
