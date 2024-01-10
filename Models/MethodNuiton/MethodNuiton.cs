using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace math_in.Models.MethodNuiton {
  public class MethodNuiton {
    public static double Nutone(double a, double b, double e) {
      double x0;

      x0 = (a + b) / 2;

      double x1 = x0;

      do {
        x0 = x1;
        x1 = x0 - (Derivative(x0) / Derivative2(x0));
      } while (Math.Abs(x1 - x0) > e);

      if (x1 > b || x1 < a) {
        MessageBox.Show("На данном отрезке нет экстремума !!!");
      }

      return x1;
    }

    public static double NutoneIntersection(double a, double b, double e) {
      double x0;
      double h = 0.001;

      x0 = (a + b) / 2;

      double x1 = x0;

      do {
        x0 = x1;
        if (Derivative(x0) == 0) {
          x0 = x0 / 2 - e;
        }

        x1 = x0 - (Func.Fun(x0) / Derivative(x0));
      } while (Math.Abs(Func.Fun(x0) / Derivative(x0)) > h);

      if (x1 > b || x1 < a) {
       MessageBox.Show("На данном отрезке нет точек пересечения !!!");
      }

      return x1;
    }

    public static double Derivative(double x) {
      Func.idsValues[0] = x;

      double e = 0.01;

      double result1 = Func.Fun(Func.idsValues[0] + e);
      Func.idsValues[0] -= e;

      double result2 = Func.Fun(Func.idsValues[0] - e);
      Func.idsValues[0] += e;

      return (result1 - result2) / (2 * e);
    }

    public static double Derivative2(double x) {
      Func.idsValues[0] = x;

      double e = 0.01;

      double result1 = Func.Fun(Func.idsValues[0] + e);
      Func.idsValues[0] -= e;

      double result2 = Func.Fun(Func.idsValues[0] - e);
      Func.idsValues[0] += e;

      double result3 = Func.Fun(Func.idsValues[0]);

      return (result1 - 2 * result3 + result2) / (e * e);
    }
  }
}
