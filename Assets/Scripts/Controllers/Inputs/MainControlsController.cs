using Input;
using Models;
using UnityEngine;

namespace Controllers.Inputs
{
    public class MainControlsController : MonoBehaviour
    {
        private MainControls _mainControls;

        private Vector2? _moveDirection;

        private void Start()
        {
            _mainControls = new MainControls();

            _mainControls.Player.Move.performed += context => _moveDirection = context.ReadValue<Vector2>();
            _mainControls.Player.Move.canceled += _ => _moveDirection = null;
            _mainControls.Player.Back.performed += _ => InputEvents.Back();
            _mainControls.Player.OpenInventory.performed += _ => InputEvents.OpenInventory();

            _mainControls.Enable();
        }

        private void Update()
        {
            if (_moveDirection.HasValue) InputEvents.Move(_moveDirection.Value);
        }
    }
}
