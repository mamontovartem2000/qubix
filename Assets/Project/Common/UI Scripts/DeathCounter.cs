using ME.ECS;
using UnityEngine;

// using TMPro;

namespace Project.Common.UI_Scripts
{
	public class DeathCounter : MonoBehaviour
	{
		public GlobalEvent DeathEvent;
		// public TextMeshProUGUI CounterText;

		private void Start()
		{
			DeathEvent.Subscribe(IncreaseCounter);
		}

		private void IncreaseCounter(in Entity entity)
		{
			// if(!Utilitiddies.CheckLocalPlayer(entity)) return;

			// CounterText.SetText(entity.Read<PlayerScore>().Deaths.ToString());
		}

		private void OnDestroy()
		{
			DeathEvent.Unsubscribe(IncreaseCounter);
		}
	}
}
