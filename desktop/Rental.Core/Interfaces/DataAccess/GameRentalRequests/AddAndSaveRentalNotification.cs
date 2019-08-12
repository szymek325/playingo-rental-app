﻿using MediatR;
using Rental.Core.Models;

namespace Rental.Core.Interfaces.DataAccess.GameRentalRequests
{
    internal class AddAndSaveRentalNotification : INotification
    {
        public AddAndSaveRentalNotification(GameRental gameRental)
        {
            GameRental = gameRental;
        }

        public GameRental GameRental { get; set; }
    }
}