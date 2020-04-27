using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petsHospital.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }
        public String Name { get; set; }
        public String  Type { get; set; }
        public DateTime AdmissionDate { get; set; }
        public int OwnerID { get; set; }

        public Animal()
        {}

        public Animal(int idAnimal, string name, string type, DateTime admissionDate, int ownerID)
        {
            IdAnimal = idAnimal;
            Name = name;
            Type = type;
            AdmissionDate = admissionDate;
            OwnerID = ownerID;
        }
    }
}
