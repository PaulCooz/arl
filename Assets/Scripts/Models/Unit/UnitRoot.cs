using UnityEngine;

namespace Models.Unit
{
    public class UnitRoot : MonoBehaviour
    {
        [SerializeField]
        private BaseUnit unit;

        public BaseUnit Unit => unit;
    }
}