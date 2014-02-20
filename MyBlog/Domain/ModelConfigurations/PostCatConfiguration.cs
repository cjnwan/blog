using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.ModelConfigurations
{
    public class PostCatConfiguration : EntityTypeConfiguration<PostCat>

    {
      
        public PostCatConfiguration()
        {
            HasKey(p => p.PostCatId);
            Property(p => p.PostCatId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}