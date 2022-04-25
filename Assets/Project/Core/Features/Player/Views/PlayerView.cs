using ME.ECS;
using ME.ECS.Views.Providers;
using Project.Common.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Core.Features.Player.Views
{
	public class PlayerView : MonoBehaviourView
	{
		[SerializeField] private Image _healthBar;
		public override bool applyStateJob => true;
		public override void OnInitialize() {}
		public override void OnDeInitialize() {}
		public override void ApplyState(float deltaTime, bool immediately)
		{
			transform.position = entity.GetPosition();
			transform.rotation = entity.GetRotation();
			
			var fill = entity.Read<PlayerHealth>().Value / 100;

			_healthBar.fillAmount = fill;
			_healthBar.color = Color.Lerp(Color.red, Color.green, fill);
			_healthBar.transform.rotation = Quaternion.Euler(45f, 45f, 0);
		}
	}
}