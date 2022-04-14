﻿using ME.ECS;
using Project.Common.Components;
using Project.Core.Features.Events;
using Project.Core.Features.GameState.Components;
using Project.Core.Features.Player;
using Project.Core.Features.Player.Components;
using Project.Core.Features.SceneBuilder;
using Project.Modules;
using UnityEngine;

namespace Project.Mechanics.Features.Avatar.Systems
{
    #region usage
#pragma warning disable
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class PlayerHealthSystem : ISystemFilter 
    {
        public World world { get; set; }
        private SceneBuilderFeature _scene;
        
        void ISystemBase.OnConstruct() {
            world.GetFeature(out _scene);
        }

        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-PlayerHealthSystem")
                .With<PlayerHealth>()
                .WithoutShared<GameFinished>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var health = entity.Read<PlayerHealth>().Value;
            if(health > 0) return;

            // if (entity.Has<LastHit>())
            // {
            //     ref var enemy = ref entity.Get<LastHit>().Enemy;
            //     enemy.Get<PlayerScore>().Kills += 1;
            //
            //     world.GetFeature<EventsFeature>().PlayerKill.Execute(enemy);
            // }

            ref var player = ref entity.Get<Owner>().Value;
            // var player = world.GetFeature<PlayerFeature>().GetPlayer(world.GetModule<NetworkModule>().GetCurrentHistoryEvent().order);
            Debug.Log(player);
            
            player.Get<PlayerScore>().Deaths += 1;
            player.Get<RespawnTime>().Value = 5f;
            player.Remove<PlayerAvatar>();
            
            world.GetFeature<EventsFeature>().PlayerDeath.Execute(player);      
            _scene.ReleaseTheCell(entity.Read<PlayerMoveTarget>().Value);

            entity.Destroy();
        }
    }
}