using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using petsHospital.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace petsHospital.DTOs.Requests
{
    public class AddAnimalRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string AdmissionDate { get; set; }
        [Required]
        public int IdOwner { get; set; }
        public List<Procedure> procedures { get; set; }
    }
}
