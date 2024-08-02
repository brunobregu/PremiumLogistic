namespace PremiumLogistic_DataLayer;

public class PremiumLogisticDbContext : IdentityDbContext<ApplicationUser>
{
    public PremiumLogisticDbContext(DbContextOptions<PremiumLogisticDbContext> options) : base(options)
    {
    }

    public DbSet<Ocean> Oceans { get; set; }
    public DbSet<Transportation> Transportation { get; set; }
    public DbSet<AuditLogs> AuditLogs { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<Provider> Providers { get; set; }

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
        builder.Entity<Transportation>().HasQueryFilter(x => !x.Invalidated);
        builder.Entity<Ocean>().HasQueryFilter(x => !x.Invalidated);
        builder.Entity<OrderDetails>().HasQueryFilter(x => !x.Invalidated);
        #endregion
    }
}
