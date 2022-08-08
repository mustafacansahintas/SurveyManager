using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseContext:DbContext
    {
        public DbSet<_User> user { get; set; }
        public DbSet<Survey> survey { get; set; }
        public DbSet<Company> company { get; set; }
        public DbSet<Question> question { get; set; }
        public DbSet<Response> response { get; set; }
        public DbSet<UserSurvey> _survey { get; set; }
        public DbSet<UserResponse> _response { get; set; }

        public DatabaseContext() : base("DatabaseContext")
        {

        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Survey>()
        //        .HasMany(b => b.user)
        //        .WithMany(c => c.survey)
        //        .Map(cs =>
        //        {
        //            cs.MapLeftKey("SurveyId");
        //            cs.MapRightKey("UserId");
        //            cs.ToTable("aratablo");

        //        });

        //}



    }
}
