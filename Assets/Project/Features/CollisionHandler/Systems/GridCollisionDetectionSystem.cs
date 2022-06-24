using ME.ECS;
using ME.ECS.Buffers;
using ME.ECS.Transform;
using Project.Common.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;

namespace Project.Features.CollisionHandler.Systems
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

		[BurstCompile(FloatPrecision.Low, FloatMode.Fast, CompileSynchronously = true)]
		public struct CollisionJob : IJobParallelFor
		{
			public FilterBag<Position, Owner, ProjectileSpeed, ProjectileDirection, Collided> dynamicBag;
			public FilterBag<Position, Owner> staticBag;
			public fp dt;
			public int width;
			
			public void Execute(int index)
			{
				var sPos = staticBag.ReadT0(index).ToVector3();
				var sOwner = staticBag.ReadT1(index).Value;
				var dist = (fp)2 * 2;

				for (int i = 0; i < dynamicBag.Length; i++)
				{
					var dPos = dynamicBag.ReadT0(i).ToVector3();
					var dOwner = dynamicBag.ReadT1(i).Value;
					var speed = dynamicBag.ReadT2(i).Value;
					var dir = dynamicBag.ReadT3(i).Value;

					if (dOwner == sOwner) continue;

					if (fpmath.distancesq(dPos, sPos) > dist) continue;

					var sIndex = SceneUtils.BurstConvert(sPos,width);
					var dModPos = dPos + dir * speed * dt;
					var dIndex = 0;

					var tmp = ClosestPointToSegment(sPos.XZ(), dPos.XZ(), dModPos.XZ());

					dIndex = SceneUtils.BurstConvert(new fp3(tmp.x, 0, tmp.y), width);

					if (sIndex != dIndex) continue;

					dynamicBag.Set(i, new Collided {ApplyTo = sOwner, ApplyFrom = dOwner});
				}
			}
		}
		
		public void AdvanceTick(in float deltaTime)
		{
			var staticBag = new FilterBag<Position, Owner>(_staticFilter, Allocator.TempJob);
			var dynamicBag = new FilterBag<Position, Owner,ProjectileSpeed,ProjectileDirection,Collided>(_dynamicFilter, Allocator.TempJob);

			var job = new CollisionJob() {dynamicBag = dynamicBag, staticBag = staticBag, dt = deltaTime, width = SceneUtils.Width};
			job.Schedule(staticBag.Length, 50).Complete();
			
			staticBag.Revert();
			dynamicBag.Push();
		}
		
		private static fp2 ClosestPointToSegment(fp2 P, fp2 A, fp2 B)
		{
			fp2 a_to_p = new fp2(), a_to_b = new fp2();
			
			a_to_p.x = P.x - A.x;
			a_to_p.y = P.y - A.y; //     # Storing vector A->P  
			a_to_b.x = B.x - A.x;
			a_to_b.y = B.y - A.y; //     # Storing vector A->B

			float atb2 = a_to_b.x * a_to_b.x + a_to_b.y * a_to_b.y;
			float atp_dot_atb = a_to_p.x * a_to_b.x + a_to_p.y * a_to_b.y; // The dot product of a_to_p and a_to_b
			float t = atp_dot_atb / atb2;  //  # The normalized "distance" from a to the closest point
			return new fp2(A.x + a_to_b.x * t, A.y + a_to_b.y * t);
		}
	}
}