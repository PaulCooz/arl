using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models.PopupSystem
{
    [Serializable]
    public class PopupPrefabs : Dictionary<string, BasePopupController>, ISerializationCallbackReceiver
    {
        [Serializable]
        private struct Prefab
        {
            public string prefabName;
            public BasePopupController prefab;
        }

        [SerializeField]
        private List<Prefab> pairs = new();

        public void OnBeforeSerialize()
        {
            pairs.Clear();

            foreach (var (key, value) in this)
            {
                pairs.Add(new Prefab {prefabName = key, prefab = value});
            }
        }

        public void OnAfterDeserialize()
        {
            Clear();

            foreach (var pair in pairs)
            {
                if (TryAdd(pair.prefabName, pair.prefab)) continue;

                Add("prefabKey", pair.prefab);
            }
        }
    }
}
