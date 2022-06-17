using ME.ECS;
using ME.ECS.Buffers;
using ME.ECS.Transform;
using Project.Common.Components;
using Project.Core;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Mechanics.Features.CollisionHandler.Systems
{
#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class GridCollisionDetectionSystem : ISystem, IAdvanceTick
	{
		public World world { get; set; }
		
		private CollisionHandlerFeature _feature;
		private Filter _dynamicFilter,_staticFilter;

		void ISystemBase.OnConstruct()
		{
			this.GetFeature(out _feature);
			Filter.Create("Filter-PlayerFilter")
				.With<CollisionDynamic>()
				.Push(ref _dynamicFilter);
			
			Filter.Create("Filter-GridCollisionDetectionSystem")
				.With<CollisionStatic>()
				.Push(ref _staticFilter);
		}
		void ISystemBase.OnDeconstruct() {}

		public void AdvanceTick(in float deltaTime)
		{
			
			var staticBag = new FilterBag<Position, Owner>(_staticFilter, Allocator.TempJob);
			var dynamicBag = new FilterBag<Position, Owner,ProjectileSpeed,ProjectileDirection,Collided>(_dynamicFilter, Allocator.TempJob);

			var job = new CollisionJob { staticBag = staticBag, dt = deltaTime, width = SceneUtils.Width};
			job.Schedule(dynamicBag).Complete();
			
			staticBag.Revert();
			dynamicBag.Push();
		}

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast, CompileSynchronously = true)]
		private struct CollisionJob : IJobParallelForFilterBag<FilterBag<Position, Owner, ProjectileSpeed, ProjectileDirection, Collided>>
		{
			public FilterBag<Position, Owner> staticBag;
			public float dt;
			public int width;

			[BurstDiscard]
			public void Execute(ref FilterBag<Position, Owner, ProjectileSpeed, ProjectileDirection, Collided> bag, int index)
			{
				var dist = 2f * 2f;

				for (var i = 0; i < staticBag.Length; i++)
				{
					ref readonly var sOwner = ref staticBag.ReadT1(i).Value;
					ref readonly var dOwner = ref bag.ReadT1(index).Value;
					
					if (dOwner == sOwner) continue;

					ref readonly var dPos = ref bag.ReadT0(index).value;
					ref readonly var sPos = ref staticBag.ReadT0(i).value;
					
					if (math.distancesq(dPos, sPos) > dist) continue;

					ref readonly var speed = ref bag.ReadT2(index).Value;
					ref readonly var dir = ref bag.ReadT3(index).Value;

					var sIndex = SceneUtils.BurstConvert(sPos, width);
					// if (sIndex == 0)
					// {
					// 	Debug.Log($"vec: {sPos}, en: {staticBag.GetEntity(i).ToSmallString()}");
					// }
					var dModPos = dPos + dir * speed * dt;
					
					var dIndex = 0;

					var tmp = ClosestPointToSegment(sPos.XZ(), dPos.XZ(), dModPos.XZ());

					dIndex = SceneUtils.BurstConvert(new float3(tmp.x, 0, tmp.y), width);
					if (dIndex == 0)
					{
						Debug.Log($"vec: {dPos}, en: {bag.GetEntity(index).ToSmallString()}");
					}
					// Debug.Log($"sIndex: {sIndex} - {dIndex}: dIndex");
					if (sIndex != dIndex) continue;
					bag.GetT4(index).ApplyTo = sOwner;
				}
			}
		}

		private static float2 ClosestPointToSegment(float2 P, float2 A, float2 B)
		{
			var a_to_p = new float2();
			var a_to_b = new float2();
			
			a_to_p.x = P.x - A.x;
			a_to_p.y = P.y - A.y; //     # Storing vector A->P  
			a_to_b.x = B.x - A.x;
			a_to_b.y = B.y - A.y; //     # Storing vector A->B

			var atb2 = a_to_b.x * a_to_b.x + a_to_b.y * a_to_b.y;
			var atp_dot_atb = a_to_p.x * a_to_b.x + a_to_p.y * a_to_b.y; // The dot product of a_to_p and a_to_b
			var t = atp_dot_atb / atb2;  //  # The normalized "distance" from a to the closest point
			return new float2(A.x + a_to_b.x * t, A.y + a_to_b.y * t);
		}
	}
}