using Microsoft.Azure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Coombu.Models
{
    public class CoombuContext : DbContext
    {
        public CoombuContext() : base(RoleEnvironment.GetConfigurationSettingValue("CoombuConnectionString")) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserProfile> Users { get; set; }        
        public DbSet<Like> Likes { get; set; }
        public DbSet<UserToken> UsersToken { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserProfile>()
            .HasMany(u => u.Groups)            
            .WithMany(g => g.Users)
            .Map(mc =>
            {
                mc.MapLeftKey("UserId");
                mc.MapRightKey("GroupId");
                mc.ToTable("UserProfile_Group");
            })            ;

            modelBuilder.Entity<Post>()
                .HasMany(l => l.Likes)                
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Post>()
                .HasMany(c => c.Comments)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(true);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }    
}
