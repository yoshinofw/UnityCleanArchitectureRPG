namespace UCARPG.Domain.StatDomain.Entities
{
    public class StatValueModified : StatEvent
    {
        public StatValueModified(Stat stat) : base(stat) { }
    }
}