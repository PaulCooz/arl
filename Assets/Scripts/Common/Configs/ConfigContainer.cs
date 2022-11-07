using System.Linq;
using UnityEngine;

namespace Common.Configs
{
    [CreateAssetMenu]
    public class ConfigContainer : ScriptableObject
    {
        [SerializeField]
        private Pair<string, IntervalTriggerConfigObject>[] triggerConfigs;

        private static ConfigContainer _instance;

        private static ConfigContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<ConfigContainer>("Configs/ConfigsContainer");
                }

                return _instance;
            }
        }

        public static IntervalTriggerConfigObject GetTrigger(string config)
        {
            return Instance.triggerConfigs.First(c => c.key == config).value;
        }
    }
}