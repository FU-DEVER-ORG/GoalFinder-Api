using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "ErrorLoggings" table.
/// </summary>
internal sealed class ErrorLoggingEntityConfiguration :
    IEntityTypeConfiguration<ErrorLogging>
{
    public void Configure(EntityTypeBuilder<ErrorLogging> builder)
    {
        builder.ToTable(
            name: $"{nameof(ErrorLogging)}s",
            buildAction: table => table.HasComment(
                comment: "Contain error logging records."));

        // Primary key configuration.
        builder.HasKey(keyExpression: errorLogging => errorLogging.Id);

        // ErrorMessage property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.ErrorMessage)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // ErrorStackTrace property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.ErrorStackTrace)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // Data property configuration.
        builder
            .Property(propertyExpression: userDetail => userDetail.Data)
            .HasColumnType(typeName: CommonConstant.Database.DataType.TEXT)
            .IsRequired();
    }
}
