using UnityEngine;

namespace Common.Configs
{
    [CreateAssetMenu]
    public class BulletConfigObject : ScriptableObject
    {
        public Color color;
        public float damage;
        public float force;

        [TextArea]
        public string onCollide;
    }
}