using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp.Models
{
    public class PlaylistsSongs
    {
        [Key]
        public int ID { get; set; }
        public int PlaylistID { get; set; }
        public Playlist Playlist { get; set; }
        public int SongID { get; set; }
        public Song Song { get; set; }
    }
}
