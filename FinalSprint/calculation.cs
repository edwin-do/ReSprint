using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalSprint
{
    class Calculation
    {
        public Calculation() { }

        public double CalcResistance(double voltage, double current)
        {
            return voltage / current;
        }

        public double CalcResistivity(double resistance, double area, double length)
        {
            return (resistance * area) / length;
        }

        public double CalcTemperature(double t_volt, double j_temp, int th_type, double temperature)
        {
            if (th_type == 1)  // type K
            {
                if (-200 <= temperature && temperature < 0)
                {
                    return j_temp + 0 + (2.5173462e-2) * t_volt + (-1.1662878e-6) * Math.Pow(t_volt, 2) + (-1.0833638e-9) * Math.Pow(t_volt, 3) + (-8.9773540e-13) * Math.Pow(t_volt, 4) + (-3.7342377e-16) * Math.Pow(t_volt, 5) + (-8.6632643e-20) * Math.Pow(t_volt, 6) + (-1.0450598e-23) * Math.Pow(t_volt, 7) + (-5.1920577e-28) * Math.Pow(t_volt, 8);
                }

                else if (0 <= temperature && temperature < 500)
                {
                    return j_temp + 0 + (2.508355e-2) * t_volt + (7.860106e-8) * Math.Pow(t_volt, 2) + (-2.503131e-10) * Math.Pow(t_volt, 3) + (8.315270e-14) * Math.Pow(t_volt, 4) + (-1.228034e-17) * Math.Pow(t_volt, 5) + (9.804036e-22) * Math.Pow(t_volt, 6) + (-4.413030e-26) * Math.Pow(t_volt, 7) + (1.057734e-30) * Math.Pow(t_volt, 8) + (-1.052755e-35) * Math.Pow(t_volt, 9);
                }

                else if (500 <= temperature && temperature <= 1372)
                {
                    return j_temp - 1.318058e2 + (4.830222e-2) * t_volt + (-1.646031e-6) * Math.Pow(t_volt, 2) + (5.464731e-11) * Math.Pow(t_volt, 3) + (-9.650715e-16) * Math.Pow(t_volt, 4) + (8.802193e-21) * Math.Pow(t_volt, 5) + (-3.110810e-26) * Math.Pow(t_volt, 6);
                }
                else return 99999;
            }

            else if (th_type == 2)  // type R
            {
                if (-50 <= temperature && temperature < 250)
                {
                    return j_temp + 0 + (1.8891380e-1) * t_volt + (1.3068619e-7) * Math.Pow(t_volt, 3) + (-2.2703580e-10) * Math.Pow(t_volt, 4) + (3.5145659e-13) * Math.Pow(t_volt, 5) + (-8.6632643e-20) * Math.Pow(t_volt, 6) + (-1.0450598e-23) * Math.Pow(t_volt, 7) + (-5.1920577e-28) * Math.Pow(t_volt, 8);
                }

                else if (250 <= temperature && temperature < 1132)
                {
                    return j_temp + 1.334584505e1 + (1.472644573e-1) * t_volt + (4.031129726e-9) * Math.Pow(t_volt, 3) + (-6.249428360e-13) * Math.Pow(t_volt, 4) + (6.468412046e-17) * Math.Pow(t_volt, 5) + (9.804036e-22) * Math.Pow(t_volt, 6) + (-4.413030e-26) * Math.Pow(t_volt, 7) + (1.057734e-30) * Math.Pow(t_volt, 8) + (-1.052755e-35) * Math.Pow(t_volt, 9);
                }

                else if (1132 <= temperature && temperature < 1664.5)
                {
                    return j_temp - 8.199599416e1 + (1.553962042e-1) * t_volt + (4.279433549e-10) * Math.Pow(t_volt, 3) + (-1.191577910e-14) * Math.Pow(t_volt, 4) + (1.492290091e-19) * Math.Pow(t_volt, 5);
                }

                else if (1664.5 <= temperature && temperature <= 1768.1)
                {
                    return j_temp + 3.406177836e4 + (-7.023729171) * t_volt + (-5.582903813e-4) * Math.Pow(t_volt, 3) + (-1.952394635e-8) * Math.Pow(t_volt, 4) + (2.560740231e-13) * Math.Pow(t_volt, 5);
                }
                else return 99999;
            }

            else
            {
                return 99999;
            }
        }

        public double CalcSlope(double resistivity, double temperature)
        {
            return resistivity / temperature;
        }

        public bool CalcChange(double temperature, double slope)
        {
            double d_Slope1 = slope + 0.1 * slope;
            double d_Slope2 = slope - 0.1 * slope;
            double d_Temp1 = temperature + 100;
            double d_Temp2 = temperature - 100;

            if ((temperature == d_Temp1 && slope == d_Slope1) || (temperature == d_Temp2 && slope == d_Slope2))
                return true;
            else return false;
        }

        public string CalcAperture(double rate)
        {
            double aper_num = 1 / rate;  // 1000/rate
            //Debug.WriteLine(aper_num.ToString("F5"));

            Debug.WriteLine(aper_num.ToString("G5"));
            return aper_num.ToString("G5");   // F5
        }
    }
}