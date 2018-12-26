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
                       int conditionN = -1;
            int m = 5;
            int r = 0;
            int[] checkN = new int[6];
            int N = 0;
            int p=0;
            int l = 16;
            bool firstFlag = true;
            bool secondFlag = true;
            while (secondFlag)
            {
                firstFlag = true;
                while (firstFlag)
                {
                    p = FindP(l);
                    int[] x = alg781(p);
                    int c = x[0];
                    int d = x[1];
                    checkN[0] = p + 1 + (c + 3 * d);
                    checkN[1] = p + 1 + (c - 3 * d);
                    checkN[2] = p + 1 + (2 * c);
                    checkN[3] = p + 1 - (c + 3 * d);
                    checkN[4] = p + 1 + (c - 3 * d);
                    checkN[5] = p + 1 + (2 * c);
                    for (int j = 0; j < 6; j++)
                    {
                        if (IsPrimeNumber(checkN[j]))
                        {
                            conditionN = 0;
                            N = checkN[j];
                            r = checkN[j];
                            firstFlag = false;
                            break;
                        }
                        else if (checkN[j] % 2 == 0 && IsPrimeNumber(checkN[j] / 2))
                        {
                            conditionN = 1;
                            N = checkN[j];
                            r = checkN[j] / 2;
                            firstFlag = false;
                            break;
                        }
                        else if (checkN[j] % 3 == 0 && IsPrimeNumber(checkN[j] / 3))
                        {
                            conditionN = 2;
                            N = checkN[j];
                            r = checkN[j] / 3;
                            firstFlag = false;
                            break;
                        }
                        else if (checkN[j] % 6 == 0 && IsPrimeNumber(checkN[j] / 6))
                        {
                            conditionN = 3;
                            N = checkN[j];
                            r = checkN[j] / 6;
                            firstFlag = false;
                            break;
                        }
                    }
                }

                if (p == r)
                {
                    continue;
                }
                for (int i = 1; i <= m; i++)
                {
                    if ((long)Math.Pow(p, i) % r == 1)
                        continue;
                }
                secondFlag = false;

            }
            bool thirdFlag = true;
            int x0 = 0;
            int y0 = 0;
            Random random = new Random();
            int B = 0;
            bool fourthFlag = true;
            while (fourthFlag)
            {
                thirdFlag = true;
                while (thirdFlag)
                {
                    x0 = random.Next(int.MinValue, int.MaxValue);
                    y0 = random.Next(int.MinValue, int.MaxValue);
                    B = Math.Abs(y0 * y0 - x0 * x0 * x0) % p;
                    switch (conditionN)
                    {
                        case 0:
                            if ((int)Math.Pow(B, (p - 1) / 3) % p != 1 && (int)Math.Pow(B, (p - 1) / 2) % p != 1)
                            {
                                thirdFlag = false;
                                continue;
                            }
                            break;
                        case 1:
                            if ((int)Math.Pow(B, (p - 1) / 3) % p == 1 && (int)Math.Pow(B, (p - 1) / 2) % p != 1)
                            {
                                thirdFlag = false;
                                continue;
                            }
                            break;
                        case 2:
                            if ((int)Math.Pow(B, (p - 1) / 3) % p != 1 && (int)Math.Pow(B, (p - 1) / 2) % p == 1)
                            {
                                thirdFlag = false;
                                continue;
                            }
                            break;
                        case 3:
                            if ((int)Math.Pow(B, (p - 1) / 3) % p == 1 && (int)Math.Pow(B, (p - 1) / 2) % p == 1)
                            {
                                thirdFlag = false;
                                continue;
                            }
                            break;
                    }
                }

            }

        }
        
         public static int FindP(int length)
        {
            int p = 0;
            bool pFound = false;
            Random rand = new Random();

            // 2^(l-1) < p < 2^l
            while (!pFound)
            {
                p = rand.Next((int)Math.Pow(2, length - 1), (int)Math.Pow(2, length));

                if (IsPrimeNumber(p) && (p % 6 == 1))
                {
                    pFound = true;
                }
            }

            return p;
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
