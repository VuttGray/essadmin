using ESS.Admin.Core.Domain.Administration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ESS.Admin.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(e => new { e.RecordId });
            modelBuilder.Entity<Message>()
                .HasKey(e => new { e.RecordId });
            modelBuilder.Entity<Message>()
                .Property(e => e.Recipients)
                .HasConversion(v => string.Join(";", v), v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList());
            modelBuilder.Entity<Message>()
                .Property(e => e.CcRecipients)
                .HasConversion(v => string.Join(";", v), v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList());
            modelBuilder.Entity<Message>()
                .Property(e => e.BccRecipients)
                .HasConversion(v => string.Join(";", v), v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList());
            modelBuilder.Entity<Message>()
                .Property(e => e.Attachments)
                .HasConversion(v => string.Join(",", v), v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
