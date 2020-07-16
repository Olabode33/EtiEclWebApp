using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemo.EclShared.Temp
{
    public static class StaticEclGenerator
    {
        public static double RandomImpairment(double? exposure, double? impairment, int? stage)
        {
            if (exposure == null || exposure == 0)
            {
                Random r = new Random();
                int range = 201385;
                return  r.NextDouble() * range; //for doubles
            }

            if (impairment <= 1000)
            {
                switch (stage)
                {
                    case 1:
                        return (exposure == null ? 4201385 : (double)exposure) * 0.0515;
                    case 2:
                        return (exposure == null ? 4201385 : (double)exposure)  * 0.15;
                    case 3:
                        return (exposure == null ? 4201385 : (double)exposure)  * 0.35;
                    default:
                        return 201385;
                }
            }
            else
            {
                return impairment?? 201385;
            }
        }
    }
}
