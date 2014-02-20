﻿using System;

namespace Domain.Model
{
    public class CalendarEvent
    {
        public int Id { get; set; }
    
        public string Title { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }
    }
}