using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.IntegralMethods {
  public class MethodTrapezoid {
    public static double TrapezoidMethodConcreteN(double a, double b, double e, double N) {
      double lengthSigment = b - a;
      double step;
      double valIntegral2 = 0;

      step = lengthSigment / N;

      int k = 0;

      for (double x = a; x <= b - step; x += step) {
        valIntegral2 += ((Func.Fun(x) + (Func.Fun(x + step)) / 2) * step);

        ++k;
      }

      Func.countN[1] = N;
      Func.result[1] = valIntegral2;

      return valIntegral2;
    }

    public static double TrapezoidMethod(double a, double b, double e) {
      double N = 1d / 2d;
      double lengthSigment = b - a;
      double step;
      double valIntegral1 = TrapezoidMethodConcreteN(a, b, e, N *= 2);
      double valIntegral2 = valIntegral1;

      do {
        N *= 2;
        valIntegral1 = valIntegral2;
        valIntegral2 = 0;

        step = lengthSigment / N;

        for (double x = a; x <= b - step; x += step) {
          valIntegral2 += ((Func.Fun(x) + Func.Fun(x + step)) / 2) * step;
        }

        decimal bruh = Math.Abs(Math.Round((decimal)valIntegral2, Math.Abs((int)Math.Log10(e)) + 1) - Math.Round((decimal)valIntegral1, Math.Abs((int)Math.Log10(e)) + 1));
      } while (Math.Round((decimal)valIntegral2, Math.Abs((int)Math.Log10(e))) != Math.Round((decimal)valIntegral1, Math.Abs((int)Math.Log10(e))));

      if (N == 1) {
        Func.countN[1] = N;
      } else {
        if (N != 2) {
          Func.countN[1] = SearchN.Search(TrapezoidMethodConcreteN, a, b, e, N / 4, N / 2, valIntegral2);

          if (Func.countN[1] == -1) {
            Func.countN[1] = SearchN.Search(TrapezoidMethodConcreteN, a, b, e, N / 2, N, valIntegral2);
          }
        } else {
          Func.countN[1] = SearchN.Search(TrapezoidMethodConcreteN, a, b, e, N / 2, N, valIntegral2);
        }
      }

      Func.result[1] = TrapezoidMethodConcreteN(a, b, e, Func.countN[1]);

      return Func.result[1];
    }
  }
}

