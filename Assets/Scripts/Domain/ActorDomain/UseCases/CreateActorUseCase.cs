using System;
using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class CreateActorUseCase
    {
        private IEventPublisher _eventPublisher;
        private IRepository<Actor> _repository;

        public CreateActorUseCase(IEventPublisher eventPublisher, IRepository<Actor> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id, string configId, string weaponConfigId)
        {
            id = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;
            Actor actor = new Actor(id, configId, weaponConfigId);
            _repository.Add(actor);
            _eventPublisher.PostAll(actor.Events);
        }
    }
}