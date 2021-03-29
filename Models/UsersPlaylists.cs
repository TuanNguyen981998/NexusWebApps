using CheckListApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp.Models
{
    public class UsersPlaylists
    {
        [Key]
        public int ID { get; set; }
        public string UserID { get; set; }
        public CheckListAppUser CheckListAppUser { get; set; }
        public int PlaylistID { get; set; }
        public Playlist Playlist { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
