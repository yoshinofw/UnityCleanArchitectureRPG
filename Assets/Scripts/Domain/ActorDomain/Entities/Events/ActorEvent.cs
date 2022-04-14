using System.Collections.ObjectModel;

namespace UCARPG.Domain.ActorDomain.Entities
{
    public abstract class ActorEvent
    {
        public string Id { get => _actor.Id; }
        public ReadOnlyDictionary<string, string> StatIdsByType { get; private set; }
        public string ConfigId { get => _actor.ConfigId; }
        public string WeaponConfigId { get => _actor.WeaponConfigId; }
        public string MagicConfigId { get => _actor.MagicConfigId; }
        public (float X, float Y) Direction { get => _actor.Direction; }
        public bool IsRun { get => _actor.IsRun; }
        public bool IsLocomotion { get => _actor.IsLocomotion; }
        public string Action { get => _actor.Action; }
        private Actor _actor;

        public ActorEvent(Actor actor)
        {
            _actor = actor;
            StatIdsByType = new ReadOnlyDictionary<string, string>(_actor.StatIdsByType);
        }
    }
}