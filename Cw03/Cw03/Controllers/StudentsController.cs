using System;
using Cw03.DAL;
using Cw03.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw03.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            using (var con = new SqlConnection("[Data Source=db-mssql;Initail Catalog=s19282;Integrated Security=True]"))
            using (var com = new SqlCommand() )
            {
                com.Connection = con;
                com.CommandText = "select * from Students";

                con.Open();
                var dr = con.ExecuteReader();
                while(dr.Read())
                {
                    var st = new Student();
                    st.FirstName = dr["FirstName"].ToString();
                }
            }
            return Ok(_dbService.GetStudents());
        }
        [HttpGet("{IndexNumber}")]
        public IActionResult GetStudentsSemester(String IndexNumber)
        {
            string res = "";
                        using (var con = new SqlConnection("[Data Source=db-mssql;Initail Catalog=s19282;Integrated Security=True]"))
            using (var com = new SqlCommand() )
            {
                com.Connection = con;
                com.CommandText = "select * from Enrollment inner join Student on Enrollment.idEnrollment=Student.idEnrollment where IndexNumber=@IndexNumber";
                com.Parameters.AddWithValue("IndexNumber",IndexNumber);
                con.Open();
                var dr = con.ExecuteReader();
                while(dr.Read())
                {
                    res+="Semester: "+dr["Semester"]+", Start Date: "+dr["StartDate"].ToString();
                }
                return Ok(res);
            }

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