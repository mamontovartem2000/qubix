using ME.ECS;
using UnityEngine;

namespace Project.Features.Player.Components 
{
    public struct PlayerTag : IComponent
    {
        public int PlayerID; 
    }

    public struct FaceDirection : IComponent
    {
        public Vector3 Value;
    }

    public struct PlayerScore : IComponent
    {
        public int Kills;
        public int Deaths;
    }

    public struct PlayerIsRotating : IComponent
    {
        public bool Clockwise;
    }
}