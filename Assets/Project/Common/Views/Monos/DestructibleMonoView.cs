using ME.ECS;
using UnityEngine;

namespace Project.Common.Views.Monos
{
	using ME.ECS.Views.Providers;

	public class DestructibleMonoView : MonoBehaviourView
	{
		public override bool applyStateJob => true;
		public Animator _anim;
		public override void OnInitialize() {}
		public override void OnDeInitialize() {}
		public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {}

		public override void ApplyState(float deltaTime, bool immediately)
		{
			transform.position = entity.GetPosition();
		}
	}
}