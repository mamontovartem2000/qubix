﻿using ME.ECS;
using Project.Features.Components;

namespace Project.Features.Player.Systems
{
    #region usage



#pragma warning disable
    using Components;
    using Markers;
    using Modules;
    using Project.Components;
    using Project.Markers;
    using Project.Modules;
    using Project.Systems;
    using Systems;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class PlayerRespawnSystem : ISystemFilter 
    {
        public World world { get; set; }
    
        private PlayerFeature _feature;
        private CollisionHandlerFeature _coll;
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _coll);
        }
        
        void ISystemBase.OnDeconstruct() {}
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter() 
        {
            return Filter.Create("Filter-PlayerRespawnSystem")
                .With<DeadBody>()
                .WithoutShared<GameFinished>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var deadbody = ref entity.Get<DeadBody>();
            deadbody.Time -= deltaTime;
            
            if(deadbody.Time <= 0)
            {
                var newPlayer = _feature.RespawnPlayer(deadbody.ActorID);
                newPlayer.SetAs<PlayerScore>(entity);
                entity.Destroy();
            }
        }
    }
}