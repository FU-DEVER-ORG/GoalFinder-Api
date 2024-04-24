using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetUserProfile;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetUserProfile
{
    public class GetUserProfileFakeDb : IGetUserProfileRepository
    {


        public Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FootballMatch>> GetFootballMatchByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            return CreateUserDetailMatches();
        }

        async Task<UserDetail> IGetUserProfileRepository.GetUserDetailAsync(Guid userId, CancellationToken cancellationToken)
        {
            await Task.Delay(1);
            return CreateUserDetail();
        }

        private static UserDetail CreateUserDetail()
        {
            var userDetail = new UserDetail
            {
                UserId = Guid.NewGuid(),
                LastName = "Doe",
                FirstName = "John",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                PrestigeScore = 75,
                Address = "123 Main St, City, Country",
                AvatarUrl = "https://example.com/avatar.jpg",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                RemovedAt = DateTime.MinValue,
                RemovedBy = Guid.Empty,
                ExperienceId = Guid.NewGuid(),
                CompetitionLevelId = Guid.NewGuid()
            };

            var positions = new List<Position>
            {
                new Position
                {
                    Id = Guid.NewGuid(),
                    FullName = "Forward",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = Guid.NewGuid(),
                    RemovedAt = DateTime.MinValue,
                    RemovedBy = Guid.Empty
                },
                new Position
                {
                    Id = Guid.NewGuid(),
                    FullName = "Midfielder",
                    UpdatedAt = DateTime.UtcNow,
                    UpdatedBy = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = Guid.NewGuid(),
                    RemovedAt = DateTime.MinValue,
                    RemovedBy = Guid.Empty
                }
            };

            var userPositions = new List<UserPosition>
            {
                new UserPosition
                {
                    UserId = userDetail.UserId,
                    PositionId = positions[0].Id

                },
                new UserPosition
                {
                    UserId = userDetail.UserId,
                    PositionId = positions[1].Id
                }
            };
            userPositions[0].Position = positions[0];
            userPositions[1].Position = positions[1];

            userDetail.UserPositions = userPositions;

            userDetail.Experience = new Experience
            {
                Id = userDetail.ExperienceId,
                FullName = "Professional Experience",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                RemovedAt = DateTime.MinValue,
                RemovedBy = Guid.Empty,
                UserDetails = new List<UserDetail> { userDetail }
            };

            userDetail.CompetitionLevel = new CompetitionLevel
            {
                Id = userDetail.CompetitionLevelId,
                FullName = "High School Level",
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                RemovedAt = DateTime.MinValue,
                RemovedBy = Guid.Empty,
                UserDetails = new List<UserDetail> { userDetail }
            };

            return userDetail;
        }

        public static IEnumerable<FootballMatch> CreateUserDetailMatches()
        {
            var userDetail = CreateUserDetail();

            var footballMatches = new List<FootballMatch>
        {
            new FootballMatch
            {
                Id = Guid.NewGuid(),
                PitchAddress = "ABC Stadium",
                MaxMatchPlayersNeed = 10,
                PitchPrice = 2000,
                Description = "Friendly match",
                MinPrestigeScore = 50,
                StartTime = DateTime.UtcNow.AddDays(7),
                EndTime = DateTime.UtcNow.AddDays(7).AddHours(2),
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                RemovedAt = DateTime.MinValue,
                RemovedBy = Guid.Empty,
                Address = "XYZ Street",
                HostId = userDetail.UserId,
                CompetitionLevelId = userDetail.CompetitionLevelId
            },
            new FootballMatch
            {
                Id = Guid.NewGuid(),
                PitchAddress = "DEF Arena",
                MaxMatchPlayersNeed = 12,
                PitchPrice = 1500,
                Description = "Training match",
                MinPrestigeScore = 30,
                StartTime = DateTime.UtcNow.AddDays(14),
                EndTime = DateTime.UtcNow.AddDays(14).AddHours(2),
                UpdatedAt = DateTime.UtcNow,
                UpdatedBy = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid(),
                RemovedAt = DateTime.MinValue,
                RemovedBy = Guid.Empty,
                Address = "LMN Boulevard",
                HostId = userDetail.UserId,
                CompetitionLevelId = userDetail.CompetitionLevelId
            }
        };

            return footballMatches;
        }
    }
}