using math_in.Views.Message_Boxes;
using System.Windows;

namespace math_in.Views {
  public partial class MessageBox_Sorting : Window {

    public int EnteredNumber { get; private set; }

    public MessageBox_Sorting() {
      InitializeComponent();
    }

    private void OK_Button_Click(object sender, RoutedEventArgs e) {
      if (int.TryParse(TextBox_Number.Text, out int result)) {
        EnteredNumber = result;
        DialogResult = true;
        Close();
      } else {
        MessageBox_Custom.Show("Внимание!", "Введите корректное число", "элементов!");
        return;
      }
    }
  }
}
