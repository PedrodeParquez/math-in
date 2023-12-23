using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models {
  public class Function {
    private static string textFunction;
    public static string TextFunction {
      get { return textFunction; }
      set {
        if (Parser.Expression.IsExpression(value, idsNames)) {
          textFunction = value;
        } else {
          throw new Exception("Функция написана неверно !!!");
        }
      }
    }

    public static string[] idsNames;
    public static double[] idsValues;
    public static double a, b, e, x, y;
    public const double step = 0.01;

    public static double Fun(double x) {
      if (Parser.Expression.IsExpression(TextFunction, idsNames)) {
        Parser.Expression expression = new Parser.Expression(TextFunction, idsNames, null);

        idsValues[0] = x;

        double res = expression.CalculateValue(idsValues);

        return res;
      } else {
        throw new Exception("Функция написана неверно !!!");
      }
    }
  }
}
