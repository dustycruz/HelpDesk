using HelpDesk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.DAL
{
    public interface ITicketRepository
    {
        List<Ticket> GetAll(string? status = null, int? categoryId = null, string? keyword = null);
        Ticket Get(int id);
        void Add(Ticket ticket);
        void Update(Ticket ticket);
        void Delete(int id);
        int Save();
    }
}
