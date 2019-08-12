﻿using FluentValidation;

namespace Rental.Core.Models.Validation
{
    internal class RentalValidation : AbstractValidator<GameRental>
    {
        public RentalValidation()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.ClientId).NotEmpty().NotNull();
            RuleFor(x => x.BoardGameId).NotEmpty().NotNull();
            RuleFor(x => (int) x.Status).GreaterThan(0);
        }
    }
}