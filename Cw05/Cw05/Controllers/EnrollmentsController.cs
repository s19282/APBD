using Cw05.DTOs.Requests;
using Cw05.DTOs.Responses;
using Cw05.Models;
using Cw05.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;

namespace Cw05.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var student = new Student(request);
            var response = new EnrollStudentResponse();
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19282;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                try
                {
                    com.CommandText = "select IdStudy from Studies where Studies.Name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);
                    var dr = com.ExecuteReader();

                    if (!dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        return BadRequest("Studia nie istnieją");
                    }
                    int idStudies = (int)dr["IdStudy"];
                    dr.Close();
                    com.CommandText = "select IdEnrollment,StartDate from Enrollment where Semester=1 and " +
                        "idStudy=@idStudy and StartDate=(select max(startDate) from Enrollment " +
                        "where Semester=1 and idStudy=@idStudy)";
                    com.Parameters.AddWithValue("idStudy", idStudies);
                    dr = com.ExecuteReader();
                   
                    int idEnrollment = -1;
                    DateTime date;

                    if (!dr.Read())
                    {
                        dr.Close();
                        com.CommandText = "select max(IdEnrollment) as 'id' from Enrollment";
                        dr = com.ExecuteReader();
                        if (dr.Read())
                            idEnrollment = (int)dr["id"]+1;
                        else
                            idEnrollment = 1;
                        dr.Close();
                        com.CommandText = "Insert into Enrollment(IdEnrollment,Semester,IdStudy,StartDate) " +
                            "values (@idEnrollment,1,@idStudy,@date);";
                        date = DateTime.Now;
                        com.Parameters.AddWithValue("date", date.ToString("yyyy-mm-dd"));
                        com.Parameters.AddWithValue("idEnrollment", idEnrollment);
                        dr = com.ExecuteReader();
                        dr.Close();
                    }
                    else
                    {
                        date = (DateTime)dr["StartDate"];
                        //date = DateTime.Parse((string)dr["StartDate"]);
                        idEnrollment = (int)dr["IdEnrollment"];
                        dr.Close();
                    }

                    com.CommandText = "select * from Student where IndexNumber=@index";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    dr = com.ExecuteReader();
                    if(dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        return BadRequest("Student o podanym indeksie już istnieje!");
                    }
                    dr.Close();
                    response.IdEnrollment = idEnrollment;
                    response.IdStudy = idStudies;
                    response.Semester = 1;
                    response.StartDate = date;

                    com.CommandText = "insert into Student(IndexNumber,Firstname,LastName,BirdthDate,IdEnrollment)" +
                        " values(@index,@fname,@lname,@bDate,@enrollId)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    com.Parameters.AddWithValue("fname", request.FirstName);
                    com.Parameters.AddWithValue("lname", request.LastName);
                    com.Parameters.AddWithValue("bDate", request.BirthDate);
                    com.Parameters.AddWithValue("enrollId", idEnrollment);
                    com.ExecuteNonQuery();


                    dr.Close();
                    tran.Commit();
                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }

            return Ok(response);
        }
    }
}