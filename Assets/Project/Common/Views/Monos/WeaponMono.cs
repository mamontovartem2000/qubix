using ME.ECS;
using Project.Common.Components;
using UnityEngine;

namespace Project.Common.Views.Monos
{
	using ME.ECS.Views.Providers;

	public class WeaponMono : MonoBehaviourView
	{
		public override bool applyStateJob => true;
		[SerializeField] private Animator _anim;

		public override void OnInitialize() {}
		public override void OnDeInitialize() {}
		public override void ApplyStateJob(UnityEngine.Jobs.TransformAccess transform, float deltaTime, bool immediately) {}
		public override void ApplyState(float deltaTime, bool immediately)
		{
			transform.position = entity.GetPosition();
			transform.rotation = entity.GetRotation();
			
			if(!entity.Has<MeleeWeapon>()) return;
			
			if(!GetComponentInChildren<ParticleSystem>().isPlaying)
				GetComponentInChildren<ParticleSystem>().Play();

			_anim.SetBool("Attack", entity.Has<LeftWeaponShot>());
		}
	}
}