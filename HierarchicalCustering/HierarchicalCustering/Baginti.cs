using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HierarchicalCustering
{
    public abstract class Baginti
    {
        protected Baginti _sonrakiBaginti;
        public Baginti sonrakiBaginti
        {
            set { _sonrakiBaginti = value; }
        }
        public abstract void islem(string baginti, Data egitim, Data test);
 
    }
}
