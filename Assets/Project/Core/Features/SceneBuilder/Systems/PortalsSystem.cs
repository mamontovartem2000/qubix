using ME.ECS;
using Project.Common.Components;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Core.Features.SceneBuilder.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
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
                .With<AvatarTag>()
                .Push(ref _playerFilter);

            Filter.Create("Filter-Portals")
                .With<PortalTag>()
                .Push(ref _portalFilter);
        }

        void ISystemBase.OnDeconstruct() { }

        void IAdvanceTick.AdvanceTick(in float deltaTime)
        {
            Entity[] portals = SceneUtils.ConvertFilterToEntityArray(_portalFilter);

            for (int i = 0; i < portals.Length; i++)
            {
                foreach (var player in _playerFilter)
                {
                    if (player.Has<TeleportPlayer>()) continue;

                    if ((player.GetPosition() - portals[i].GetPosition()).sqrMagnitude <= SceneUtils.ItemRadius)
                    {
                        Vector3 newPosition;
                        int rndIndex;
                        List<Entity> lastPortals = portals.ToList();
                        lastPortals.RemoveAt(i);

                        while (true)
                        {
                            rndIndex = world.GetRandomRange(0, lastPortals.Count);
                            newPosition = lastPortals[rndIndex].GetPosition();

                            if (SceneUtils.IsFree(newPosition))
                                break;
                            else
                                lastPortals.RemoveAt(rndIndex);

                            if (lastPortals.Count == 0)
                            {
                                //TODO: If all portals are busy?
                                Debug.Log("Error. No free portals!");
                                return;
                            }
                        }

                        player.Set(new TeleportPlayer());
                        _scene.CreatePortal(newPosition, portals[i].GetPosition());

                        SceneUtils.Move(portals[i].GetPosition(), newPosition);
                        player.SetPosition(newPosition);
                        player.Get<PlayerMoveTarget>().Value = newPosition;
                    }
                }
            }
        }

        void IUpdate.Update(in float deltaTime) { }
    }
}