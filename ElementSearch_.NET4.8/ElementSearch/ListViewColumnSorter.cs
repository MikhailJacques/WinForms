
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ElementSearch
{
    public class ListViewColumnSorter : IComparer
    {
        public int Column { get; set; }
        public SortOrder Order { get; set; }
        public HashSet<int> NumericColumns { get; set; }

        public ListViewColumnSorter()
        {
            Column = 0;
            Order = SortOrder.None;
            NumericColumns = new HashSet<int>();
        }

        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listViewX = (ListViewItem)x;
            ListViewItem listViewY = (ListViewItem)y;

            if (NumericColumns.Contains(Column))
            {
                // Compare as numeric values
                bool xIsNumber = int.TryParse(listViewX.SubItems[Column].Text, out int xValue);
                bool yIsNumber = int.TryParse(listViewY.SubItems[Column].Text, out int yValue);

                if (xIsNumber && yIsNumber)
                {
                    compareResult = xValue.CompareTo(yValue);
                }
                else if (!xIsNumber && !yIsNumber)
                {
                    compareResult = String.Compare(listViewX.SubItems[Column].Text, listViewY.SubItems[Column].Text);
                }
                else
                {
                    compareResult = xIsNumber ? -1 : 1;
                }
            }
            else
            {
                // Compare as strings
                compareResult = String.Compare(listViewX.SubItems[Column].Text, listViewY.SubItems[Column].Text);
            }

            // If the order is descending, invert the result.
            if (Order == SortOrder.Descending)
            {
                compareResult = -compareResult;
            }

            return compareResult;
        }
    }
}
