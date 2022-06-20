using ME.ECS;
using Project.Common.Components;
using Project.Mechanics.Features.Lifetime;
using UnityEngine;

namespace Project.Mechanics.Features.LifeTime.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class MeleeAimInitSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private LifeTimeFeature _feature;
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
        }
        void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-MeleeAimLifeTime")
                .With<MeleeWeapon>()
                .Without<MeleeDamageSpot>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var spot = new Entity("spot");
            entity.Read<ProjectileConfig>().Value.Apply(spot);
            spot.Set(new MeleeAimer());
            spot.Get<Owner>().Value = entity.Get<Owner>().Value;
            entity.Get<MeleeDamageSpot>().Value = spot;
            spot.Remove<CollisionDynamic>();
        }
    }
}