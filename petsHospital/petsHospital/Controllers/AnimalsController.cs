using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using petsHospital.DTOs;

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
    }
}