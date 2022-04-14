using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.ActorDomain.Entities;

namespace UCARPG.Domain.ActorDomain.UseCases
{
    public class GetActorMagicConfigIdUseCase
    {
        private IRepository<Actor> _repository;

        public GetActorMagicConfigIdUseCase(IRepository<Actor> repository)
        {
            _repository = repository;
        }

        public string Execute(string id)
        {
            return _repository[id].MagicConfigId;
        }
    }
}