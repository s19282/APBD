using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cw02
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string csvPath = "data.csv";
                string xmlPath = "result.xml";
                string outFormat = "xml";
                try
                {
                    csvPath = args[0];
                    xmlPath = args[1];
                    outFormat = args[2];
                }
                catch (Exception) { }

                if (File.Exists(csvPath) && Directory.Exists(xmlPath))
                {
                    string[] students = File.ReadAllLines(csvPath);
                    List<String> toLog = new List<string>();
                    Dictionary<String,String> toOutputFile = new Dictionary<string, string>();

                    foreach (string student in students)
                    {
                        if (student.Split(",").Length != 9)
                            toLog.Add(String.Concat("[Number of columns] " + student));
                        foreach (string data in student.Split(","))
                        {
                            if (data.Equals(""))
                            {
                                toLog.Add(String.Concat("[Empty values] " + student));
                                break;
                            }
                        }
                        string key = String.Concat(student.Split(",")[0] + "," + student.Split(",")[1] + "," + student.Split(",")[4]);
                        if (toOutputFile.ContainsKey(key))
                        {
                            toLog.Add(String.Concat("[Duplicate values] " + student));
                        }
                        else
                        {
                            toOutputFile.Add(key, student);
                        }
                    }
                    foreach(string error in toLog)
                    {
                        ErrorLogging(new Exception(error));
                    }


                    XElement xml = new XElement("Uczelnia",
                        new XAttribute("createdAt", DateTime.Now.ToString("dd/MM/yyyy")),
                        new XAttribute("author", "Mateusz Godlewski"),
                        from str in toOutputFile.Values
                        let fields = str.Split(",")
                        select new XElement("studenci",
                            new XAttribute("indexNumber", "s" + fields[4]),
                            new XElement("fname", fields[0]),
                            new XElement("lname", fields[1]),
                            new XElement("birthdate", new DateTime(Int32.Parse(fields[5].Split("-")[0]), Int32.Parse(fields[5].Split("-")[1]), Int32.Parse(fields[5].Split("-")[2])).ToString("dd.MM.yyyy")),
                            new XElement("e-mails", fields[6]),
                            new XElement("mothersname", fields[7]),
                            new XElement("fathersname", fields[8]),
                            new XElement("studies",
                                new XElement("name", fields[2]),
                                new XElement("mode", fields[3])
                                )
                            ),
                        new XElement("activeStudies",
                            from str in toOutputFile.Values
                            let fields = str.Split(",")
                            group fields by fields[2] into groupped
                            select new XElement("studies",
                                new XAttribute("name", groupped.Key),
                                new XAttribute("numberOfStudents",groupped.Count())
                                )
                           )
                        );
                    xml.Save(String.Concat(xmlPath + "result.xml"));
                }
                else
                {
                    if (!File.Exists(csvPath))
                    {
                        throw new FileNotFoundException("Plik nie istnieje");
                    }
                    if (!Directory.Exists(xmlPath))
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
            sw.WriteLine("Start" + DateTime.Now);
            sw.WriteLine("Error Message: " + ex.Message);
            sw.WriteLine("END" + DateTime.Now);
            sw.Close();
        }

    }
}
