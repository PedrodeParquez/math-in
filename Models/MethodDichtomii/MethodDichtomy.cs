using math_in.Views.Message_Boxes;

namespace math_in.Models.MethodDichtomii {
  public class MethodDichtomy : Func {
    public static double Dychotomy(double a, double b, double e, double x = default) {
      if (Fun(a) * Fun(b) > 0) {
        MessageBox_Custom.Show("Предупреждение", "Нет единственного решения", $"на отрезке [{a}; {b}]!");
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
