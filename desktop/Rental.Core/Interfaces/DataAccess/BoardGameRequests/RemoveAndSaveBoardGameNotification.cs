﻿using System;
using MediatR;

namespace Rental.Core.Interfaces.DataAccess.BoardGameRequests
{
    public class RemoveAndSaveBoardGameNotification : INotification
    {
        public RemoveAndSaveBoardGameNotification(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}