namespace UCARPG.Domain.ActorDomain.Entities
{
    public class ActorRunStateChanged : ActorEvent
    {
        public ActorRunStateChanged(Actor actor) : base(actor) { }
    }
}