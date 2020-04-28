using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using petsHospital.DTOs;
using petsHospital.DTOs.Requests;
using petsHospital.Models;

namespace petsHospital.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAnimals(String sortBy)
        {
            var list = new List<GetAnimalResponse>();

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19282;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                try
                {
                    if (sortBy == null)
                        sortBy = "AdmissionDate desc";
                    com.CommandText = "select * from Animal inner join Owner on Owner.idOwner=Animal.idOwner order by "+sortBy;
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        return BadRequest();
                    }
                    while (dr.Read())
                    {
                        list.Add(new GetAnimalResponse((String)dr["Name"], (String)dr["Type"], (DateTime)dr["AdmissionDate"], (String)dr["LastName"]));
                    }
                    dr.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest(e.Message);
                }
            }
            return Ok(list);
        }
        [HttpGet("add")]
        public IActionResult AddAnimals(AddAnimalRequest animal)
        {
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19282;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();
                try
                {
                    com.Transaction = tran;
                    com.CommandText = "insert into Animal values(@name,@type,@date,@owner)";
                    com.Parameters.AddWithValue("name", animal.Name);
                    com.Parameters.AddWithValue("type", animal.Type);
                    com.Parameters.AddWithValue("date", animal.AdmissionDate);
                    com.Parameters.AddWithValue("owner", animal.IdOwner);
                    var dr = com.ExecuteReader();
                    dr.Close();
                    int animalID;
                    com.CommandText = "select idAnimal from Animal where name=@name and type=@type and admissiondate=@date and idowner=@owner";
                    dr = com.ExecuteReader();
                    animalID = (int)dr["idAnimal"];
                    dr.Close();
                    foreach(Procedure p in animal.procedures)
                    {
                        com.CommandText = "insert into Procedure values(@name,@description)";
                        com.Parameters.AddWithValue("name", p.Name);
                        com.Parameters.AddWithValue("description", p.Description);
                        dr = com.ExecuteReader();
                        dr.Close();
                        int procedureID;
                        com.CommandText = "select idProcedure from procedure where name=@name and description=@description";
                        dr = com.ExecuteReader();
                        procedureID = (int)dr["idProcedure"];
                        dr.Close();
                        com.CommandText = "insert into Procedure_Animal values(@p, @a, @d)";
                        com.Parameters.AddWithValue("p", procedureID);
                        com.Parameters.AddWithValue("a", animalID);
                        com.Parameters.AddWithValue("d", DateTime.Today.ToString("yyyy-MM-dd"));
                        dr = com.ExecuteReader();
                        dr.Close();
                        tran.Commit();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    tran.Rollback();
                    return BadRequest(e.Message);
                }
            }
            return Ok();
        }
    }
}