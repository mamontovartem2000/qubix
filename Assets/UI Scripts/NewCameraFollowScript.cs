using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features;
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

        private void SetPlayer(in Entity entity)
        {
            if(!SceneUtils.CheckLocalPlayer(entity)) return;
            _player = entity.Read<PlayerAvatar>().Value;
        }
    
        private void OnDestroy()
        {
            _playerPassEvent.Unsubscribe(SetPlayer);
        }
    }
}
