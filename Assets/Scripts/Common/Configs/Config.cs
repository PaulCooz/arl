using System.Linq;
using UnityEngine;

namespace Common.Configs
{
    [CreateAssetMenu]
    public class Config : ScriptableObject
    {
        [SerializeField]
        private Pair<string, UnitConfigObject>[] unitConfigs;
        [SerializeField]
        private Pair<string, IntervalTriggerConfigObject>[] triggerConfigs;

        private static Config _instance;

        public static Config Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<Config>("Configs/MainConfig");
                }

                return _instance;
            }
        }

        public static UnitConfigObject GetUnit(string config)
        {
            return Instance.unitConfigs.First(c => c.key == config).value;
        }

        public static IntervalTriggerConfigObject GetTrigger(string config)
        {
            return Instance.triggerConfigs.First(c => c.key == config).value;
        }
    }
}