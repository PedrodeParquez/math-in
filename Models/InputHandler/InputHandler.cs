using math_in.Views.Message_Boxes;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace math_in.Models.InputHandler {
  public class InputHandler {
    public static bool IsCorrectInput(TextBox functionField, TextBox startPointField, TextBox endPointField, TextBox presicionField) {
      if (functionField.Text == "") {
        MessageBox_Custom.Show("Внимание!", "Поле функции не заполнено!", "");
        return false;
      }

      if (startPointField.Text == "") {
        MessageBox_Custom.Show("Внимание!", "Поле начальной точки не заполнено!", "");
        return false;
      }

      if (endPointField.Text == "") {
        MessageBox_Custom.Show("Внимание!", "Поле конечной точки не заполнено!", "");
        return false;
      }

      if (presicionField.Text == "") {
        MessageBox_Custom.Show("Внимание!", "Поле погрешности не заполнено!", "");
        return false;
      }

      if (!double.TryParse(startPointField.Text, out Func.a)) {
        MessageBox_Custom.Show("Внимание!", "Неправильный формат для", "начальной точки!");
        return false;
      }

      if (!double.TryParse(endPointField.Text, out Func.b)) {
        MessageBox_Custom.Show("Внимание!", "Неправильный формат для", "конечной точки!");
        return false;
      }

      if (Func.a > Func.b) {
        MessageBox_Custom.Show("Внимание!", "Начальная точка должна быть", "меньше или равна конечной");
        return false;
      }

      if (!Regex.IsMatch(presicionField.Text, @"(1|10+)|(0,(1|0+1))")
         || (!double.TryParse(presicionField.Text, out Func.e)
         || presicionField.Text[0] == '-')) {
        MessageBox_Custom.Show("Внимание!", "Неправильный формат для", "точности!");
        return false;
      }

      return true;
    }

    public static bool IsCorrectInputIntegral(TextBox functionField, TextBox lowerField, TextBox upperField,TextBox presicionField){
      if (functionField.Text == "") {
        MessageBox_Custom.Show("Внимание!", "Неправильный формат для", "конечной точки!");
        return false;
      }

      if (lowerField.Text == "") {
        MessageBox_Custom.Show("Внимание!", "Поле нижнего предела,", "не заполнено!");
        return false;
      }

      if (upperField.Text == "") {
        MessageBox_Custom.Show("Внимание!", "Поле верхнего предела,", "не заполнено!");
        return false;
      }

      if (presicionField.Text == "") {
        MessageBox_Custom.Show("Внимание!", "Поле погрешности,", "не заполнено!");
        return false;
      }

      if (FunctionParser.Expression.IsExpression(lowerField.Text + "+0*x", Func.idsNames)) {
        FunctionParser.Expression expression = new FunctionParser.Expression(lowerField.Text + "+0*x", Func.idsNames, null);

        Func.idsValues[0] = 0d;
        Func.a = expression.CalculateValue(Func.idsValues);
      } 
      
      if (!double.TryParse(lowerField.Text, out Func.a)) {
        MessageBox_Custom.Show("Внимание!", "Неправильный формат данных для", "значения нижнего предела!");
        return false;
      }

      if (FunctionParser.Expression.IsExpression(upperField.Text + "+0*x", Func.idsNames)) {
        FunctionParser.Expression expression = new FunctionParser.Expression(upperField.Text + "+0*x", Func.idsNames, null);

        Func.idsValues[0] = 0d;

        Func.b = expression.CalculateValue(Func.idsValues);
      } else if (!double.TryParse(upperField.Text, out Func.b)) {
        MessageBox_Custom.Show("Внимание!", "Неправильный формат данных для", "значения верхнего предела!");
        return false;
      }

      if (Func.a > Func.b) {
        MessageBox_Custom.Show("Внимание!", "Значения должны быть такие, что:", "Нижний предел <= Верхний предел!");
        return false;
      }

      if (!double.TryParse(presicionField.Text, out Func.e) || !Regex.IsMatch(presicionField.Text, @"(1|10+)|(0,(1|0+1))") || presicionField.Text[0] == '-') {
        MessageBox_Custom.Show("Внимание!", "Неправильный формат данных", "для точности!");
        return false;
      }

      return true;
    }

    public static bool IsCorrectInputSLAE(bool flagExist, bool flagIncorrect) {
      if (!flagExist) {
        MessageBox_Custom.Show("Внимание!", "Необходимо создать форму!", "");
        return false;
      }

      if (!flagIncorrect) {
        MessageBox_Custom.Show("Внимание!", "Введены некорректные числа", "или ячейки пустые!");
        return false;
      }

      return true;
    }
  }
}
