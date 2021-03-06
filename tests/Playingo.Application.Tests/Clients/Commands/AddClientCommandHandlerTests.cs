﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Playingo.Application.Clients.Commands;
using Playingo.Application.Common.Exceptions;
using Playingo.Application.Common.Interfaces;
using Playingo.Application.Common.Mediator;
using Playingo.Application.Common.Validation;
using Playingo.Domain.Clients;
using Xunit;

namespace Playingo.Application.Tests.Clients.Commands
{
    public class AddClientCommandHandlerTests
    {
        public AddClientCommandHandlerTests()
        {
            _validator = new Mock<IValidator<Client>>(MockBehavior.Strict);
            _validationMessageBuilder = new Mock<IValidationMessageBuilder>(MockBehavior.Strict);
            _unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _sut = new AddClientCommandHandler(_unitOfWork.Object, _validationMessageBuilder.Object, _validator.Object);
        }

        private readonly Mock<IValidator<Client>> _validator;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IValidationMessageBuilder> _validationMessageBuilder;
        private readonly ICommandHandler<AddClientCommand> _sut;

        private readonly AddClientCommand _inputCommand =
            new AddClientCommand(Guid.NewGuid(), "First Name", "Last Name", "123456", "email.google.pl");

        [Fact]
        public async Task Handle_Should_SendAddAndSaveCommand_When_ValidationIsPassed()
        {
            _validator.Setup(x => x.Validate(It.Is((Client client) => client.Id == _inputCommand.NewClientGuid)))
                .Returns(new ValidationResult());
            var cancellationToken = new CancellationToken();
            _unitOfWork.Setup(x =>
                x.ClientRepository.AddAsync(It.Is((Client b) => b.Id == _inputCommand.NewClientGuid),
                    cancellationToken)).Returns(Task.CompletedTask);
            _unitOfWork.Setup(x => x.SaveChangesAsync(cancellationToken)).Returns(Task.CompletedTask);

            await _sut.Handle(_inputCommand, cancellationToken);

            _unitOfWork.Verify(
                x => x.ClientRepository.AddAsync(It.Is((Client b) => b.Id == _inputCommand.NewClientGuid),
                    cancellationToken), Times.Once);
            _unitOfWork.Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
        }

        [Fact]
        public void Handle_Should_ThrowCustomValidationException_When_ValidatorReturnsValidationErrors()
        {
            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("test", "test")
            };
            _validator.Setup(x => x.Validate(It.Is((Client client) => client.Id == _inputCommand.NewClientGuid)))
                .Returns(new ValidationResult(validationErrors));
            const string errorsMessage = "errors happened";
            _validationMessageBuilder.Setup(x => x.CreateMessage(validationErrors)).Returns(errorsMessage);

            Func<Task> act = async () => await _sut.Handle(_inputCommand, new CancellationToken());

            act.Should().Throw<CustomValidationException>().WithMessage(errorsMessage);
        }

        [Fact]
        public void Handle_Should_ThrowException_When_AddAndSaveClientThrows()
        {
            var exception = new ArgumentException("test");
            var cancellationToken = new CancellationToken();
            _validator.Setup(x => x.Validate(It.IsAny<Client>())).Returns(new ValidationResult());
            _unitOfWork.Setup(x =>
                x.ClientRepository.AddAsync(It.Is((Client b) => b.Id == _inputCommand.NewClientGuid),
                    cancellationToken)).Returns(Task.CompletedTask);
            _unitOfWork.Setup(x => x.SaveChangesAsync(cancellationToken)).Throws(exception);

            Func<Task> act = async () => await _sut.Handle(_inputCommand, new CancellationToken());

            act.Should().Throw<ArgumentException>().WithMessage(exception.Message);
        }

        [Fact]
        public void Handle_Should_ThrowException_When_GetFormattedValidationMessageQueryThrows()
        {
            var exception = new ArgumentException("test");
            _validator.Setup(x => x.Validate(It.IsAny<Client>())).Returns(new ValidationResult(
                new List<ValidationFailure>
                {
                    new ValidationFailure("test", "test")
                }));
            _validationMessageBuilder.Setup(x => x.CreateMessage(It.IsAny<IList<ValidationFailure>>()))
                .Throws(exception);

            Func<Task> act = async () => await _sut.Handle(_inputCommand, new CancellationToken());

            act.Should().Throw<ArgumentException>().WithMessage(exception.Message);
        }

        [Fact]
        public void Handle_Should_ThrowException_When_ValidatorThrows()
        {
            var exception = new ArgumentException("test");
            _validator.Setup(x => x.Validate(It.IsAny<Client>())).Throws(exception);

            Func<Task> act = async () => await _sut.Handle(_inputCommand, new CancellationToken());

            act.Should().Throw<ArgumentException>().WithMessage(exception.Message);
        }
    }
}