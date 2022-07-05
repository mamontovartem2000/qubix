using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using FMODUnity;
using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using UnityEngine;

public class SoundPlayerScript : MonoBehaviour
{
    [SerializeField] private GlobalEvent _playSoundEvent;

    private void Start()
    {
        _playSoundEvent.Subscribe(Play);
    }

    private void Play(in Entity entity)
    {
        if (entity.IsAlive() == false) return;
        
        RuntimeManager.PlayOneShot(entity.Read<SoundPath>().Value, entity.GetPosition());
    }

    private void OnDestroy()
    {
        _playSoundEvent.Unsubscribe(Play);
    }
}
