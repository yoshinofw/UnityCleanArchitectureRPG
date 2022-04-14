namespace UCARPG.Domain.ActorDomain.Entities
{
    public class ActorDirectionChanged : ActorEvent
    {
        public ActorDirectionChanged(Actor actor) : base(actor) { }
    }
}