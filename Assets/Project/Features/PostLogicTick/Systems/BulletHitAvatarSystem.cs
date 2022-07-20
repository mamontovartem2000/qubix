﻿using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.Projectile;

namespace Project.Features.PostLogicTick.Systems {

    #pragma warning disable
#pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class BulletHitAvatarSystem : ISystemFilter {
        
        private PostLogicTickFeature feature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);
            
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-BulletHitSystem")
                .With<BulletHit>()
                .With<PlayerAvatar>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var collision = new Entity("collision");
            var bulletHit = entity.Read<BulletHit>();
            collision.Get<LifeTimeLeft>().Value = Consts.Main.DEFAULT_LIFETIME;
            collision.Set(new ApplyDamage { ApplyTo = entity.Avatar(), ApplyFrom = bulletHit.ApplyFrom, Damage = bulletHit.Bullet.Read<ProjectileDamage>().Value }, 
                ComponentLifetime.NotifyAllSystems);
            entity.Read<BulletHit>().Bullet.Set(new ShouldDestroy());
        }
    }
}