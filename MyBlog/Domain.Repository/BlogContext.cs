using System.Data.Entity;
using Domain.Model;
using Domain.ModelConfigurations;

namespace Domain.Repository
{
   
    public class BlogContext:DbContext
    {
        public BlogContext(): base("MyBlog")
        {
            //this.Configuration.AutoDetectChangesEnabled = true;
            //this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PostCat> PostCats { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CommentConfiguration());
            modelBuilder.Configurations.Add(new TagConfiguration());
            modelBuilder.Configurations.Add(new ImageConfiguration());
            modelBuilder.Configurations.Add(new PostConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new PostCatConfiguration());
            modelBuilder.Configurations.Add(new PostTagConfiguration());
            modelBuilder.Configurations.Add(new CalendarEventConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}