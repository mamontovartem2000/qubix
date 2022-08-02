using ME.ECS;

namespace Project.Common.Components 
{
    public struct GameStageOne : IComponent
    {
        public float Time;
    }

    public struct GameStageTwo : IComponent
    {
        public float Time;
    }

    public struct GameStageThree : IComponent
    {
        public float Time;
    }
    
    public struct EndOfGameStage : IComponent { }
}