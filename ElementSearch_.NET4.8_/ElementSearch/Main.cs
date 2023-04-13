using System;
using System.Windows.Forms;

namespace ElementSearch
{
    internal static class MainProgram
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormElementSearch());
        }
    }
}