using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "Users" table.
/// </summary>
internal sealed class UserEntityConfiguration :
    IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(
            name: $"{nameof(User)}s",
            buildAction: table => table.HasComment(
                comment: "Contain user records."));

        // Table relationships configurations.
        // [Users] - [UserRoles] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.UserRoles)
            .WithOne(navigationExpression: userRole => userRole.User)
            .HasForeignKey(foreignKeyExpression: userRole => userRole.UserId)
            .IsRequired();

        // [Users] - [UserTokens] (1 - N).
        builder
            .HasMany(navigationExpression: user => user.UserTokens)
            .WithOne(navigationExpression: userToken => userToken.User)
            .HasForeignKey(foreignKeyExpression: userToken => userToken.UserId)
            .IsRequired();

        // [Users] - [UserDetails] (1 - 1).
        builder
            .HasOne(navigationExpression: user => user.UserDetail)
            .WithOne(navigationExpression: userDetail => userDetail.User)
            .HasForeignKey<UserDetail>(foreignKeyExpression: userDetail => userDetail.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
