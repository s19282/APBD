using Cw11.Models;
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
    }
}
