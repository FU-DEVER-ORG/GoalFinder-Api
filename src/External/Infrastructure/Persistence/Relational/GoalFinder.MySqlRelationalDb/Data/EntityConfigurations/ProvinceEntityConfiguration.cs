using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Provinces" table.
/// </summary>
internal sealed class ProvinceEntityConfiguration :
    IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable(
            name: $"{nameof(Province)}s",
            buildAction: table => table.HasComment(
                comment: "Contain province records."));

        // Primary key configuration.
        builder.HasKey(keyExpression: province => province.Id);

        // FullName property configuration
        builder
            .Property(propertyExpression: province => province.FullName)
                .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                    length: Province.MetaData.FullName.MaxLength))
                .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: province => province.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: province => province.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: province => province.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: province => province.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: province => province.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: province => province.RemovedBy)
            .IsRequired();

        // Relationship configurations.
        // [Provinces] - [Districts] (1 - N)
        builder
            .HasMany(navigationExpression: province => province.Districts)
            .WithOne(navigationExpression: district => district.Province)
            .HasForeignKey(foreignKeyExpression: district => district.ProvinceId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
