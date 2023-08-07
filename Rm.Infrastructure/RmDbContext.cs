using Microsoft.EntityFrameworkCore;
using Rm.Core.Entity;
using System;

namespace Rm.Infrastructure
{
    public class RmDbContext : DbContext
    {
        public RmDbContext(DbContextOptions<RmDbContext> options)
       : base(options)
        {
           // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Table> Table { get; set; }
        public DbSet<Reservation> Reservation { get; set; }

        public DbSet<ReservationTable> ReservationTable { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().HasKey(x => x.Id);
            modelBuilder.Entity<Table>().HasKey(x => x.Id);
            modelBuilder.Entity<ReservationTable>().HasKey(x => x.Id);

            modelBuilder.Entity<Table>().HasData(
                new Table() { Id=Guid.NewGuid(), TableName="Table -1", Capacity=5 },
                new Table() { Id=Guid.NewGuid(), TableName="Table -2", Capacity=4 },
                new Table() { Id=Guid.NewGuid(), TableName="Table -3", Capacity=3 }
      );

            base.OnModelCreating(modelBuilder);
        }

    }
}

