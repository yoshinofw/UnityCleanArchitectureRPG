using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.StatDomain.Entities;

namespace UCARPG.Domain.StatDomain.UseCases
{
    public class RemoveStatUseCase
    {
        private IEventPublisher _eventPublisher;
        private IRepository<Stat> _repository;

        public RemoveStatUseCase(IEventPublisher eventPublisher, IRepository<Stat> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id)
        {
            Stat stat = _repository[id];
            _repository.Remove(id);
            _eventPublisher.Post(new StatRemoved(stat));
        }
    }
}