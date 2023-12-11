using Microsoft.EntityFrameworkCore;

namespace CoreWeb_MVC.Models
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext (DbContextOptions<TicketDbContext> options) : base(options) { }

        public DbSet<Ticket> tickets { get; set; }

    }
}
