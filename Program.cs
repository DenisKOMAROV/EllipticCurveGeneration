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

            Console.WriteLine(L(3,3));
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


        public static int L(int a, int p) // return -1 0 1
        {
            int result;
            if (a < 0)
            {
                result = L(-a,p) * Convert.ToInt32(Math.Pow((-1), ((p- 1) / 2)));
            }else if(a % p == 0)
            {
                result = 0;
            }
            else if (a == 1)
            {
                result = 1;
            }
            else if (a % 2 == 0)
            {
                result = L(a / 2, p) * (int)Math.Pow(-1, (p*p-1)/8); 
            }
            else
            {
                result = L(p % a, a) * (int)Math.Pow(-1, (a - 1) * (p - 1) / 4);
            }

            return result;
        }
        
        public static int firstAlg(int q, int a) //поиск корня в Zp
        {
            int x = Convert.ToInt32(Math.Pow(a, ((q + 1) / 4))) % q;
            if ((x * x) % q == a)
            {
                return x;
            }
            else return 0;  // нет решений
        }
        
        
        public static int secondAlg(int q, int a)   //поиск корня в Zp
        {
            int x = 0;

            int b = (int)(Math.Pow(a, (q + 3) / 8)) % q;
            long c = Convert.ToInt64(Math.Pow(a, (q - 1) / 4));
            c = (long)(Math.Pow(a, (q - 1) / 4)) % q;
            if (c != 1 && c != q - 1)
            {
                x = 0;
            }
            else if (c == 1)
            {
                x = b;
            }
            else
            {
                int i = Convert.ToInt32(Math.Pow(2, (q - 1) / 4)) % q;
                x = (b * i) % q;
            }
            return x;
        }
        
        
        public static int thirdAlg(int q, int a)  // поиск корня в Zp
        {
            for(int i = 1; i < q; i++)
            {
                if((i*i)%q == a)
                {
                    return i;
                }
            }
            return 0;
        }

        public static int calculateA(int u, int a, int b)  // расчет а в алгоритме 7.8.1
        {
            if (((u * a + 3 * b) % (a * a + 3 * b * b)) == 0)
                return (u * a + 3 * b) / (a * a + 3 * b * b);
            else
                return (-u * a + 3 * b) / (a * a + 3 * b * b);
        }

        public static int calculateB(int u, int a, int b)   // расчет b в алгоритме 7.8.1
        {
            if (((u*b - a) % (a * a + 3 * b * b)) == 0)
                return (u * b - a) / (a * a + 3 * b * b);
            else
                return (-u * b - a) / (a * a + 3 * b * b);
        }



        public static int[] alg781 (int p)
        {
            int[] u = new int[15];
            int[] m = new int[15];
            int uStart = thirdAlg(p, p-3);
            int[] x = new int[2];
            if (L(-2,p) == -1)
            {
                x[0] = -1;
                x[1] = -1;
                return x;
            } else
            {
                u[0] = uStart;
                m[0] = p;
                int i = 1;
                for (; i<p; i++)
                {
                    m[i] = ((u[i - 1] * u[i - 1]) + 3) / m[i - 1];
                    u[i] = Math.Min((u[i - 1] % m[i]), (m[i] - (u[i - 1] % m[i])));
                    if (m[i] == 1)
                        break;
                }
                i--;
                int[] a = new int[i+1];
                int[] b = new int[i+1];
                a[i] = u[i];
                b[i] = 1;
                for (; i > 0; i--)
                {
                    a[i - 1] = calculateA(u[i - 1], a[i], b[i]);
                    b[i - 1] = calculateB(u[i - 1], a[i], b[i]);
                }
                x[0] = Math.Abs(a[0]);
                x[1] = Math.Abs(b[0]);
            }
            return x;
        }
    }
}
