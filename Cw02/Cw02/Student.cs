using System;

namespace Cw02
{
    [Serializable]
    public class Student
    {
        public Student(string indexNumber, string fname, string lname, string email, string mothersName, string fathersName, string studiesName, string studiesMode, DateTime birthDate)
        {
            this.indexNumber = "s"+indexNumber;
            this.fname = fname;
            this.lname = lname;
            this.email = email;
            this.mothersName = mothersName;
            this.fathersName = fathersName;
            studies = new Studies(studiesName, studiesMode);
            this.birthDate = birthDate;
        }

        public String indexNumber { get; set; }
        public String fname { get; set; }
        public String lname { get; set; }
        public DateTime birthDate { get; set; }
        public String email { get; set; }
        public String mothersName { get; set; }
        public String fathersName { get; set; }
        public Studies studies { get; set; }
    }
}
