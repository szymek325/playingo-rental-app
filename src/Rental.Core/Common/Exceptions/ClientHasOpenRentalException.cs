﻿using System;

namespace Rental.Core.Common.Exceptions
{
    internal class ClientHasOpenRentalException : Exception
    {
        public ClientHasOpenRentalException(Guid guid) : base(
            $"Client with id {guid} has rentals that are in progress!")
        {
        }
    }
}