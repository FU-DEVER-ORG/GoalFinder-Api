using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Experiences" table.
/// </summary>
internal sealed class PositionEntityConfiguration :
    IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable(
            name: $"{nameof(Position)}s",
            buildAction: table => table.HasComment(
                comment: "Contain position records."));

        // Primary key configuration.
        builder.HasKey(keyExpression: position => position.Id);

        // FullName property configuration
        builder
            .Property(propertyExpression: position => position.FullName)
                .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                    length: Position.MetaData.FullName.MaxLength))
                .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: position => position.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: position => position.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: position => position.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: position => position.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: position => position.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: position => position.RemovedBy)
            .IsRequired();

        // Relationship configurations.
        // [Positions] - [UserPositions] (1 - N)
        builder
            .HasMany(navigationExpression: position => position.UserPositions)
            .WithOne(navigationExpression: userPosition => userPosition.Position)
            .HasForeignKey(foreignKeyExpression: userPosition => userPosition.PositionId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
