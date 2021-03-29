using CheckListApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp.Models
{
    public class Playlist
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string PlaylistTitle { get; set; }
        [MaxLength(300)]
        public string PlaylistDescription { get; set; }
        public ICollection<PlaylistsSongs> PlaylistsSongs { get; set; }
        public DateTime DateCreated { get; set; }
        public CheckListAppUser User { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
    }
}
