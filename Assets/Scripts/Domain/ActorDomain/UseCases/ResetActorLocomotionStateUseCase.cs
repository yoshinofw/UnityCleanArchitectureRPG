using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class ResetActorLocomotionStateUseCase
    {
        IEventPublisher _eventPublisher;
        IRepository<Actor> _repository;

        public ResetActorLocomotionStateUseCase(IEventPublisher eventPublisher, IRepository<Actor> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id)
        {
            Actor actor = _repository[id];
            actor.ResetLocomotionState();
            _eventPublisher.PostAll(actor.Events);
        }
    }
}