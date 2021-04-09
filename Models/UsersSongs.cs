using CheckListApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp.Models
{
    //Many to many relationships must have an intermediate class
    public class UsersSongs
    {
        [Key]
        public int ID { get; set; }

        public string UserID { get; set; }
        public CheckListAppUser CheckListAppUser { get; set; }

        public int SongID { get; set; }
        public Song Song { get; set; }

        public DateTime UserSongAddedDate { get; set; }

        public int HitCount { get; set; }
    }
}
