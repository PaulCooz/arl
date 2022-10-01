using UnityEngine;

namespace Abstracts.Models.Unit
{
    public class UnitRoot : MonoBehaviour
    {
        [SerializeField]
        private BaseUnit unit;

        public BaseUnit Unit => unit;
    }
}