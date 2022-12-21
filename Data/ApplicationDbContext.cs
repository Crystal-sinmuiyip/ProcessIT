using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Areas.Admin.Models;
using Restaurant.Areas.Admin.Models.Reservation;


namespace Restaurant.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationTable> ReservationTables { get; set; }
        public DbSet<Sitting> Sittings { get; set; }
        public DbSet<SittingType> SittingTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<TableForSitting> TableForSittings { get; set; }
        public DbSet<TableReference> TableReferences { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();
        }

        public DbSet<Restaurant.Areas.Admin.Models.Reservation.Test> Test { get; set; }
    }
}
