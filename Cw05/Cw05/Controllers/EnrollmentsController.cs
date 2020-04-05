using Cw05.DTOs.Requests;
using Cw05.DTOs.Responses;
using Cw05.Models;
using Cw05.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Cw05.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentDbService _service;

        public EnrollmentsController(IStudentDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            
            var response = new EnrollStudentResponse();
            //response.LastName = request.FirstName;


            return Ok(response);
        }
        //52min
    }
}