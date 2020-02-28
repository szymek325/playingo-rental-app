﻿using System;
using Rental.CQS;

namespace Rental.Core.Interfaces.DataAccess.Queries
{
    public class GetRentalByIdQuery : IQuery<Playingo.Domain.Rentals.Rental>
    {
        public GetRentalByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}