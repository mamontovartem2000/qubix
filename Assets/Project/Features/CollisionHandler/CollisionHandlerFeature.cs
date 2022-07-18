using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using Project.Common.Utilities;
using Project.Features.CollisionHandler.Systems;
using UnityEngine;

namespace Project.Features.CollisionHandler 
{
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    public sealed class CollisionHandlerFeature : Feature
    {
        public MonoBehaviourViewBase Portal, Mine, Health;
        private ViewId _portal, _mine, _health;
        
        protected override void OnConstruct()
        {
            AddSystem<GridCollisionDetectionSystem>();
            AddSystem<GrenadeExplosionSystem>();
            AddSystem<SpawnMineSystem>();
            AddSystem<NewHealthDispenserSystem>();
            AddSystem<NewPortalDispenserSystem>();
            AddSystem<MineBlinkSystem>();
            AddSystem<MineBlinkLifeTime>();

            _portal = world.RegisterViewSource(Portal);
            _mine = world.RegisterViewSource(Mine);
            _health = world.RegisterViewSource(Health);
        }
        
        protected override void InjectFilter(ref FilterBuilder builder)
        {
            builder.WithoutShared<GamePaused>();
        }

        protected override void OnDeconstruct() {}
        
        public Entity SpawnHealth(Entity owner)
        {
            var entity = new Entity("Health");
            entity.Set(new HealthDispenserTag());
            entity.InstantiateView(_health);
            
            entity.Set(new CollisionDynamic());
            entity.Get<ProjectileDirection>().Value = fp3.zero;
            entity.Get<Owner>().Value = owner;
            entity.Get<FaceDirection>().Value = new fp3(1, 0, 0);
            
            return entity;
        }   

        public void SpawnMine()
        {
            var entity = new Entity("Mine");

            entity.Set(new MineTag());

            entity.SetPosition(SceneUtils.GetRandomPosition());

            SceneUtils.ModifyFree(entity.GetPosition(), false);
            entity.InstantiateView(_mine);
            entity.Get<MineBlinkTimerDefault>().Value = world.GetRandomRange(Consts.Scene.Mines.BLINK_FREQUENCY_MIN, Consts.Scene.Mines.BLINK_FREQUENCY_MAX);
            entity.Get<MineDamage>().Value = world.GetRandomRange(Consts.Scene.Mines.DAMAGE_MIN, Consts.Scene.Mines.DAMAGE_MAX);
            entity.Get<MineBlinkTimer>().Value = entity.Get<MineBlinkTimerDefault>().Value;
            
            entity.Set(new CollisionDynamic());
            entity.Get<Owner>().Value = new Entity("mineowner");
        }

        public Entity SpawnPortal(Entity owner)
        {
            var entity = new Entity("TeleportVFX");
            entity.Set(new PortalTag());
            entity.InstantiateView(_portal);
            
            entity.Set(new CollisionDynamic());
            entity.Get<Owner>().Value = owner;
            
            return entity;
        }
        
    }
}