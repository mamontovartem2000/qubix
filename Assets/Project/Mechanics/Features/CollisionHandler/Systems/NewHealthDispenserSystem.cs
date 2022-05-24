using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.CollisionHandler.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class NewHealthDispenserSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private CollisionHandlerFeature _feature;

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
            return Filter.Create("Filter-NewHealthDispenserSystem")
                .With<DispenserTag>()
                .Without<Spawned>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var dispenser = ref entity.Get<DispenserTag>();
            dispenser.Timer -= deltaTime;

            if (dispenser.Timer <= 0)
            {
                var health = _feature.SpawnHealth(entity);
                health.SetPosition(entity.GetPosition());

                dispenser.Timer = dispenser.TimerDefault;
                entity.Set(new Spawned());
            }
        }
    }
}