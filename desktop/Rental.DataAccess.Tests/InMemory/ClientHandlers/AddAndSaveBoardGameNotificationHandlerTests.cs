﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rental.Core.Interfaces.DataAccess.ClientRequests;
using Rental.Core.Models;
using Rental.DataAccess.Context;
using Rental.DataAccess.Handlers.ClientHandlers;
using Rental.DataAccess.Mapping;
using Xunit;

namespace Rental.DataAccess.Tests.InMemory.ClientHandlers
{
    public class AddAndSaveClientNotificationHandlerTests
    {
        public AddAndSaveClientNotificationHandlerTests()
        {
            var contextOptions = new DbContextOptionsBuilder<RentalContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _rentalContext = new RentalContext(contextOptions);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<EntitiesMapping>(); }));
            _sut = new AddAndSaveClientNotificationHandler(mapper, new RentalContext(contextOptions));
        }

        private readonly RentalContext _rentalContext;
        private readonly INotificationHandler<AddAndSaveClientNotification> _sut;

        [Fact]
        public async Task Handle_Should_AddClientToDb_When_MethodCalled()
        {
            var client = new Client("mat", "szym", "123456", "test@test.pl");
            var input = new AddAndSaveClientNotification(client);

            await _sut.Handle(input, new CancellationToken());

            var result = _rentalContext.Clients.FirstOrDefault(x => x.Id == client.Id);
            result.ContactNumber.Should().Be(client.ContactNumber);
            result.EmailAddress.Should().Be(client.EmailAddress);
            result.FirstName.Should().Be(client.FirstName);
            result.LastName.Should().Be(client.LastName);
        }

        [Fact]
        public void Handle_Should_ThrowArgumentException_When_ElementWithThisIdExist()
        {
            var client = new Client("mat", "szym", "123456", "test@test.pl");
            var input = new AddAndSaveClientNotification(client);
            var existingEntity = new Entities.Client
            {
                Id = client.Id
            };
            _rentalContext.Clients.Add(existingEntity);
            _rentalContext.SaveChanges();

            Func<Task> act = async () => await _sut.Handle(input, new CancellationToken());

            act.Should().Throw<ArgumentException>();
        }
    }
}