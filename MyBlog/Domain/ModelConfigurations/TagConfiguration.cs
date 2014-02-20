using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.ModelConfigurations
{
    public class TagConfiguration : EntityTypeConfiguration<Tag>
    {
         public TagConfiguration()
         {
             HasKey(t => t.TagId);
             Property(t => t.TagId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
             Property(t => t.TagName).HasMaxLength(20);
             Property(t => t.TagSlug).HasMaxLength(100);
         }
    }
}