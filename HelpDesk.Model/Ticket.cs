using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public string IssueTitle { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public TicketCategory Category { get; set; }
        public int AssignedEmployeeId { get; set; }
        public Employee AssignedEmployee { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string? ResolutionNotes { get; set; }
        public DateTime? DateResolved { get; set; }
    }
}
