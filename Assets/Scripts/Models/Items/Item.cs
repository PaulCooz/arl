using Common.Interpreters;
using UnityEngine;

namespace Models.Items
{
    public class Item : MonoBehaviour
    {
        private ItemData itemData;

        public void Setup(in ItemData data)
        {
            itemData = data;
        }

        public void OnCollect()
        {
            var script = new Script();
            script.SetVariable("item_id", itemData.id.ToScriptValue());
            script.Run(itemData.command);

            Destroy(gameObject);
        }
    }
}