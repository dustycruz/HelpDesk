using HelpDesk.DAL;
using HelpDesk.DTO;
using HelpDesk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.BLL
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public List<DTO.Ticket> GetAll(string? status, int? category, string? keyword)
        {
            return _ticketRepository
                .GetAll(status, category, keyword)
                .Select(m => new DTO.Ticket 
                { 
                    Id = m.Id,
                    IssueTitle = m.IssueTitle,
                    Description = m.Description,
                    Category = m.Category.Name,
                    AssignedEmployee = m.AssignedEmployee.FullName,
                    Status = m.Status,
                    DateCreated = m.DateCreated
                })
                .ToList();
        }

        public (bool isOk, string message) Add(Model.Ticket ticket)
        {
            try
            {
                if (string.IsNullOrEmpty(ticket.IssueTitle))
                    return (false, "Title must not be empty!");

                if (ticket.CategoryId == null || ticket.CategoryId == 0)
                    return (false, "Category must be selected!");

                if (string.IsNullOrEmpty(ticket.Status))
                    return (false, "Status must be selected!");

                ticket.DateCreated = DateTime.Now;

                if (ticket.Status == "New")
                {
                    ticket.ResolutionNotes = null;
                    ticket.DateResolved = null;
                }

                if (ticket.Status == "In-Progress")
                {
                    ticket.DateResolved = null;
                }

                if (ticket.Status == "Resolved" || ticket.Status == "Closed")
                {
                    if (string.IsNullOrEmpty(ticket.ResolutionNotes))
                        return (false, "Resolution must not be empty!");

                    if (ticket.AssignedEmployeeId == null)
                        return (false, "Employee must be selected!");

                    ticket.DateResolved = DateTime.Now;

                    if (ticket.DateResolved < ticket.DateCreated)
                        return (false, "Date Resolved cannot be earlier than Date Created!");
                }

                _ticketRepository.Add(ticket);
                _ticketRepository.Save();

                return (true, "Ticket added successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error adding ticket: {ex.Message}");
            }
        }

        public (bool isOk, string message) Update(Model.Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public (bool isOk, string message) Delete(int ticketId)
        {
            throw new NotImplementedException();
        }
    }
}
