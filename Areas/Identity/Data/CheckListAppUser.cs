using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckListApp.Models;
using Microsoft.AspNetCore.Identity;

namespace CheckListApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the CheckListAppUser class
   
    public class CheckListAppUser : IdentityUser
    {
        public ICollection<CheckListTask> CheckListTasks { get; set; }
        public ICollection<UsersSongs> UsersSongs { get; set; }
        public ICollection<Playlist> PlayLists { get; set; }

    }

}
