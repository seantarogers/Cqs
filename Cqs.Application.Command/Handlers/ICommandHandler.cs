namespace Cqs.Application.Command.Handlers
{
    using Cqs.Infrastructure.Dto;

    public interface ICommandHandler<in TCommand, out TResult>
        where TCommand : Commands.Command where TResult : Dto
    {
        TResult Result { get; }
        void Handle(TCommand command);
    }
}