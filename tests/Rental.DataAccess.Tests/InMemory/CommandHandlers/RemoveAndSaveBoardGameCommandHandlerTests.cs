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
//     public class RemoveAndSaveBoardGameCommandHandlerTests
//     {
//         public RemoveAndSaveBoardGameCommandHandlerTests()
//         {
//             var contextOptions = new DbContextOptionsBuilder<Context>()
//                 .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                 .Options;
//             _context = new Context(contextOptions);
//             IMapper mapper = new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<EntitiesMapping>(); }));
//             _sut = new RemoveAndSaveBoardGameCommandHandler(mapper, new Context(contextOptions));
//         }
//
//         private readonly Context _context;
//         private readonly ICommandHandler<RemoveAndSaveBoardGameCommand> _sut;
//
//         [Fact]
//         public async Task Handle_Should_RemoveClientFromDb_When_ClientExists()
//         {
//             var boardGame = new BoardGame(Guid.NewGuid(), "SomeGame", 15);
//             var entities = new List<Playingo.Infrastructure.Persistence.Entities.BoardGame>
//             {
//                 new Playingo.Infrastructure.Persistence.Entities.BoardGame
//                 {
//                     Id = boardGame.Id,
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
//             await _sut.Handle(new RemoveAndSaveBoardGameCommand(boardGame), new CancellationToken());
//
//             _context.BoardGames.Any(x => x.Id == boardGame.Id).Should().BeFalse();
//         }
//
//         [Fact]
//         public void Handle_Should_ThrowDbUpdateConcurrencyException_When_BoardGameWithProvidedIdDoesNotExist()
//         {
//             var boardGame = new BoardGame(Guid.NewGuid(), "SomeGame", 15);
//
//             Func<Task> act = async () =>
//                 await _sut.Handle(new RemoveAndSaveBoardGameCommand(boardGame), new CancellationToken());
//
//             act.Should().Throw<DbUpdateConcurrencyException>();
//         }
//     }
// }

