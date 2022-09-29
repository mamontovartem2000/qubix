using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using UnityEngine;

namespace Project.Features.Skills.Systems.Lomix {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class LomixBombSkillSystem : ISystemFilter {
        
        private SkillsFeature _feature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this._feature);
            
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-LomixBombSkillSystem")
                .With<LomixBombAffect>()
                .With<ActivateSkill>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner().Avatar();
            if (avatar.IsAlive() == false) return;
            
            var mine = new Entity("lomixBomb");
            entity.Read<ProjectileConfig>().Value.Apply(mine);
            mine.Get<Owner>().Value = entity.Read<Owner>().Value;
            mine.Set(new Grenade());
            mine.SetPosition((Vector3)Vector3Int.RoundToInt(avatar.GetPosition()));
            var view = world.RegisterViewSource(mine.Read<ViewModel>().Value);
            mine.InstantiateView(view);
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
        }
    }
}