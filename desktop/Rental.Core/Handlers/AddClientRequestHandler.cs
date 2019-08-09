﻿using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rental.Core.Interfaces.DataAccess.Repositories;
using Rental.Core.Models.Validation;
using Rental.Core.Requests;

namespace Rental.Core.Handlers
{
    internal class AddClientRequestHandler : IRequestHandler<AddClientRequest, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddClientRequestHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        async Task<Guid> IRequestHandler<AddClientRequest, Guid>.Handle(AddClientRequest request, CancellationToken cancellationToken)
        {
            var validator = new ClientValidator();
            var result = validator.Validate(request.Client);
            if (result.IsValid)
            {
                request.Client.Id = Guid.NewGuid();
                await _unitOfWork.ClientsRepository.AddAsync(request.Client);
                await _unitOfWork.SaveChangesAsync();
                return request.Client.Id;
            }

            var builder = new StringBuilder();
            foreach (var validationFailure in result.Errors)
                builder.AppendLine($"{validationFailure.PropertyName}- {validationFailure.ErrorMessage}");
            Trace.WriteLine(builder.ToString());
            return Guid.Empty;
        }
    }
}