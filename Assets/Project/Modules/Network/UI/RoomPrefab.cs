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
        [SerializeField] private TMP_Text _buttonText;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Image _image;
        [SerializeField] private Image[] _ownerImage;

        [Header("Hover Sprites")]
        [SerializeField] private Sprite _hoverUp;
        [SerializeField] private Sprite _hoverCenter;
        [SerializeField] private Sprite _hoverDown;
        [SerializeField] private Sprite _unselectedButton;
        [SerializeField] private Sprite _hoverButton;
        [SerializeField] private Sprite _selectedButton;
        [SerializeField] private Color _unselectedText;
        [SerializeField] private Color _selectedText;

        private Image _buttonImage;
        private RoomInfo _roomInfo;
        private int _roomNumber;
        private int _roomCount;
        private bool _selected = false;

        private void Start()
        {
            _buttonImage = _joinButton.gameObject.GetComponent<Image>();
            _joinButton.onClick.AddListener(JoinTheRoom);
            JoinRoom += UnselectRoom;
            InitHoverImage();
        }

        public void UpdateRoomInfo(RoomInfo info, int roomNumber, int roomCount)
        {
            _roomInfo = info;
            _roomNumber = roomNumber;
            _roomCount = roomCount;
            UpdateRoomDisplay();
        }

        private void UpdateRoomDisplay()
        {
            _playersCount.text = $"{_roomInfo.PlayersCount}/{_roomInfo.MaxPlayersCount}";
            _roomId.text = $"Room {_roomNumber}";

            if (_roomInfo.PlayersCount == _roomInfo.MaxPlayersCount || _selected)
            {
                _joinButton.interactable = false;
            }
            else
            {
                _joinButton.interactable = true;
            }
        }

        private void InitHoverImage()
        {
            if (_roomNumber == 1)
                _image.sprite = _hoverUp;
            else if (_roomNumber == _roomCount)
                _image.sprite = _hoverDown;
            else
                _image.sprite = _hoverCenter;
        }

        public void EnterHover()
        {
            if (_selected == false)
            {
                _buttonImage.sprite = _hoverButton;
            }
        }

        public void ExitHover()
        {
            if (_selected == false)
            {
                _buttonImage.sprite = _unselectedButton;
            }
        }

        private void JoinTheRoom()
        {
            JoinRoom?.Invoke(_roomInfo.Id);
            SelectRoom();
        }

        private void SelectRoom()
        {
            EnterHover();
            _buttonImage.sprite = _selectedButton;
            _playersCount.color = _selectedText;
            _ownerImage[0].enabled = false;
            _ownerImage[1].enabled = true;
            _image.enabled = true;
            _selected = true;
            _joinButton.interactable = false;
            _buttonText.text = "Joined";
        }

        private void UnselectRoom(string id)
        {
            _image.enabled = false;
            _selected = false;
            _joinButton.interactable = true;
            _playersCount.color = _unselectedText;
            _ownerImage[0].enabled = true;
            _ownerImage[1].enabled = false;
            _buttonText.text = "Join";
            ExitHover();
        }

        private void OnDisable()
        {
            _joinButton.onClick.RemoveAllListeners();
            JoinRoom -= UnselectRoom;
        }
    }
}
