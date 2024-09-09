namespace ElementSearch
{
    internal static class MainProgram
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormElementSearch());
        }

        //private static int cnt = 0;
        //private static int temp = 7;

        //static int Sum(int var1, int var2)
        //{
        //    cnt++;

        //    int sum = var1 + var2;

        //    return sum + cnt;
        //}

        //static void Kewl()
        //{
        //    for (var ii = 0; ii < 5; ii++)
        //    {
        //        Console.WriteLine("Hello " + ii);
        //    }
        //}

    }
}