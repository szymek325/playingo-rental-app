﻿using FluentValidation;

namespace Rental.Core.Models.Validation
{
    internal class BoardGameValidator : AbstractValidator<BoardGame>
    {
        public BoardGameValidator()
        {
            RuleFor(x => x.Name).Length(2, 30);
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }

    internal class UpdateBoardGameValidator : BoardGameValidator
    {
        public UpdateBoardGameValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }
}