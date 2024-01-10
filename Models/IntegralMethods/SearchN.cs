using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.IntegralMethods {
  public class SearchN {
    public static double Search(Func<double, double, double, double, double> integralMethod,
      double a, double b, double e, double left, double right, double valintegral, bool flag = true) {
      double N = (left + right) / 2;

      if (Math.Abs(right - left) <= 2) {
        if (!flag) {
          return -1;
        }

        decimal bruh1 = Math.Round((decimal)valintegral, Math.Abs((int)Math.Log10(e)));
        decimal bruhleft = (decimal)integralMethod(a, b, e, left);
        decimal bruhLeft = Math.Round((decimal)integralMethod(a, b, e, left), Math.Abs((int)Math.Log10(e)));
        decimal bruhn = (decimal)integralMethod(a, b, e, N);
        decimal bruhN = Math.Round((decimal)integralMethod(a, b, e, N), Math.Abs((int)Math.Log10(e)));
        decimal bruhright = (decimal)integralMethod(a, b, e, right);
        decimal bruhRight = Math.Round((decimal)integralMethod(a, b, e, right), Math.Abs((int)Math.Log10(e)));

        if (Math.Round((decimal)valintegral, Math.Abs((int)Math.Log10(e))) == Math.Round((decimal)integralMethod(a, b, e, left), Math.Abs((int)Math.Log10(e)))) {
          return left;
        } else if (Math.Round((decimal)valintegral, Math.Abs((int)Math.Log10(e))) == Math.Round((decimal)integralMethod(a, b, e, N), Math.Abs((int)Math.Log10(e)))) {
          return N;
        } else if (Math.Round((decimal)valintegral, Math.Abs((int)Math.Log10(e))) == Math.Round((decimal)integralMethod(a, b, e, right), Math.Abs((int)Math.Log10(e)))) {
          return right;
        }
      }

      if (Math.Round((decimal)valintegral, Math.Abs((int)Math.Log10(e))) == Math.Round((decimal)integralMethod(a, b, e, N), Math.Abs((int)Math.Log10(e)))) {
        N = Search(integralMethod, a, b, e, left, N, valintegral, true);
      } else {
        double N1 = Search(integralMethod, a, b, e, left, N, valintegral, false);
        double N2;

        if (N1 != -1) {
          N = N1;
        } else {
          N2 = Search(integralMethod, a, b, e, N, right, valintegral, false);

          if (N2 != -1) {
            N = N2;
          } else {
            return -1;
          }
        }
      }

      return N;
    }
  }
}
