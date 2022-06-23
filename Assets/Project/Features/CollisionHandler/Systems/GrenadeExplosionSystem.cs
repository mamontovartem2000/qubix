﻿using ME.ECS;
using ME.ECS.Mathematics;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;

namespace Project.Features.CollisionHandler.Systems {

    #pragma warning disable
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GrenadeExplosionSystem : ISystemFilter {
        
        private CollisionHandlerFeature feature;
        private Filter _playerFilter;
        private VFXFeature _vfx;

        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);
            world.GetFeature(out _vfx);

            Filter.Create("Player-Filter")
                .With<AvatarTag>()
                .Push(ref _playerFilter);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-GrenadeExplosionSystem")
                .With<Grenade>()
                .Push();
            
        }
    
        // ReSharper disable Unity.PerformanceAnalysis
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) 
        {
            if (entity.GetPosition().y > 0f) return;
            SoundUtils.PlaySound(entity);

                foreach (var player in _playerFilter)
                {
                    if (math.distancesq(player.GetPosition(), entity.GetPosition()) > Consts.Weapons.GRENADE_EXPLOSION_SQUARED_RADIUS) continue;
                    
                    var debuff = new Entity("debuff");
                    debuff.Get<Owner>().Value = entity.Read<Owner>().Value;
                    entity.Read<SecondaryDamage>().Value.Apply(debuff);
                    debuff.Set(new ProjectileActive());
                    debuff.Set(new CollisionDynamic());
                   
                    
                    debuff.SetPosition(SceneUtils.SafeCheckPosition(player.GetPosition()));

                    if (!debuff.Has<Slowness>()) continue;
                    
                    player.Get<Slowness>().Value = debuff.Read<Slowness>().Value/100;
                    player.Get<Slowness>().LifeTime = debuff.Read<Slowness>().LifeTime;
                }

                _vfx.SpawnVFX(
                    entity.Read<SecondaryDamage>().Value.Has<Slowness>()
                        ? VFXFeature.VFXType.SlowExplosion
                        : VFXFeature.VFXType.GrenadeVFX, entity.GetPosition());

                entity.Destroy();
            
        }
    }
}