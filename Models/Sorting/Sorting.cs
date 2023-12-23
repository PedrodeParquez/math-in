using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace math_in.Models.Sorting {
  public class Sorting {
    public static void Swap (ref int e1, ref int e2) {
        var temp = e1;
        e1 = e2;
        e2 = temp;
    }
  } 
}
