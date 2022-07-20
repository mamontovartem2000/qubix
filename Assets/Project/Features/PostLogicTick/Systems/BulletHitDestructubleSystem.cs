using ME.ECS;
using Project.Common.Components;
using Project.Features.Projectile;
using Project.Features.VFX;

namespace Project.Features.PostLogicTick.Systems {

    #pragma warning disable
#pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class BulletHitDestructubleSystem : ISystemFilter {
        
        private PostLogicTickFeature feature;
        private VFXFeature _vfx;

        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);
            world.GetFeature(out _vfx);

        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-BulletHitEnviromentSystem")
                .With<BulletHit>()
                .With<Enviroment>()
                .With<PlayerHealth>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var bullet = ref entity.Get<BulletHit>().Bullet;
            entity.Get<PlayerHealth>().Value -= bullet.Read<ProjectileDamage>().Value;
            _vfx.SpawnVFX(bullet.Read<VFXConfig>().Value, bullet.GetPosition());
            bullet.Set(new ShouldDestroy(), ComponentLifetime.NotifyAllSystemsBelow);
        }
    }
}