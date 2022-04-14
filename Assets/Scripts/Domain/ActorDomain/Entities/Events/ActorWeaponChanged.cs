namespace UCARPG.Domain.ActorDomain.Entities
{
    public class ActorWeaponChanged : ActorEvent
    {
        public ActorWeaponChanged(Actor actor) : base(actor) { }
    }
}