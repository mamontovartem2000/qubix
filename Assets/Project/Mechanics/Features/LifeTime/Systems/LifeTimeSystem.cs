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
    public sealed class LifeTimeSystem : ISystemFilter
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
            return Filter.Create("Filter-LifeTimeSystem")
                .With<LifeTimeLeft>()
                .Push();
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var lifeTime = ref entity.Get<LifeTimeLeft>().Value;
            lifeTime -= deltaTime;

            if (!(lifeTime <= 0)) return;
            
            if (entity.Has<SkillTag>())
            {
                ref var type = ref entity.Get<SkillEffect>().Value;
                var skillValue = entity.Read<SkillAmount>().Value;

                switch (type)
                {
                    case SkillAttribute.MoveSpeed:
                    {
                        entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<MoveSpeedModifier>().Value -= skillValue;
                        break;
                    }
                    case SkillAttribute.AttackSpeed:
                    {
                        entity.Get<Owner>().Value.Get<PlayerAvatar>().Value.Get<FiringCooldownModifier>().Value -= skillValue;
                        break;
                    }
                }
            }

            entity.Destroy();
        }
    }
}