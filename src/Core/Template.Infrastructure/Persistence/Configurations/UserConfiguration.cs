using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template.Infrastructure.Persistence.Entities;

namespace Template.Infrastructure.Persistence.Configurations;

/// <summary>
/// Configures the database mapping for the <see cref="ExempleEntity"/> entity.
/// Defines constraints, column types, and owned entities for EF Core.
/// </summary>
public class ExempleConfiguration : IEntityTypeConfiguration<UserEntity>
{
    /// <summary>
    /// Configures the entity properties, relationships, and constraints.
    /// </summary>
    /// <param name="builder">The entity type builder used for configuration.</param>
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("User");

        #region Property Configuration

        builder.HasKey(b => b.Id);

        builder
            .Property(entity => entity.FirstName)
            .IsRequired() // NOT NULL
            .IsUnicode(false) // VARCHAR
            .HasMaxLength(100);

        builder
            .Property(entity => entity.LastName)
            .IsRequired() // NOT NULL
            .IsUnicode(false) // VARCHAR
            .HasMaxLength(100);

        builder
            .Property(entity => entity.Gender)
            .IsRequired() // NOT NULL
            .IsUnicode(false) // VARCHAR
            .HasMaxLength(6)
            .HasConversion<string>(); // Stores Enum as string

        builder
            .Property(entity => entity.Email)
            .IsRequired() // NOT NULL
            .IsUnicode(false) // VARCHAR
            .HasMaxLength(100);

        builder
            .Property(entity => entity.Phone)
            .IsRequired() // NOT NULL
            .IsUnicode(false) // VARCHAR
            .HasMaxLength(100);

        #endregion


    }
}
