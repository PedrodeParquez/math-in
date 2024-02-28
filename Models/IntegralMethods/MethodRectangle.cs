using System;

namespace math_in.Models.IntegralMethods {
  public class MethodRectangle {
    public static double RectangleMethodConcreteN(double a, double b, double e, double N) {
      double lengthSigment = b - a;
      double step;
      double valintegral2 = 0;

      step = lengthSigment / N;

      for (double x = a; x <= b - step; x += step) {
        valintegral2 += step * Func.Fun(x);
      }

      Func.countN[0] = N;
      Func.result[0] = valintegral2;

      return valintegral2;
    }

    public static double RectangleMethod(double a, double b, double e) {
      double N = 1d / 2d;
      double lengthSigment = b - a;
      double step;
      double valIntegral1 = RectangleMethodConcreteN(a, b, e, N *= 2);
      double valIntegral2 = valIntegral1;

      do {
        N *= 2;
        valIntegral1 = valIntegral2;
        valIntegral2 = 0;

        step = lengthSigment / N;

        for (double x = a; x <= b - step; x += step) {
          valIntegral2 += step * Func.Fun(x);
        }
      } while (Math.Round((decimal)valIntegral2, Math.Abs((int)Math.Log10(e))) != Math.Round((decimal)valIntegral1, Math.Abs((int)Math.Log10(e))));

      if (N == 1) {
        Func.countN[0] = N;
      } else {
        if (N != 2) {
          Func.countN[1] = SearchN.Search(RectangleMethodConcreteN, a, b, e, N / 4, N / 2, valIntegral2);

          if (Func.countN[1] == -1) {
            Func.countN[1] = SearchN.Search(RectangleMethodConcreteN, a, b, e, N / 2, N, valIntegral2);
          }
        } else {
          Func.countN[1] = SearchN.Search(RectangleMethodConcreteN, a, b, e, N / 2, N, valIntegral2);
        }
      }

      Func.result[0] = RectangleMethodConcreteN(a, b, e, Func.countN[0]);

      return Func.result[0];
    }
  }
}
