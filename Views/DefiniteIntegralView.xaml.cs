using HarfBuzzSharp;
using math_in.Models;
using math_in.Models.IntegralMethods;
using ScottPlot;
using ScottPlot.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SkiaSharp.HarfBuzz.SKShaper;

namespace math_in.Views {
  public partial class DefiniteIntegralView : Page {
    public DefiniteIntegralView() {
      InitializeComponent();
    }

    private void Calculate_Integral_Button_Click(object sender, RoutedEventArgs e) {
      TextBlock_Result.Clear();

      if (FunctionParser.Expression.IsExpression(TextBox_Lower_Limit.Text + "+0*x", Func.idsNames)) {
        FunctionParser.Expression expression = new FunctionParser.Expression(TextBox_Lower_Limit.Text + "+0*x", Func.idsNames, null);

        Func.idsValues[0] = 0d;

        Func.a = expression.CalculateValue(Func.idsValues);
      } else if (!double.TryParse(TextBox_Lower_Limit.Text, out Func.a)) {
        MessageBox.Show("Неправильный формат данных для значения нижнего предела!!!");
        return;
      }

      if (FunctionParser.Expression.IsExpression(TextBox_Upper_Limit.Text + "+0*x", Func.idsNames)) {
        FunctionParser.Expression expression = new FunctionParser.Expression(TextBox_Upper_Limit.Text + "+0*x", Func.idsNames, null);

        Func.idsValues[0] = 0d;

        Func.b = expression.CalculateValue(Func.idsValues);
      } else if (!double.TryParse(TextBox_Upper_Limit.Text, out Func.b)) {
        MessageBox.Show("Неправильный формат данных для значения верхнего предела!!!");
        return;
      }

      if (Func.a > Func.b) {
        MessageBox.Show("a и b должны быть такие, что: \n Нижний предел <= Верхний предел!");
        return;
      }

      if (!double.TryParse(TextBox_Precision.Text, out Func.e) || !Regex.IsMatch(TextBox_Precision.Text, @"(1|10+)|(0,(1|0+1))") || TextBox_Precision.Text[0] == '-') {
        MessageBox.Show("Неправильный формат данных для точности!");
        return;
      }

      bool hasN = true;

      if (TextBox_Number_Splits.Text == string.Empty) {
        hasN = false;
      } else if (!int.TryParse(TextBox_Number_Splits.Text, out Func.N)) {
        MessageBox.Show("Неправильный формат данных для точности!");

        return;
      }

      try {
        Func.TextFunction = TextBox_Function.Text;
      } catch (Exception ex) {
        MessageBox.Show(ex.Message);
        return;
      }

      if (hasN == true) {
        if (CheckBox_Method_Rectangle.IsChecked == true) {
          double integralValue = Math.Round((double)MethodRectangle.RectangleMethodConcreteN(Func.a, Func.b, Func.e, Func.N), Math.Abs((int)Math.Log10(Func.e)));


          TextBlock_Result.Text += $"Количество разбиений Методом Прямоугольника:" +
            $" {Func.countN[0]}\nРезультат: {Func.result[0]}\n";
        }

        if (CheckBox_Method_Trapezoid.IsChecked == true) {
          double integralValue = Math.Round((double)MethodTrapezoid.TrapezoidMethodConcreteN(Func.a, Func.b, Func.e, Func.N), Math.Abs((int)Math.Log10(Func.e)));

          TextBlock_Result.Text += $"Количество разбиений Методом Трапеций:" +
            $" {Func.countN[1]}\nРезультат: {Func.result[1]}\n";
        }

        if (CheckBox_Method_Simpson.IsChecked == true) {
          double integralValue = Math.Round((double)MethodSimpson.SimpsonMethodConcreteN(Func.a, Func.b, Func.e, Func.N), Math.Abs((int)Math.Log10(Func.e)));

          TextBlock_Result.Text += $"Количество разбиений Методом Симпсона:" +
            $" {Func.countN[2]}\nРезультат: {Func.result[2]}\n";
        }
      } else {
        if (CheckBox_Method_Rectangle.IsChecked == true) {
          double integralValue = Math.Round((double)MethodRectangle.RectangleMethod(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));

          TextBlock_Result.Text += $"Количество разбиений Методом Прямоугольника:" +
            $" {Func.countN[0]}\nРезультат: {Func.result[0]}\n";
        }

        if (CheckBox_Method_Trapezoid.IsChecked == true) {
          double integralValue = Math.Round((double)MethodTrapezoid.TrapezoidMethod(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));

          TextBlock_Result.Text += $"Количество разбиений Методом Трапеций:" +
            $" {Func.countN[1]}\nРезультат: {Func.result[1]}\n";
        }

        if (CheckBox_Method_Simpson.IsChecked == true) {
          double integralValue = Math.Round((double)MethodSimpson.SimpsonMethod(Func.a, Func.b, Func.e), Math.Abs((int)Math.Log10(Func.e)));

          TextBlock_Result.Text += $"Количество разбиений Методом Симпсона:" +
            $" {Func.countN[2]}\nРезультат: {Func.result[2]}\n";
        }
      }

      double maxCountN = Math.Max(Math.Max(Func.countN[0], Func.countN[1]), Func.countN[2]);

      if (maxCountN % 2 != 0) {
        ++maxCountN;
      }

      if (CheckBox_Method_Simpson.IsChecked == true && CheckBox_Method_Rectangle.IsChecked == true && CheckBox_Method_Trapezoid.IsChecked == true && !hasN) {
        TextBlock_Result.Text += $"\nОптимальное N для всех методов: {maxCountN}\n";
        TextBlock_Result.Text += $"Результат Метода Прямоугольников при N = {maxCountN}: {MethodRectangle.RectangleMethodConcreteN(Func.a, Func.b, Func.e, maxCountN)}\n";
        TextBlock_Result.Text += $"Результат Метода Трапеций при N = {maxCountN}: {MethodTrapezoid.TrapezoidMethodConcreteN(Func.a, Func.b, Func.e, maxCountN)}\n";
        TextBlock_Result.Text += $"Результат Метода Симпсона при N = {maxCountN}: {MethodSimpson.SimpsonMethodConcreteN(Func.a, Func.b, Func.e, maxCountN)}\n";
      }

      var (xValues, yValues) = Func.CalculateFunctionValuesInRange();
      Chart.Plot.AddScatter(xValues, yValues);      
      Chart.Refresh();
    }
  }
}

