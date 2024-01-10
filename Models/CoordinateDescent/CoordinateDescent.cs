using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.CoordinateDescent {
  internal class CoordinateDescent : Func {
    public static double CoordinateDescentLocMin(double a, double b, double e) {
      double x = a;

      while (Fun(x) > Fun(x + e) && Math.Abs(x - b) > e) {
        if (Fun(x) > Fun(x + e)) {
          x += e;
        } else {
          x -= e;
        }
      }

      if (x == a || x == b) {
        if (Fun(a) < Fun(b)) {
          x = a;
        } else {
          x = b;
        }
      }

      return x;
    }

    public static double CoordinateDescentLocMax(double a, double b, double e) {
      double x = b;

      while (-Fun(x) < -Fun(x + e) && Math.Abs(x - a) > e) {
        if (-Fun(x) > -Fun(x + e)) {
          x += e;
        } else {
          x -= e;
        }
      }

      if (x == a || x == b) {
        if (Fun(a) > Fun(b)) {
          x = a;
        } else {
          x = b;
        }
      }

      return x;
    }
  }
}
