using UnityEngine.InputSystem;

namespace Input.Scripts
{
#if UNITY_EDITOR
    [UnityEditor.InitializeOnLoad]
#endif
    public class HoldingInteraction : IInputInteraction
    {
        private InputInteractionContext _context;
        private bool _isUpdating;

        static HoldingInteraction()
        {
            InputSystem.RegisterInteraction<HoldingInteraction>();
        }

        public void Process(ref InputInteractionContext context)
        {
            if (context.phase == InputActionPhase.Waiting && context.ControlIsActuated())
            {
                context.Started();

                _isUpdating = true;
                InputSystem.onAfterUpdate += OnUpdate;
            }

            _context = context;
        }

        private void OnUpdate()
        {
            if (_context.phase is InputActionPhase.Canceled or InputActionPhase.Disabled ||
                !_context.ControlIsActuated())
            {
                _context.Canceled();
                return;
            }

            _context.PerformedAndStayPerformed();
        }

        public void Reset()
        {
            if (!_isUpdating) return;

            _isUpdating = false;
            InputSystem.onAfterUpdate -= OnUpdate;
        }
    }
}