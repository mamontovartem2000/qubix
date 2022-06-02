using DG.Tweening;
using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Views.Monos
{
	public class ObjectsMonoView : MonoBehaviourView
	{
		public MeshRenderer Rend;

		public override bool applyStateJob => true;
		public Animator _anim;

		public override void OnInitialize()
		{
		}

		public override void OnDeInitialize()
		{
		}

		public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately)
		{
		}

		public override void ApplyState(float deltaTime, bool immediately)
		{
			transform.position = entity.GetPosition();

			if (entity.Has<HealthTag>())
			{
				_anim.SetBool("Animate", true);
			}

			if (entity.Has<MineTag>())
			{

				if (entity.Has<MineBlink>())
				{
					Rend.materials[0].EnableKeyword("_EMISSION");
				}
				else
				{
					Rend.materials[0].DisableKeyword("_EMISSION");
				}
			}
		}
	}
}