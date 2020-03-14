using System;
using System.Collections.Generic;
using System.IO;


namespace Cw02
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string csvPath = "data.csv";
                string outPath = "result.xml";
                string outFormat = "xml";
                try
                {
                    csvPath = args[0];
                    outPath = args[1];
                    outFormat = args[2];
                }
                catch (Exception) { }

                if (File.Exists(csvPath) && Directory.Exists(outPath))
                {
                    List<String> data = checkCorrectness(csvPath);
                    switch (outFormat)
                    {
                        case "xml":
                        {
                                toXML.Save(data, outPath);
                            break;
                        }
                        case "json":
                        {
                            
                            break;
                        }
                        default:
                        {
                            Console.WriteLine("Unsupported output format");
                            break;
                        }
                    }
                }
                else
                {
                    if (!File.Exists(csvPath))
                    {
                        throw new FileNotFoundException("Plik nie istnieje");
                    }
                    if (!Directory.Exists(outPath))
                    {
                        throw new ArgumentException("Podana ścieżka jest niepoprawna");
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorLogging(ex);
            }
        }

        public static void ErrorLogging(Exception ex)
        {
            string logPath = String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\Log.txt");
            if(!File.Exists(logPath))
            {
                File.Create(logPath).Dispose();
            }
            StreamWriter sw = File.AppendText(logPath);
            sw.WriteLine("Start " + DateTime.Now);
            sw.WriteLine("Error Message: " + ex.Message);
            sw.WriteLine("END " + DateTime.Now);
            sw.Close();
        }

        public static List<String> checkCorrectness(String csvPath)
        {
            string[] students = File.ReadAllLines(csvPath);
            Dictionary<String, String> toOutputFile = new Dictionary<string, string>();

            foreach (string student in students)
            {
                if (student.Split(",").Length != 9)
                {
                    ErrorLogging(new Exception(String.Concat("[Number of columns] " + student)));
                    continue;
                }
                bool foundError = false;
                foreach (string data in student.Split(","))                {
                    if (data.Equals(""))
                    {
                        ErrorLogging(new Exception(String.Concat("[Empty values] " + student)));
                        foundError = true;
                        break;
                    }
                }
                if (foundError)
                    continue;

                String key = String.Concat(student.Split(",")[0] + "," + student.Split(",")[1] + "," + student.Split(",")[4]);
                
                if (toOutputFile.ContainsKey(key))
                {
                    ErrorLogging(new Exception(String.Concat("[Duplicate values] " + student)));
                }
                else
                {
                    toOutputFile.Add(key, student);
                }
            }
            return new List<string>(toOutputFile.Keys);
        }

    }
}
