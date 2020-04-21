﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Cw05.DTOs.Requests;
using Cw05.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Cw05.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        public StudentsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [HttpGet]
        //[Authorize(Roles = "admin")]
        public IActionResult GetStudents()
        {
            var list = new List<Student>();

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19282;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                try
                {
                    com.CommandText = "select * from Student join enrollment on student.idEnrollment=enrollment.idEnrollment join studies on enrollment.idStudy=studies.idStudy";
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        return null;
                    }
                    while(dr.Read())
                    {
                        list.Add(new Student("s" + dr["IndexNumber"], (string)dr["FirstName"], (string)dr["LastName"], (DateTime)dr["BirthDate"], (string)dr["Name"]));
                    }
                    dr.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }

            return Ok(list);
        }

        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            string login = request.Login;
            string password = request.Password;
            string id;
            string name;
           
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19282;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                try
                {
                    com.CommandText = "select * from Student where IndexNumber=@login";
                    com.Parameters.AddWithValue("login", login);
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        return NotFound("user not found");
                    }
                    var validate = BCrypt.Net.BCrypt.Verify(password, (string)dr["Password"]);
                    if (!validate)
                        return NotFound("wrong password");
                    id = (string)dr["IndexNumber"];
                    name = (string)dr["FirstName"];
                    dr.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }

            var claims = new[]
{
                new Claim(ClaimTypes.NameIdentifier, "s"+id),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "employee")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            }); ;
        }

        [HttpPost("refresh")]
        public IActionResult renewBearerToken(LoginRequest request)
        {
            string login = request.Login;
            string password = request.Password;
            string id;
            string name;

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19282;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                try
                {
                    com.CommandText = "select * from Student where IndexNumber=@login";
                    com.Parameters.AddWithValue("login", login);
                    var dr = com.ExecuteReader();
                    if (!dr.Read())
                    {
                        dr.Close();
                        return NotFound("user not found");
                    }
                    var validate = BCrypt.Net.BCrypt.Verify(password, (string)dr["Password"]);
                    if (!validate)
                        return NotFound("wrong password");
                    id = (string)dr["IndexNumber"];
                    name = (string)dr["FirstName"];
                    dr.Close();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }

            var claims = new[]
{
                new Claim(ClaimTypes.NameIdentifier, "s"+id),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "employee")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            }); ;
        }

    }
}