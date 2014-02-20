using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Service;

namespace WebUI.ViewModel
{
    public class EventManager
    {
        private ICalendarEventService _calendarEventService;

        public EventManager()
        {
            _calendarEventService = new CalendarEventService();
        }


        public class  Event
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public DateTime Start { get; set; }

            public DateTime End { get; set; }
        }

        public  List<CalendarEvent> GetEvent(DateTime start, DateTime end)
        {
            List<CalendarEvent> list =new List<CalendarEvent>();
                list= _calendarEventService.Entities.Where(e => e.Start >= start && e.End <= end).ToList();
            return list;
        }

        public  CalendarEvent Get(string id)
        {
            int tempId = Convert.ToInt32(id);
            return _calendarEventService.Entities.Single(e => e.Id == tempId);
        }

        public  void CreateEvent(DateTime start, DateTime end, string title)
        {
            var eEvent = new CalendarEvent();
            eEvent.Start = start;
            eEvent.End = end;
            eEvent.Title = title;

            _calendarEventService.Insert(eEvent);
        }

        public  void EditEvent(string id, string title, DateTime start, DateTime end)
        {
            int tempId = Convert.ToInt32(id);

            var eEvent = _calendarEventService.Entities.Single(e => e.Id ==tempId );

            eEvent.Start = start;
            eEvent.End = end;
            eEvent.Title = title;
            _calendarEventService.Update(eEvent);
        }

        public  void MoveEvent(string id, DateTime start, DateTime end)
        {
            int tempId = Convert.ToInt32(id);
            var eEvent = _calendarEventService.Entities.Single(e => e.Id == tempId);
            eEvent.Start = start;
            eEvent.End = end;
            _calendarEventService.Update(eEvent);
        }
        public  void RemoveEvent(string id)
        {
            int tempId = Convert.ToInt32(id);
            _calendarEventService.Delete(tempId);
        }
        public  void RemoveEvent(string id, DateTime start, DateTime end)
        {
            int tempId = Convert.ToInt32(id);
            _calendarEventService.Delete(tempId);
        }
 
    }
}