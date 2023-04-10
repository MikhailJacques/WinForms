using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SanitizeTextFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string inputFile = Path.Combine(projectDirectory, "data", "_lst_LogData_database_input.txt");
            string outputFile = Path.Combine(projectDirectory, "data", "_lst_LogData_database.txt");

            // Input and output file names (hardcoded)
            // string inputFile = "input.txt";
            // string outputFile = "output.txt";

            try
            {
                // Read all lines from the input file
                string[] lines = File.ReadAllLines(inputFile);

                // Create and open the output file for writing
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    // Process each line
                    foreach (string line in lines)
                    {
                        // Remove all white space characters from the line
                        string sanitizedLine = Regex.Replace(line, @"\s", "");

                        // Write the sanitized line to the output file
                        writer.WriteLine(sanitizedLine);
                    }
                }

                Console.WriteLine("File sanitized successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
