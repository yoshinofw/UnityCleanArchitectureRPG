using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class MakeActorPerformActionUseCase
    {
        private IEventPublisher _eventPublisher;
        private IRepository<Actor> _repository;

        public MakeActorPerformActionUseCase(IEventPublisher eventPublisher, IRepository<Actor> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id, string action)
        {
            Actor actor = _repository[id];
            actor.PerformAction(action);
            _eventPublisher.PostAll(actor.Events);
        }
    }
}