using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Cw02
{
    public partial class Academy
    {
        public Academy(List<Student> students)
        {
            date = DateTime.Now.ToString("dd.MM.yyyy");
            author = "Mateusz Godlewski";
            this.students = students.ToArray();
            List<activeStudies> sadf = new List<activeStudies>();
            var tmp = from abc in students
                      group abc by abc.studies.name into groups
                      select new activeStudies(groups.Key, groups.Count());
            foreach(var abc in tmp)
            {
                sadf.Add(new activeStudies(abc.name, abc.numberOfStudents));
            }
            activeStudies = sadf.ToArray();
        }

        [JsonPropertyName("createdAt")]
        public string date { get; set; }
        public string author { get; set; }
        [JsonPropertyName("studenci")]
        public Student[] students { get; set; }
        public activeStudies[] activeStudies { get; set; }



    }
}
