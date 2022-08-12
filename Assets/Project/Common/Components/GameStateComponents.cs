using ME.ECS;

namespace Project.Common.Components 
{
    public struct GamePaused : IComponent {}
    
    public struct GameFinished : IComponent { }
    
    public struct ResultSent : IComponent { }

    public struct GameTimer : IComponent
    {
        public float Value;
    }
    
    public struct MapInitialized : IComponent {}
    
    public struct GameWithoutStages : IComponent
    {
        public float Time;
    }
    
    public struct WinningTeam : IComponent
    {
        public int Team;
    }
    
    public struct MatchPoint : IComponent { }
    
    public struct GameStage : IComponent
    {
        public float Time;
        public int StageNumber;
    }
    
    public struct LastGameStage : IComponent
    {
        public float Time;
    }

    public struct EndOfGameStage : IComponent
    {
        public int StageNumber;
    }
}