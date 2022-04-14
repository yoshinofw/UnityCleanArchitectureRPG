using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class CommitActorStatIdUseCase
    {
        private IRepository<Actor> _repository;

        public CommitActorStatIdUseCase(IRepository<Actor> repository)
        {
            _repository = repository;
        }

        public void Execute(string id, string statType, string statId)
        {
            _repository[id].StatIdsByType.Add(statType, statId);
        }
    }
}