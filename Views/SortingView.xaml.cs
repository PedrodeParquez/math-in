using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using math_in.Models.Sorting;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace math_in.Views {
  public partial class SortingView : Page {
    public SortingView() {
      InitializeComponent();
    }

    private void Button_Random_Array_Click(object sender, RoutedEventArgs e) {
      int enteredNumber = 0;

      MessageBox_Sorting messageBox = new MessageBox_Sorting();

      if (messageBox.ShowDialog() == true) {
        enteredNumber = messageBox.EnteredNumber;
      }

      int[] randomArray = RandomArray.GenerateRandomArray(enteredNumber, 1, 100);
      TextBox_Array.Text = RandomArray.ArrayToString(randomArray);
    }

    private void Button_Clear_Array_Click(object sender, RoutedEventArgs e) {
      TextBox_Array.Clear();      
    }

    private void Button_Add_Element_Click(object sender, RoutedEventArgs e) {
      if (int.TryParse(TextBox_Add_Element.Text, out int number)) {
       TextBox_Array.Text += number + " ";

       TextBox_Add_Element.Text = string.Empty;
      } else {
        MessageBox.Show("Пожалуйста, вводите только целые числа!");
      }
    }

    private void Button_Sorting_Click(object sender, RoutedEventArgs e) {
      string resultString = "";
      string inputText = TextBox_Array.Text;

      if (inputText == "") {
        MessageBox.Show("Введите хотя бы одно число в массив!", "Внимание!", MessageBoxButtons.OK);
        return;
      }

      string[] numbersAsString = inputText.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
      int[] numbers = numbersAsString.Select(int.Parse).ToArray();

      if (CheckBox_Bubble.IsChecked == true) {
        var bubbleResult = BubbleSorting.BubbleSort(numbers);
        resultString += $"Сортировка пузырьком:\nВремя: {bubbleResult.Item3} микросекунд\nИтерации = {bubbleResult.Item2} раз\n";
      }

      if (CheckBox_Shaker.IsChecked == true) {
        var shakerResult = ShakerSorting.ShakerSort(numbers);
        resultString += $"Сортировка Шейкером:\nВремя: {shakerResult.Item2} мс\nИтерации = {shakerResult.Item1} раз\n";
      }

      if (CheckBox_Insertion.IsChecked == true) {
        var insertionResult = InsertionSorting.InsertionSort(numbers);
        resultString += $"Сортировка Вставками:\nВремя: {insertionResult.Item2} мс\nИтерации = {insertionResult.Item1} раз\n";
      }
        
      if (CheckBox_Bogo.IsChecked == true) {
        var bogoResult = BogoSorting.BogoSort(numbers);
        resultString += $"Сортировка Bogo:\nВремя: {bogoResult.Item2} мс\nИтерации = {bogoResult.Item1} раз\n";
      }
      
      if (CheckBox_Quick.IsChecked == true) {
        var timer = new Stopwatch();
        timer.Start();
        var result = QuickSorting.QuickSort(numbers);
        timer.Stop();
        resultString += resultString + $"Быстрая сортировка: {result} мс\nИтерации = {QuickSorting.GetIterationCount()} раз";
      }

      if (resultString == "") {
        MessageBox.Show("Выберите хотя бы один способ сортировки!", "Внимание!");
        return;
      }

      MessageBox.Show(resultString, "Результаты", MessageBoxButtons.OK);
      string resulti = string.Join(" ", BubbleSorting.BubbleSort(numbers).Item1);
      TextBox_Result.Text = resulti;
    }
  }
}