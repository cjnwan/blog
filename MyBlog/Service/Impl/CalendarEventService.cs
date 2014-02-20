using Domain.Model;
using Domain.Repository;

namespace Service
{
    public class CalendarEventService : ServiceBase<CalendarEvent>, ICalendarEventService
    {
        private readonly IRepository<CalendarEvent> _repository;

        public CalendarEventService()
        {//ICalendarEventRepository repository
            _repository = new CalendarEventRepository() ;
        }

        protected override Domain.Repository.IRepository<CalendarEvent> CurrRepository
        {
            get { return _repository; }
        }
    }
}