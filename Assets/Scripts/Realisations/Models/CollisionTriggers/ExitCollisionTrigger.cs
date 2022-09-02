using Abstracts.Models.CollisionTriggers;
using Common.Keys;
using UnityEngine;

namespace Realisations.Models.CollisionTriggers
{
    public class ExitCollisionTrigger : BaseCollisionTrigger
    {
        private GameMaster _gameMaster;

        public void Init(in GameMaster gameMaster)
        {
            _gameMaster = gameMaster;
        }

        protected override bool IsTrigger(in Collider2D other)
        {
            var isPlayer = other.CompareTag(Tags.Player);
            if (isPlayer) _gameMaster.NextLevel();

            return isPlayer;
        }
    }
}