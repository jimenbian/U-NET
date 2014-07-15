using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PicView
{
    class Valulea
    {
    
        
        public double Compute(int a1,int a2, int a3 )
        {
            double a5;
            a5 = (1.11 * (Math.Log10(a1)) - 0.7) * a3 - (1.56 * (Math.Log10(a1)) - 0.8);
            return a5;
        
        }
    
    
    
    }
}
