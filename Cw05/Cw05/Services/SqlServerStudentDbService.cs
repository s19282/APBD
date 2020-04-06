using Cw05.DTOs.Requests;
using Cw05.DTOs.Responses;
using Cw05.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cw05.Services
{
    public class SqlServerStudentDbService : IStudentDbService
    {
        public void EnrollStudent(EnrollStudentRequest request)
        {
            var st = new Student(request);
            st.FirstName = request.FirstName;


            using (var con = new SqlConnection(""))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();

                try
                {
                    com.CommandText = "select IdStudies from studies where name=@name";
                    com.Parameters.AddWithValue("name", request.Studies);

                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        tran.Rollback();
                        //return BadRequest("Studia nie istnieją");
                    }
                    int idStudies = (int)dr["IdStudies"];

                    com.CommandText = "insert into student(IndexNumber,Firstname) values(@index,@fname)";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    com.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (SqlException exc)
                {
                    tran.Rollback();
                }
            }
            var response = new EnrollStudentResponse();
            response.LastName = request.FirstName;


            //return Ok(response);
        }


        public void PromoteStudents(int semester, string studies)
        {
            throw new NotImplementedException();
        }
    }
}
