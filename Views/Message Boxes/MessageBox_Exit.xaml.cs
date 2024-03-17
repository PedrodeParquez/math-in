using System.Windows;

namespace math_in.Views.Message_Boxes {
  public partial class MessageBox_Exit : Window {
    public MessageBox_Exit() {
      InitializeComponent();
    }

    private void OK_Button_Click(object sender, RoutedEventArgs e) {
      DialogResult = true;
    }

    private void NO_Button_Click(object sender, RoutedEventArgs e) {
      DialogResult = false;
    }

    public static bool Show(string title, string firstString, string secondString) {
      MessageBox_Exit messageBox = new MessageBox_Exit();

      messageBox.DefineMessageBox(title, firstString, secondString);

      if (messageBox.ShowDialog() == true) {
        return false;
      }

      return true;
    }

    public void DefineMessageBox(string title, string firstString, string secondString) {
      Title_TextBlock.Text = title;
      FirstString_TextBlock.Text = firstString;
      SecondString_TextBlock.Text = secondString;
    }
  }
}
