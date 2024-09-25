using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesBackend.Data
{
    public class NotesBackendDbContext : IdentityDbContext<User>
    {
        public NotesBackendDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<NoteTag> NoteTags { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP").ValueGeneratedOnAddOrUpdate();

                entity
                .HasOne(e => e.User)
                .WithMany(u => u.Notes)
                .HasForeignKey(e => e.UserId);

                entity
                .HasOne(n => n.Reminder)
                .WithOne(r => r.Note)
                .HasForeignKey<Reminder>(r => r.NoteId);

                entity
                .HasMany(e => e.NoteTags)
                .WithOne(e => e.Note)
                .HasForeignKey(e => e.NoteId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reminder>(entity =>
            {
                entity
                .HasKey(e => e.Id);
                entity
                .HasOne(e => e.Note)
                .WithOne(e => e.Reminder)
                .HasForeignKey<Reminder>(r => r.NoteId);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity
                .Property(e => e.TagName)
                .IsRequired()
                .HasMaxLength(255);

                entity
                .HasMany(e => e.NoteTags)
                .WithOne(e => e.Tag)
                .HasForeignKey(e => e.TagId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<NoteTag>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.Note)
                    .WithMany(n => n.NoteTags)
                    .HasForeignKey(e => e.NoteId);

                entity.HasOne(e => e.Tag)
                    .WithMany(t => t.NoteTags)
                    .HasForeignKey(e => e.TagId);
            });

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
