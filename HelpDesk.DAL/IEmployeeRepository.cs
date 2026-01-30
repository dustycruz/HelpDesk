using HelpDesk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.DAL
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
    }
}
