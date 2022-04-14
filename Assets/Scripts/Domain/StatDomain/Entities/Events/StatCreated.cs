namespace UCARPG.Domain.StatDomain.Entities
{
    public class StatCreated : StatEvent
    {
        public StatCreated(Stat stat) : base(stat) { }
    }
}