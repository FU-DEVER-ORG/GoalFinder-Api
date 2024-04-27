using GoalFinder.Data.Entities;
using GoalFinder.MySqlRelationalDb.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoalFinder.MySqlRelationalDb.Data.EntityConfigurations;

/// <summary>
///     Represents configuration of "CompetitionLevels" table.
/// </summary>
internal sealed class CompetitionLevelEntityConfiguration
    : IEntityTypeConfiguration<CompetitionLevel>
{
    public void Configure(EntityTypeBuilder<CompetitionLevel> builder)
    {
        builder.ToTable(
            name: $"{nameof(CompetitionLevel)}s",
            buildAction: table => table.HasComment(comment: "Contain competition level records.")
        );

        // Primary key configuration.
        builder.HasKey(keyExpression: competitionLevel => competitionLevel.Id);

        // FullName property configuration
        builder
            .Property(propertyExpression: competitionLevel => competitionLevel.FullName)
            .HasColumnType(
                typeName: CommonConstant.Database.DataType.VarcharGenerator.Get(
                    length: CompetitionLevel.MetaData.FullName.MaxLength
                )
            )
            .IsRequired();

        // CreatedAt property configuration.
        builder
            .Property(propertyExpression: competitionLevel => competitionLevel.CreatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // CreatedBy property configuration.
        builder
            .Property(propertyExpression: competitionLevel => competitionLevel.CreatedBy)
            .IsRequired();

        // UpdatedAt property configuration.
        builder
            .Property(propertyExpression: competitionLevel => competitionLevel.UpdatedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // UpdatedBy property configuration.
        builder
            .Property(propertyExpression: competitionLevel => competitionLevel.UpdatedBy)
            .IsRequired();

        // RemovedAt property configuration.
        builder
            .Property(propertyExpression: competitionLevel => competitionLevel.RemovedAt)
            .HasColumnType(typeName: CommonConstant.Database.DataType.DATETIME)
            .IsRequired();

        // RemovedBy property configuration.
        builder
            .Property(propertyExpression: competitionLevel => competitionLevel.RemovedBy)
            .IsRequired();

        // Relationship configurations.
        // [CompetitionLevels] - [UserDetails] (1 - N)
        builder
            .HasMany(navigationExpression: competitionLevel => competitionLevel.UserDetails)
            .WithOne(navigationExpression: userDetail => userDetail.CompetitionLevel)
            .HasForeignKey(foreignKeyExpression: userDetail => userDetail.CompetitionLevelId)
            .OnDelete(deleteBehavior: DeleteBehavior.NoAction);
    }
}
