using ME.ECS;
using Project.Common.Components;

namespace Project.Features.Weapon.Systems
{
    #region usage
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion
    public sealed class CooldownSystem : ISystemFilter
    {
        public World world { get; set; }
        private WeaponFeature _feature;
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
        }

        void ISystemBase.OnDeconstruct() { }
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-WeaponCooldownSystem")
                .With<Cooldown>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var cooldown = ref entity.Get<Cooldown>().Value;
            cooldown -= deltaTime;

            if(cooldown <= (fp)0)
                entity.Remove<Cooldown>();
        }
    }
}