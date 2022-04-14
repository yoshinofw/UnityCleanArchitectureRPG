namespace UCARPG.Domain.ActorDomain.Entities
{
    public class ActorCreated : ActorEvent
    {
        public ActorCreated(Actor actor) : base(actor) { }
    }
}