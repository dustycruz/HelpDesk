using HelpDesk.Model;
using HelpDesk.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.BLL
{
    public interface ITicketService
    {
        List<DTO.Ticket> GetAll(string? status, int? categoryId, string? keyword);
        (bool isOk, string message) Add(Model.Ticket ticket);
        (bool isOk, string message) Update(Model.Ticket ticket);
        (bool isOk, string message) Delete(int ticketId);
    }
}
