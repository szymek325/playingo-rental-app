﻿using System;
using Rental.CQRS;

namespace Rental.Core.Interfaces.DataAccess.Queries
{
    public class CheckIfBoardGameHasOnlyCompletedRentalsQuery : IQuery<bool>
    {
        public CheckIfBoardGameHasOnlyCompletedRentalsQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}