using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.ModelConfigurations
{
    public class PostConfiguration : EntityTypeConfiguration<Post>
    {
         public PostConfiguration()
         {
             HasKey(p => p.PostId);
             Property(p => p.PostId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
             Property(p => p.PostTitle).HasMaxLength(100);

             //HasMany(p => p.Tags).WithMany(t => t.Posts).Map(r => 
             //{ 
             //    r.ToTable("TagMapping");
             //    r.MapLeftKey("PostId");
             //    r.MapRightKey("TagId");
             //});

             //HasMany(p => p.Categories).WithMany(c => c.Posts).Map(r =>
             //{
             //    r.ToTable("CategoryMapping");
             //    r.MapLeftKey("PostId");
             //    r.MapRightKey("CategoryId");
             //});

             HasMany(p => p.Comments).WithRequired(c => c.Post).Map(r => r.MapKey("PostId"));

             



         }
    }
}