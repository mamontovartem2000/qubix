using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using ME.ECS;
using Project.Common.Components;
using UnityEngine;

public class SoundUtils : MonoBehaviour
{
    public static void PlaySound(Entity entity)
    {
        if (!entity.Has<SoundPath>()) return;
        
        RuntimeManager.PlayOneShot(entity.Read<SoundPath>().Value, entity.GetPosition());
    }
    
    public static void PlaySound(Entity entity, string path)
    {
        RuntimeManager.PlayOneShot(path, entity.GetPosition());
    }
}
