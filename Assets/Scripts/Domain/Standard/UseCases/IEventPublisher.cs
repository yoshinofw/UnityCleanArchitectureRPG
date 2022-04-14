using System.Collections.Generic;

namespace UCARPG.Domain.Standard.UseCases
{
    public interface IEventPublisher
    {
        void Post(object signal);
        void PostAll(List<object> signals);
    }
}