using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Player;
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
            if (_player.IsAlive())
                transform.position = _player.GetPosition() + _offset;
        }

        private void SetPlayer(in Entity player)
        {
            if(player != Worlds.current.GetFeature<PlayerFeature>().GetPlayer(NetworkData.PlayerIdInRoom)) return;
            _player = player.Read<PlayerAvatar>().Value;
        }
    
        private void OnDestroy()
        {
            _playerPassEvent.Unsubscribe(SetPlayer);
        }
    }
}
