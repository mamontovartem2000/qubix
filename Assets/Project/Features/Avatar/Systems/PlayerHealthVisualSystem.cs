using System;
using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Unity.Mathematics;

namespace Project.Features.Avatar.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class PlayerHealthVisualSystem : ISystemFilter
    {
        public World world { get; set; }
        private AvatarFeature _feature;

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
            return Filter.Create("Filter-PlayerHealthVisualSystem")
                .With<PlayerHealthOverlay>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner().Avatar();
            if (avatar.IsAlive())
            {
                ref readonly var health = ref avatar.Read<PlayerHealth>().Value;
                ref var over = ref entity.Get<PlayerHealthOverlay>().Value;

                if(Math.Abs(over - health) < 0.05) return;
                
                if (over > health)
                {
                    over -= math.max(math.abs(over - health), 25) * deltaTime;
                }
                else if (over < health)
                {
                    over = health;
                }
            }
        }
    }
}