using System;
using ExitGames.Client.Photon.StructWrapping;
using ME.ECS;
using Project.Features.Player.Components;
using Project.Features.SceneBuilder.Views;
using UnityEngine;

namespace Project.Features.SceneBuilder.Systems {
    #region usage

    

    #pragma warning disable
    using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
    using Components; using Modules; using Systems; using Markers;
    #pragma warning restore
    
    #if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
    #endif
    #endregion
    public sealed class SpawnMineSystem : ISystem, IAdvanceTick, IUpdate {
        
        private SceneBuilderFeature feature;
        public World world { get; set; }

        private Filter _mineFilter;
        private Filter _healthFilter;
        private Filter _playerFilter;
        
        void ISystemBase.OnConstruct() {
            
            this.GetFeature(out this.feature);

            Filter.Create("Mine Filter")
                .With<MineTag>()
                .Push(ref _mineFilter);

            Filter.Create("Health Filter")
                .With<HealthTag>()
                .Push(ref _healthFilter);

            Filter.Create("Player Filter")
                .With<PlayerTag>()
                .Push(ref _playerFilter);

        }
        
        void ISystemBase.OnDeconstruct() {}

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            if (_mineFilter.Count < 8)
            {
                feature.SpawnMine();
            }

            if (_healthFilter.Count < 4)
            {
                feature.SpawnHealth();
            }

            foreach (var mine in _mineFilter)
            {
                var minePos = new Vector3(mine.GetPosition().x, 0.5f, mine.GetPosition().z);

                foreach (var player in _playerFilter)
                {
                    var playerPos = player.GetPosition();

                    // if (playerPos == minePos)
                    if((playerPos - minePos).sqrMagnitude <= 0.4f * 0.4f)
                    {
                        // var hp = player.Read<PlayerHealth>().Value;
                        player.Get<PlayerHealth>().Value -= 10;
                        // hp -= 10;
                        // player.Set(new PlayerHealth {Value = hp});
                        
                        feature.MineCollision.Execute(player);
                        mine.Destroy();
                    }
                }
            }
            
            foreach (var health in _healthFilter)
            {
                var minePos = new Vector3(health.GetPosition().x, 0.5f, health.GetPosition().z);

                foreach (var player in _playerFilter)
                {
                    var playerPos = player.GetPosition();

                    // if (playerPos == minePos)
                    if((playerPos - minePos).sqrMagnitude <= 0.4f * 0.4f)
                    {
                        ref var hp = ref player.Get<PlayerHealth>().Value;
                        hp = Mathf.Min(hp + 10, 100);

                        //
                        // if (hp + 10 < 100)
                        // {
                        //     hp += 10;
                        // }
                        // else
                        // {
                        //     hp = 100;
                        // }
                        // player.Set(new PlayerHealth {Value = hp});

                        var score = player.Read<PlayerScore>().Value;
                        score += 1;
                        player.Set(new PlayerScore {Value = score});
                        
                        feature.HealthCollision.Execute(player);
                        health.Destroy();
                    }
                }
            }
            
        }
        
        void IUpdate.Update(in float deltaTime) {}
        
    }
    
}