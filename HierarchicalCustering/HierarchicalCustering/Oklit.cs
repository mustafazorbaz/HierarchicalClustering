using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HierarchicalCustering
{
    class Oklit : Baginti
    {

        public override void islem(string baginti, Data egitim, Data test)
        {
            if (baginti == "Oklit")
            { 
                double hesapla = 0;
                for (int sut = 0; sut < egitim.values.Count; sut++)//Veri Sınıfındaki List ti dolduguruyor.
                {
                    hesapla += Math.Pow(egitim.values[sut] - test.values[sut], 2); 
                }
                egitim.setDistance(Convert.ToDouble(Math.Sqrt(hesapla)));
            }
            else
            {
                if (_sonrakiBaginti != null)
                {
                    _sonrakiBaginti.islem(baginti, egitim, test);
                }
            }
        }
    }
}
