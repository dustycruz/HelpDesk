using HelpDesk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HelpDeskDbContext _context;

        public EmployeeRepository(HelpDeskDbContext context)
        {
            _context = context;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
    }
}
