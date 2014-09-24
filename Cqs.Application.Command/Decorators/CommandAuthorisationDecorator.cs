namespace Cqs.Application.Command.Decorators
{
    using Cqs.Application.Authorisation;
    using Cqs.Application.Command.Commands;
    using Cqs.Application.Command.Handlers;
    using Cqs.Infrastructure.Dto;

    public class CommandAuthorisationDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : Command, IAuthorisable where TResult : Dto
    {
        private readonly IAuthorisationManager authorisationManager;
        public TResult Result { get; private set; }

        private readonly ICommandHandler<TCommand, TResult> unitOfWorkDecorator;

        public CommandAuthorisationDecorator(
            IAuthorisationManager authorisationManager,
            ICommandHandler<TCommand, TResult> unitOfWorkDecorator)
        {
            this.authorisationManager = authorisationManager;
            this.unitOfWorkDecorator = unitOfWorkDecorator;
        }

        public void Handle(TCommand command)
        {
            this.authorisationManager.Authorise(command);
            this.unitOfWorkDecorator.Handle(command);
            this.Result = this.unitOfWorkDecorator.Result;
        }
    }
}