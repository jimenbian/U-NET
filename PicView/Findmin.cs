using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicView
{
    class Findmin
    {
        public int findmin(int a1, int a2, int a3)
        {
            if (a2<a1)
            {
                if (a2 < a3) { return a2; }
                else { return a3; }
            }
            else
            {
                if (a1 < a3) { return a1; }
                else return a3;
            }
        }
    }
}
