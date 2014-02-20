using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Domain.Model;

namespace Domain.ModelConfigurations
{
    public class CalendarEventConfiguration:EntityTypeConfiguration<CalendarEvent>
    {
          public CalendarEventConfiguration()
          {
              HasKey(e => e.Id);
              Property(e => e.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
              Property(e => e.Title).HasMaxLength(200);
          }
    }
}