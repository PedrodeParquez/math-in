using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.Sorting {
  public class InsertionSorting : Sorting {
    public static (int, long) InsertionSort(int[] array) {
      var timer = new Stopwatch();
      int iterations = 0;
      int[] resArray = array.Clone() as int[];

      timer.Start();

      for (var i = 1; i < resArray.Length; i++) {
        var key = resArray[i];
        var j = i;
        while ((j > 1) && (resArray[j - 1] > key)) {
          Swap(ref resArray[j - 1], ref resArray[j]);
          j--;
        }

        resArray[j] = key;
        iterations++;
      }

      timer.Stop();

      return (iterations, (timer.ElapsedTicks * 1_000_000) / Stopwatch.Frequency);
    }
  }
}
