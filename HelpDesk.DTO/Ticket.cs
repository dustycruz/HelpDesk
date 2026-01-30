using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.DTO
{
    public class Ticket
    {
        public int Id { get; set; }
        public string IssueTitle { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string AssignedEmployee { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public string? ResolutionNotes { get; set; }
        public DateTime? DateResolved { get; set; }
    }
}
