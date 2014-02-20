using Domain.Model;

namespace Domain.Repository
{
    public class CalendarEventRepository:Repository<CalendarEvent>,ICalendarEventRepository
    {
        public CalendarEventRepository()
        {
            UnitOfWork = new BlogUnitofContext(); //UnitOfWork;
        }
    }
}