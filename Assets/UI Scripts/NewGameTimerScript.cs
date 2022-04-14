using ME.ECS;
using Project.Core.Features.GameState.Components;
using TMPro;
using UnityEngine;

namespace UI_Scripts
{
	public class NewGameTimerScript : MonoBehaviour
	{

		public GlobalEvent RunTimerEvent;
		public TextMeshProUGUI TimerText;

		private void Start()
		{
			RunTimerEvent.Subscribe(UpdateTimer);
		}

		private void UpdateTimer(in Entity entity)
		{
			var mins = entity.Read<GameTimer>().Value / 60;
			var secs = entity.Read<GameTimer>().Value % 60;

			var timer = $"{mins:0}:{secs:00}";
			
			TimerText.SetText(timer);
		}

		private void OnDestroy()
		{
			RunTimerEvent.Unsubscribe(UpdateTimer);
		}
	}
}
