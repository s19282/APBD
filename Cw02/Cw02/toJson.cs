using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Cw02
{
    class toJson
    {
        public static void Save(List<String> data, String path)
        {
            var students = new List<Student>();
            foreach (String student in data)
            {
                string[] tmp = student.Split(",");
                students.Add(new Student(tmp[4], tmp[0], tmp[1], tmp[6], tmp[7], tmp[8], tmp[2], tmp[3], new DateTime(Int32.Parse(tmp[5].Split("-")[0]), Int32.Parse(tmp[5].Split("-")[1]), Int32.Parse(tmp[5].Split("-")[2]))));
            }
            Academy academy = new Academy(students);

            var academyWrapper = new
            {
                uczelnia = academy
            };
            var jsonString = JsonSerializer.Serialize(academyWrapper);
            File.WriteAllText(String.Concat(path + "result.json"), jsonString);
        }
    }
}
