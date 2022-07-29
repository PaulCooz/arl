using Input;
using UnityEngine;

namespace Realisations.Controllers
{
    public class MainControlsController : MonoBehaviour
    {
        private MainControls _mainControls;

        [SerializeField]
        private InputEventsController inputEvents;

        private void Start()
        {
            _mainControls = new MainControls();

            SetUpControls();

            _mainControls.Enable();
        }

        private void SetUpControls()
        {
            _mainControls.Player.Move.performed += c => inputEvents.Move(c.ReadValue<Vector2>());
        }
    }
}