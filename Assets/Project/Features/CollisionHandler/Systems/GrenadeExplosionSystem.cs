using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;
using UnityEngine;

namespace Project.Features.CollisionHandler.Systems
{
#pragma warning disable
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class GrenadeExplosionSystem : ISystemFilter
    {
        private CollisionHandlerFeature feature;
        private Filter _playerFilter;
        private VFXFeature _vfx;


        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this.feature);
            world.GetFeature(out _vfx);

            Filter.Create("Player-Filter")
                .With<AvatarTag>()
                .Push(ref _playerFilter);
        }

        void ISystemBase.OnDeconstruct()
        {
        }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-GrenadeExplosionSystem")
                .With<Grenade>()
                .With<GrenadeExplode>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            foreach (var player in _playerFilter)
            {
                if ((player.GetPosition() - entity.GetPosition()).sqrMagnitude > entity.Read<ExplodeSquaredRadius>().Value) continue;

                var debuff = new Entity("debuff");
                debuff.Get<Owner>().Value = entity.Read<Owner>().Value;
                entity.Read<SecondaryDamage>().Value.Apply(debuff);
                Debug.Log(entity.Read<SecondaryDamage>().Value);
                debuff.Set(new ProjectileActive());
                debuff.Set(new CollisionDynamic());
                debuff.Set(new LifeTimeLeft { Value = 0.3f});
                // Debug.Log("hit");
                debuff.SetPosition(player.GetPosition());
            }
            
            SoundUtils.PlaySound(entity, entity.Read<SoundPath>().Value);

            _vfx.SpawnVFX(entity.Read<VFXConfig>().Value, entity.GetPosition());
            
            entity.Get<LifeTimeLeft>().Value = 0.1f;
            entity.Get<ProjectileSpeed>().Value = 0;
            entity.Remove<Grenade>();
        }
    }
}