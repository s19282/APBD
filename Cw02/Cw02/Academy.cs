using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Cw02
{
    public partial class Academy
    {
        public Academy(List<Student> students)
        {
            date = DateTime.Now.ToString("dd.MM.yyyy");
            author = "Mateusz Godlewski";
            this.students = students.ToArray();
            //Do poprawy///////
            List<activeStudies> list = new List<activeStudies>();
            var tmp = from student in students
                      group student by student.studies.name into groups
                      select new activeStudies(groups.Key, groups.Count());
            foreach(var studies in tmp)
            {
                list.Add(new activeStudies(studies.name, studies.numberOfStudents));
            }
            activeStudies = list.ToArray();
            ///////////////////
        }
        public Academy()
        {
            date = null;
            author = null;
            students = null;
            activeStudies = null;
        }

        [JsonPropertyName("createdAt")]
        [XmlAttribute(AttributeName = "createdAt")]
        public string date { get; set; }
        [XmlAttribute]
        public string author { get; set; }
        [JsonPropertyName("studenci")]
        [XmlArray("studenci")]
        public Student[] students { get; set; }
        [XmlArrayItem("studies")]
        public activeStudies[] activeStudies { get; set; }



    }
}
