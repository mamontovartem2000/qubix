using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.CollisionHandler;
using Project.Features.CollisionHandler.Systems;
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
    public sealed class MinePlantSkillSystem : ISystemFilter {
        
        private SkillsFeature _feature;
        private CollisionHandlerFeature _collisionHandlerFeature;
        public World world { get; set; }
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this._feature);
            world.GetFeature(out _collisionHandlerFeature);
        }
        
        void ISystemBase.OnDeconstruct() {}
        
        #if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
        #endif
        Filter ISystemFilter.filter { get; set; }
        Filter ISystemFilter.CreateFilter() {
            
            return Filter.Create("Filter-MinePlantSkillSystem")
                .With<MinePlantAffect>()
                .With<ActivateSkill>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var mine = _collisionHandlerFeature.SpawnMine();
            mine.SetPosition((Vector3)Vector3Int.RoundToInt(entity.Owner().Avatar().GetPosition()));
            mine.Get<Owner>().Value = entity.Owner();
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
            SoundUtils.PlaySound(mine, "event:/Skills/Lomix/MinePlant");
        }
    }
}