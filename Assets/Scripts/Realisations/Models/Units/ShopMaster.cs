using Abstracts.Models.Units;
using Common.Keys;
using UnityEngine;

namespace Realisations.Models.Units
{
    public class ShopMaster : BaseUnit
    {
        private void OnTriggerEnter2D(Collider2D collider2d)
        {
            if (!collider2d.CompareTag(Tags.ShopTrigger)) return;

            Debug.Log("can open shop");
        }

        private void OnTriggerExit2D(Collider2D collider2d)
        {
            if (!collider2d.CompareTag(Tags.ShopTrigger)) return;

            Debug.Log("can't open shop");
        }
    }
}