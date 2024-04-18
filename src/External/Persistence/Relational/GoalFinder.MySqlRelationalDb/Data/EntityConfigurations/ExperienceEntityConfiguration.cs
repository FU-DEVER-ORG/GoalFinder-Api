using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Experiences" table.
/// </summary>
internal sealed class ExperienceEntityConfiguration :
    IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable(
            name: $"{nameof(Experience)}s",
            buildAction: table => table.HasComment(
                comment: "Contain experience records."));

        // Primary key configuration.
        builder.HasKey(keyExpression: experience => experience.Id);

        // FullName property configuration
        builder
            .Property(propertyExpression: experience => experience.FullName)
                .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                    length: Experience.MetaData.FullName.MaxLength))
                .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: experience => experience.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: experience => experience.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: experience => experience.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: experience => experience.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: experience => experience.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: experience => experience.RemovedBy)
            .IsRequired();

        // Relationship configurations.
        // [Experiences] - [UserDetails] (1 - N)
        builder
            .HasMany(navigationExpression: experience => experience.UserDetails)
            .WithOne(navigationExpression: userDetail => userDetail.Experience)
            .HasForeignKey(foreignKeyExpression: userDetail => userDetail.ExperienceId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
