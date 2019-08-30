﻿using System;
using Rental.CQS;

namespace Rental.Core.Commands
{
    public class AddRentalCommand : ICommand
    {
        public AddRentalCommand(Guid newGameRentalGuid, Guid clientGuid, Guid boardGameGuid, decimal chargedDeposit)
        {
            NewGameRentalGuid = newGameRentalGuid;
            ClientGuid = clientGuid;
            BoardGameGuid = boardGameGuid;
            ChargedDeposit = chargedDeposit;
        }

        public Guid NewGameRentalGuid { get; set; }
        public Guid ClientGuid { get; }
        public Guid BoardGameGuid { get; }
        public decimal ChargedDeposit { get; }
    }
}