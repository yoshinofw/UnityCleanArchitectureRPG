using UCARPG.Utilities;

namespace UCARPG.View.ActorComponent
{
    public interface IEventBusUser
    {
        EventBus EventBus { set; }
    }
}