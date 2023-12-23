using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.Sorting {
  public class BogoSorting : Sorting {
    public static Stopwatch timeExecution = new Stopwatch();
    public static long iteration = new long();
    public static int countValue;

    public static (long, long) BogoSort(int[] array) {
      int[] resArray = array.Clone() as int[];
      iteration = 0;

      var timer = new Stopwatch();
      timer.Start();

      bool flag = false;

      while (!IsSorted(resArray)) {
        resArray = RandomPermutation(resArray);

        flag = true;
      }

      if (flag == false) {
        iteration++;
      }

      timer.Stop();
      timeExecution = timer;

      return (iteration, (timer.ElapsedTicks * 1_000_000) / Stopwatch.Frequency);
    }

    public static int[] BogoSortRev(int[,] array) {
      int[] resArray = array.Clone() as int[];
      iteration = 0;

      var timer = new Stopwatch();

      timer.Start();

      bool flag = false;

      while (!IsSorted(resArray)) {
        resArray = RandomPermutation(resArray);

        flag = true;
      }

      if (flag == false) {
        ++iteration;
      }

      timer.Stop();

      timeExecution = timer;

      Array.Reverse(resArray);

      return resArray;
    }

    private static bool IsSorted(int[] array) {
      for (int i = 0; i < array.Length - 1; i++) {
        if (array[i] > array[i + 1]) {
          return false;
        }
      }

      return true;
    }

    private static int[] RandomPermutation(int[] array) {
      Random random = new Random();
      int arrCount = array.Length;

      while (arrCount > 1) {
        arrCount--;

        int i = random.Next(arrCount + 1);
        Swap(ref array[i], ref array[arrCount]);

        iteration++;
      }

      return array;
    }
  }
}
