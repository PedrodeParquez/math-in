using System.Windows;

namespace math_in.Views.Message_Boxes {
  public partial class MessageBox_Custom : Window {
    public MessageBox_Custom() {
      InitializeComponent();
    }

    private void OK_Button_Click(object sender, RoutedEventArgs e) {
      DialogResult = true;
    }

    public static void Show(string title, string firstString, string secondString) {
      MessageBox_Custom messageBox = new MessageBox_Custom();

      messageBox.DefineMessageBox(title, firstString, secondString);

      if (messageBox.ShowDialog() == true) {
        return;
      }
    }

    public void DefineMessageBox(string title, string firstString, string secondString) {
      Title_TextBlock.Text = title;
      FirstString_TextBlock.Text = firstString;
      SecondString_TextBlock.Text = secondString;
    }
  }
}
