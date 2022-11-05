using UnityEngine;

namespace Common.Configs
{
    [CreateAssetMenu]
    public class UnitConfigObject : ScriptableObject
    {
        public GunConfigObject gunConfig;
        public BulletConfigObject bulletConfig;

        public int health;
        public float speed;
        public string onHpChange;
        public string onDie;
        public string levelExp;
        public string prefabName;
    }
}