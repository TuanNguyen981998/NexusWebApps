using CheckListApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp.Models
{
    public class Song
    { 
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Song Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public string Artist { get; set; }

        [Display(Name = "Duration")]
        public string Duration { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Added Date")]
        public DateTime AddedDate { get; set; }

        public ICollection<UsersSongs> UsersSongs { get; set; }
        public ICollection<PlaylistsSongs> PlaylistsSongs { get; set; }
    }
}
