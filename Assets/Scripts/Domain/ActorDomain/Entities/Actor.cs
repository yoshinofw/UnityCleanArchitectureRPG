using System.Collections.Generic;
using UCARPG.Domain.Standard.Eneities;

namespace UCARPG.Domain.ActorDomain.Entities
{
    public class Actor : IEntity
    {
        public List<object> Events { get; private set; }
        public string Id { get; private set; }
        public Dictionary<string, string> StatIdsByType { get; private set; }
        public string ConfigId { get; private set; }
        public string WeaponConfigId { get; private set; }
        public string MagicConfigId { get; private set; }
        public (float X, float Y) Direction { get; private set; }
        public bool IsRun { get; private set; }
        public bool IsLocomotion { get; private set; }
        public string Action { get; private set; }

        public Actor(string id, string configId, string weaponConfigId)
        {
            Events = new List<object>();
            Id = id;
            StatIdsByType = new Dictionary<string, string>();
            ConfigId = configId;
            WeaponConfigId = weaponConfigId;
            Events.Add(new ActorCreated(this));
        }

        public void ChangeDirection(float x, float y)
        {
            if (Direction.X == x && Direction.Y == y)
            {
                return;
            }
            Direction = (x, y);
            Events.Add(new ActorDirectionChanged(this));
        }

        public void ChangeRunState(bool isRun)
        {
            if (IsRun == isRun)
            {
                return;
            }
            IsRun = isRun;
            Events.Add(new ActorRunStateChanged(this));
        }

        public void ResetLocomotionState()
        {
            if (IsLocomotion)
            {
                return;
            }
            IsLocomotion = true;
            Action = string.Empty;
            Events.Add(new ActorLocomotionStateReseted(this));
        }

        public void PerformAction(string action)
        {
            switch (action)
            {
                case "GetHit":
                case "Death":
                    if (Action == "Death")
                    {
                        return;
                    }
                    break;
                default:
                    if (!IsLocomotion)
                    {
                        return;
                    }
                    break;
            }
            IsLocomotion = false;
            Action = action;
            Events.Add(new ActorActionPerformed(this));
        }

        public void ChangeWeapon(string configId)
        {
            if (WeaponConfigId == configId)
            {
                return;
            }
            WeaponConfigId = configId;
            Events.Add(new ActorWeaponChanged(this));
        }

        public void ChangeMagic(string configId)
        {
            if (MagicConfigId == configId)
            {
                return;
            }
            MagicConfigId = configId;
            Events.Add(new ActorMagicChanged(this));
        }
    }
}