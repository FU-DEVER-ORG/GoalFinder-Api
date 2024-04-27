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

        // Address property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.Address)
            .HasColumnType(typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                length: UserDetail.MetaData.Address.MaxLength))
            .IsRequired();

        // Description property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.Description)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // AvatarUrl property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.AvatarUrl)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // BackgroundUrl property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.BackgroundUrl)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // PrestigeScore property configuration.
        builder
            .Property(propertyExpression: roleDetail => roleDetail.PrestigeScore)
            .IsRequired();

        // ExperienceId property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.ExperienceId)
            .IsRequired();

        // CompetitionLevelId property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.CompetitionLevelId)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.RemovedBy)
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

        builder
            .HasMany(navigationExpression: userDetail => userDetail.RefreshTokens)
            .WithOne(navigationExpression: refreshToken => refreshToken.UserDetail)
            .HasForeignKey(foreignKeyExpression: refreshToken => refreshToken.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

        builder
            .HasMany(navigationExpression: userDetail => userDetail.FootballMatches)
            .WithOne(navigationExpression: footballMatch => footballMatch.UserDetail)
            .HasForeignKey(foreignKeyExpression: footballMatch => footballMatch.HostId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

    }
}
