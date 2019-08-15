﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rental.Core.Helpers;
using Rental.Core.Interfaces.DataAccess.Commands;
using Rental.Core.Interfaces.DataAccess.Queries;
using Rental.Core.Models;
using Rental.Core.Requests.BoardGames;

namespace Rental.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BoardGameController
    {
        private readonly IMediatorService _mediatorService;

        public BoardGameController(IMediatorService mediatorService)
        {
            _mediatorService = mediatorService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInput input)
        {
            var newGuid = Guid.NewGuid();
            var result = await _mediatorService.Request(new AddBoardGameRequest(newGuid, input.Name, input.Price));
            return new OkObjectResult(new CreateOutput(newGuid, result.Message));
        }

        [HttpPut]
        public async Task<IActionResult> Update(BoardGame input)
        {
            await _mediatorService.Notify(new UpdateAndSaveBoardGameCommand(input));
            return new OkResult();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(Guid id)
        {
            var result = await _mediatorService.Request(new RemoveBoardGameRequest(id));
            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediatorService.Request(new GetBoardGameByIdQuery(id));
            return new OkObjectResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediatorService.Request(new GetAllBoardGamesQuery());
            return new OkObjectResult(result);
        }
    }

    public class CreateOutput
    {
        public CreateOutput(Guid newBoardGameId)
        {
            NewBoardGameId = newBoardGameId;
        }

        public CreateOutput(Guid newBoardGameId, string message)
        {
            NewBoardGameId = newBoardGameId;
            Message = message;
        }

        public Guid NewBoardGameId { get; set; }
        public string Message { get; set; }
    }

    public class CreateInput
    {
        public string Name { get; set; }
        public float Price { get; set; }
    }
}