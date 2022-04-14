using UCARPG.Domain.Standard.UseCases;
using UCARPG.Domain.StatDomain.Entities;

namespace UCARPG.Domain.StatDomain.UseCases
{
    public class GetStatValueUseCase
    {
        private IRepository<Stat> _repository;

        public GetStatValueUseCase(IRepository<Stat> repository)
        {
            _repository = repository;
        }

        public float Execute(string id)
        {
            return _repository[id].Value;
        }
    }
}