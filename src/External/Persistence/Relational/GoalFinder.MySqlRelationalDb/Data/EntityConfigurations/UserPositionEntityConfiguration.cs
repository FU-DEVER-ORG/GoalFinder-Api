using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "UserPositions" table.
/// </summary>
internal sealed class UserPositionEntityConfiguration :
    IEntityTypeConfiguration<UserPosition>
{
    public void Configure(EntityTypeBuilder<UserPosition> builder)
    {
        builder.ToTable(
            name: $"{nameof(UserPosition)}s",
            buildAction: table => table.HasComment(
                comment: "Contain user position records."));

        // Primary key configuration.
        builder.HasKey(keyExpression: userPosition => new
        {
            userPosition.UserId,
            userPosition.PositionId
        });
    }
}
