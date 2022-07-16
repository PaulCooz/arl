using Controllers.Items;
using Controllers.Map;
using Controllers.Units.Player;
using Models;
using Models.PopupSystem;
using UnityEngine;

namespace Controllers.Game
{
    public class GameSceneBehaviours : MonoBehaviour
    {
        [SerializeField]
        private MapCreatorController mapCreatorController;
        [SerializeField]
        private PlayerUnitController playerUnitController;
        [SerializeField]
        private PopupsController popupsController;
        [SerializeField]
        private PlayerInventoryController playerInventory;

        public MapCreatorController MapCreator => mapCreatorController;
        public PlayerUnitController PlayerUnit => playerUnitController;
        public PopupsController Popups => popupsController;
        public PlayerInventoryController PlayerInventory => playerInventory;
    }
}