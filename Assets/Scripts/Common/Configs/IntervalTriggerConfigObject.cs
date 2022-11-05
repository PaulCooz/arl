using UnityEngine;

namespace Common.Configs
{
    [CreateAssetMenu]
    public class IntervalTriggerConfigObject : ScriptableObject
    {
        public float interval;
        public int count;
        public bool isEnemyTrigger;
        public Vector2 colliderSize;
        public Vector2 colliderOffset;
        public float colliderEdgeRadius;
        public float damage;
        public string onTrigger;
    }
}