using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

internal sealed class WardEntityConfiguration :
    IEntityTypeConfiguration<Ward>
{
    public void Configure(EntityTypeBuilder<Ward> builder)
    {
        builder.ToTable(
            name: $"{nameof(Ward)}s",
            buildAction: table => table.HasComment(
                comment: "Contain ward records."));

        // Primary key configuration.
        builder.HasKey(keyExpression: ward => ward.Id);

        // FullName property configuration
        builder
            .Property(propertyExpression: ward => ward.FullName)
                .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                    length: Ward.MetaData.FullName.MaxLength))
                .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: ward => ward.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: ward => ward.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: ward => ward.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: ward => ward.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: ward => ward.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: ward => ward.RemovedBy)
            .IsRequired();

        // Foreign key configuration.
        // DistrictId property configuration.
        builder
            .Property(propertyExpression: ward => ward.DistrictId)
            .IsRequired();

        // Relationship configurations.
        // [Wards] - [UserDetails] (1 - N)
        builder
            .HasMany(navigationExpression: ward => ward.UserDetails)
            .WithOne(navigationExpression: userDetail => userDetail.Ward)
            .HasForeignKey(foreignKeyExpression: userDetail => userDetail.WardId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
