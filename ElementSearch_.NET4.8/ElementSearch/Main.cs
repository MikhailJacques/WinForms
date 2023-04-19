using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace ElementSearch
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ElementSearch());
            }
            catch (Exception ex)
            {
                string logFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "error_log.txt");

                // Log the exception details to a file or the console
                File.WriteAllText(logFilePath, ex.ToString());
            }
        }
    }
}

