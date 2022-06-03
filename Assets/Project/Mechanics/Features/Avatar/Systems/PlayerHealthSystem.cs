﻿using ME.ECS;
using Project.Common.Components;
using Project.Core;
using Project.Core.Features.Events;
using Project.Mechanics.Features.VFX;
using UnityEngine;

namespace Project.Mechanics.Features.Avatar.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class PlayerHealthSystem : ISystemFilter 
    {
        public World world { get; set; }
        private VFXFeature _vfx;
        private Filter _ownerFilter;
        
        void ISystemBase.OnConstruct() 
        {
            world.GetFeature(out _vfx);
            Filter.Create("Filer-Owner")
                .With<PlayerTag>()
                .Push(ref _ownerFilter);
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
                .With<AvatarTag>()
                .WithoutShared<GameFinished>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var health = entity.Read<PlayerHealth>().Value;
            if(health > 0) return;

            if (entity.Get<Owner>().Value.Has<DamagedBy>())
            {
                ref var enemy = ref entity.Get<Owner>().Value.Get<DamagedBy>().Value;
                enemy.Get<PlayerScore>().Kills += 1;
                world.GetFeature<EventsFeature>().PlayerKill.Execute(enemy);
            }
            
            ref var player = ref entity.Get<Owner>().Value;
            player.Get<PlayerScore>().Deaths += 1;
            
            foreach (var owner in _ownerFilter)
            {
                world.GetFeature<EventsFeature>().TabulationScreenNumbersChanged.Execute(owner);
            }
            
            world.GetFeature<EventsFeature>().PlayerDeath.Execute(player);      

            SceneUtils.ModifyWalkable(entity.Read<PlayerMoveTarget>().Value, true);
            
            _vfx.SpawnVFX(VFXFeature.VFXType.QubixDeath, entity.GetPosition());
            player.Remove<PlayerAvatar>();
            entity.Destroy();
        }
    }
}