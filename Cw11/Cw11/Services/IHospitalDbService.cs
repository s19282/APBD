using Cw11.Models;
using System.Collections.Generic;

namespace Cw11.Services
{
    public interface IHospitalDbService
    {
        public IEnumerable<Doctor> GetDoctors();
    }
}
