using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class ChangeActorDirectionUseCase
    {
        private IEventPublisher _eventPublisher;
        private IRepository<Actor> _repository;

        public ChangeActorDirectionUseCase(IEventPublisher eventPublisher, IRepository<Actor> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id, float x, float y)
        {
            Actor actor = _repository[id];
            actor.ChangeDirection(x, y);
            _eventPublisher.PostAll(actor.Events);
        }
    }
}