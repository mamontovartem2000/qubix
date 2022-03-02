using System;
using ME.ECS;
using Project.Features;
using UnityEngine;

namespace Project.Utilities
{

    public static class Utilitiddies
    {
        public static bool CheckLocalPlayer(in Entity entity)
        {
            var result = entity == Worlds.current.GetFeature<PlayerFeature>().GetActivePlayer();
            
            // Debug.Log(result);
            
            return result;
        }
    }
}