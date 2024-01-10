using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.IntegralMethods {
  public class MethodSimpson {
    public static double SimpsonMethodConcreteN(double a, double b, double e, double N) {
      double valIntegral2;

      double step = (b - a) / (N);
      double summOdd = 0d;
      double summEven = 0d;

      double indexN = 1;

      while (indexN < N) {
        var odd = a + step * indexN;

        if (indexN <= N - 1) {
          summOdd += Func.Fun(odd);

          indexN++;
        }

        var even = a + step * indexN;

        if (indexN <= N - 2) {
          summEven += Func.Fun(even);

          indexN++;
        }
      }

      valIntegral2 = (step / 3d) * (Func.Fun(a) + 4 * summOdd + 2 * summEven + Func.Fun(b));

      Func.countN[2] = N;
      Func.result[2] = valIntegral2;


      return valIntegral2;
    }

    public static double SimpsonMethod(double a, double b, double e) {
      double N = 1;
      double step;
      double valIntegral1 = SimpsonMethodConcreteN(a, b, e, N *= 2);
      double valIntegral2 = valIntegral1;

      do {
        N = 2 * N;
        step = (b - a) / (N);
        double summOdd = 0d;
        double summEven = 0d;
        valIntegral1 = valIntegral2;
        valIntegral2 = 0;
        double indexN = 1;

        while (indexN < N) {
          var odd = a + step * indexN;

          if (indexN <= N - 1) {
            summOdd += Func.Fun(odd);

            indexN++;
          }

          var even = a + step * indexN;

          if (indexN <= N - 2) {
            summEven += Func.Fun(even);

            indexN++;
          }
        }

        valIntegral2 = (step / 3d) * (Func.Fun(a) + 4 * summOdd + 2 * summEven + Func.Fun(b));
      } while ((1m / 3m) * Math.Abs(Math.Round((decimal)valIntegral2, Math.Abs((int)Math.Log10(e)))
      - Math.Round((decimal)valIntegral1, Math.Abs((int)Math.Log10(e)))) > 0);

      if (N == 1 && SimpsonMethodConcreteN(a, b, e, Func.countN[2]) == 0) {
        ++N;
        Func.countN[2] = N;
      } else if (N == 2) {
        Func.countN[2] = N;
      } else {
        if (N >= 8) {
          Func.countN[2] = SearchN.Search(SimpsonMethodConcreteN, a, b, e, N / 4, N / 2, valIntegral2);

          if (Func.countN[2] == -1) {
            Func.countN[2] = SearchN.Search(SimpsonMethodConcreteN, a, b, e, N / 2, N, valIntegral2);
          }
        } else {
          Func.countN[2] = SearchN.Search(SimpsonMethodConcreteN, a, b, e, N / 2, N, valIntegral2);
        }
      }


      if (Func.countN[2] % 2 != 0) {
        if (Math.Abs(Math.Round((decimal)valIntegral2, Math.Abs((int)Math.Log10(e)))
          - Math.Round((decimal)SimpsonMethodConcreteN(a, b, e, Func.countN[2] - 1), Math.Abs((int)Math.Log10(e)))) < (decimal)e) {
          ++Func.countN[2];
          Func.countN[2] = Func.countN[2] - 1;
        } else if (Math.Abs(Math.Round((decimal)valIntegral2, Math.Abs((int)Math.Log10(e)))
          - Math.Round((decimal)SimpsonMethodConcreteN(a, b, e, Func.countN[2] + 1), Math.Abs((int)Math.Log10(e)))) < (decimal)e) {
          Func.countN[2] = Func.countN[2] + 1;
        }
      }

      Func.result[2] = SimpsonMethodConcreteN(a, b, e, Func.countN[2]);

      return Func.result[2];
    }
  }
}
