using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HierarchicalCustering
{
    class Manhattan : Baginti
    {
        public override void islem(string baginti, Data egitim, Data test)
        {
            if (baginti == "Manhattan")
            {

                double hesapla = 0;
                for (int sut = 0; sut < egitim.values.Count; sut++)//Veri Sınıfındaki List ti dolduguruyor.
                {
                    hesapla += Math.Abs(egitim.values[sut] - test.values[sut]);

                }
                egitim.setDistance(Math.Round(Convert.ToDouble(Math.Sqrt(hesapla)), 5));
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
