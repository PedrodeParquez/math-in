using math_in.Models.SLAE;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace math_in.Views {
  public partial class SLAEView : Page {
    public SLAEView() {
      InitializeComponent();      
    }

    double[,] array;
    double[] resultSlau;
    int n;

    private void Generate_Form_Button_Click(object sender, RoutedEventArgs e) {
      if (int.TryParse(TextBox_Size_Enter.Text, out int value) && value >= 2 && value <= 50) {
        CreateTextBoxRectangle(FormGrid, value, value + 1);
      } else {
        MessageBox.Show("Введите значение от 2 до 50");
      }
    }

    private void CreateTextBoxRectangle (Grid FormGrid, int totalRows, int totalColumns) {
      FormGrid.Children.Clear();
      FormGrid.RowDefinitions.Clear();
      FormGrid.ColumnDefinitions.Clear();

      for (int row = 0; row < totalRows; row++) {
        FormGrid.RowDefinitions.Add(new RowDefinition());
        for (int col = 0; col < totalColumns; col++) {
          FormGrid.ColumnDefinitions.Add(new ColumnDefinition());

          TextBox textBox = new TextBox {
            Margin = new Thickness(0, 0, 10, 5),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            VerticalAlignment = VerticalAlignment.Top,
            Width = 30,
            FontSize = 16,
            FontFamily = new FontFamily("Arial")
          };

          Grid.SetRow(textBox, row);
          Grid.SetColumn(textBox, col);
          FormGrid.Children.Add(textBox);

          if (col == totalColumns - 1) {
            break;
          }

            TextBlock label = new TextBlock {
              Margin = new Thickness(70, 0, 0, 5),
              HorizontalAlignment = HorizontalAlignment.Stretch,
              VerticalAlignment = VerticalAlignment.Top,
              Text = $"X{col + 1}",
              FontSize = 16,
              FontFamily = new FontFamily("Arial")
            };

          if (col == totalColumns - 2) {
            label.Text = $"X{col + 1} = ";
          }

          Grid.SetRow(label, row);
          Grid.SetColumn(label, col);
          FormGrid.Children.Add(label);
        }
      }

      n = totalRows;
    }

    private void Random_Values_Button_Click(object sender, RoutedEventArgs e) {
      Random random = new Random();

      foreach (var child in FormGrid.Children) {
        if (child is TextBox textBox) {
          textBox.Text = random.Next(1, 100).ToString();
        }
      }
    }

    private (double[,], double[]) ReadDataFromTextBoxes(int n) {
      double[,] resultArray = new double[n, n];
      double[] rightSide = new double[n];

      for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
          TextBox textBox = (TextBox)FormGrid.Children
              .OfType<TextBox>()
              .FirstOrDefault(tb => Grid.GetRow(tb) == i && Grid.GetColumn(tb) == j);

          if (textBox != null) {
            if (double.TryParse(textBox.Text, out double value)) {
              resultArray[i, j] = value;
            } else {
              MessageBox.Show("Введите корректные числа в TextBoxы!");
              return (resultArray, rightSide);
            }
          }
        }
      }

      for (int i = 0; i < n; i++) {
        TextBox textBox = (TextBox)FormGrid.Children.OfType<TextBox>()
            .FirstOrDefault(tb => Grid.GetRow(tb) == i && Grid.GetColumn(tb) == n);

        if (textBox != null) {
          if (double.TryParse(textBox.Text, out double value)) {
            rightSide[i] = value;
          } else {
            MessageBox.Show("Введите корректные числа в TextBoxы!");
            return (resultArray, rightSide);
          }
        }
      }

      return (resultArray, rightSide);
    }

    private void Clear_Form_Button_Click(object sender, RoutedEventArgs e) {
      FormGrid.Children.Clear();
    }

    private void Calculate_Buttton_Click(object sender, RoutedEventArgs e) { 
      if (CheckBox_Method_Gauss.IsChecked == false && CheckBox_Method_Cramer.IsChecked == false && 
        CheckBox_Method_Jordano.IsChecked == false) {
        MessageBox.Show("Выберите один метод!");
        return;
      }

      if (CheckBox_Method_Gauss.IsChecked == true && CheckBox_Method_Cramer.IsChecked == false &&
        CheckBox_Method_Jordano.IsChecked == false) {
        (array, resultSlau) = ReadDataFromTextBoxes(n);
        double [] res = SlaeMethods.MethodGauss(array, resultSlau, n);

        TextBox_Result.Clear();

        for (int i = 0; i < res.Length; i++) {
          TextBox_Result.Text += $"x{i + 1} = {res[i]:F2}\n";
        }

        return;
      }

      if (CheckBox_Method_Cramer.IsChecked == true && CheckBox_Method_Jordano.IsChecked == false && 
        CheckBox_Method_Gauss.IsChecked == false) {
        (array, resultSlau) = ReadDataFromTextBoxes(n);
        double[] res = SlaeMethods.MethodСramer(array, resultSlau, n);

        TextBox_Result.Clear();

        for (int i = 0; i < res.Length; i++) {
          TextBox_Result.Text += $"x{i + 1} = {res[i]:F2}\n";
        }

        return;
      }

      if (CheckBox_Method_Jordano.IsChecked == true && CheckBox_Method_Gauss.IsChecked == false && CheckBox_Method_Cramer.IsChecked == false) {
        (array, resultSlau) = ReadDataFromTextBoxes(n);
        double[] res = SlaeMethods.MethodGaussJordano(array, resultSlau, n);

        TextBox_Result.Clear();

        for (int i = 0; i < res.Length; i++) {
          TextBox_Result.Text += $"x{i + 1} = {res[i]:F2}\n";
        }

        return;
      }

      MessageBox.Show("Выберите только один метод!");
    }
  }
}

