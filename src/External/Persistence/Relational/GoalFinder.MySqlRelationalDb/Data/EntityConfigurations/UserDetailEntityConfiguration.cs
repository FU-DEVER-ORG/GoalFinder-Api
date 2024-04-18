using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GoalFinder.MySqlRelationalDb.Commons;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "UserDetails" table.
/// </summary>
internal sealed class UserDetailEntityConfiguration :
    IEntityTypeConfiguration<UserDetail>
{
    public void Configure(EntityTypeBuilder<UserDetail> builder)
    {
        builder.ToTable(
            name: $"{nameof(UserDetail)}s",
            buildAction: table => table.HasComment(
                comment: "Contain user detail records."));

        // Primary key configuration.
        builder.HasKey(keyExpression: userDetail => userDetail.UserId);

        // LastName property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.LastName)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                length: UserDetail.MetaData.LastName.MaxLength))
            .IsRequired();

        // FirstName property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.FirstName)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                length: UserDetail.MetaData.FirstName.MaxLength))
            .IsRequired();

        // Description property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.Description)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // PrestigeScore property configuration.
        builder
            .Property(propertyExpression: roleDetail => roleDetail.PrestigeScore)
            .IsRequired();

        // Foreign key configuration.
        // WardId property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.WardId)
            .IsRequired();

        // ExperienceId property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.ExperienceId)
            .IsRequired();

        // CompetitionLevelId property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.CompetitionLevelId)
            .IsRequired();

        // Relationship configurations.
        // [UserDetails] - [UserPositions] (1 - N)
        builder
            .HasMany(navigationExpression: userDetail => userDetail.UserPositions)
            .WithOne(navigationExpression: userPosition => userPosition.UserDetail)
            .HasForeignKey(foreignKeyExpression: userPosition => userPosition.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        // [UserDetails] - [MatchPlayers] (1 - N)
        builder
            .HasMany(navigationExpression: userDetail => userDetail.MatchPlayers)
            .WithOne(navigationExpression: matchPlayer => matchPlayer.UserDetail)
            .HasForeignKey(foreignKeyExpression: matchPlayer => matchPlayer.PlayerId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
