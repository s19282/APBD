using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw03.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw03.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public string GetStudent(string orderBy)
        {
            return $"Kowalski, Malewski, Andrzejewski sortowanie={orderBy}";
        }
        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }
        [HttpPut("{id}")]
        public IActionResult updateStudent(int id)
        {
            if(id == 1)
            {
                return Ok("Aktualizacja zakończona");
            }
            return NotFound("Nie znaleziono studenta");
        }
        [HttpDelete("{id}")]
        public IActionResult deleteStudent(int id)
        {
            if (id == 2)
            {
                return Ok("Usuwanie ukończone");
            }
            return NotFound("Nie znaleziono studenta");
        }
    }
}