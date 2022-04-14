namespace UCARPG.Domain.ActorDomain.Entities
{
    public class ActorRemoved : ActorEvent
    {
        public ActorRemoved(Actor actor) : base(actor) { }
    }
}