using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using ME.ECS;
using ME.ECS.Network;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.Player;
using Project.Modules.Network;
using UnityEngine;

public class SoundPlayerScript : MonoBehaviour
{
    [SerializeField] private GlobalEvent _playSoundEvent;
    [SerializeField] private GlobalEvent _playSoundPrivateEvent;
    private PlayerFeature _feature;

    private void Start()
    {
        _playSoundEvent.Subscribe(PlaySound);
        _playSoundPrivateEvent.Subscribe(PlaySoundPrivate);
    }

    private void PlaySound(in Entity entity)
    {
        if (entity.IsAlive() == false) return;
        if (entity.Read<SoundPath>().Value == null) return;
        Debug.Log(entity.Read<SoundPath>().Value);
        RuntimeManager.PlayOneShot(entity.Read<SoundPath>().Value, entity.GetPosition());
    }
    
    private void PlaySoundPrivate(in Entity entity)
    {
        if (entity.IsAlive() == false) return;
        
        if (entity.Owner().Read<PlayerTag>().PlayerLocalID == NetworkData.SlotInRoom)
        {
            RuntimeManager.PlayOneShot(entity.Read<PrivateSoundPath>().Value, entity.GetPosition());
        }
    }

    private void OnDestroy()
    {
        _playSoundEvent.Unsubscribe(PlaySound);
        _playSoundPrivateEvent.Unsubscribe(PlaySoundPrivate);
    }
}
