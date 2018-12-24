using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllipticCurveGenerationJ0
{
    class Program
    {
        static void Main(string[] args)
        {
            // Elliptic curve generation with j=0

            int length = 16; // l
            int securityParameter = 5; // m table 11.5

            // p = 1mod6
            int p = 64033;
            // p = c**2 + 3d**2
            int c = 239;
            int d = 48;

            Console.WriteLine(LegendreSymbol(3,3));
            // output Elliptic curve E(Fp), point Q simple order r
        }


        public static bool IsPrimeNumber(long number)
        {
            for(int i = 2; i < Math.Sqrt(number) + 1; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }


        public static int LegendreSymbol(int a, int p) // return -1 0 1
        {
            int result;
            if(a % p == 0)
            {
                result = 0;
            }
            else if (a == 1)
            {
                result = 1;
            }
            else if (a % 2 == 0)
            {
                result = LegendreSymbol(a / 2, p) * (int)Math.Pow(-1, (p*p-1)/8); 
            }
            else
            {
                result = LegendreSymbol(p % a, a) * (int)Math.Pow(-1, (a - 1) * (p - 1) / 4);
            }

            return result;
        }
    }
}
