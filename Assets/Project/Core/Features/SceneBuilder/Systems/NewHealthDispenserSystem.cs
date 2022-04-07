using ME.ECS;
using Project.Common.Views;
using Project.Core.Features.SceneBuilder.Components;

namespace Project.Core.Features.SceneBuilder.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class NewHealthDispenserSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private SceneBuilderFeature _feature;
        private ViewId _health;

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            _health = world.RegisterViewSource(_feature.HealthView);
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
            
            if (dispenser.Timer - deltaTime > 0)
            {
                dispenser.Timer -= deltaTime;
            }
            else
            {
                var health = new Entity("Health");
                health.SetParent(entity);

                health.Set(new HealthTag());
                health.SetPosition(entity.GetPosition());

                health.InstantiateView(_health);
                dispenser.Timer = dispenser.TimerDefault;
                entity.Set(new Spawned());
            }
        }
    }
}