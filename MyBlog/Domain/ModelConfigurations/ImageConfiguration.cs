using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.ModelConfigurations
{
    public class ImageConfiguration : EntityTypeConfiguration<Image>
    {
       public ImageConfiguration()
       {
           HasKey(i => i.ImageId);
           Property(i => i.ImageId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           Property(i => i.ImageName).HasMaxLength(50);
           Property(i => i.ImageUrl).HasMaxLength(200);
           Property(i => i.ImageDescription).HasMaxLength(100);
       }
    }
}