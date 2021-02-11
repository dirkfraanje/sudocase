using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SuDoCase
{
    public class SuDoCaseLabel : Label
    {
        public int RowNumber { get; private set; }
        public int ColumnNumber { get; private set; }
        public int GridNumber { get; private set; }

        public SuDoCaseLabel(int r, int c, int g)
        {
            RowNumber = r;
            ColumnNumber = c;
            GridNumber = g;
        }
    }
}
