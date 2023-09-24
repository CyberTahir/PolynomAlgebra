using System;
using System.Linq;

namespace PolynomAlgebra
{
    class Polynom : ICloneable
    {
        /// <summary>
        /// Array of polynom coefficients.
        /// </summary>
        private readonly double[] _Coefficients;

        /// <summary>
        /// Power of the current polynom.
        /// </summary>
        private int _Power;

        /// <summary>
        /// Gets power of the polynom.
        /// </summary>
        public int Power => _Power;

        /// <summary>
        /// Constructs a new polynom with given coefficients' array.
        /// </summary>
        /// <param name="coefficients">Array of coefficients.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public Polynom(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException("coefficients");
            }

            var length = coefficients.Length;
            if (length == 0)
            {
                throw new ArgumentException("coefficients", "Длина массива = 0");
            }

            var power = length - 1;
            while (power >= 0)
            {
                if (coefficients[power] != 0)
                {
                    break;
                }
                power--;
            }

            _Power = power;
            _Coefficients = new double[power + 1];
            Array.Copy(coefficients, _Coefficients, power + 1);
        }


        // Вычисление значения
        /// <summary>
        /// Gets a value of polynom in x.
        /// </summary>
        /// <param name="x">Argument of polynom.</param>
        /// <returns></returns>
        public double GetValueGorner(double x)
        {
            var value = 0.0;

            for (var i = _Coefficients.Length - 1; i >= 0; i--)
            {
                value *= x;
                value += _Coefficients[i];
            }

            return value;
        }
        public double GetValuePow1(double x)
        {
            var value = 0.0;
            var q = 1.0;

            for (var i = 0; i < _Coefficients.Length; i++)
            {
                value += _Coefficients[i] * q;
                q *= x;
            }

            return value;
        }
        public double GetValuePow(double x)
        {
            var value = 0.0;

            for (var i = 0; i < _Coefficients.Length; i++)
            {
                value += _Coefficients[i] * Math.Pow(x, i);
            }

            return value;
        }


        // IClonable
        public object Clone()
        {
            return new Polynom(_Coefficients.Clone() as double[]);
        }


        // Операторы равенства и неравенства
        static public bool operator == (Polynom p1, Polynom p2)
        {
            if (p1.Power != p2.Power)
            {
                return false;
            }

            for (int i = 0; i <= p1.Power; i++)
            {
                if (p1._Coefficients[i] != p2._Coefficients[i])
                {
                    return false;
                }
            }

            return true;
        }
        static public bool operator == (Polynom p, double x)
        {
            return p.Power == 0 && p._Coefficients[0] == x;
        }
        static public bool operator == (double x, Polynom p)
        {
            return p == x;
        }
        static public bool operator != (Polynom p, double x)
        {
            return p.Power != 0 || p._Coefficients[0] != x;
        }
        static public bool operator != (double x, Polynom p)
        {
            return p != x;
        }
        static public bool operator != (Polynom p1, Polynom p2)
        {
            return !(p1 == p2);
        }


        // Сложение
        static public Polynom operator + (Polynom p, double x)
        {
            var len = p.Power + 1;
            double[] coefficients = new double[len];
            Array.Copy(p._Coefficients, coefficients, len);

            coefficients[0] += x;

            return new Polynom(coefficients);
        }
        static public Polynom operator + (double x, Polynom p)
        {
            return p + x;
        }
        static public Polynom operator + (Polynom p1, Polynom p2)
        {
            double[] c1, c2;
            if (p1.Power > p2.Power)
            {
                c1 = p1._Coefficients;
                c2 = p2._Coefficients;
            }
            else
            {
                c1 = p2._Coefficients;
                c2 = p1._Coefficients;
            }

            int maxLen = c1.Length;
            int minLen = c2.Length;
            var coefficients = new double[maxLen];

            for (var i = 0; i < minLen; i++)
            {
                coefficients[i] = c1[i] + c2[i];
            }

            for (var i = minLen; i < maxLen; ++i)
            {
                coefficients[i] = c1[i];
            }

            return new Polynom(coefficients);
        }
        static public Polynom Add(Polynom p1, Polynom p2)
        {
            var c1 = p1._Coefficients;
            var c2 = p2._Coefficients;

            if (p1.Power < p2.Power)
            {
                c1 = p2._Coefficients;
                c2 = p1._Coefficients;
            }

            var coefficients = new double[c1.Length];
            Array.Copy(c1, coefficients, c1.Length);

            for (int i = 0; i < c2.Length; i++)
            {
                coefficients[i] += c2[i];
            }

            return new Polynom(coefficients);
        }


        // Вычитание
        static public Polynom operator - (Polynom p1, Polynom p2)
        {
            return p1 + ((-1) * p2);
        }
        static public Polynom operator - (Polynom p, double x)
        {
            var coefficients = p._Coefficients.Clone() as double[];
            coefficients[0] -= x;

            return new Polynom(coefficients);
        }


        // Умножение
        static public Polynom operator * (Polynom p, double k)
        {
            var coefficients = p._Coefficients.Select(alpha => k * alpha);

            return new Polynom(coefficients.ToArray());
        }
        static public Polynom operator * (double k, Polynom p)
        {
            var coefficients = p._Coefficients.Select(alpha => k * alpha);

            return new Polynom(coefficients.ToArray());
        }
        static public Polynom operator *(Polynom p1, Polynom p2)
        {
            int len1 = p1.Power + 1;
            int len2 = p2.Power + 1;

            double[] coefficients = new double[len1 + len2 - 1];

            for (int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    coefficients[i + j] += p1._Coefficients[i] * p2._Coefficients[j];
                }
            }

            return new Polynom(coefficients);
        }


        // Возведение в степень
        static public Polynom Pow(Polynom p, int n)
        {
            if (n == 0)
            {
                return new Polynom(1);
            }

            if (n == 1)
            {
                return new Polynom(p._Coefficients);
            }

            if (p.Power == 0)
            {
                return new Polynom(Math.Pow(p._Coefficients[0], n));
            }

            var coefficients = new double[p.Power * n + 1];
            var q = p._Coefficients;
            var curPower = p.Power;
            var qPower = q.Length - 1;

            Array.Copy(q, coefficients, q.Length);

            while (n > 1)
            {
                for (int k = curPower + qPower; k >= 0; k--)
                {
                    double coef = 0.0;
                    for (int i = Math.Min(k, qPower); i >= 0 && k <= i + curPower; i--)
                    {
                        coef += q[i] * coefficients[k - i];
                    }

                    coefficients[k] = coef;
                }

                curPower += qPower;
                n--;
            }

            return new Polynom(coefficients);
        }
        public static Polynom operator ^ (Polynom p, int n)
        {
            return Polynom.Pow(p, n);
        }


        // Деление (взятие целого, частного и того и другого)
        static public Polynom[] DivFull(Polynom p, Polynom q)
        {
            if (p.Power < q.Power)
            {
                return new Polynom[2] { new Polynom(0), p.Clone() as Polynom };
            }

            var coefficients = new double[p.Power - q.Power + 1];
            var pCoef = p._Coefficients.Clone() as double[];
            var qCoef = q._Coefficients;

            for (int k = p.Power - q.Power, i = p.Power; k >= 0; k--, i--)
            {
                var value = pCoef[i] / qCoef[q.Power];
                for (int j = 0; j <= q.Power; j++)
                {
                    pCoef[k + j] -= value * qCoef[j];
                }

                coefficients[k] = value;
            }

            return new Polynom[2] { new Polynom(coefficients), new Polynom(pCoef) };
        }
        static public Polynom Div(Polynom p, Polynom q)
        {
            if (p.Power < q.Power)
            {
                return new Polynom(0);
            }

            var coefficients = new double[p.Power - q.Power + 1];
            var pCoef = p._Coefficients.Clone() as double[];
            var qCoef = q._Coefficients;

            for (int k = p.Power - q.Power, i = p.Power; k >= 0; k--, i--)
            {
                var value = pCoef[i] / qCoef[q.Power];
                for (int j = 0; j <= q.Power; j++)
                {
                    pCoef[k + j] -= value * qCoef[j];
                }

                coefficients[k] = value;
            }

            return new Polynom(coefficients);
        }
        static public Polynom operator % (Polynom p, Polynom q)
        {
            if (p.Power < q.Power)
            {
                return new Polynom(p._Coefficients.Clone() as double[]);
            }

            var coefficients = p._Coefficients.Clone() as double[];
            var qCoef = q._Coefficients;

            for (int k = p.Power - q.Power, i = p.Power; k >= 0; k--, i--)
            {
                var value = coefficients[i] / qCoef[q.Power];
                for (int j = 0; j <= q.Power; j++)
                {
                    coefficients[k + j] -= value * qCoef[j];
                }
            }

            return new Polynom(coefficients);
        }
        static public Polynom operator / (Polynom p, Polynom q)
        {
            return Polynom.Div(p, q);
        }


        // Преобразование в строчку
        public override string ToString()
        {
            var terms = _Coefficients.Select(
                (coef, power) => {
                    if (coef == 0)
                    {
                        return "";
                    }

                    var sign = coef > 0 ? '+' : '-';
                    var absCoef = Math.Abs(coef);

                    if (power == 0)
                    {
                        return $" {sign} {coef}";
                    }

                    var coefStr = absCoef == 1 ? "" : absCoef.ToString();
                    var powStr = power == 1 ? "" : $"^{power}";

                    return $" {sign} {coefStr}x{powStr}";
                }
            ).Where(str => str != "");

            return string.Join("", terms)[3..];
        }
    }
}
