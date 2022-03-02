using System;
using ME.ECS;
using Project.Features;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
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
        if(!CheckLocalPlayer(entity)) return;
        _player = entity;
    }
    
    private bool CheckLocalPlayer(in Entity entity)
    {
        return entity == Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer();
    }

    private void OnDestroy()
    {
        _playerPassEvent.Unsubscribe(SetPlayer);
    }
}
