using System;
using System.IO;
using System.Linq;

namespace ProcessTextFile
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            string inputFile = Path.Combine(projectDirectory, "data", "_lst_LogData_elements.txt");
            string outputFile = Path.Combine(projectDirectory, "data", "_lst_LogData_elements_new.txt");

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
                        // Split the line into an array of strings using the '@' delimiter
                        string[] parameters = line.Split('@');

                        // Process the 4th, 5th, and 6th parameters (indexes 3, 4, and 5)
                        for (int i = 3; i <= 5; i++)
                        {
                            // Parse the numeric value and reduce it by 1000s as necessary
                            int value = int.Parse(parameters[i]);

                            value %= 1000;

                            // Replace the original parameter value with the modified value
                            parameters[i] = value.ToString();
                        }

                        // Join the parameters back together using the '@' delimiter
                        string modifiedLine = string.Join("@", parameters);

                        // Write the modified line to the output file
                        writer.WriteLine(modifiedLine);
                    }
                }

                Console.WriteLine("File processed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}