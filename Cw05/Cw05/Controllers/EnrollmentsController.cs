using Cw05.DTOs.Requests;
using Cw05.DTOs.Responses;
using Cw05.Models;
using Cw05.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Web;

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
            EnrollStudentResponse esr;
            var db = new s19282Context();

            var studies = db.Studies.Where(s => s.Name.Equals(request.Studies)).FirstOrDefault();
            if (studies==null)
                return BadRequest("Studia nie istnieją");

            var enrollment = db.Enrollment.Where(s => s.IdStudy == studies.IdStudy && s.Semester==1).OrderBy(s => s.StartDate).First();
            
            if (enrollment == null)
            {
                enrollment = new Enrollment
                {
                    IdEnrollment = db.Enrollment.OrderBy(e => e.IdEnrollment).Last().IdEnrollment + 1,
                    Semester = 1,
                    IdStudy = studies.IdStudy,
                    StartDate=DateAndTime.Today
                };
                db.Attach(enrollment);
                db.Entry(enrollment).State = EntityState.Modified;
            }
            var student = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                IdEnrollment = enrollment.IdEnrollment
            };
            db.Attach(student);
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();

            esr = new EnrollStudentResponse(enrollment);
            return Created("",esr);
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(PromoteStudentsRequest request)
        {
            var db = new s19282Context();
            var studies = db.Enrollment
                .Join(db.Studies, e => e.IdStudy, s => s.IdStudy, (e, s) => new { s.Name, s.IdStudy, e.Semester })
                .Where(e => e.Semester == request.Semester && e.Name.Equals(request.Studies)).FirstOrDefault();
            if (studies == null)
                return NotFound();
            db.ExecuteSqlRaw("Exec promoteStudents ..............");
            return Ok();
        }

    }
}