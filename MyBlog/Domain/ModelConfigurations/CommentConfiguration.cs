using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.ModelConfigurations
{
    public class CommentConfiguration : EntityTypeConfiguration<Comment>
    {
         public CommentConfiguration()
         {
             HasKey<int>(c => c.CommentId);
             Property(c => c.CommentId).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
             Property(c => c.CommentEmail).HasMaxLength(50);
             Property(c => c.CommentUserName).HasMaxLength(100);        
         }
    }
}