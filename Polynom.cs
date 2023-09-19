using Iced.Intel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomAlgebra
{
    class Polynom
    {
        /// <summary>
        /// Array of polynom coefficients.
        /// </summary>
        private readonly double[] _Coefficients;

        /// <summary>
        /// Gets power of the polynom.
        /// </summary>
        public int Power => _Coefficients.Length - 1;

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

            if (coefficients.Length == 0)
            {
                throw new ArgumentException("coefficients", "Длина массива = 0");
            }

            _Coefficients = coefficients;
        }

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

        static public Polynom operator +(Polynom p, double x)
        {
            var len = p.Power + 1;
            double[] coefficients = new double[len];
            Array.Copy(p._Coefficients, coefficients, len);

            coefficients[0] += x;

            return new Polynom(coefficients);
        }

        static public Polynom operator +(double x, Polynom p)
        {
            return p + x;
        }

        static public Polynom operator +(Polynom p1, Polynom p2)
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

        static public Polynom operator -(Polynom p1, Polynom p2)
        {
            return p1 + ((-1) * p2);
        }

        static public Polynom operator *(Polynom p, double k)
        {
            var coefficients = p._Coefficients.Select(alpha => k * alpha);

            return new Polynom(coefficients.ToArray());
        }

        static public Polynom operator *(double k, Polynom p)
        {
            var coefficients = p._Coefficients.Select(alpha => k * alpha);

            return new Polynom(coefficients.ToArray());
        }

        static public Polynom operator *(Polynom p1, Polynom p2)
        {
            int len1 = p1.Power + 1;
            int len2 = p2.Power + 1;

            double[] coefficients = new double[len1 + len2 - 1];

            for(int i = 0; i < len1; i++)
            {
                for (int j = 0; j < len2; j++)
                {
                    coefficients[i + j] += p1._Coefficients[i] * p2._Coefficients[j];
                }
            }

            return new Polynom(coefficients);
        }

        static public Polynom Pow(Polynom p1, uint n)
        {
            var coefficients = new double[p1.Power * n + 1];

            // TODO

            return new Polynom(coefficients);
        }
    }
}
