using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence.Contexts
{
    public class OrderContext : DbContext
    {
        private const string STANDARD_CREATEDBY_USER = "danielsouza21";

        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; } //Table Entity

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // Set default entity base column values for save (add or modify) commands

            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = STANDARD_CREATEDBY_USER;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = STANDARD_CREATEDBY_USER;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
