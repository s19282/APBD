using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petsHospital.DTOs
{
    public class GetAnimalResponse
    {
        public String Name { get; set; }
        public String AnimalType { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public String LastNameOfOwner { get; set; }

        public GetAnimalResponse(string name, string animalType, DateTime dateOfAdmission, string lastNameOfOwner)
        {
            Name = name;
            AnimalType = animalType;
            DateOfAdmission = dateOfAdmission;
            LastNameOfOwner = lastNameOfOwner;
        }
    }
}
