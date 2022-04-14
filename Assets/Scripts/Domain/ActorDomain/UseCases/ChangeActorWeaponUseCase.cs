using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class ChangeActorWeaponUseCase
    {
        private IEventPublisher _eventPublisher;
        private IRepository<Actor> _repository;

        public ChangeActorWeaponUseCase(IEventPublisher eventPublisher, IRepository<Actor> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id, string configId)
        {
            Actor actor = _repository[id];
            actor.ChangeWeapon(configId);
            _eventPublisher.PostAll(actor.Events);
        }
    }
}