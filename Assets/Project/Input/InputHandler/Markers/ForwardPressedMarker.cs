using ME.ECS;
using UnityEngine;

namespace Project.Input.InputHandler.Markers 
{
   public enum InputState { Pressed, Released}

   public struct ForwardMarker : IMarker
   {
      public int ActorID;
      public InputState State;
   }
   public struct BackwardMarker : IMarker
   {
      public int ActorID;
      public InputState State;
   }
   public struct LeftMarker : IMarker
   {
      public int ActorID;
      public InputState State;
   }
   public struct RightMarker : IMarker
   {
      public int ActorID;
      public InputState State;
   }
   public struct MouseLeftMarker : IMarker
   {
      public int ActorID;
      public InputState State;
   }
   
   public struct MouseRightMarker : IMarker
   {
      public int ActorID;
      public InputState State;
   }

   
}