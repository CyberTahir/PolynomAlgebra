using PolynomAlgebra;
using System.Diagnostics;
using System.Numerics;
// using BenchmarkDotNet.Running;

var p1 = new Polynom(5, 6, -4);
var p2 = new Polynom(1, 3, 4);

var p3 = p1 * p2;

Console.WriteLine("");

// BenchmarkRunner.Run<PolynomBenchmark>();