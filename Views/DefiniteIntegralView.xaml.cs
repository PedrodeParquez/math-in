using math_in.Models;
using math_in.Models.InputHandler;
using math_in.Models.IntegralMethods;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace math_in.Views {
  public partial class DefiniteIntegralView : Page {
    public DefiniteIntegralView() {
      InitializeComponent();
    }

    private void Calculate_Integral_Button_Click(object sender, RoutedEventArgs e) {
      TextBlock_Result.Clear();

      if (!InputHandler.IsCorrectInputIntegral(TextBox_Function, TextBox_Lower_Limit , TextBox_Upper_Limit, TextBox_Precision)) {
        return;
      }

      bool hasN = true;

      if (TextBox_Number_Splits.Text == string.Empty) {
        hasN = false;
      }

      if (!int.TryParse(TextBox_Number_Splits.Text, out Func.N) && hasN) {
        MessageBox.Show("Неправильный формат данных для количества разбиений!");
        return;
      }

      if ((CheckBox_Method_Rectangle.IsChecked == false) && (CheckBox_Method_Trapezoid.IsChecked == false) && (CheckBox_Method_Simpson.IsChecked == false)) {
        MessageBox.Show("Выберите хотя бы один метод!");
        return;
      }

      try {
        Func.TextFunction = TextBox_Function.Text;
      } catch (Exception ex) {
        MessageBox.Show(ex.Message);
        return;
      }

      switch (hasN) {
        case true:
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

          break;
        case false:
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

          break;
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
      Chart.Plot.AddScatter(xValues, yValues, color: Color.Orange);      
      Chart.Refresh();
    }
  }
}