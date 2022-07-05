using ME.ECS;
using Project.Common.Components;
using Project.Common.Events;
using UnityEngine;

namespace Project.Common.Utilities
{
    public class SoundUtils : MonoBehaviour
    {
        public static void PlaySound(Entity entity)
        {   
            // if (!entity.Has<SoundPath>()) return;
        
            Worlds.current.GetFeature<EventsFeature>().PlaySound.Execute(entity);
            // RuntimeManager.PlayOneShot(entity.Read<SoundPath>().Value, entity.GetPosition());
        }
    
        public static void PlaySound(Entity entity, string path)
        {
            entity.Get<SoundPath>().Value = path;
        
            PlaySound(entity);
        }
    }
}
