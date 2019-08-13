﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rental.Core.Interfaces.DataAccess.BoardGameRequests;
using Rental.Core.Models;
using Rental.DataAccess.Context;
using Rental.DataAccess.Handlers.BoardGameHandlers;
using Rental.DataAccess.Mapping;
using Xunit;

namespace Rental.DataAccess.Tests.BoardGameHandlers
{
    public class UpdateAndSaveBoardGameNotificationHandlerTests
    {
        public UpdateAndSaveBoardGameNotificationHandlerTests()
        {
            _rentalContext = new RentalContext(new DbContextOptionsBuilder<RentalContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);
            _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<EntitiesMapping>(); }));
            _sut = new UpdateAndSaveBoardGameNotificationHandler(_mapper, _rentalContext);
        }

        private readonly IMapper _mapper;
        private readonly RentalContext _rentalContext;
        private readonly INotificationHandler<UpdateAndSaveBoardGameNotification> _sut;

        [Fact]
        public async Task Handle_Should_UpdateEntity_When_ItExists()
        {
            var input = new BoardGame("Test Updated", 20);
            var entities = new List<Entities.BoardGame>
            {
                new Entities.BoardGame
                {
                    Id = input.Id,
                    Name = "test1"
                },
                new Entities.BoardGame
                {
                    Id = Guid.NewGuid(),
                    Name = "test2"
                }
            };
            await _rentalContext.BoardGames.AddRangeAsync(entities);
            await _rentalContext.SaveChangesAsync();

            await _sut.Handle(new UpdateAndSaveBoardGameNotification(input), new CancellationToken());

            _rentalContext.BoardGames.Count().Should().Be(entities.Count);
            var result = _rentalContext.BoardGames.FirstOrDefault(x => x.Id == input.Id);
            result.Name.Should().Be(input.Name);
            result.Price.Should().Be(input.Price);
        }
    }
}