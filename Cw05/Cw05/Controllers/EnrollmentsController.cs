﻿using Cw05.DTOs.Requests;
using Cw05.DTOs.Responses;
using Cw05.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cw05.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    [Authorize(Roles ="employee")]
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
            EnrollStudentResponse response = _service.EnrollStudent(request);
            if(response!=null)
                return Ok(response);
            return BadRequest(400);
        }
        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            PromoteStudentsResponse response = _service.PromoteStudents(request);
            if (response != null)
                return Ok(response);
            return BadRequest(404);
        }

    }
}