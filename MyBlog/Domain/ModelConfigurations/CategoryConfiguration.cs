using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Model;


namespace Domain.ModelConfigurations
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
       public CategoryConfiguration()
       {
           HasKey<int>(c => c.CategoryId);
           Property(c => c.CategoryId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           Property(c => c.CategoryName).HasMaxLength(25);
           Property(c => c.CategorySlug).HasMaxLength(100);
           
       }
    }
}