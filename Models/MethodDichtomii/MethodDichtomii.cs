using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace math_in.Models.MethodDichtomii {
  public class MethodDichtomii : Func {
    public static double Dychotomy(double a, double b, double e, double x = default) {
      if (Fun(a) * Fun(b) > 0) {
        MessageBox.Show($"Нет единственного решения на отрезке [{a}; {b}] !!!\n\n");
      }

      while ((b - a) > e) {
        x = (b - a) / 2 + a;

        if (Fun(a) * Fun(x) < 0) {
          b = x;
        } else if (Fun(a) == 0) {
          return a;
        } else if (Fun(b) * Fun(x) < 0) {
          a = x;
        } else if (Fun(b) == 0) {
          return b;
        } else if (Fun(x) == 0) {
          return x;
        }
      }

      return x;
    }

    public static double DychotomyLocMin(double a, double b, double e) {
      double delta = e / 10;

      while (b - a >= e) {
        double middle = (a + b) / 2;
        double lambda = a + delta, mu = b - delta; ;
        if (Fun(lambda) < Fun(mu))
          b = mu;
        else
          a = lambda;
      }

      return (a + b) / 2;
    }

    public static double DychotomyLocMax(double a, double b, double e) {
      double delta = e / 10;

      while (b - a >= e) {
        double middle = (a + b) / 2;
        double lambda = a + delta, mu = b - delta;
        if (-Fun(lambda) < -Fun(mu))
          b = mu;
        else
          a = lambda;
      }

      return (a + b) / 2;
    }
  }
}
