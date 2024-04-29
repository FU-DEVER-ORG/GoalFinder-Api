using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

internal sealed class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> builder)
    {
        builder.ToTable(
            name: $"{nameof(UserToken)}s",
            buildAction: table => table.HasComment(comment: "Contain user token records.")
        );

        builder
            .Property(propertyExpression: userToken => userToken.ExpiredAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();
    }
}
