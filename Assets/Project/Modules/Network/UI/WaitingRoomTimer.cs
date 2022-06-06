using FlatMessages;
using System;
using TMPro;
using UnityEngine;

namespace Project.Modules.Network.UI
{
    public class WaitingRoomTimer : MonoBehaviour
    {
        public static Action ShowCharacterSelectionWindow;

        [SerializeField] private GameObject _textPlace;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private bool _showOnlyLast10sec;

        private float _timer = 0f;

        private void Start()
        {
            NetworkEvents.SetTimer += SetTimer;

        }

        public void SetTimer(TimeRemaining time)
        {
            if (_showOnlyLast10sec)
            {
                if (time.State != "starting")
                    return;
            }

            if (time.State == "starting")
            {
                ShowCharacterSelectionWindow?.Invoke();
            }

            ShowTimer(time);
        }

        private void ShowTimer(TimeRemaining time)
        {
            _timer = time.Value / 1000;

            if (_textPlace.activeSelf == false)
                _textPlace.SetActive(true);
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
                _text.text = $"Loading...";
            }
        }

        private void OnDestroy()
        {
            NetworkEvents.SetTimer -= SetTimer;
        }
    }
}
