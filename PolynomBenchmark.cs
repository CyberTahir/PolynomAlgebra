using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolynomAlgebra
{
    [MemoryDiagnoser]
    public class PolynomBenchmark
    {
        private static Polynom _Pol = new Polynom(5, 6, -4, 2, -3);

        [Benchmark(Baseline=true)]
        public double GetValueGorner()
        {
            return _Pol.GetValueGorner(1);
        }

        [Benchmark]
        public double GetValuePow()
        {
            return _Pol.GetValuePow(1);
        }

        [Benchmark]
        public double GetValuePow1()
        {
            return _Pol.GetValuePow1(1);
        }

        [Benchmark]
        public double GetValueGornerMem()
        {
            Polynom pol = new(5, 6, -4, 2, -3);

            return pol.GetValueGorner(1);
        }
    }
}
