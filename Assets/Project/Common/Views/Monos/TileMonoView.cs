using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Views.Monos
{
	public class TileMonoView : MonoBehaviourView
	{
		public override bool applyStateJob => true;

		public MeshRenderer Rend;
		private Color _color = new Color(0,3,3,2);
		private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

		public override void OnInitialize() {}
		public override void OnDeInitialize() {}
		public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {}

		public override void ApplyState(float deltaTime, bool immediately)
		{
			// if (Rend != null)
			// {
			// 	_color = Rend.materials[0].GetColor(EmissionColor);
			// }
			
			transform.position = entity.GetPosition();

			if (entity.Has<GlowTile>())
			{
				var intencity = entity.Read<GlowTile>().Amount;
				Rend.materials[0].SetColor(EmissionColor, _color * intencity);
			}
		}
	}
}