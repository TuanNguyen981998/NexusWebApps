using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheckListApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CheckListApp.Models;
using CheckListApp.ViewModels;

namespace CheckListApp.Data
{
    public class CheckListAppContext : IdentityDbContext<CheckListAppUser>
    {
        public CheckListAppContext(DbContextOptions<CheckListAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CheckListAppUser>()
                .HasMany<CheckListTask>(u => u.CheckListTasks)
                .WithOne(t => t.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CheckListAppUser>()
                .HasMany<Playlist>(p => p.PlayLists)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PlaylistsSongs>()
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistsSongs)
                .HasForeignKey(ps => ps.PlaylistID);

            builder.Entity<PlaylistsSongs>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistsSongs)
                .HasForeignKey(ps => ps.SongID);

            builder.Entity<UsersSongs>()
                .HasOne(us => us.CheckListAppUser)
                .WithMany(s => s.UsersSongs)
                .HasForeignKey(us => us.UserID);

            builder.Entity<UsersSongs>()
                .HasOne(us => us.Song)
                .WithMany(s => s.UsersSongs)
                .HasForeignKey(us => us.SongID);
            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<CheckListTask> CheckListTasks { get; set; }
        
        public DbSet<UsersSongs> UsersSongs { get; set; }

        public DbSet<Song> Songs { get; set; }

        public DbSet<PlaylistsSongs> PlaylistsSongs { get; set; }
        //public DbSet<UsersPlaylists> UsersPlaylists { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
    }
}
