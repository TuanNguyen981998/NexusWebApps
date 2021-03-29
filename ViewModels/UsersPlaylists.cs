using CheckListApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckListApp.ViewModels
{
    public class UsersPlaylists
    {
        public Playlist Playlist { get; set; }
        public string SelectedSortOption { get; set; }
        public string InputFilterName { get; set; }
    }
}
