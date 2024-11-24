
namespace Application.Data;

public class ApplicationDBContext:DbContext
{
    public DbSet<BookCollection> BookCollections => Set<BookCollection>();
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        foreach(var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if(entry.State != EntityState.Added)
            {
                var entity = entry.Entity;
                entity.Created =  DateTime.UtcNow;
                entity.Updated = DateTime.UtcNow;
            }

            if (entry.State != EntityState.Modified)
            {
                var entity = entry.Entity;
                entity.Updated = DateTime.UtcNow;
            }
        }


        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}
