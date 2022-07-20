using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using UnityEngine;

namespace Project.Features.Skills.Systems.Bloodlov {

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class ScytheThrowSkillSystem : ISystemFilter {
        
        private SkillsFeature feature;
        
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);
            
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-SkyteBoomerangSkillSystem")
                .With<ScytheThrowAffect>()
                .With<ActivateSkill>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner(out var owner).Avatar();
            if (avatar.IsAlive() == false) return;
            
            var flyingSkythe = new Entity("flyingSkythe");
            entity.Read<ProjectileConfig>().Value.Apply(flyingSkythe);
            
            flyingSkythe.Set(new Owner{Value = entity.Read<Owner>().Value});
            flyingSkythe.Get<ProjectileDirection>().Value = new Vector3(avatar.Read<FaceDirection>().Value.x * 1f, 0, avatar.Read<FaceDirection>().Value.z * 1f) ;
            flyingSkythe.SetPosition(avatar.GetPosition());
			
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
            
            SoundUtils.PlaySound(avatar, "event:/Skills/Buller/ThrowGrenade");
			
            var view = world.RegisterViewSource(flyingSkythe.Read<ViewModel>().Value);
            flyingSkythe.InstantiateView(view);
        }
    }
}