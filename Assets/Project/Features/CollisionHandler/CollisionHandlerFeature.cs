using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
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

        [Header("Mine Configs")] 
        public int MineCount;
        public Vector2 MineDamage;
        [HideInInspector] public float MineSpawnDelay = 0f;
        public float MineSpawnDelayDefault;
        public Vector2 MineBlinkFrequency;
        
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
            entity.Set(new HealthTag());
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

            var tmpPos = SceneUtils.GetRandomPosition();
            
            
            entity.SetPosition(SceneUtils.GetRandomPosition());

            Debug.Log($"tmpPos: {tmpPos}, plrPos: {entity.GetPosition()}");

            SceneUtils.ModifyFree(entity.GetPosition(), false);
            entity.InstantiateView(_mine);
            entity.Get<MineBlinkTimerDefault>().Value = world.GetRandomRange(MineBlinkFrequency.x, MineBlinkFrequency.y);
            entity.Get<MineDamage>().Value = world.GetRandomRange(MineDamage.x, MineDamage.y);
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