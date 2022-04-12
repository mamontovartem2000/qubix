// using ME.ECS;
// using Project.Common.Components;
// using Project.Core.Features.GameState.Components;
// using Project.Core.Features.Player.Components;
//
// namespace Project.Core.Features.Player.Systems
// {
//     #region usage
// #if ECS_COMPILE_IL2CPP_OPTIONS
//     [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
//      Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
// #endif
//     #endregion
//     public sealed class PlayerRespawnSystem : ISystemFilter 
//     {
//         public World world { get; set; }
//     
//         private PlayerFeature _feature;
//         void ISystemBase.OnConstruct() 
//         {
//             this.GetFeature(out _feature);
//         }
//         
//         void ISystemBase.OnDeconstruct() {}
//         #if !CSHARP_8_OR_NEWER
//         bool ISystemFilter.jobs => false;
//         int ISystemFilter.jobsBatchCount => 64;
//         #endif
//         Filter ISystemFilter.filter { get; set; }
//
//         Filter ISystemFilter.CreateFilter() 
//         {
//             return Filter.Create("Filter-PlayerRespawnSystem")
//                 .With<DeadBody>()
//                 .WithoutShared<GameFinished>()
//                 .Push();
//         }
//
//         void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime)
//         {
//             ref var deadbody = ref entity.Get<DeadBody>();
//             deadbody.Time -= deltaTime;
//             
//             if(deadbody.Time <= 0)
//             {
//                 var newPlayer = _feature.RespawnPlayer(deadbody.ActorID);
//                 newPlayer.SetAs<PlayerScore>(entity);
//                 entity.Destroy();
//             }
//         }
//     }
// }