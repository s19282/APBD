using System;
using System.Xml.Serialization;

namespace Cw02
{
    public class activeStudies
    {
        public activeStudies(string name, int numberOfStudents)
        {
            this.numberOfStudents = numberOfStudents;
            this.name = name;
        }
        public activeStudies()
        {
            name = null;
            numberOfStudents = 0;
        }
        [XmlAttribute]

        public String name { get; set; }
        [XmlAttribute]
        public int numberOfStudents { get; set; }
    }
}
