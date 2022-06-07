using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Views.Monos
{
	public class PropsMonoView : MonoBehaviourView
	{
		public override bool applyStateJob => true;

		public MeshRenderer Rend;
		private Color matColor;
		private Material mat;
		private bool _init = false;
		private bool _rot = false;

		private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

		public override void OnInitialize() {}
		public override void OnDeInitialize() {}
		public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {}
		public override void ApplyState(float deltaTime, bool immediately)
		{
			transform.position = entity.GetPosition();
			// transform.rotation = entity.GetRotation();

			if (entity.Has<TileRotation>())
			{
				if (!_rot)
				{
					var rot = entity.Read<TileRotation>().Value;
					transform.rotation *= Quaternion.Euler(rot + new Vector3(0,180,0));
					_rot = true;
				}
			}

			if (entity.Has<GlowTile>())
			{
				if (!_init)
				{
					mat = Rend.materials[0];
					matColor = mat.GetColor(EmissionColor);
					_init = true;
				}

				var intencity = entity.Read<GlowTile>().Amount;
				mat.SetColor(EmissionColor, matColor * (intencity * 0.5));
			}
		}
	}
}