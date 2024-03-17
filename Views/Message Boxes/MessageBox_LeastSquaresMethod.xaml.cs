using math_in.Views.Message_Boxes;
using System.Windows;

namespace math_in.Views.MessagBoxes {
  public partial class MessageBox_LeastSquaresMethod : Window {

    public double AmountPoints { get; private set; }
    public double MinNumber    { get; private set; }
    public double MaxNumber    { get; private set; }

    public MessageBox_LeastSquaresMethod() {
      InitializeComponent();
    }

    private void OK_Button_Click(object sender, RoutedEventArgs e) {
      if (TextBox_Max_Value.Text == "" && TextBox_Min_Value.Text == "" && TextBox_Amount_Points.Text == "") {
        DialogResult = false;
        return;
      }

      if (double.TryParse(TextBox_Amount_Points.Text, out double firstValue)) {
        AmountPoints = firstValue;    
      } else {
        MessageBox_Custom.Show("Внимание!", "Введите корректное число", "элементов!");
        return;
      }

      if (double.TryParse(TextBox_Min_Value.Text, out double secondValue)) {
        MinNumber = secondValue;
      } else {
        MessageBox_Custom.Show("Внимание!", "Введите корректное", "минимальное число!");
        return;
      }

      if (double.TryParse(TextBox_Max_Value.Text, out double thirdValue)) {
        MaxNumber = thirdValue;
      } else {
        MessageBox_Custom.Show("Внимание!", "Введите корректное", "максимальное число!");
        return;
      }

      DialogResult = true;
    }
  }
}
