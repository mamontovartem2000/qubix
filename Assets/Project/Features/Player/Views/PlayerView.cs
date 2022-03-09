using System;
using ME.ECS;
using Project.Features.Player.Components;
using Project.Utilities;
using UnityEngine;

namespace Project.Features.Player.Views {
    
    using ME.ECS.Views.Providers;
    
    public class PlayerView : MonoBehaviourView
    {
        public Renderer[] Parts;
        public GameObject Rocket;
        public GameObject Rifle;
        
        public override bool applyStateJob => true;

        public override void OnInitialize()
        {
            foreach (var part in Parts)
            {
                part.sharedMaterial = entity.Read<PlayerTag>().Material;
            }
        }

        public override void OnDeInitialize() {}

        public override void ApplyState(float deltaTime, bool immediately)
        {
            transform.position = entity.GetPosition();
            transform.rotation = entity.GetRotation();
            
            if (entity.Has<RightWeapon>())
            {
                var type = entity.Read<RightWeapon>().Type;

                switch (type)
                {
                    case WeaponType.Gun:
                        break;
                    case WeaponType.Rocket:
                        Rocket.SetActive(true);
                        break;
                    case WeaponType.Rifle:
                        Rifle.SetActive(true);
                        break;
                    case WeaponType.Shotgun:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                Rocket.SetActive(false);
                Rifle.SetActive(false);
            }
        }
    }
}