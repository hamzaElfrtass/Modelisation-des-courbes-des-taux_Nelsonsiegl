using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_NSS.Classes
{
    internal class NelsonSiegelModel
    {
        public static double[] CalculerTauxPredits(Vector<double> parametres, double[] maturities)
        {
            double beta0 = parametres[0];
            double beta1 = parametres[1];
            double beta2 = parametres[2];
            double beta3 = parametres[3];
            ;

            double[] tauxPredits = new double[maturities.Length];

            for (int i = 0; i < maturities.Length; i++)
            {
                double T = maturities[i];
                tauxPredits[i] = beta0 + beta1 * (1 - Math.Exp(-T / beta2)) / (T / beta2) + beta3 * ((1 - Math.Exp(-T / beta2)) / (T / beta2) - Math.Exp(-T / beta2)) ;
            }

            return tauxPredits;
        }
    }
}
