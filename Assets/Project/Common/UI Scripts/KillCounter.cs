using ME.ECS;
using UnityEngine;

// using TMPro;

namespace Project.Common.UI_Scripts
{
	public class KillCounter : MonoBehaviour
	{
		public GlobalEvent KillEvent;
		// public TextMeshProUGUI CounterText;

		private void Start()
		{
			KillEvent.Subscribe(IncreaseCounter);
		}

		private void IncreaseCounter(in Entity entity)
		{
			// if(!Utilitiddies.CheckLocalPlayer(entity)) return;

			// CounterText.SetText(entity.Read<PlayerScore>().Kills.ToString());
		}

		private void OnDestroy()
		{
			KillEvent.Unsubscribe(IncreaseCounter);
		}
	}
}
