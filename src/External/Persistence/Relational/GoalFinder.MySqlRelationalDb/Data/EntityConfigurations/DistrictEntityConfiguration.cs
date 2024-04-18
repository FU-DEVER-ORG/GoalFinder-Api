using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Districts" table.
/// </summary>
internal sealed class DistrictEntityConfiguration :
    IEntityTypeConfiguration<District>
{
    public void Configure(EntityTypeBuilder<District> builder)
    {
        builder.ToTable(
            name: $"{nameof(District)}s",
            buildAction: table => table.HasComment(
                comment: "Contain district records."));

        // Primary key configuration.
        builder.HasKey(keyExpression: district => district.Id);

        // FullName property configuration
        builder
            .Property(propertyExpression: district => district.FullName)
                .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                    length: District.MetaData.FullName.MaxLength))
                .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: district => district.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: district => district.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: district => district.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: district => district.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: district => district.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: district => district.RemovedBy)
            .IsRequired();

        // Foreign key configuration.
        // ProvinceId property configuration.
        builder
            .Property(propertyExpression: district => district.ProvinceId)
            .IsRequired();

        // Relationship configurations.
        // [Districts] - [Wards] (1 - N)
        builder
            .HasMany(navigationExpression: district => district.Wards)
            .WithOne(navigationExpression: ward => ward.District)
            .HasForeignKey(foreignKeyExpression: ward => ward.DistrictId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
