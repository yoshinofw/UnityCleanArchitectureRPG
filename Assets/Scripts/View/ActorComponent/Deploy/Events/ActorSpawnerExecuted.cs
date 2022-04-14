using UnityEngine;

namespace UCARPG.View.ActorComponent
{
    public class ActorSpawnerExecuted
    {
        public string ConfigId { get; private set; }
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        public Vector3[] PatrolPath { get; private set; }

        public ActorSpawnerExecuted(string configId, Vector3 position, Quaternion rotation, Vector3[] patrolPath)
        {
            ConfigId = configId;
            Position = position;
            Rotation = rotation;
            PatrolPath = patrolPath;
        }
    }
}