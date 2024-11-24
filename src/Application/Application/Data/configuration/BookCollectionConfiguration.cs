namespace Application.Data.configuration;

public class BookCollectionConfig : IEntityTypeConfiguration<BookCollection>
{
    public void Configure(EntityTypeBuilder<BookCollection> builder)
    {
        builder.Property(p => p.CollectionName)
           .HasMaxLength(100)
           .IsRequired();
    }
}
