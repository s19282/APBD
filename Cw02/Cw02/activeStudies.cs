using System;

namespace Cw02
{
    public class activeStudies
    {
        public activeStudies(string name, int numberOfStudents)
        {
            this.numberOfStudents = numberOfStudents;
            this.name = name;
        }
        public String name { get; set; }
        public int numberOfStudents { get; set; }
    }
}
