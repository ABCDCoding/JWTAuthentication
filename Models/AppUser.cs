using System;
using System.Collections.Generic;

namespace PricingEngine.Models
{
    public partial class AppUser
    {
        
        public int AppUserId { get; set; }
        public Guid AppUserGuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePicture { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int AppUserRoleId { get; set; }
        public string LandingPage { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsTrader { get; set; }
        public bool IsLocked { get; set; }
        public short? LoginFailedCount { get; set; }
        public DateTime? LastFailedOn { get; set; }
        public DateTime? LastPasswordChangedOn { get; set; }
        public bool IsActive { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedUserId { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedUserId { get; set; }
    }

    public class UserWithToken : AppUser
    {
        public string Token { get; set; }

        public UserWithToken(AppUser user)
        {
            this.AppUserId = user.AppUserId;
            this.EmailId = user.EmailId;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
        }
    }
}
