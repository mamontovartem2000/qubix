using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.Lifetime.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class LinearLifeSystem : ISystemFilter
    {
        public World world { get; set; }
        
        private LifeTimeFeature _feature;
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out this._feature);
        }
        void ISystemBase.OnDeconstruct() {}
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-LinearLifeSystem")
                .With<Linear>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            
            ref var delay = ref entity.Get<Linear>();
            delay.StartDelay -= deltaTime;

            var avatar = entity.Owner().Avatar();
            if (avatar.IsAlive() == false) {
                entity.Destroy();
                return;
            }

            ref readonly var linIndex = ref entity.Read<LinearIndex>().Value;
            ref readonly var dir = ref avatar.Read<FaceDirection>().Value;

            if (delay.StartDelay <= 0)
            {
                if (!entity.Has<LinearActive>())
                {
                    //Linear weapon length testing view instantiation;
                    var testView = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
                    entity.InstantiateView(testView);

                    avatar.Get<ReloadTime>().Value = avatar.Read<ReloadTimeDefault>().Value;
                    entity.Set(new LinearActive());
                }

                var linPos = avatar.GetPosition() + dir * linIndex;
                entity.SetPosition(linPos);

                if (!avatar.Read<WeaponEntities>().LeftWeapon.Has<LinearActive>())
                {
                    entity.Destroy();
                }
            }
        }
    }
}