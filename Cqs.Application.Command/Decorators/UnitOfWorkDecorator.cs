namespace Cqs.Application.Command.Decorators
{
    using Cqs.Application.Command.Commands;
    using Cqs.Application.Command.Handlers;
    using Cqs.Infrastructure.Dto;
    using Cqs.Infrastructure.EntityFramework;

    public class UnitOfWorkDecorator<TCommand, TResult> : IUnitOfWorkCommandHandler<TCommand, TResult>
        where TCommand : Command where TResult : Dto
    {
        private readonly ICommandHandler<TCommand, TResult> decoratedCommandHandler;
        
        public UnitOfWorkDecorator(
            ICommandHandler<TCommand, TResult> decoratedCommandHandler)
        {
            this.decoratedCommandHandler = decoratedCommandHandler;
        }

        public CqsCommandContext CqsContext { get; set; }
        public TResult Result { get; private set; }

        public void Handle(TCommand command)
        {
            using (this.CqsContext = new CqsCommandContext())
            using (var transaction = this.CqsContext.Database.BeginTransaction())
            {
                ((IUnitOfWorkCommandHandler<TCommand, TResult>)decoratedCommandHandler).CqsContext = this.CqsContext;
                this.decoratedCommandHandler.Handle(command);
                this.CqsContext.SaveChanges();
                transaction.Commit();
            }

            this.Result = this.decoratedCommandHandler.Result;
        }
    }
}


