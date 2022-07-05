using ME.ECS;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.VFX;
using UnityEngine;

namespace Project.Features.Skills.Systems.Buller
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    public sealed class PersonalTeleportSkillSystem : ISystemFilter
    {
        public World world { get; set; }

        private SkillsFeature _feature;
        private VFXFeature _vfx;
        private fp3 randomPlayerPos;
        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _feature);
            world.GetFeature(out _vfx);
        }

        void ISystemBase.OnDeconstruct() { }
#if !CSHARP_8_OR_NEWER
        bool ISystemFilter.jobs => false;
        int ISystemFilter.jobsBatchCount => 64;
#endif
        Filter ISystemFilter.filter { get; set; }

        Filter ISystemFilter.CreateFilter()
        {
            return Filter.Create("Filter-PersonalTeleportSkillSystem")
                .With<PersonalTeleportAffect>()
                .With<ActivateSkill>()
                .Push();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
        {
            var avatar = entity.Owner().Avatar();
            if (avatar.IsAlive() == false) return;

            do
            {
                var rndX = world.GetRandomRange(3, 7);
                var rndZ = world.GetRandomRange(3, 7);
                // world.GetRandomInCircle(entity.GetPosition().XZ(), 6);
                
                switch (world.GetRandomRange(0, 4))
                {
                    case 0:
                    {
                        rndX *= -1;
                        break;
                    }

                    case 1:
                    {
                        rndZ *= -1;
                        break;
                    }

                    case 2:
                    {
                        rndX *= -1;
                        rndZ *= -1;
                        break;
                    }
                    
                    case 3:
                    {
                        break;
                    }
                    
                    default:
                    {
                        break;
                    }
                }
                var tmpPos = avatar.Read<PlayerMoveTarget>().Value;
                randomPlayerPos = new Vector3(tmpPos.x + rndX, 0, tmpPos.z + rndZ);

            }
            while (!SceneUtils.IsWalkable(new fp3(randomPlayerPos.x, 0, randomPlayerPos.z)));

            SceneUtils.ModifyWalkable(avatar.Read<PlayerMoveTarget>().Value, true);
            SceneUtils.ModifyWalkable(new fp3(randomPlayerPos.x, 0, randomPlayerPos.z), false);
            
            SoundUtils.PlaySound(avatar, "event:/VFX/TeleportIn");
            avatar.SetPosition(randomPlayerPos);
            avatar.Get<PlayerMoveTarget>().Value = new fp3(randomPlayerPos.x, 0, randomPlayerPos.z);
            SoundUtils.PlaySound(avatar, "event:/VFX/TeleportOut");
            
            entity.Get<Cooldown>().Value = entity.Read<CooldownDefault>().Value;
            
            _vfx.SpawnVFX(VFXFeature.VFXType.PlayerTelerortIn, avatar.GetPosition(), avatar);
            
        }
    }
}