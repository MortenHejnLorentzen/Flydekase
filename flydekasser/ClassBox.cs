using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flydekasser
{
    class ClassBox
    {
        private string _strHight = "";
        public string strHight { get { return _strHight; } set { _strHight = value; } }
        private string _strwidth = "";
        public string strwidth { get { return _strwidth; } set { _strwidth = value; } }
        private string _strDepth = "";
        public string strDepth { get { return _strDepth; } set { _strDepth = value; } }
        public ClassBox()
        {

        }
        public ClassBox(decimal H, decimal W, decimal D)
        {
            {
                this.strHight = H.ToString();
                this.strwidth = W.ToString();
                this.strDepth = D.ToString();
            }
        }
    }
}
