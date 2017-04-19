using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
//using SapientGuardian.MySql.Data.EntityFrameworkCore;

namespace WebApp.Contexts
{
    public class MySqlContext : DbContext
    {

        public MySqlContext(DbContextOptions<MySqlContext> options) :base (options) {}

        public DbSet<User> User { get; set; }
        public DbSet<University> University { get; set; }
        public DbSet<Rso> Rso { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<EventStatus> EventStatus { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<UserEvent> UserEvent { get; set; }
        public DbSet<UserUniversity> UserUniversity { get; set; }
        public DbSet<UserRso> UserRso { get; set; }
        public DbSet<UserStatus> UserStatus { get; set; }
        public DbSet<UrAccessLvl> UrAccessLvl { get; set; }
        public DbSet<UuAccessLvl> UuaccessLvl { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().ToTable("Category").HasKey(x => x.Id);
            builder.Entity<Event>().ToTable("EventWithInfo").HasKey(x => x.Id);
            builder.Entity<EventStatus>().ToTable("EventStatus").HasKey(x => x.Id);
            builder.Entity<EventType>().ToTable("EventType").HasKey(x => x.Id);
            builder.Entity<University>().ToTable("University").HasKey(x => x.Id);
            builder.Entity<UserUniversity>().ToTable("UserUniversity").HasKey(x => x.Id);
            builder.Entity<UuAccessLvl>().ToTable("UuAccessLvl").HasKey(x => x.Id);
            builder.Entity<Location>().ToTable("Location").HasKey(x => x.Id);
            builder.Entity<User>().ToTable("User").HasKey(x => x.Id);
            builder.Entity<UserStatus>().ToTable("UserStatus").HasKey(x => x.Id);
            builder.Entity<Rso>().ToTable("Rso").HasKey(x => x.Id);
            builder.Entity<UserRso>().ToTable("UserRso").HasKey(x => x.Id);
            builder.Entity<UrAccessLvl>().ToTable("UrAccessLvl").HasKey(x => x.Id);
            builder.Entity<UserEvent>().ToTable("UserEvent").HasKey(x => x.Id);

            //var mems = builder.Entity<RsoMembership>().HasOne(x => x.rso).WithMany(s => s.rsoMem);

            //builder.Entity<RsoMember>()
            //.HasRequired(e => e.User)
            //.WithMany(e => e.Model1s)
            //.HasForeignKey(e => new { e.fk_one, e.fk_two })
            //.WillCascadeOnDelete(false);
        }
        
    }
}
