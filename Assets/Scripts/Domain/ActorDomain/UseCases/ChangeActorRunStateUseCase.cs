using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class ChangeActorRunStateUseCase
    {
        private IEventPublisher _eventPublisher;
        private IRepository<Actor> _repository;

        public ChangeActorRunStateUseCase(IEventPublisher eventPublisher, IRepository<Actor> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id, bool isRun)
        {
            Actor actor = _repository[id];
            actor.ChangeRunState(isRun);
            _eventPublisher.PostAll(actor.Events);
        }
    }
}