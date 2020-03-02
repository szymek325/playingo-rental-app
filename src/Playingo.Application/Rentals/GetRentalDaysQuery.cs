﻿using System;
using System.Collections.Generic;
using Playingo.Application.Common.Mediator;
using Playingo.Domain.Rentals;

namespace Playingo.Application.Rentals
{
    internal class GetRentalDaysQuery : IQuery<IList<RentalDay>>
    {
        public GetRentalDaysQuery(decimal boardGamePrice, DateTime rentStartDate)
        {
            BoardGamePrice = boardGamePrice;
            RentStartDate = rentStartDate;
        }

        public GetRentalDaysQuery(decimal boardGamePrice, DateTime rentStartDate, DateTime? rentFinishDate)
        {
            BoardGamePrice = boardGamePrice;
            RentStartDate = rentStartDate;
            RentFinishDate = rentFinishDate;
        }

        public decimal BoardGamePrice { get; set; }
        public DateTime RentStartDate { get; set; }
        public DateTime? RentFinishDate { get; set; }
    }
}