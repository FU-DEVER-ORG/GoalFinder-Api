using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "FootballMatches" table.
/// </summary>
internal sealed class FootballMatchEntityConfiguration :
    IEntityTypeConfiguration<FootballMatch>
{
    public void Configure(EntityTypeBuilder<FootballMatch> builder)
    {
        builder.ToTable(
            name: $"{nameof(FootballMatch)}ss",
            buildAction: table => table.HasComment(
                comment: "Contain football match records."));

        // Primary key configuration.
        builder.HasKey(keyExpression: footballMatch => footballMatch.Id);

        // PitchAddress property configuration
        builder
            .Property(propertyExpression: footballMatch => footballMatch.PitchAddress)
                .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                    length: FootballMatch.MetaData.PitchAddress.MaxLength))
                .IsRequired();

        // MaxMatchPlayersNeed property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.MaxMatchPlayersNeed)
            .IsRequired();

        // PitchPrice property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.PitchPrice)
            .IsRequired();

        // Description property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.Description)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // MinPrestigeScore property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.MinPrestigeScore)
            .IsRequired();

        // StartTime property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.StartTime)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // EndTime property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.EndTime)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.RemovedBy)
            .IsRequired();

        // Foreign key configuration.
        // HostId property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.HostId)
            .IsRequired();

        // WardId property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.WardId)
            .IsRequired();

        // CompetitionLevelId property configuration.
        builder
            .Property(propertyExpression: footballMatch => footballMatch.CompetitionLevelId)
            .IsRequired();

        // Relationship configurations.
        // [FootballMatches] - [MatchPlayers] (1 - N)
        builder
            .HasMany(navigationExpression: footballMatch => footballMatch.MatchPlayers)
            .WithOne(navigationExpression: matchPlayer => matchPlayer.FootballMatch)
            .HasForeignKey(foreignKeyExpression: matchPlayer => matchPlayer.MatchId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}

