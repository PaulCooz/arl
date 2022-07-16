using System;
using UnityEngine;

namespace Models
{
    public static class InputEvents
    {
        public static event Action<Vector2> OnMove;
        public static event Action OnBack;
        public static event Action OnInventoryOpen;

        public static void Move(Vector2 delta)
        {
            OnMove?.Invoke(delta);
        }

        public static void Back()
        {
            OnBack?.Invoke();
        }

        public static void OpenInventory()
        {
            OnInventoryOpen?.Invoke();
        }
    }
}
