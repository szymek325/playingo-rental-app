﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rental.Core.Interfaces.DataAccess.Queries;
using Rental.Core.Models;
using Rental.CQRS;
using Rental.DataAccess.Context;

namespace Rental.DataAccess.QueryHandlers
{
    internal class CheckIfClientCanBeRemovedQueryHandler : IQueryHandler<CheckIfClientCanBeRemovedQuery, bool>
    {
        private readonly RentalContext _rentalContext;

        public CheckIfClientCanBeRemovedQueryHandler(RentalContext rentalContext)
        {
            _rentalContext = rentalContext;
        }

        public async Task<bool> Handle(CheckIfClientCanBeRemovedQuery query, CancellationToken cancellationToken)
        {
            var canBeRemoved =
                await _rentalContext.Clients.AnyAsync(x => x.Id == query.Id, cancellationToken) &&
                await _rentalContext.GameRentals.Where(x => x.ClientId == query.Id)
                    .AllAsync(x => x.Status == Status.Completed, cancellationToken);


            return canBeRemoved;
        }
    }
}