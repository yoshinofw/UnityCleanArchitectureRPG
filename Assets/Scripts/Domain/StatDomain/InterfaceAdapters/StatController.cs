using UCARPG.Domain.StatDomain.UseCases;

namespace UCARPG.Domain.StatDomain.InterfaceAdapters
{
    public class StatController
    {
        private CreateStatUseCase _createStatUseCase;
        private ModifyStatValueUseCase _modifyStatValueUseCase;
        private RemoveStatUseCase _removeStatUseCase;
        private GetStatValueUseCase _getStatValueUseCase;

        public StatController(CreateStatUseCase createStatUseCase,
                              ModifyStatValueUseCase modifyStatValueUseCase,
                              RemoveStatUseCase removeStatUseCase,
                              GetStatValueUseCase getStatValueUseCase)
        {
            _createStatUseCase = createStatUseCase;
            _modifyStatValueUseCase = modifyStatValueUseCase;
            _removeStatUseCase = removeStatUseCase;
            _getStatValueUseCase = getStatValueUseCase;
        }

        public void CreateStat(string actorId, string type, float maxValue)
        {
            _createStatUseCase.Execute(string.Empty, actorId, type, maxValue);
        }

        public void ModifyStatValue(string id, float modifier)
        {
            _modifyStatValueUseCase.Execute(id, modifier);
        }

        public void RemoveStat(string id)
        {
            _removeStatUseCase.Execute(id);
        }

        public float GetStatValue(string id)
        {
            return _getStatValueUseCase.Execute(id);
        }
    }
}