using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Features.Projectile.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class ProjectileMovementSystem : ISystemFilter
    {
        public World world { get; set; }

        void ISystemBase.OnConstruct() { }

        void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-ProjectileMovementSystem")
                .With<ProjectileActive>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var direction = ref entity.Get<ProjectileDirection>().Value;
            var speed = entity.Read<ProjectileSpeed>().Value;
            var newPosition = entity.GetPosition() + direction * (speed * deltaTime);
            entity.SetPosition(newPosition);
        }
    }
}