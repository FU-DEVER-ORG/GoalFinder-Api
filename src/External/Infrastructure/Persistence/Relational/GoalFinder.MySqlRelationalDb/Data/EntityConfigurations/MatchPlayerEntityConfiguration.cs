using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "MatchPlayers" table.
/// </summary>
internal sealed class MatchPlayerEntityConfiguration : IEntityTypeConfiguration<MatchPlayer>
{
    public void Configure(EntityTypeBuilder<MatchPlayer> builder)
    {
        builder.ToTable(
            name: $"{nameof(MatchPlayer)}s",
            buildAction: table => table.HasComment(comment: "Contain match player records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: matchPlayer => new
        {
            matchPlayer.MatchId,
            matchPlayer.PlayerId
        });

        builder
            .Property(propertyExpression: matchPlayer => matchPlayer.NumberOfReports)
            .IsRequired();

        builder
            .Property(propertyExpression: matchPlayer => matchPlayer.IsReported)
            .HasDefaultValue(false);
    }
}
