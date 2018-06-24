using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace getEER
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write(getEER(50, 10, 10, 50));
            Console.Read();
        }

        /// <summary>
        ///                                                水箱Q1
        /// 求压缩机能效比EER=----------------------------
        ///                                             压缩机耗电量
        ///                                             
        /// 水箱Q1=Q水+Q冰
        /// Q水=c(m总-m冰)(t现在-t初始)
        /// Q冰=Q水(变成冰这部分)+Q水变冰+Q过冷水
        ///  = cm冰(t现在-t初始)+km冰+c1m冰(t现在-t初始)
        /// </summary>
        /// <param name="mIce">冰质量</param>
        /// <param name="cpresW">压缩机耗电量</param>
        /// <param name="tankTemp">当前温度</param>
        /// <param name="interval">时间间隔</param>
        /// <returns></returns>
        static public double getEER(double mIce, double cpresW, double tankTempInterval, int interval)
        {
            //设置一些常量
            const double cWater=1;
            const double cIce=1;
            const double kIce=1;
            const double mSum=1;
            
            double Q1 = 0;
            //如果压缩机用电为0，分母为零
            if (cpresW==0.0)
            {
                return 0.0;
            }
            else
            {
                Q1 = cWater * (mSum - mIce) + cIce * mIce * tankTempInterval + kIce * mIce + cIce * mIce * tankTempInterval;
                Q1 = Math.Abs(Q1);
                return Q1 / cpresW;
            }
            
        }
    }
}
