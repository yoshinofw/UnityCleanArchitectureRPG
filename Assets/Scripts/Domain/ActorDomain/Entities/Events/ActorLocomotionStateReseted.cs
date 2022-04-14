namespace UCARPG.Domain.ActorDomain.Entities
{
    public class ActorLocomotionStateReseted : ActorEvent
    {
        public ActorLocomotionStateReseted(Actor actor) : base(actor) { }
    }
}