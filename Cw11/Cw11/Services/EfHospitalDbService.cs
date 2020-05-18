using Cw11.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw11.Services
{
    public class EfHospitalDbService : IHospitalDbService
    {
        private readonly HospitalDbContext _context;
        public EfHospitalDbService(HospitalDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Doctor> GetDoctors()
        {
            return _context.Doctors.ToList();
        }
        public string AddDoctor(Doctor doctor)
        {
            try
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return "Failure "+e.Message+" "+e.InnerException.Message;
            }
            return "Succes";
        }

        public string ModifyDoctor(Doctor doctor)
        {
            var d = _context.Doctors.Where(d => d.IdDoctor == doctor.IdDoctor).FirstOrDefault();
            if (d == null) return "Incorrect doctor ID";
            if (doctor.FirstName != null)
                d.FirstName = doctor.FirstName;
            if (doctor.LastName != null)
                d.LastName = doctor.LastName;
            if (doctor.Email != null)
                d.Email = doctor.Email;
            _context.SaveChanges();
            return "Data changed";
        }

        public string DropDoctor(int IdDoctor)
        {
            var d = _context.Doctors.Where(d => d.IdDoctor == IdDoctor).FirstOrDefault();
            if (d == null) return "Incorrect doctor ID";
            _context.Doctors.Remove(d);
            _context.SaveChanges();
            return "Doctor removed";
        }
    }
}
