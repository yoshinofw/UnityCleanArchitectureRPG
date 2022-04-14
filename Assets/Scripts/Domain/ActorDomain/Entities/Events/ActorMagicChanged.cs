namespace UCARPG.Domain.ActorDomain.Entities
{
    public class ActorMagicChanged : ActorEvent
    {
        public ActorMagicChanged(Actor actor) : base(actor) { }
    }
}