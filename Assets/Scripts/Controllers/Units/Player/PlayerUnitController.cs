using Models.Guns.Bullet;
using Models.Units;
using UnityEngine;

namespace Controllers.Units.Player
{
    public class PlayerUnitController : BaseUnit, IAttackListener
    {
        [SerializeField]
        private PlayerEventController playerEvents;

        public PlayerEventController PlayerEvents => playerEvents;

        public void Handle(in AttackData attackData)
        {
            Health -= attackData.damage;
            if (Health > 0) return;

            playerEvents.PlayerDie();
        }

        private void OnDestroy()
        {
            Application.Quit();
        }
    }
}
