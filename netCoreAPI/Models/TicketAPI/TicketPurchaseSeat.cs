﻿using System;
using System.Collections.Generic;

namespace netCoreAPI.Models.TicketAPI
{
    public partial class TicketPurchaseSeat
    {
        public int PurchaseId { get; set; }
        public int EventSeatId { get; set; }
        public decimal? SeatSubtotal { get; set; }

        public virtual EventSeat EventSeat { get; set; }
        public virtual TicketPurchase Purchase { get; set; }
    }
}
