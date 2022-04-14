using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.StatDomain.Entities;

namespace UCARPG.Domain.StatDomain.UseCases
{
    public class ModifyStatValueUseCase
    {
        private IEventPublisher _eventPublisher;
        private IRepository<Stat> _repository;

        public ModifyStatValueUseCase(IEventPublisher eventPublisher, IRepository<Stat> repository)
        {
            _eventPublisher = eventPublisher;
            _repository = repository;
        }

        public void Execute(string id, float modifier)
        {
            Stat stat = _repository[id];
            stat.ModifyValue(modifier);
            _eventPublisher.PostAll(stat.Events);
        }
    }
}