using System;
using MathNet.Numerics.Optimization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Net;

using Test_NSS.Classes;
using Extreme.Mathematics.Optimization;
using MathNet.Numerics;
using MathNet.Numerics.Statistics;
using Meta.Numerics.Statistics;

namespace Test_NSS
{
     class Program
    {
        static void Main(string[] args)
        {
            double[] maturites = new double[] { 1,2,3,4,5};
            double[] tauxObserves= new double[] { 0.02, 0.025, 0.03, 0.035, 0.038 };
            Vector<double> parametresInitiux =Vector<double>.Build.DenseOfArray(new double[] { 0.01, -0.02, 0.03, 0.02 });
            Func<Vector<double>, double> fonctionOptimisation = (parametres) =>
            {
                double[] taupredicts = NelsonSiegelModel.CalculerTauxPredits(parametres, maturites);
                double error = 0;
                for (int i=0;i<maturites.Length;i++)
                { 
                    double ecart = tauxObserves[i] - taupredicts[i];
                    error+= ecart*ecart;
                }
                return error;
            };
            IObjectiveFunction obj = ObjectiveFunction.Value(fonctionOptimisation);
            MinimizationResult minresult = NelderMeadSimplex.Minimum(objectiveFunction: obj, initialGuess: parametresInitiux,convergenceTolerance:1e-5,maximumIterations:1000);
            var FitCs = minresult.MinimizingPoint;//fitted coeffecient
            var ErrorValue = minresult.FunctionInfoAtMinimum.Value;
           // Console.WriteLine(minresult.MinimizingPoint.ToString());
            //var bj = ObjectiveFunction.Value(fonctionOptimisation);
            //var solver = new NelderMeadSimplex(1e-5, maximumIterations: 100000);
            // var initialguess = new DenseVector(new[] { 0.01, -0.02, 0.03, 0.02 });
            //var result =solver.FindMinimum(bj,parametresInitiux);
            //var resultoptimization = result.MinimizingPoint.ToString();


            Console.WriteLine(FitCs);
            Console.WriteLine(ErrorValue.ToString());

        }
    }
}