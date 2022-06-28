using UnityEngine;

namespace Project.Modules.Network.UI
{
    public class LoadingAnimation : MonoBehaviour
    {
        private Vector3 _angel = new Vector3(0f, 0f, -50f);

        private void Update()
        {
            transform.Rotate(_angel * Time.deltaTime);
        }
    }
}
