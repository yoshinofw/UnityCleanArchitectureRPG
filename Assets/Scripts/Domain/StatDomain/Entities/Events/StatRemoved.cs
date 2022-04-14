namespace UCARPG.Domain.StatDomain.Entities
{
    public class StatRemoved : StatEvent
    {
        public StatRemoved(Stat stat) : base(stat) { }
    }
}