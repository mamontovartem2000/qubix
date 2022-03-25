using ME.ECS;
using Project.Core.Features.Player.Components;
using Project.Core.Features.SceneBuilder.Components;
using UnityEngine;

namespace Project.Core.Features.SceneBuilder.Systems
{
    #region usage
#pragma warning disable
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
    #endregion

    public sealed class PortalsSystem : ISystem, IAdvanceTick, IUpdate
    {
        private SceneBuilderFeature _scene;
        private Filter _playerFilter, _portalFilter;

        public World world { get; set; }

        void ISystemBase.OnConstruct()
        {
            this.GetFeature(out _scene);
            CreateFilters();
        }

        private void CreateFilters()
        {
            Filter.Create("Filter-Players")
                .With<PlayerTag>()
                .Push(ref _playerFilter);

            Filter.Create("Filter-Portals")
                .With<PortalTag>()
                .Push(ref _portalFilter);
        }

        void ISystemBase.OnDeconstruct() { }

        //TODO: Если все порталы заняты?
        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            Entity[] portals = SceneUtils.ConvertFilterToEntityArray(_portalFilter);

            for (int i = 0; i < portals.Length; i++)
            {
                foreach (var player in _playerFilter)
                {
                    if (player.Has<TeleportPlayer>()) continue;

                    if (player.GetPosition() == portals[i].GetPosition())
                    {
                        Vector3 newPosition = Vector3.zero;
                        var rndIndex = i;
                        while (true)
                        {
                            rndIndex = world.GetRandomRange(0, portals.Length);
                            if (rndIndex == i)
                                continue;

                            newPosition = portals[rndIndex].GetPosition();
                            //if (_scene.IsFree(newPosition))
                                break;
                        }

                        player.Set(new TeleportPlayer());
                        player.SetPosition(portals[rndIndex].GetPosition());
                        _scene.Move(player.GetPosition(), newPosition);
                        player.SetPosition(newPosition);
                        player.Get<PlayerMoveTarget>().Value = newPosition;
                    }
                }
            }
        }     

        void IUpdate.Update(in float deltaTime) { }
        }
    }
