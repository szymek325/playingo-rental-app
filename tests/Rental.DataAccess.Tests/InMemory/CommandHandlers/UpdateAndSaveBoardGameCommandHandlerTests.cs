﻿// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using AutoMapper;
// using FluentAssertions;
// using Microsoft.EntityFrameworkCore;
// using Playingo.Application.Common.Mediator;
// using Playingo.Application.Interfaces.DataAccess.Commands;
// using Playingo.Domain.BoardGames;
// using Playingo.Infrastructure.Persistence.CommandHandlers;
// using Playingo.Infrastructure.Persistence.Context;
// using Playingo.Infrastructure.Persistence.Mapping;
// using Xunit;
//
// namespace Rental.DataAccess.Tests.InMemory.CommandHandlers
// {
//     public class UpdateAndSaveBoardGameCommandHandlerTests
//     {
//         public UpdateAndSaveBoardGameCommandHandlerTests()
//         {
//             var contextOptions = new DbContextOptionsBuilder<Context>()
//                 .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                 .Options;
//             _context = new Context(contextOptions);
//             IMapper mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<EntitiesMapping>(); }));
//             _sut = new UpdateAndSaveBoardGameCommandHandler(mapper, new Context(contextOptions));
//         }
//
//         private readonly Context _context;
//         private readonly ICommandHandler<UpdateAndSaveBoardGameCommand> _sut;
//
//         [Fact]
//         public void Handle_Should_Throw_When_EntityDoesNotExist()
//         {
//             var input = new BoardGame(Guid.NewGuid(), "Test Updated", 20);
//
//             Func<Task> act = async () =>
//                 await _sut.Handle(new UpdateAndSaveBoardGameCommand(input), new CancellationToken());
//
//             act.Should().Throw<DbUpdateConcurrencyException>();
//         }
//
//         [Fact]
//         public async Task Handle_Should_UpdateEntity_When_ItExists()
//         {
//             var input = new BoardGame(Guid.NewGuid(), "Test Updated", 20);
//             var entities = new List<Playingo.Infrastructure.Persistence.Entities.BoardGame>
//             {
//                 new Playingo.Infrastructure.Persistence.Entities.BoardGame
//                 {
//                     Id = input.Id,
//                     Name = "test1"
//                 },
//                 new Playingo.Infrastructure.Persistence.Entities.BoardGame
//                 {
//                     Id = Guid.NewGuid(),
//                     Name = "test2"
//                 }
//             };
//             await _context.BoardGames.AddRangeAsync(entities);
//             await _context.SaveChangesAsync();
//
//             await _sut.Handle(new UpdateAndSaveBoardGameCommand(input), new CancellationToken());
//
//             _context.BoardGames.Count().Should().Be(entities.Count);
//             var result = _context.BoardGames.FirstOrDefault(x => x.Id == input.Id);
//             result.Name.Should().Be(input.Name);
//             result.Price.Should().Be(input.Price);
//         }
//     }
// }

