using System;
using System.Collections.Generic;

namespace GoalFinder.WebApi.Shared.DTO
{
    public class UpdateUserInfoDTO
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName {  get; set; }  
        public string Description { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; }
        public string Experience { get; set; }
        //public List<string> Position { get; set; }
        public string CompetitionLevel { get; set; }






    }
}
