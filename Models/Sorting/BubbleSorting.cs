using System.Diagnostics;

namespace math_in.Models.Sorting {
  public class BubbleSorting : Sorting {
    public static (int[], int, long) BubbleSort(int[] array) {
      int iterations = 0;
      int[] resArray = array.Clone() as int[];
      
      var timer = new Stopwatch();
      timer.Start();

      bool flag;

      for (var i = 1; i < resArray.Length; i++) {
        flag = false;

        for (var j = 0; j < resArray.Length - i - 1; j++) {
          if (resArray[j] > resArray[j + 1]) {
            Swap(ref resArray[j], ref resArray[j + 1]);

            if (!flag) {
              iterations++;
              flag = true;
            }
          }
        }

        if (flag == false) {
          iterations++;

          break;
        }
      }
        
      timer.Stop();

      return (resArray, iterations, (timer.ElapsedTicks * 1_000_000) / Stopwatch.Frequency);
    }
  }
}
