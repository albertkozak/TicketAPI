﻿using System;
using System.Collections.Generic;

namespace netCoreAPI.Models.TicketAPI
{
    public partial class Event
    {
        public Event()
        {
            EventSeat = new HashSet<EventSeat>();
        }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public string VenueName { get; set; }

        public virtual Venue VenueNameNavigation { get; set; }
        public virtual ICollection<EventSeat> EventSeat { get; set; }
    }
}
