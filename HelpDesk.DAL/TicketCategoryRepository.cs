using HelpDesk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.DAL
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly HelpDeskDbContext _context;

        public TicketCategoryRepository(HelpDeskDbContext context)
        {
            _context = context;
        }

        public List<TicketCategory> GetAll()
        {
            return _context.TicketCategories.ToList();
        }
    }
}
