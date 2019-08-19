﻿using System;

namespace Rental.Core.Models.Rentals
{
    public class Rental
    {
        public Rental(Guid id, Guid clientId, Guid boardGameId, float chargedDeposit)
        {
            Id = id;
            ClientId = clientId;
            BoardGameId = boardGameId;
            ChargedDeposit = chargedDeposit;
        }

        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid BoardGameId { get; set; }
        public float ChargedDeposit { get; set; }
        public float PaidMoney { get; set; }
        public Status Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? FinishTime { get; set; }
    }
}