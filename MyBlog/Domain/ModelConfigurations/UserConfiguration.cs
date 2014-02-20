using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.ModelConfigurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
         public UserConfiguration()
         {
             HasKey(u => u.UserId);
             Property(u => u.UserId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
             Property(u => u.UserName).HasMaxLength(20);
             Property(u => u.UserEmail).HasMaxLength(50);
             Property(u => u.DisplayName).HasMaxLength(20);
             Property(u => u.Password).HasMaxLength(50);

            
         }
    }
}