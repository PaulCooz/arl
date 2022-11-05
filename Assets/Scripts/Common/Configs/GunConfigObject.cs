using UnityEngine;

namespace Common.Configs
{
    [CreateAssetMenu]
    public class GunConfigObject : ScriptableObject
    {
        public float scatter;
        public int bulletsCount;
        public float attackRadius;
        public float attackSpeed;
    }
}