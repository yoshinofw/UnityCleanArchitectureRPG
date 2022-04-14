using System.Collections.Generic;
using UCARPG.Domain.Standard.Eneities;

namespace UCARPG.Domain.StatDomain.Entities
{
    public class Stat : IEntity
    {
        public List<object> Events { get; private set; }
        public string Id { get; private set; }
        public string ActorId { get; private set; }
        public string Type { get; private set; }
        public float MaxValue { get; private set; }
        public float Value { get; private set; }

        public Stat(string id, string actorId, string type, float maxValue)
        {
            Events = new List<object>();
            Id = id;
            ActorId = actorId;
            Type = type;
            MaxValue = maxValue;
            Value = MaxValue;
            Events.Add(new StatCreated(this));
        }

        public void ModifyValue(float modifier)
        {
            float original = Value;
            Value += modifier;
            if (Value < 0)
            {
                Value = 0;
            }
            else if (Value > MaxValue)
            {
                Value = MaxValue;
            }
            if (original != Value)
            {
                Events.Add(new StatValueModified(this));
            }
        }
    }
}