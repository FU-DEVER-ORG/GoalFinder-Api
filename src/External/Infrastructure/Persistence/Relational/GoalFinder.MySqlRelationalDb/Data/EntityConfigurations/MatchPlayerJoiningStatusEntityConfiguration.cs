using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

internal sealed class MatchPlayerJoiningStatusEntityConfiguration
    : IEntityTypeConfiguration<MatchPlayerJoiningStatus>
{
    public void Configure(EntityTypeBuilder<MatchPlayerJoiningStatus> builder)
    {
        builder.ToTable(
            name: $"{nameof(MatchPlayerJoiningStatus)}es",
            buildAction: table =>
                table.HasComment(comment: "Contain Match Player Joining Status records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: joiningstatus => joiningstatus.Id);

        // FullName property configuration
        builder
            .Property(propertyExpression: joiningstatus => joiningstatus.FullName)
            .HasColumnType(
                typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                    length: MatchPlayerJoiningStatus.MetaData.FullName.MaxLength
                )
            )
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: joiningstatus => joiningstatus.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: joiningstatus => joiningstatus.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: joiningstatus => joiningstatus.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: joiningstatus => joiningstatus.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: joiningstatus => joiningstatus.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: joiningstatus => joiningstatus.RemovedBy)
            .IsRequired();

        // Relationship configurations.
        // [MatchPlayers] - [MatchPlayerJoiningStatus] (1 - N)
        builder
            .HasMany(navigationExpression: joiningstatus => joiningstatus.MatchPlayers)
            .WithOne(navigationExpression: matchPlayer => matchPlayer.MatchPlayerJoiningStatus)
            .HasForeignKey(foreignKeyExpression: matchPlayer => matchPlayer.JoiningStatusId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
