using Controllers.Game;
using Controllers.Items;
using Controllers.Items.GameItems;
using Controllers.Units.Player;
using Libs;
using Models.Guns.Bullet;
using Models.Units;
using SettingObjects;
using UnityEngine;
using Random = System.Random;

namespace Controllers.Units.Enemies.Slime
{
    public class SlimeUnitController : BaseUnit, IAttackListener
    {
        private PlayerUnitController _playerUnit;

        [SerializeField]
        private UnitsSettingObject unitsSettingObject;
        [SerializeField]
        private CoinItemController coinItemPrefab;
        [SerializeField]
        private SlimeGunController slimeGun;

        public Rigidbody2D PlayerRigidbody { get; private set; }
        public Random CurrentRandom { get; private set; }

        public void Init(in GameSceneBehaviours sceneBehaviours)
        {
            _playerUnit = sceneBehaviours.PlayerUnit;
            CurrentRandom = sceneBehaviours.MapCreator.Random;
        }

        protected override void Start()
        {
            base.Start();

            PlayerRigidbody = _playerUnit.OwnRigidbody;
            _playerUnit.PlayerEvents.onMapDone.AddListener(Die);

            slimeGun.Init(_playerUnit);
        }

        private void Die()
        {
            _playerUnit.PlayerEvents.onMapDone.RemoveListener(Die);

            Destroy(transform.gameObject);
        }

        public void Handle(in AttackData attackData)
        {
            Health -= attackData.damage;
            if (Health > 0) return;

            Die();
            CheckDrop();
        }

        private void CheckDrop()
        {
            if (!CurrentRandom.Chance(unitsSettingObject.unitsSetting.slimeSetting.dropItemChance)) return;

            var coin = Instantiate(coinItemPrefab, transform.position, Quaternion.identity);
            coin.SetEvents(_playerUnit.PlayerEvents);
        }
    }
}
