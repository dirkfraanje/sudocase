using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SuDoCase
{
    public class SuDoCaseEntry : Entry
    {
        public int RowNumber { get; private set; }
        public int ColumnNumber { get; private set; }
        public int GridNumber { get;  private set; }
        public bool Gamble { get; set; }
        public bool GambleLevel2 { get; set; }
        public bool GambleLevel3 { get; set; }
        public HashSet<int> GamblesTried { get; set; }
        public HashSet<int> GamblesTriedLevel2 { get; set; }
        public HashSet<int> GamblesTriedLevel3 { get; set; }
        public HashSet<int> Hint { get; set; }
        public bool CanRestoreHint { get; set; }
        public SuDoCaseEntry(int r, int c, int g)
        {
            RowNumber = r;
            ColumnNumber = c;
            GridNumber = g;
            Hint = new HashSet<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            GamblesTried = new HashSet<int>();
            GamblesTriedLevel2 = new HashSet<int>();
            GamblesTriedLevel3 = new HashSet<int>();
        }
    }
}
