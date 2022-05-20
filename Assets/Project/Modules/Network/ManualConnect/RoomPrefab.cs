using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Modules.Network
{
    public class RoomPrefab : MonoBehaviour
    {
        public static Action<string> JoinRoom;

        [SerializeField] private TMP_Text _playersCount;
        [SerializeField] private TMP_Text _roomId;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Image _image;

        [Header("Hover Sprites")]
        [SerializeField] private Sprite _hoverUp;
        [SerializeField] private Sprite _hoverCenter;
        [SerializeField] private Sprite _hoverDown;
        [SerializeField] private Sprite _unselectedButton;
        [SerializeField] private Sprite _selectedButton;
        [SerializeField] private Color _unselectedText;
        [SerializeField] private Color _selectedText;

        private RoomInfo _roomInfo;
        private int _roomNumber;
        private bool _selected = false;

        private void Start()
        {
            _joinButton.onClick.AddListener(JoinTheRoom);
            JoinRoom += UnselectRoom;
        }

        public void UpdateRoomInfo(RoomInfo info, int roomNumber)
        {
            _roomInfo = info;
            _roomNumber = roomNumber;
            SetRoomId();
            SetPlayersCount();
            InitHoverImage();
            UpdateButton();
        }

        private void UpdateButton()
        {
            if (_roomInfo.PlayersCount == _roomInfo.MaxPlayersCount || _selected)
            {
                _joinButton.interactable = false;
            }
            else
            {
                _joinButton.interactable = true;
            }
        }

        private void SetPlayersCount()
        {
            _playersCount.text = $"{_roomInfo.PlayersCount}/{_roomInfo.MaxPlayersCount}";
        }

        private void SetRoomId()
        {
            _roomId.text = $"Room {_roomNumber}";
        }

        private void InitHoverImage()
        {
            if (_roomNumber == 1)
                _image.sprite = _hoverUp;
            else if (_roomNumber == 5)
                _image.sprite = _hoverDown;
            else
                _image.sprite = _hoverCenter;
        }

        private void JoinTheRoom()
        {
            JoinRoom?.Invoke(_roomInfo.Id);
            SelectRoom();
        }

        private void SelectRoom()
        {
            _selected = true;
            _joinButton.interactable = false;
            _image.enabled = true;
            _joinButton.gameObject.GetComponent<Image>().sprite = _selectedButton;
            _playersCount.color = _selectedText;
        }

        private void UnselectRoom(string id)
        {
            _selected = false;
            _joinButton.interactable = true;
            _image.enabled = false;
            _joinButton.gameObject.GetComponent<Image>().sprite = _unselectedButton;
            _playersCount.color = _unselectedText;
        }

        private void OnDisable()
        {
            _joinButton.onClick.RemoveAllListeners();
            JoinRoom -= UnselectRoom;
        }
    }
}
