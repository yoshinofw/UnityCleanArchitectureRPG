using System;
using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.StatDomain.Entities;

namespace UCARPG.Domain.StatDomain.UseCases
{
    public class CreateStatUseCase
    {
        private IEventPublisher _eventPublisher;
        private IRepository<Stat> _repository;

        public CreateStatUseCase(IEventPublisher eventPublisher, IRepository<Stat> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id, string actorId, string type, float maxValue)
        {
            id = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;
            Stat stat = new Stat(id, actorId, type, maxValue);
            _repository.Add(stat);
            _eventPublisher.PostAll(stat.Events);
        }
    }
}