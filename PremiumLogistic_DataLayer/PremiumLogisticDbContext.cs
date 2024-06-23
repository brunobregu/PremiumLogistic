using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace PremiumLogistic_DataLayer;

public class PremiumLogisticDbContext : IdentityDbContext<ApplicationUser>
{
    public PremiumLogisticDbContext(DbContextOptions<PremiumLogisticDbContext> options) : base(options)
    {
    }

    public DbSet<Port> Ports { get; set; }
    public DbSet<LocalTransportation> LocalTransportations { get; set; }
    public DbSet<AuditLogs> AuditLogs { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(user =>
        {
            user.Property(p => p.CreatedOn).IsRequired();
        });

        builder.Entity<ApplicationRole>(user =>
        {
            user.Property(p => p.CreatedOn).IsRequired();
        });

        builder.Entity<ApplicationUser>()
        .HasMany(e => e.OrderDetails)
        .WithOne(e => e.User)
        .HasForeignKey(e => e.UserId)
        .HasPrincipalKey(e => e.Id);

        #region InvalidatedQueryFilter
        builder.Entity<Port>().HasQueryFilter(x => !x.Invalidated);
            builder.Entity<LocalTransportation>().HasQueryFilter(x => !x.Invalidated);
            builder.Entity<OrderDetails>().HasQueryFilter(x => !x.Invalidated);
        #endregion
    }
}
