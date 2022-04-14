namespace UCARPG.Domain.ActorDomain.Entities
{
    public class ActorActionPerformed : ActorEvent
    {
        public ActorActionPerformed(Actor actor) : base(actor) { }
    }
}