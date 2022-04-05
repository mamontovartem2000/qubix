using ME.ECS;
using Project.Common.Components;

namespace Project.Mechanics.Features.Projectile.Systems
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
        
        private ProjectileFeature _feature;
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
            ref var del = ref entity.Get<Linear>();

            if (del.StartDelay - deltaTime > 0)
            {
                del.StartDelay -= deltaTime;
            }
            else
            {
                if (!entity.Has<LinearActive>())
                {
                    var view = world.RegisterViewSource(entity.Read<ProjectileView>().Value);
                    entity.InstantiateView(view);
                    entity.Set(new LinearActive());
                }
            }
            
            if (!entity.GetParent().Has<LinearActive>())
            {
                if (del.EndDelay - deltaTime > 0)
                {
                    del.EndDelay -= deltaTime;
                }
                else
                {
                    entity.Destroy();
                }
            }
        }
    }
}