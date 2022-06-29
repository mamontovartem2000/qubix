using System;
using ME.ECS;
using Unity.Mathematics;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features;
using Project.Features.Avatar;
using Project.Features.VFX;
using UnityEngine;
namespace Project.Features.Modifiers.Systems {

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class DashSkillModifier : ISystemFilter {
        
        private ModifiersFeature _feature;
        private VFXFeature _vfx;
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
            
            return Filter.Create("Filter-DashSkillModifier")
                .With<DashModifier>()
                .Push();
            
        }

        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            ref var speed = ref entity.Get<MoveSpeedModifier>().Value;
            speed *= 20;
            
            if ((entity.Read<PlayerMoveTarget>().Value - entity.GetPosition()).sqrMagnitude <= Consts.Movement.MIN_DISTANCE)
            {
                entity.SetPosition((Vector3)Vector3Int.CeilToInt(entity.Read<PlayerMoveTarget>().Value));
                var newTarget = entity.GetPosition() + entity.Read<FaceDirection>().Value;

                if (SceneUtils.IsWalkable(newTarget) && entity.Read<DashModifier>().Step <= Consts.Skills.DASH_LENGTH)
                {
                    SceneUtils.ModifyWalkable(entity.Read<PlayerMoveTarget>().Value, true);
                    SceneUtils.ModifyWalkable(newTarget, false);

                    entity.Get<DashModifier>().Step++;
                    entity.Get<PlayerMoveTarget>().Value = newTarget;
                }
                else
                {
                    entity.Remove<DashModifier>();
                    return;
                }
            }

            var pos = entity.GetPosition();
            var target = entity.Read<PlayerMoveTarget>().Value;
            ref readonly var hover = ref entity.Read<Hover>().Amount;

            var posDelta = Vector3.MoveTowards(new Vector3((float)pos.x, (float)hover, (float)pos.z),
                new Vector3((float)target.x, (float)hover, (float)target.z), (float)(speed * deltaTime));
			
            entity.SetPosition(posDelta);
        }
    }
}