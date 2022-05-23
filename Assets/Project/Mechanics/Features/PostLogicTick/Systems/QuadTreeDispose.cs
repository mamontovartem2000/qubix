// using ME.ECS;
// using ME.ECS.Collections;
// using Project.Mechanics.Features.PreLogicTick.Systems;
//
// namespace Project.Mechanics.Features.PostLogicTick.Systems {
//
//     #pragma warning disable
//     using Project.Components; using Project.Modules; using Project.Systems; using Project.Markers;
//     using Components; using Modules; using Systems; using Markers;
//     #pragma warning restore
//     
//     #if ECS_COMPILE_IL2CPP_OPTIONS
//     [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
//     #endif
//     public sealed class QuadTreeDispose : ISystem, IAdvanceTick {
//         
//         private PostLogicTickFeature feature;
//
//         public World world { get; set; }
//         
//         void ISystemBase.OnConstruct() {
//             
//             this.GetFeature(out this.feature);
//             
//         }
//         
//         void ISystemBase.OnDeconstruct() {}
//
//         void IAdvanceTick.AdvanceTick(in float deltaTime)
//         {
//             NativeQuadTreeUtils.EndTick();
//             QuadTreeInitSystem.array.Dispose();
//         }
//     }
// }