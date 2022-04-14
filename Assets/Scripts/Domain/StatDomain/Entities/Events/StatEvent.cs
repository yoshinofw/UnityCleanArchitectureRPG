namespace UCARPG.Domain.StatDomain.Entities
{
    public abstract class StatEvent
    {
        public string Id { get => _stat.Id; }
        public string ActorId { get => _stat.ActorId; }
        public string Type { get => _stat.Type; }
        public float MaxValue { get => _stat.MaxValue; }
        public float Value { get => _stat.Value; }
        private Stat _stat;

        public StatEvent(Stat stat)
        {
            _stat = stat;
        }
    }
}