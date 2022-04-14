using UCARPG.Domain.ActorDomain.UseCases;

namespace UCARPG.Domain.ActorDomain.InterfaceAdapters
{
    public class ActorController
    {
        private CreateActorUseCase _createActorUseCase;
        private ChangeActorDirectionUseCase _changeActorDirectionUseCase;
        private ChangeActorRunStateUseCase _changeActorRunStateUseCase;
        private ResetActorLocomotionStateUseCase _resetActorLocomotionStateUseCase;
        private MakeActorPerformActionUseCase _makeActorPerformActionUseCase;
        private ChangeActorWeaponUseCase _changeActorWeaponUseCase;
        private ChangeActorMagicUseCase _changeActorMagicUseCase;
        private RemoveActorUseCase _removeActorUseCase;
        private CommitActorStatIdUseCase _commitActorStatIdUseCase;
        private GetActorActionUseCase _getActorActionUseCase;
        private GetActorStatIdUseCase _getActorStatIdUseCase;
        private GetActorConfigIdUseCase _getActorConfigIdUseCase;
        private GetActorWeaponConfigIdUseCase _getActorWeaponConfigIdUseCase;
        private GetActorMagicConfigIdUseCase _getActorMagicConfigIdUseCase;

        public ActorController(CreateActorUseCase createActorUseCase,
                               ChangeActorDirectionUseCase changeActorDirectionUseCase,
                               ChangeActorRunStateUseCase changeActorRunStateUseCase,
                               ResetActorLocomotionStateUseCase resetActorLocomotionStateUseCase,
                               MakeActorPerformActionUseCase makeActorPerformActionUseCase,
                               ChangeActorWeaponUseCase changeActorWeaponUseCase,
                               ChangeActorMagicUseCase changeActorMagicUseCase,
                               RemoveActorUseCase removeActorUseCase,
                               CommitActorStatIdUseCase commitActorStatIdUseCase,
                               GetActorActionUseCase getActorActionUseCase,
                               GetActorStatIdUseCase getActorStatIdUseCase,
                               GetActorConfigIdUseCase getActorConfigIdUseCase,
                               GetActorWeaponConfigIdUseCase getActorWeaponConfigIdUseCase,
                               GetActorMagicConfigIdUseCase getActorMagicConfigIdUseCase)
        {
            _createActorUseCase = createActorUseCase;
            _changeActorDirectionUseCase = changeActorDirectionUseCase;
            _changeActorRunStateUseCase = changeActorRunStateUseCase;
            _resetActorLocomotionStateUseCase = resetActorLocomotionStateUseCase;
            _makeActorPerformActionUseCase = makeActorPerformActionUseCase;
            _changeActorWeaponUseCase = changeActorWeaponUseCase;
            _changeActorMagicUseCase = changeActorMagicUseCase;
            _removeActorUseCase = removeActorUseCase;
            _commitActorStatIdUseCase = commitActorStatIdUseCase;
            _getActorActionUseCase = getActorActionUseCase;
            _getActorStatIdUseCase = getActorStatIdUseCase;
            _getActorConfigIdUseCase = getActorConfigIdUseCase;
            _getActorWeaponConfigIdUseCase = getActorWeaponConfigIdUseCase;
            _getActorMagicConfigIdUseCase = getActorMagicConfigIdUseCase;
        }

        public void CreateActor(string configId, string weaponConfigId)
        {
            _createActorUseCase.Execute(string.Empty, configId, weaponConfigId);
        }

        public void ChangeActorDirection(string id, float x, float y)
        {
            _changeActorDirectionUseCase.Execute(id, x, y);
        }

        public void ChangeActorRunState(string id, bool isRun)
        {
            _changeActorRunStateUseCase.Execute(id, isRun);
        }

        public void ResetActorLocomotionState(string id)
        {
            _resetActorLocomotionStateUseCase.Execute(id);
        }

        public void MakeActorPerformAction(string id, string action)
        {
            _makeActorPerformActionUseCase.Execute(id, action);
        }

        public void ChangeActorWeapon(string id, string configId)
        {
            _changeActorWeaponUseCase.Execute(id, configId);
        }

        public void ChangeActorMagic(string id, string configId)
        {
            _changeActorMagicUseCase.Execute(id, configId);
        }

        public void RemoveActor(string id)
        {
            _removeActorUseCase.Execute(id);
        }

        public void CommitActorStatId(string id, string statType, string statId)
        {
            _commitActorStatIdUseCase.Execute(id, statType, statId);
        }

        public string GetActorAction(string id)
        {
            return _getActorActionUseCase.Execute(id);
        }

        public string GetActorStatId(string id, string type)
        {
            return _getActorStatIdUseCase.Execute(id, type);
        }

        public string GetActorConfigId(string id)
        {
            return _getActorConfigIdUseCase.Execute(id);
        }

        public string GetActorWeaponConfigId(string id)
        {
            return _getActorWeaponConfigIdUseCase.Execute(id);
        }

        public string GetActorMagicConfigId(string id)
        {
            return _getActorMagicConfigIdUseCase.Execute(id);
        }
    }
}