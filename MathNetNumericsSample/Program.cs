using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MathNet.Numerics;
using TakeAshUtility;

namespace MathNetNumericsSample {

    class Program {

        static void Main(string[] args) {

            if (args.Length < 1) {
                Console.WriteLine("MathNetNumericsSample <data file>");
                return;
            }
            var order = 1;
            var xs = new List<double>();
            var ys = new List<double>();
            using (var reader = new StreamReader(args[0], Encoding.UTF8)) {
                order = reader.ReadLine()
                    .TryParse<int>();
                if (order < 1) {
                    Console.WriteLine("Invalid order");
                    return;
                }
                while (!reader.EndOfStream) {
                    var pair = reader.ReadLine()
                        .Split(new[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                    if (pair.Length < 2) {
                        Console.WriteLine("Invalid pair");
                        return;
                    }
                    xs.Add(pair[0].TryParse<double>());
                    ys.Add(pair[1].TryParse<double>());
                }
            }
            // [Curve Fitting: Linear Regression](http://numerics.mathdotnet.com/Regression.html)
            var coefficients = Fit.Polynomial(xs.ToArray(), ys.ToArray(), order);
            Console.WriteLine(String.Join("\n", coefficients.Select((p, i) => i + "\t" + p.ToString("0.0000"))));
        }
    }
}
