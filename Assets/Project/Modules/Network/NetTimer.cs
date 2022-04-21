using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Modules.Network
{
    public class NetTimer : MonoBehaviour
    {
        public static NetTimer Timer;

        [SerializeField] private TMP_Text _text;
        private float _timer = 0f;

        private void Awake()
        {
            Timer = this;
        }

        public void SetTime(float time)
        {
            _timer = time;
        }

        private void Update()
        {
            if (_timer != 0)
            {
                _timer -= Time.deltaTime;

                if (_timer < 0)
                    _timer = 0;

                //_text.text = "Time: " + Mathf.Round(_timer);
                int min = Mathf.FloorToInt(_timer / 60);
                float sec = _timer - 60 * min;

                string minStr = min.ToString();
                if (min < 10)
                    minStr = "0" + minStr;

                string secStr = Mathf.Round(sec).ToString();
                if (sec < 10)
                    secStr = "0" + secStr;


                _text.text = $"Time: {minStr}:{secStr}";
            }
        }
    }
}
