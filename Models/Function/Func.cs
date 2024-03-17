using math_in.Views.Message_Boxes;
using System;

namespace math_in.Models {
  public class Func {
    private static string textFunction;

    public static string[] idsNames;
    public static double[] idsValues;
    public static double a, b, e, x, y;
    public const double step = 0.01;
    public static double[] countN = new double[3];
    public static double[] result = new double[3];
    public static int N;

    public static string TextFunction {
      get { return textFunction; }
      set {
        if (FunctionParser.Expression.IsExpression(value, idsNames)) {
          textFunction = value;
        } else {
          throw new Exception("Функция написана неверно !!!");
        }
      }
    }

    public static double Fun(double x) {
      if (FunctionParser.Expression.IsExpression(TextFunction, idsNames)) {
        FunctionParser.Expression expression = new FunctionParser.Expression(TextFunction, idsNames, null);

        idsValues[0] = x;

        double res = expression.CalculateValue(idsValues);

        return res;
      } else {
        MessageBox_Custom.Show("Предупреждение", "Функция написана неверно !", "");
        return 0;
      }
    }

    public static (double[], double[]) CalculateFunctionValuesInRange() {
      int pointsCount = (int)((b - a) / step) + 1;
      double[] xValues = new double[pointsCount];
      double[] yValues = new double[pointsCount];

      for (int i = 0; i < pointsCount; i++) {
        xValues[i] = a + i * step;
        yValues[i] = Fun(xValues[i]);
      }

      return (xValues, yValues);
    }
  }
}
