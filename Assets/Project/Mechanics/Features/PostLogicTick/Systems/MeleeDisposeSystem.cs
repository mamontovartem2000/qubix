using ME.ECS;
using Project.Common.Components;
using Project.Modules.Network;
using UnityEngine;

namespace Project.Mechanics.Features.PostLogicTick.Systems
{
#pragma warning disable
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class MeleeDisposeSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private PostLogicTickFeature _feature;
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
            return Filter.Create("Filter-MeleeDisposeSystem")
                .With<MeleeAimer>()
                .With<Collided>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var owner = ref entity.Get<Collided>().ApplyTo;
            if(!owner.IsAlive()) return;
            ref var from = ref entity.Get<Collided>().ApplyFrom;
            ref readonly var damage = ref entity.Read<ProjectileDamage>().Value;

            if (owner.Has<PlayerAvatar>())
            {
                ref var player = ref owner.Get<PlayerAvatar>().Value;

                if (owner.Read<PlayerTag>().Team != from.Read<PlayerTag>().Team || NetworkData.Team == string.Empty)
                {
                    var collision = new Entity("collision");
                    collision.Set(new ApplyDamage { ApplyTo = player, ApplyFrom = from, Damage = damage }, ComponentLifetime.NotifyAllSystems);
                }
            }
            if (owner.Has<DestructibleTag>())
            {
                owner.Get<PlayerHealth>().Value -= damage;
            }

            entity.Remove<Collided>();
        }
    }
}