using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api_web_qlsk.Models
{
    public partial class EventDBContext : DbContext
    {
        public EventDBContext()
        {
        }

        public EventDBContext(DbContextOptions<EventDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(config.GetConnectionString("PRN231DB"));

            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .HasColumnName("location");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK__Events__tag_id__3D5E1FD2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Events__user_id__3C69FB99");

                entity.HasMany(d => d.Tags)
                    .WithMany(p => p.EventsNavigation)
                    .UsingEntity<Dictionary<string, object>>(
                        "EventTag",
                        l => l.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Event_Tag__tag_i__44FF419A"),
                        r => r.HasOne<Event>().WithMany().HasForeignKey("EventId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Event_Tag__event__440B1D61"),
                        j =>
                        {
                            j.HasKey("EventId", "TagId").HasName("PK__Event_Ta__57599D0C7E90A6F9");

                            j.ToTable("Event_Tags");

                            j.IndexerProperty<int>("EventId").HasColumnName("event_id");

                            j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                        });
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndTime)
                    .HasColumnType("datetime")
                    .HasColumnName("end_time");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .HasColumnName("location");

                entity.Property(e => e.StartTime)
                    .HasColumnType("datetime")
                    .HasColumnName("start_time");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK__Schedules__event__412EB0B6");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.TagId).HasColumnName("tag_id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .HasColumnName("full_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
