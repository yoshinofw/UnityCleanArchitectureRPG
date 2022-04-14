using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class RemoveActorUseCase
    {
        private IEventPublisher _eventPublisher;
        private IRepository<Actor> _repository;

        public RemoveActorUseCase(IEventPublisher eventPublisher, IRepository<Actor> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id)
        {
            Actor actor = _repository[id];
            _repository.Remove(id);
            _eventPublisher.Post(new ActorRemoved(actor));
        }
    }
}