using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.Sorting {
  public class ShakerSorting : Sorting {
    public static (int, long) ShakerSort(int[] array) {
      var timer = new Stopwatch();
      int iterations = 0;
   
      int[] resArray = array.Clone() as int[];
      int n = resArray.Length;
      int start = 0;
      int end = n - 1;

      bool flag;

      timer.Start();

      do {
        flag = false;
        for (int i = start; i < end; ++i) {
          if (resArray[i] > resArray[i + 1]) {
            Swap(ref resArray[i], ref resArray[i + 1]);
            flag = true;
          }
        }

        end--;

        if (!flag) {
          break;
        }

        iterations++;

        flag = false;

        for (int i = end - 1; i >= start; --i) {
          if (resArray[i] > resArray[i + 1]) {
            Swap(ref resArray[i], ref resArray[i + 1]);
            flag = true;
          }
        }

        start++;

        iterations++;
      } while (flag);

      timer.Stop();

      return (iterations, (timer.ElapsedTicks * 1_000_000) / Stopwatch.Frequency);
    }
  }
}
