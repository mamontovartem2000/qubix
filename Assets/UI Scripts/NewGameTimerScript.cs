using ME.ECS;
using Project.Common.Components;
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
			var timerValue = entity.Read<GameTimer>().Value;

			if (timerValue <= 0)
			{
				TimerText.SetText("00:00");
				return;
			}

			var mins = timerValue / 60;
			var minsFloor = Mathf.FloorToInt(mins);
			var secsLost = timerValue - 60 * minsFloor;
			var secs = secsLost % 60;

			var timer = $"{minsFloor:0}:{Mathf.FloorToInt(secs):00}";
			
			TimerText.SetText(timer);
		}

		private void OnDestroy()
		{
			RunTimerEvent.Unsubscribe(UpdateTimer);
		}
	}
}
