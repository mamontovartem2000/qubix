using TMPro;
using UnityEngine;

namespace Project.Modules.Network
{
    public class WaitingRoomTimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        private float _timer = 0f;

        public void SetTime(float time)
        {
            _timer = time;
        }

        private void Update()
        {
            _timer -= Time.deltaTime;

            if (_timer > 0)
            {
                var mins = Mathf.FloorToInt(_timer / 60);
                var sec = _timer - 60 * mins;
                var secs = sec % 60;

                _text.text = $"{mins:00}:{Mathf.FloorToInt(secs):00}";
            }
            else
            {
                _timer = 0;
                _text.text = $"00:00";
            }
        }
    }
}
