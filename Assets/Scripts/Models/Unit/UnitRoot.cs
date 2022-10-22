using UnityEngine;

namespace Models.Unit
{
    public class UnitRoot : MonoBehaviour
    {
        [SerializeField]
        private BaseUnit unit;

        public BaseUnit Unit => unit;

        public void Initialization()
        {
            Unit.Initialization();
        }

        public void UnitDestroy() // call in animator
        {
            unit.UnitDestroy();
        }
    }
}