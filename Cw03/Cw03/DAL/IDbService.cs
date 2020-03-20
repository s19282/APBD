using Cw03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw03.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
