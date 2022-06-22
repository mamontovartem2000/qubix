using FMODUnity;
using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Utilities
{
    public class SoundUtils : MonoBehaviour
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public static void PlaySound(Entity entity)
        {
            if (!entity.Has<SoundPath>()) return;
        
            RuntimeManager.PlayOneShot(entity.Read<SoundPath>().Value, entity.GetPosition());
        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        public static void PlaySound(Entity entity, string path)
        {
            RuntimeManager.PlayOneShot(path, entity.GetPosition());
        }
    }
}
