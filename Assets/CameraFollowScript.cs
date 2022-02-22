using ME.ECS;
using Photon.Pun;
using Project.Features.Player.Components;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GlobalEvent TestEvent;
    [SerializeField] private Vector3 _offset;
    private Entity _player;
    
    private void Start()
    {
        this.TestEvent.Subscribe(ThisTestEventCallback);
    }

    private void ThisTestEventCallback(in Entity entity)
    {
        if(entity.Read<PlayerTag>().PlayerID == PhotonNetwork.LocalPlayer.ActorNumber)
            _player = entity;
    }

    private void Update()
    {
        if (_player.IsAlive())
            transform.position = _player.GetPosition() + _offset;
    }
}
