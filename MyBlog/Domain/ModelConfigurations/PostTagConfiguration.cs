using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.ModelConfigurations
{
    public class PostTagConfiguration : EntityTypeConfiguration<PostTag>
    {
        public PostTagConfiguration()
        {
            HasKey(p=>p.PostTagId);
            Property(p => p.PostTagId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}