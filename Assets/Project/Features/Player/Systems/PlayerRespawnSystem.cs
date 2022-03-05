using ME.ECS;
using UnityEngine;

namespace Project.Features.Player.Systems 
{
    #region usage

    

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
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
    
        private PlayerFeature feature;
        private CollisionHandlerFeature _coll;
        void ISystemBase.OnConstruct() 
        {
            this.GetFeature(out this.feature);
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
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var smth = ref entity.Get<DeadBody>();
            smth.Time -= deltaTime;
            
            if(smth.Time <= 0)
            {
                var newPlayer = feature.RespawnPlayer(smth.ActorID);
                newPlayer.SetAs<PlayerScore>(entity);
                entity.Destroy();
            }
            
            //
            // if (entity.Read<DeadBody>().Time - deltaTime > 0)
            // {
            //     entity.Get<DeadBody>().Time -= deltaTime;
            // }
            // else
            // {
        }
    }
}