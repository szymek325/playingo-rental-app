﻿using System;
using Rental.CQRS;

namespace Rental.Core.Commands
{
    internal class CompleteGameRentalCommand : ICommand
    {
        public CompleteGameRentalCommand(Guid gameRentalId, float paidMoney)
        {
            GameRentalId = gameRentalId;
            PaidMoney = paidMoney;
        }

        public Guid GameRentalId { get; }
        public float PaidMoney { get; }
    }
}