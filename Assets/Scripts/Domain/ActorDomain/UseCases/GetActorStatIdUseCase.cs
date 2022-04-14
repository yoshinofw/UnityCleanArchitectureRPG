using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class GetActorStatIdUseCase
    {
        private IRepository<Actor> _repository;

        public GetActorStatIdUseCase(IRepository<Actor> repository)
        {
            _repository = repository;
        }

        public string Execute(string id, string type)
        {
            return _repository[id].StatIdsByType[type];
        }
    }
}