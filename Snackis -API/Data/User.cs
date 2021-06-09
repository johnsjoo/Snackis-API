using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Api.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {

        public string FullName { get; set; }
        public string  Image { get; set; }
        public virtual UserSettings Settings { get; set; }
        public virtual UserGDPR GDPR { get; set; }
    }

    public class UserSettings
    {
        [Key]
        public string Id { get; set; }
        public bool DarkMode { get; set; }

        // assign foreign key (which is the primary key of the parent table, in this case (Id) of user)
        [ForeignKey("Id")]
        public virtual User User { get; set; }


    }

    public class UserGDPR
    {
        [Key]
        public string Id { get; set; }
        [PersonalData]
        public bool UseMyData { get; set; }

        [ForeignKey("Id")]
        public virtual User User { get; set; }
    }
}
