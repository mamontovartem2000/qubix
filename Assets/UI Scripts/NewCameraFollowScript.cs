using ME.ECS;
using Project.Common.Components;
using Project.Features.Player;
using Project.Modules.Network;
using UnityEngine;

namespace UI_Scripts
{
    public class NewCameraFollowScript : MonoBehaviour
    {
        [SerializeField] private GlobalEvent _playerPassEvent;
        [SerializeField] private Vector3 _offset;
    
        private Entity _player;

        private void Start()
        {
            _playerPassEvent.Subscribe(SetPlayer);
        }

        private void Update()
        {
            if (Worlds.currentWorld == null) return;

            if (_player.IsAlive())
                transform.position = new Vector3(_player.GetPosition().x, 0, _player.GetPosition().z) + _offset;
        }

        private void SetPlayer(in Entity player)
        {
            if(player != Worlds.current.GetFeature<PlayerFeature>().GetPlayerByID(NetworkData.SlotInRoom)) return;
            _player = player.Read<PlayerAvatar>().Value;
        }
    
        private void OnDestroy()
        {
            _playerPassEvent.Unsubscribe(SetPlayer);
        }
    }
}
