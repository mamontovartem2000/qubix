using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Project.Modules.Network
{
    public class RoomPrefab : MonoBehaviour
    {
        public static Action ChooseRoom;

        [SerializeField] private TMP_Text _playersCount;
        [SerializeField] private TMP_Text _roomId;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _hoverUp;
        [SerializeField] private Sprite _hoverCenter;
        [SerializeField] private Sprite _hoverDown;
        [SerializeField] private Sprite _hoverButton;
        [SerializeField] private Color _textHover;

        private RoomInfo _roomInfo;

        private void Start()
        {
            _joinButton.onClick.AddListener(JoinRoom);
            RoomListConnect.ShowSelectedRoom += DisableButton;
        }

        public void UpdateRoomInfo(RoomInfo info, int roomNumber)
        {
            _roomInfo = info;
            SetRoomId(roomNumber);
            SetPlayersCount();
            HoverEffect(roomNumber);
        }

        private void SetPlayersCount()
        {
            _playersCount.text = $"{_roomInfo.PlayersCount}/{_roomInfo.MaxPlayersCount}";
        }

        private void SetRoomId(int roomNumber)
        {
            _roomId.text = $"Room {roomNumber}";
        }

        private void JoinRoom()
        {
            ChooseRoom?.Invoke();
            FillFrame();
            _joinButton.gameObject.GetComponent<Image>().sprite = _hoverButton;
            _playersCount.color = _textHover;
            var rnd = Random.Range(0f, 1f);
            StartCoroutine(ManualRoomCreating.LoadJoinRequest(_roomInfo.Id, "Player" + rnd, Stepsss.ProcessJoinRequestWithoutSocket));
        }

        private void HoverEffect(int roomNumber)
        {
            if (roomNumber == 1)
                _image.sprite = _hoverUp;
            else if (roomNumber == 5)
                _image.sprite = _hoverDown;
            else
                _image.sprite = _hoverCenter;
        }

        private void DisableButton()
        {
            _joinButton.onClick.RemoveAllListeners();
        }

        private void FillFrame()
        {
            _image.enabled = true;
        }

        private void OnDisable()
        {
            _joinButton.onClick.RemoveAllListeners();
            RoomListConnect.ShowSelectedRoom -= DisableButton;
        }
    }
}
