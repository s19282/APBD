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
                        //return BadRequest("Studia nie istnieją");
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
                        date = DateTime.Today;
                        dr.Close();
                        com.CommandText = "select max(IdEnrollment) as 'id' from Enrollment";
                        dr = com.ExecuteReader();
                        if (dr.Read())
                            idEnrollment = (int)dr["id"] + 1;
                        else
                            idEnrollment = 1;
                        dr.Close();
                        com.CommandText = "Insert into Enrollment(IdEnrollment,Semester,IdStudy,StartDate) " +
                            "values (@idEnrollment,1,@idStudy,@date)";
                        com.Parameters.AddWithValue("date", date.ToString("yyyy-MM-dd"));
                        com.Parameters.AddWithValue("idEnrollment", idEnrollment);
                        dr = com.ExecuteReader();
                        dr.Close();
                    }
                    else
                    {
                        date = (DateTime)dr["StartDate"];
                        idEnrollment = (int)dr["IdEnrollment"];
                        dr.Close();
                    }

                    com.CommandText = "select * from Student where IndexNumber=@index";
                    com.Parameters.AddWithValue("index", request.IndexNumber);
                    dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        tran.Rollback();
                        //return BadRequest("Student o podanym indeksie już istnieje!");
                    }
                    dr.Close();
                    //response.IdEnrollment = idEnrollment;
                    //response.IdStudy = idStudies;
                    //response.Semester = 1;
                    //response.StartDate = date;

                    com.CommandText = "insert into Student(IndexNumber,Firstname,LastName,BirthDate,IdEnrollment)" +
                        " values(@index,@fname,@lname,@bDate,@idEnrollment)";
                    com.Parameters.AddWithValue("fname", request.FirstName);
                    com.Parameters.AddWithValue("lname", request.LastName);
                    com.Parameters.AddWithValue("bDate", request.BirthDate.ToString("yyyy-MM-dd"));
                    com.ExecuteNonQuery();

                    dr.Close();
                    tran.Commit();
                }
                catch (SqlException exc)
                {
                    Console.WriteLine(exc.Message);
                    tran.Rollback();
                }
            }
        }


        public void PromoteStudents(PromoteStudentsRequest request)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19282;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                try
                {
                    com.CommandText = "PromoteStudents";
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Studies", request.Studies);
                    com.Parameters.AddWithValue("@Semester", request.Semester);
                    var dataSet = new System.Data.DataSet();
                    using (var adapter = new SqlDataAdapter(com))
                    {
                        adapter.Fill(dataSet);
                    }
                    var dr = com.ExecuteNonQuery();
                    Console.WriteLine("test");
                }
                catch (SqlException exc)
                {
                    Console.WriteLine(exc.Message);
                    //return NotFound("404");
                }
            }
    }
}
