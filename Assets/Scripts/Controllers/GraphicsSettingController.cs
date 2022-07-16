using UnityEngine;

namespace Controllers
{
    public class GraphicsSettingController : MonoBehaviour
    {
        [SerializeField]
        private int defaultFrameRate;

        private void Awake()
        {
            Application.targetFrameRate = defaultFrameRate;
        }
    }
}
