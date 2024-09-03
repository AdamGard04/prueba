using Microsoft.EntityFrameworkCore;
using prueba_banco_guayaquil.Models;

namespace prueba_banco_guayaquil.ConectionBD
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options) { }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
