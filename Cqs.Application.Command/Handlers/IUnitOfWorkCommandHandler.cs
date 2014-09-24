namespace Cqs.Application.Command.Handlers
{
    using Cqs.Application.Command.Commands;
    using Cqs.Infrastructure.Dto;
    using Cqs.Infrastructure.EntityFramework;

    public interface IUnitOfWorkCommandHandler<in TCommand, out TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : Command where TResult : Dto
    {
        CqsCommandContext CqsContext { get; set; }
    }
}
